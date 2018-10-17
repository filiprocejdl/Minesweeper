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
        List<Bomb> Bombs = new List<Bomb>();
        int row = 0;
        int col = 0;
        int x = 16;
        int y = 30;
        int bombs = 99;
        int[] bombRow = new int[99];
        int[] bombCol = new int[99];
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
                BombGen();
                sw.Start();
                dt.Start();
                clicked = true;
                bombs = 99;
                BombCtr.Content = bombs;
                startBtn.Content = "STOP";
            } else
            {
                sw.Reset();
                Timer.Content = "00:00";
                clicked = false;
                startBtn.Content = "START";
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
                    Btn.MouseRightButtonDown += (sender, EventArgs) => { RightClick(sender, EventArgs, Field); };
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
            //bool IsMinaHere = false;
            int MinaCtr = bombs;
            int x;
            int y;
            

           
            for (int i = 0; i < bombs; i++)
            {
                x = rnd.Next(0, 16);
                y = rnd.Next(0, 30);
                if ((x == bombRow[i]) & (y == bombCol[i]))
                {

                } else
                {
                    bombRow[i] = x;
                    bombCol[i] = y;
                }

            }





        }      
        /////////////////
        //CHECK BOMB/SAVE
        void myButton_Click(object sender, RoutedEventArgs e)
        {


            Button _btn = sender as Button;
            int _row = (int)_btn.GetValue(Grid.RowProperty);
            int _column = (int)_btn.GetValue(Grid.ColumnProperty);


            for (int i = 0; i < bombCol.Length; i++)
            {
                if ((_row == bombRow[i]) & (_column == bombCol[i]))
                {
                    sw.Reset();
                    Timer.Content = "00:00";
                    clicked = false;
                    startBtn.Content = "START";
                    _btn.Background = Brushes.Red;
                    MessageBox.Show("you lost and played " + currentTime);

                }
                else
                    _btn.Background = Brushes.Green;
                {
                  // MessageBox.Show("řádek: " + _row + " sloupec: " + _column);
                }            
            }       
        }
        /////////////////
        //CHECK WHERE IS MINA
        void BombWhere()
        {
        

        
        }
        /////////////////
        //RIGHT CLICK
        void RightClick(object sender, RoutedEventArgs e, Grid Field)
        {
            Button _btn = sender as Button;
            int _row = (int)_btn.GetValue(Grid.RowProperty);
            int _column = (int)_btn.GetValue(Grid.ColumnProperty);

            for (int i = 0; i < bombCol.Length; i++)
            {
                if ((_row == bombRow[i]) & (_column == bombCol[i]))
                {
                    bombs = bombs - 1;
                    BombCtr.Content = bombs;
                }
            } _btn.Background = Brushes.Blue;



        }
    }
}
