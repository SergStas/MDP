using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using MatrixCalculator;

namespace MatrixTests
{
    public static class Common
    {
        public static Matrix CompileMatrix(int height, int width, double[] values, bool usingFill)
        {
            if (height * width < values.Length)
                throw new ArgumentException();
            Matrix result = new Matrix(height, width);
            if (!usingFill)
                for (int i = 0; i < values.Length; i++)
                    result[i / width, i % width] = values[i];
            else
                result.Fill(ToTwoDimArray(values, width));
            return result;
        }

        public static double[,] ToTwoDimArray(double[] values, int width)
        {
            double[,] result = new double[values.Length / width, width];
            for (int i = 0; i < values.Length; i++)
                result[i / width, i % width] = values[i];
            return result;
        }
    }
}