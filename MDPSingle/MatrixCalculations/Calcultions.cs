using System;

namespace MatrixCalculator
{
    public static class Calculations
    {
        public static Matrix SumMatrices(Matrix m1, Matrix m2)
        {
            if (!SizesAreEqual(m1, m2))
                throw new ArgumentException();
            Matrix result = m1.GetCopy();
            for (int i = 0; i < m2.MatrixSize.Height; i++)
            for (int j = 0; j < m2.MatrixSize.Width; j++)
                result[i, j] += m2[i, j];
            return result;
        }
        
        public static Matrix SubMatrices(Matrix m1, Matrix m2)
        {
            if (!SizesAreEqual(m1, m2))
                throw new ArgumentException();
            Matrix result = m1.GetCopy();
            for (int i = 0; i < m2.MatrixSize.Height; i++)
            for (int j = 0; j < m2.MatrixSize.Width; j++)
                result[i, j] -= m2[i, j];
            return result;
        }

        public static Matrix MulByConstant(Matrix m1, double k)
        {
            Matrix result = m1.GetCopy();
            for (int i = 0; i < m1.MatrixSize.Height; i++)
            for (int j = 0; j < m1.MatrixSize.Width; j++)
                result[i, j] *= k;
            return result;
        }
        
        public static Matrix DivByConstant(Matrix m1, double k)
        {
            if (Math.Abs(k) < Double.Epsilon)
                throw new DivideByZeroException();
            Matrix result = m1.GetCopy();
            for (int i = 0; i < m1.MatrixSize.Height; i++)
            for (int j = 0; j < m1.MatrixSize.Width; j++)
                result[i, j] /= k;
            return result;
        }

        public static bool SizesAreEqual(Matrix m1, Matrix m2) =>
            m1.MatrixSize.Width == m2.MatrixSize.Width && m1.MatrixSize.Height == m2.MatrixSize.Height;
    }
}