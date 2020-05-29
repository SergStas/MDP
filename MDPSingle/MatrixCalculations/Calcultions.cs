using System;

namespace MatrixCalculator
{
    public static class Calculations
    {
        public static Matrix SumMatrices(Matrix m1, Matrix m2)
        {
            if (!Matrix.SizesAreEqual(m1, m2))
                throw new ArgumentException();
            Matrix result = m1.GetCopy();
            for (int i = 0; i < m2.Size.Height; i++)
            for (int j = 0; j < m2.Size.Width; j++)
                result[i, j] += m2[i, j];
            return result;
        }
        
        public static Matrix SubMatrices(Matrix m1, Matrix m2)
        {
            if (!Matrix.SizesAreEqual(m1, m2))
                throw new ArgumentException();
            Matrix result = m1.GetCopy();
            for (int i = 0; i < m2.Size.Height; i++)
            for (int j = 0; j < m2.Size.Width; j++)
                result[i, j] -= m2[i, j];
            return result;
        }

        public static Matrix MulByConstant(Matrix m1, double k)
        {
            Matrix result = m1.GetCopy();
            for (int i = 0; i < m1.Size.Height; i++)
            for (int j = 0; j < m1.Size.Width; j++)
                result[i, j] *= k;
            return result;
        }
        
        public static Matrix DivByConstant(Matrix m1, double k)
        {
            if (Math.Abs(k) < Double.Epsilon)
                throw new DivideByZeroException();
            Matrix result = m1.GetCopy();
            for (int i = 0; i < m1.Size.Height; i++)
            for (int j = 0; j < m1.Size.Width; j++)
                result[i, j] /= k;
            return result;
        }

        public static Matrix MultiplyMatrices(Matrix m1, Matrix m2)
        {
            if (m1.Size.Width != m2.Size.Height)
                throw new ArgumentException();
            Matrix result = new Matrix(m1.Size.Height, m2.Size.Width);
            for (int i=0;i<m1.Size.Height;i++)
            for (int j = 0; j < m2.Size.Width; j++)
                result[i, j] = GetDotProduct(m1.GetRow(i), m2.GetColumn(j));
            return result;
        }

        public static double GetDotProduct(double[] v1, double[] v2)
        {
            if (v1.Length != v2.Length)
                throw new ArgumentException();
            double result = 0;
            for (int i = 0; i < v1.Length; i++)
                result += v1[i] * v2[i];
            return result;
        }

        public static Matrix Transpose(Matrix matrix) //TODO: tests
        {
            double [,] newCells = new double[matrix.Size.Width,matrix.Size.Height];
            double[,] cells = matrix.GetDDimArray();
            for (int i = 0; i < matrix.Size.Height; i++)
            for (int j = 0; j < matrix.Size.Width; j++)
                newCells[j, i] = cells[i, j];
            return Matrix.FromTwoDimArray(newCells);
        }
    }
}