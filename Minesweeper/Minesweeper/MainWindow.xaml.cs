using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon.Primitives;
using System.Windows.Media;

namespace Minesweeper
{
	public partial class MainWindow : Window
	{
		private const int ROWS = 16;
		private const int COLS = 30;
		private const int BOMBS = 99;
		private bool gameStarted = false;
		private int[,] grid = new int[ROWS, COLS];
		public MainWindow()
		{
			InitializeComponent();
			



			for (int i = 0; i < ROWS; ++i)
			{
				for(int j = 0; j < COLS; ++j)
				{
					Button btn = new Button();
					btn.Style = (Style)FindResource("HoverButtonStyle");


					btn.BorderThickness = new Thickness(0);


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

			//create a 2d array


		}
		protected void Button_Click(object sender, RoutedEventArgs e)
		{

			Button btn = (Button)sender;
			(int row, int col) clickPoint = (Grid.GetRow(btn), Grid.GetColumn(btn));
			if (!gameStarted)
			{
				btn.Background = Brushes.HotPink;
				generateMines(grid, clickPoint);
				gameStarted = true;
			}
			btn.Content = Grid.GetRow(btn) + "," + Grid.GetColumn(btn);
		}

		private void generateMines(int[,] grid, (int row, int col) clickPoint)
		{
			Random random = new Random();
			(int row, int col)[] bombList = new (int, int)[BOMBS];
			for(int i = 0; i < ROWS * COLS; ++i)
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
			

			//convert each number to a row and column


		}

	}

}