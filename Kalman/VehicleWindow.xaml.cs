using System;
using System.Windows;
using Kalman.Simulate;
using OxyPlot;

namespace Kalman
{
    public partial class VehicleWindow
    {
        public VehicleWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            var temp = new PlotModel();
            var fs = new LineSeries();            

            var car = new robot();
            car.set(0, 0, 0);
            for (int i = 0; i < 15; i++ )
            {
                fs.Points.Add(new DataPoint(car.x, car.y));
                if (i == 5)
                {
                    car.move(15 * (Math.PI / 180), 1);
                    continue;
                }
                if (i == 10)
                {
                    car.move(-15 * (Math.PI / 180), 1);
                    continue;
                }
                car.move(0, 1);                               
            }

            temp.Series.Add(fs);

            var fss = new LineSeries();

            car = new robot();
            car.set(0, 0, 0);
            for (int i = 0; i < 15; i++)
            {
                fss.Points.Add(new DataPoint(car.x, car.y));
                if (i == 5)
                {
                    car.move(5 * (Math.PI / 180), 1);
                    continue;
                }
                if (i == 10)
                {
                    car.move(-5 * (Math.PI / 180), 1);
                    continue;
                }
                car.move(0, 1);
            }

            temp.Series.Add(fss);



            var fss2 = new LineSeries();

            car = new robot();
            car.set(0, 0, 0);
            for (int i = 0; i < 15; i++)
            {
                fss2.Points.Add(new DataPoint(car.x, car.y));
                if (i == 5)
                {
                    car.move(1 * (Math.PI / 180), 1);
                    continue;
                }
                if (i == 10)
                {
                    car.move(-1 * (Math.PI / 180), 1);
                    continue;
                }
                car.move(0, 1);
            }

            temp.Series.Add(fss2);





            Plot.Model = temp;
        }
    }
}
