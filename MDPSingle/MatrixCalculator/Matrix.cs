using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace MatrixCalculator
{
    public class Matrix : IEnumerable<double>
    {
        public Size MatrixSize { get; }
        private double[,] _cells;

        public Matrix(int rowsCount, int columnsCount)
        {
            MatrixSize = new Size(columnsCount, rowsCount);
            _cells = new double[rowsCount, columnsCount];
        }

        public Matrix(Size size) : this(size.Height, size.Width) { }

        public Matrix(double[,] content) : this(content.GetLength(0), content.GetLength(1)) => 
            Fill(content);

        public double this[int i, int j]
        {
            get
            {
                 if (!InBounds(i,MatrixSize.Height) || !InBounds(j, MatrixSize.Width))
                     throw new IndexOutOfRangeException();
                 return _cells[i, j];
            }
            set
            {
                if (!InBounds(i,MatrixSize.Height) || !InBounds(j, MatrixSize.Width))
                    throw new IndexOutOfRangeException();
                _cells[i, j] = value;
            }
        }

        public void Fill(double[,] content)
        {
            if (MatrixSize.Width != content.GetLength(1) || MatrixSize.Height != content.GetLength(0))
                throw new ArgumentException();
            _cells = content;
        }

        public void SetRow(int index, double[] content)
        {
            if (!InBounds(index,MatrixSize.Height) || content.Length != MatrixSize.Width)
                throw new ArgumentException();
            for (int i = 0; i < content.Length; i++)
                _cells[index, i] = content[i];
        }
        
        public void SetColumn(int index, double[] content)
        {
            if (!InBounds(index,MatrixSize.Width) || content.Length != MatrixSize.Height)
                throw new ArgumentException();
            for (int i = 0; i < content.Length; i++)
                _cells[i, index] = content[i];
        }
        
        public Matrix GetCopy() =>
            new Matrix(_cells);

        public double[,] ToDDimArray() =>
            _cells;

        public IEnumerable<double> GetRow(int index)
        {
            if (!InBounds(index,MatrixSize.Height))
                throw new IndexOutOfRangeException();
            for (int i = 0; i < MatrixSize.Width; i++)
                yield return _cells[index, i];
        }

        public IEnumerable<double> GetColumn(int index)
        {
            if (!InBounds(index,MatrixSize.Width))
                throw new IndexOutOfRangeException();
            for (int i = 0; i < MatrixSize.Height; i++)
                yield return _cells[i, index];
        }

        private bool InBounds(int index, int limit) =>
            index >= 0 && index < limit;

        public IEnumerator<double> GetEnumerator()
        {
            foreach (double e in _cells)
                yield return e;
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public override string ToString() =>
            $"Matrix[{MatrixSize.Height}, {MatrixSize.Width}]";

        public override bool Equals(object? obj) =>
            obj is Matrix matrix && Equals(matrix);

        private bool Equals(Matrix matrix)
        {
            if (MatrixSize.Width != matrix.MatrixSize.Width || MatrixSize.Height != matrix.MatrixSize.Height)
                return false;
            for (int i = 0; i < MatrixSize.Height; i++) 
                for (int j = 0; j < MatrixSize.Width; j++)
                    if (Math.Abs(matrix[i, j] - _cells[i, j]) < 1e-7)
                        return false;
            return true;
        }

        public static Matrix FromDoubleArray(double[,] cells) =>
            new Matrix(cells.GetLength(0), cells.GetLength(1)) {_cells = cells};
    }
}