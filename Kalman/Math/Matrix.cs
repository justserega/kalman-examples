namespace Kalman
{
    public class Matrix
    {
        private readonly double[,] _matrix;

        public Matrix(double[,] matrix)
        {
            _matrix = matrix;
        }

        public static Matrix operator +(Matrix leftSide, Matrix rightSide)
        {
            return new Matrix(Accord.Math.Matrix.Add(leftSide._matrix, rightSide._matrix));
        }

        public static Matrix operator *(Matrix leftSide, Matrix rightSide)
        {
            return new Matrix(Accord.Math.Matrix.Multiply(leftSide._matrix, rightSide._matrix));
        }

        public static Matrix operator *(Matrix leftSide, double rightSide)
        {
            return new Matrix(Accord.Math.Matrix.Multiply(leftSide._matrix, rightSide));
        }

        public static Matrix operator -(Matrix leftSide, Matrix rightSide)
        {
            return new Matrix(Accord.Math.Matrix.Subtract(leftSide._matrix, rightSide._matrix));
        }

        public static Matrix Identity(int size)
        {
            return new Matrix(Accord.Math.Matrix.Identity(size));
        }

        public Matrix Transpose()
        {
            return new Matrix(Accord.Math.Matrix.Transpose(_matrix));
        }

        public Matrix Inverse()
        {
            return new Matrix(Accord.Math.Matrix.Inverse(_matrix));
        }

        public int ColumnCount
        {
            get { return _matrix.GetLength(0); }
        }

        public int RowCount
        {
            get { return _matrix.GetLength(1); }
        }

        public double this[int i, int j]
        {
            get { return _matrix[i, j]; }
        }

        public static Matrix CreateVector(params double[] values)
        {
            var matrix = new double[values.Length, 1];
            for (int i = 0; i < values.Length; i++) matrix[i, 0] = values[i];
            
            return new Matrix(matrix);
        }
    }
}
