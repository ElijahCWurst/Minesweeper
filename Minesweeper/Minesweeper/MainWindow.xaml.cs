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
                    Button butt = new Button();
                    butt.Style = (Style)FindResource("HoverButtonStyle");


                    butt.BorderThickness = new Thickness(0);


                    butt.Click += new RoutedEventHandler(Button_Click);
                    myGrid.Children.Add(butt);
                    if ((i + j) % 2 == 0)
                    {
                        butt.Background = Brushes.LawnGreen;
                    }
                    else
                    {
                        butt.Background = Brushes.LimeGreen;
                    }

                    Grid.SetRow(butt, i);
                    Grid.SetColumn(butt, j);
                }

			}

		}
        protected void Button_Click(object sender, RoutedEventArgs e)
        {
            Button butt = (Button)sender;
        }

    }

}