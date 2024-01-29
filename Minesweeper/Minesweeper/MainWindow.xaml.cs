using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Minesweeper
{
    public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();


            for (int i = 0; i < 16; i++)
			{
				for(int j = 0; j < 30; ++j)
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

		}
        protected void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
        }

    }

}