using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using OxyPlot;

namespace Kalman
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            var temp = new PlotModel();
            

            var rs = new LineSeries();
            rs.StrokeThickness = 1;
            var fuelData = File.ReadAllLines(@"Data/Fuel.txt").Select(double.Parse).ToList();
            for (int i = 0; i < fuelData.Count; i++) rs.Points.Add(new DataPoint(i, fuelData[i]));
            temp.Series.Add(rs);

            var f = new Matrix(new[,] {{1.0, 1}, {0, 1}});
            var b = new Matrix(new[,] {{0.0}, {0}});
            var u = new Matrix(new[,] {{0.0}, {0}});
            var r = Matrix.CreateVector(10);
            var q = new Matrix(new[,] {{0.01, 0.4}, {0.1, 0.02}});
            var h = new Matrix(new[,] {{1.0 , 0}});

            var kalman = new KalmanFilter(f, b, u, q, h, r); // задаем F, H, Q и R
            kalman.SetState(Matrix.CreateVector(fuelData[0], 0), new Matrix(new[,] {{10.0, 0}, {0 , 10.0}})); // задаем начальные State и Covariance

            var filtered = new List<double>();
            var rashod = new List<double>();
            foreach (var d in fuelData)
            {
                try
                {
                    kalman.Correct(new Matrix(new[,] {{d}}));
                    filtered.Add(kalman.State[0, 0]);
                    rashod.Add(kalman.State[1, 0]);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            var fs = new LineSeries();
            for (int i = 0; i < filtered.Count; i++) fs.Points.Add(new DataPoint(i, filtered[i]));
            temp.Series.Add(fs);

            var ras = new LineSeries();
            for (int i = 0; i < filtered.Count; i++) ras.Points.Add(new DataPoint(i, rashod[i]));
            temp.Series.Add(ras);

            temp.Axes.Add(new LinearAxis(AxisPosition.Left, 0, 4200));
            temp.Axes.Add(new LinearAxis(AxisPosition.Bottom));            
            Plot.Model = temp;
        }
    }
}
