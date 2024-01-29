using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Minesweeper
{
    public partial class MainWindow : Window
	{
		private const int ROWS = 16;
		private const int COLS = 30;
		private const int BOMBS = 99;
		private readonly Brush OpenColor = new SolidColorBrush(Colors.Wheat);
		private readonly Dictionary<int, Brush> ColorMap = new Dictionary<int, Brush>
		{
			{ 0, Brushes.White },
			{ 1, Brushes.Blue },
            { 2, Brushes.Green },
			{ 3, Brushes.Red },
			{ 4, Brushes.Purple },
			{ 5, Brushes.Maroon },
			{ 6, Brushes.Turquoise },
			{ 7, Brushes.Black },
			{ 8, Brushes.Gray }
        };
		private bool gameStarted = false;
		private int[,] grid = new int[ROWS, COLS];
		private (int row, int col)[] bombList = new (int, int)[BOMBS];
		public MainWindow()
		{
			InitializeComponent();
			for (int i = 0; i < ROWS; ++i)
			{
                myGrid.RowDefinitions.Add(new RowDefinition());
            }
			for (int j = 0 ; j < COLS; ++j)
			{
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }


			for (int i = 0; i < ROWS; ++i)
			{
				for(int j = 0; j < COLS; ++j)
				{
					Button btn = new Button();
					//btn.Style = (Style)FindResource("HoverButtonStyle");


					btn.BorderThickness = new Thickness(0);
					btn.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(Button_Click);
					btn.Click += new RoutedEventHandler(Button_Click);
					
					myGrid.Children.Add(btn);
					if ((i + j) % 2 == 0)
					{
						btn.Background = Brushes.LawnGreen;
					}
					else
					{
						btn.Background = Brushes.LimeGreen;
					}

                    Grid.SetRow(btn, i);
					Grid.SetColumn(btn, j);
				}

			}

		}
		protected void Button_Click(object sender, RoutedEventArgs e)
		{

			Button btn = (Button)sender;
			(int row, int col) clickPoint = (Grid.GetRow(btn), Grid.GetColumn(btn));

			if (!gameStarted)
			{
				btn.Background = Brushes.HotPink;
				bombList = generateMines(grid, clickPoint);
				gameStarted = true;
			}
			if(bombList.Contains(clickPoint))
			{
                btn.Background = Brushes.Red;
                btn.Content = "X";
                MessageBox.Show("You Lose!");
                //gameStarted = false;
                return;
            } else
			{
                openSelf(clickPoint, bombList, sender);

            }
			//btn.Content = Grid.GetRow(btn) + "," + Grid.GetColumn(btn);
		}

		private (int row, int col)[] generateMines(int[,] grid, (int row, int col) clickPoint)
		{
			Random random = new Random();
			(int row, int col)[] bombList = new (int, int)[BOMBS];
			for(int i = 0; i < BOMBS; ++i)
			{
				(int row, int col) temp = (random.Next(0,16),random.Next(0,30));
				if(bombList.Contains(temp))
				{
                    --i;
                    continue;
                }
                else if (Math.Abs(clickPoint.row - temp.row) <= 1 && Math.Abs(clickPoint.col - temp.col) <= 1)
                {
                    --i;
                    continue;
                } else
                {
					bombList[i] = temp;
				}

			}

			return bombList;

		}
		private int countBombsWrapper((int row, int col) clickPoint, (int row, int col)[] bombList, object sender)
		{
			int temp = 
			countBombs((clickPoint.row - 1, clickPoint.col - 1), bombList) +
			countBombs((clickPoint.row - 1, clickPoint.col + 1), bombList) +
			countBombs((clickPoint.row + 1, clickPoint.col - 1), bombList) +
			countBombs((clickPoint.row + 1, clickPoint.col + 1), bombList) +
            countBombs((clickPoint.row, clickPoint.col - 1), bombList) +
            countBombs((clickPoint.row, clickPoint.col + 1), bombList) +
            countBombs((clickPoint.row - 1, clickPoint.col), bombList) +
            countBombs((clickPoint.row + 1, clickPoint.col), bombList);
			if (temp == 0)
			{
				openSurrounding(clickPoint, bombList, sender);
			}
			return temp;
		}
        private int countBombs((int row, int col) clickPoint, (int row, int col)[] bombList)
        {
			if (bombList.Contains(clickPoint))
			{
				return 1;
			}
			else
			{
				return 0;
			}
        }
		private void openSurrounding((int row, int col) clickPoint, (int row, int col)[] bombList, object sender)
		{
			openSelf((clickPoint.row - 1, clickPoint.col - 1), bombList, sender);
			openSelf((clickPoint.row - 1, clickPoint.col + 1), bombList, sender);
			openSelf((clickPoint.row + 1, clickPoint.col - 1), bombList, sender);
			openSelf((clickPoint.row + 1, clickPoint.col + 1), bombList, sender);
			openSelf((clickPoint.row, clickPoint.col - 1), bombList, sender);
			openSelf((clickPoint.row, clickPoint.col + 1), bombList, sender);
			openSelf((clickPoint.row - 1, clickPoint.col), bombList, sender);
			openSelf((clickPoint.row + 1, clickPoint.col), bombList, sender);
        }
        private void openSelf((int row, int col) clickPoint, (int row, int col)[] bombList, object sender)
        {
            if (clickPoint.row < 0 || clickPoint.row >= ROWS || clickPoint.col < 0 || clickPoint.col >= COLS) return;
            Button btn = myGrid.Children.Cast<Button>().First(e => Grid.GetRow(e) == clickPoint.row && Grid.GetColumn(e) == clickPoint.col);
            if (btn.Background == OpenColor)
            {
                return;
            }
            btn.Background = OpenColor;
            int bombCount = countBombsWrapper(clickPoint, bombList, sender);
            btn.Content = new TextBlock()
            {
                Text = bombCount > 0 ? bombCount.ToString() : "",
                FontWeight = FontWeights.UltraBold,
				FontSize = 16,
                Foreground = ColorMap[bombCount],
				FontFamily = new FontFamily("Lucida Console")
			};
        }
    }

}