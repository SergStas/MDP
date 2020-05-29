using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace MatrixCalculator
{
    public class Matrix : IEnumerable<double>
    {
        public Size Size { get; }
        private double[,] _cells;

        public Matrix(int rowsCount, int columnsCount)
        {
            if (rowsCount < 1 || columnsCount < 1)
                throw new ArgumentException();
            Size = new Size(columnsCount, rowsCount);
            _cells = new double[rowsCount, columnsCount];
        }

        public Matrix(Size size) : this(size.Height, size.Width) { }

        public Matrix(double[,] content) : this(content.GetLength(0), content.GetLength(1)) => 
            Fill(content);

        public double this[int i, int j]
        {
            get
            {
                 if (!InBounds(i,Size.Height) || !InBounds(j, Size.Width))
                     throw new IndexOutOfRangeException();
                 return _cells[i, j];
            }
            set
            {
                if (!InBounds(i,Size.Height) || !InBounds(j, Size.Width))
                    throw new IndexOutOfRangeException();
                _cells[i, j] = value;
            }
        }

        public void Fill(double[,] content)
        {
            if (Size.Width != content.GetLength(1) || Size.Height != content.GetLength(0))
                throw new ArgumentException();
            _cells = content;
        }

        public void SetRow(int index, double[] content)
        {
            if (!InBounds(index,Size.Height) || content.Length != Size.Width)
                throw new ArgumentException();
            for (int i = 0; i < content.Length; i++)
                _cells[index, i] = content[i];
        }

        public void SetColumn(int index, double[] content)
        {
            if (!InBounds(index,Size.Width) || content.Length != Size.Height)
                throw new ArgumentException();
            for (int i = 0; i < content.Length; i++)
                _cells[i, index] = content[i];
        }

        public Matrix GetCopy() =>
            new Matrix(CopyCells());

        public double[,] GetDDimArray() =>
            _cells;

        public double[] GetRow(int index)
        {
            if (!InBounds(index,Size.Height))
                throw new IndexOutOfRangeException();
            double[] result = new double[Size.Width];
            for (int i = 0; i < Size.Width; i++)
                result[i] = _cells[index, i];
            return result;
        }

        public double[] GetColumn(int index)
        {
            if (!InBounds(index,Size.Width))
                throw new IndexOutOfRangeException();
            double[] result = new double[Size.Height];
            for (int i = 0; i < Size.Height; i++)
                result[i] = _cells[i, index];
            return result;
        }

        public Matrix Transpose() =>
            Calculations.Transpose(this);

        public IEnumerator<double> GetEnumerator()
        {
            foreach (double e in _cells)
                yield return e;
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public override string ToString() =>
            $"Matrix[{Size.Height}, {Size.Width}]";

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Size.GetHashCode();
                foreach (double value in _cells)
                    result = (result ^ value.GetHashCode()) * 511;
                return result;
            }
        }

        public override bool Equals(object? obj) =>
            obj is Matrix matrix && Equals(matrix);

        private bool Equals(Matrix matrix)
        {
            if (Size.Width != matrix.Size.Width || Size.Height != matrix.Size.Height)
                return false;
            for (int i = 0; i < Size.Height; i++) 
                for (int j = 0; j < Size.Width; j++)
                    if (Math.Abs(matrix[i, j] - _cells[i, j]) > 1e-7)
                        return false;
            return true;
        }

        public static Matrix FromTwoDimArray(double[,] cells) =>
            new Matrix(cells.GetLength(0), cells.GetLength(1)) {_cells = cells};

        public static bool SizesAreEqual(Matrix m1, Matrix m2) =>
            m1.Size.Width == m2.Size.Width && m1.Size.Height == m2.Size.Height;

        public static Matrix operator +(Matrix m1, Matrix m2) =>
            Calculations.SumMatrices(m1, m2);

        public static Matrix operator -(Matrix m1, Matrix m2) =>
            Calculations.SubMatrices(m1, m2);

        public static Matrix operator *(Matrix m1, Matrix m2) =>
            Calculations.MultiplyMatrices(m1, m2);

        public static Matrix operator *(Matrix m1, double k) =>
            Calculations.MulByConstant(m1, k);

        public static Matrix operator *(double k, Matrix m1) =>
            Calculations.MulByConstant(m1, k);
        
        public static Matrix operator /(Matrix m1, double k) =>
            Calculations.DivByConstant(m1, k);

        private static bool InBounds(int index, int limit) =>
            index >= 0 && index < limit;

        private double[,] CopyCells()
        {
            double[,] result = new double[Size.Height, Size.Width];
            for (int i = 0; i < Size.Height; i++)
            for (int j = 0; j < Size.Width; j++)
                result[i, j] = _cells[i, j];
            return result;
        }
    }
} 