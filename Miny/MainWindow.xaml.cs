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


        //FIELD GENERATOR
        public void Generator(Grid Field)
        {
            Field.Width = 250;
            Field.Height = 250;
            int x = 10;
            int y = 10;

            while (row < 10)
            {
                RowDefinition Row = new RowDefinition();
                Field.RowDefinitions.Add(Row);
                row = row++;
            }

            while (col < 10)
            {
                ColumnDefinition Col = new ColumnDefinition();
                Field.ColumnDefinitions.Add(Col);
                col = col++;
            }
        }


     


       


    }
}
