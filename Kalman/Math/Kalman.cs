namespace Kalman
{
    public sealed class KalmanFilter
    {
        //System matrices
        public Matrix X0 { get; private set; } 
        public Matrix P0 { get; private set; }

        public Matrix F { get; private set; }
        public Matrix B { get; private set; }
        public Matrix U { get; private set; }
        public Matrix Q { get; private set; }
        public Matrix H { get; private set; }
        public Matrix R { get; private set; }

        public Matrix State { get; private set; } 
        public Matrix Covariance { get; private set; }

        public KalmanFilter(Matrix f, Matrix b, Matrix u, Matrix q, Matrix h, Matrix r)
        {
            F = f;
            B = b;
            U = u;
            Q = q;
            H = h;
            R = r;
        }
       
        public void SetState(Matrix state, Matrix covariance)
        {
            // Set initial state
            State = state;
            Covariance = covariance;
        }

        public void Correct(Matrix z)
        {
            // Predict
            X0 = F*State;// +(B * U);
            P0 = F * Covariance * F.Transpose() + Q;

            // Correct
            var k = P0 * H.Transpose() * (H * P0 * H.Transpose() + R).Inverse(); // kalman gain
            State = X0 + (k * (z - (H * X0)));
            Covariance = (Matrix.Identity(P0.RowCount) - k * H) * P0;
        }
    }
}