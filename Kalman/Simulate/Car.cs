using System;

namespace Kalman.Simulate
{

    public class robot
    {
        public double x { get; set; }
        public double y { get; set; }
        public double length { get; set; }
        public double orientation { get; set; }
        public double steering_noise { get; set; }
        public double distance_noise { get; set; }
        public double steering_drift { get; set; }
        private static GuassianRandom random = new GuassianRandom();

        public robot(double length = 1.0)
        {
            this.length = length;
            steering_noise = 0.002;
            distance_noise = 0.002;
        }


        public void set(double new_x, double new_y, double new_orientation)
        {
            x = new_x;
            y = new_y;
            orientation = new_orientation%(2.0*Math.PI);
        }


        public void move(double steering, double distance, double tolerance = 0.001,
                          double max_steering_angle = Math.PI/4.0)
        {


            if (steering > max_steering_angle) steering = max_steering_angle;
            if (steering < -max_steering_angle) steering = -max_steering_angle;
            if (distance < 0.0) distance = 0.0;


            // apply noise
            var steering2 = steering_noise <= 0 ? steering : random.NextGuassian(steering, steering_noise);
            var distance2 = distance_noise <= 0 ? distance : random.NextGuassian(distance, distance_noise);

            // apply steering drift
            steering2 += steering_drift;

            // Execute motion
            var turn = Math.Tan(steering2)*distance2/length;

            if (Math.Abs(turn) < tolerance)
            {
                // approximate by straight line motion
                x = x + (distance2*Math.Cos(orientation));
                y = y + (distance2*Math.Sin(orientation));
                orientation = (orientation + turn)%(2.0*Math.PI);
            }
            else
            {
                // approximate bicycle model for motion
                var radius = distance2/turn;
                var cx = x - (Math.Sin(orientation)*radius);
                var cy = y + (Math.Cos(orientation)*radius);
                orientation = (orientation + turn)%(2.0*Math.PI);
                x = cx + (Math.Sin(orientation)*radius);
                y = cy - (Math.Cos(orientation)*radius);
            }
        }

        //############## ADD / MODIFY CODE BELOW ####################
        //
        //# ------------------------------------------------------------------------
        //#
        //# run - does a single control run.
        //
        //
        //def run(param1, param2, param3):
        //    myrobot = robot()
        //    myrobot.set(0.0, 1.0, 0.0)
        //    speed = 1.0 # motion distance is equal to speed (we assume time = 1)
        //    N = 100
        //    myrobot.set_steering_drift(10.0 / 180.0 * pi) # 10 degree bias, this will be added in by the move function, you do not need to add it below!
        //    #
        //    # Enter code here
        //    #
        //
        //# Call your function with parameters of (0.2, 3.0, and 0.004)
        //run(0.2, 3.0, 0.004)
        //


    }

}
