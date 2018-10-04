using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Miny
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        string currentTime = string.Empty;
        Boolean clicked = false;
        int row = 0;
        int col = 0;
        int x = 16;
        int y = 30;
        int[] bombRow = { 1, 3, 5, 7, 9 };
        int[] bombCol = { 1, 3, 5, 7, 9 };
        public MainWindow()
        {
            InitializeComponent();
            Grid Field = new Grid();
            dt.Tick += new EventHandler(Time);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        //STOPWATCH
        void Time(object sender, EventArgs e)
        {
            if (sw.IsRunning)
            {
                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                Timer.Content = currentTime;
            }
        }
        private void Start_button(object sender, RoutedEventArgs e)
        {
            if (clicked == false)
            {
                FieldGen();
                sw.Start();
                dt.Start();
                clicked = true;
                btn.Content = "STOP";
            } else
            {
                sw.Reset();
                Timer.Content = "00:00";
                clicked = false;
                btn.Content = "START";
            }
            

        }
        /////////////////
        //FIELD GENERATOR
        public void FieldGen()
        {   

            Grid DynamicGrid = Field;
            DynamicGrid.Background = new SolidColorBrush(Colors.Gray);
            while (col < y)
            {
                ColumnDefinition Column = new ColumnDefinition();
                DynamicGrid.ColumnDefinitions.Add(Column);
                col++;
            }
            while (row < x)
            {
                RowDefinition Row = new RowDefinition();
                DynamicGrid.RowDefinitions.Add(Row);
                row++;
            }


            for (int r = 0; r < x; r++)
            {

                for (int c = 0; c < y; c++)
                {
                    Button Btn = new Button();
                    Btn.Click += myButton_Click;
                    Grid.SetRow(Btn, r);
                    Grid.SetColumn(Btn, c);
                    DynamicGrid.Children.Add(Btn);
                }
            }

        } 
        /////////////////
        //BOMB GENERATOR
        public void BombGen()
        {
            Random rnd = new Random();

            int bombY = rnd.Next(x);
            int bombX = rnd.Next(x);

        }
        /////////////////
        //CHECK
        void myButton_Click(object sender, RoutedEventArgs e)
        {
           // bombRow = 0;
           // bombCol = 0;

             Button _btn = sender as Button;
            int _row = (int)_btn.GetValue(Grid.RowProperty);
            int _column = (int)_btn.GetValue(Grid.ColumnProperty);

            if ((_row == bombRow[0]) & (_column == bombCol[0]))
            {
                sw.Reset();
                Timer.Content = "00:00";
                clicked = false;
                btn.Content = "START";
                MessageBox.Show("you lost and played " + currentTime);
            } else
            {
                //MessageBox.Show("řádek: " + _row + " sloupec: " + _column);
            }

        }

    }
}
