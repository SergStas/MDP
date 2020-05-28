using System.Collections.Generic;
using System.Drawing;
using MatrixCalculator;
using NUnit.Framework;

namespace MatrixTests
{
    [TestFixture]
    public class MatrixCompilationTests
    {
        private object Size;

        [TestCase(1, 1, false, true)]
        [TestCase(8, 8, false, true)]
        [TestCase(0, 1, false, false)]
        [TestCase(0, 0, false, false)]
        [TestCase(-1, 4, false, false)]
        public void MatrixCreationTests(int height, int width, bool usingSize, bool expectedSuccess)
        {
            bool success = true;
            try
            {
                Matrix matrix = usingSize ? new Matrix(new Size(width, height)) : 
                    new Matrix(height, width);
            }
            catch {success = false;}
            Assert.AreEqual(expectedSuccess, success);
        }

        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, false, false, true)]
        [TestCase(2, 1, new[] {1, 0, 1, 1.0}, false, false, false)]
        [TestCase(2, 3, new[] {1, 0, 1, 1.0}, false, false, true)]
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, true, false, true)]
        [TestCase(2, 1, new[] {1, 0, 1, 1.0}, true, false, false)]
        [TestCase(2, 3, new[] {1, 0, 1, 1.0}, true, false, false)]
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, false, true, true)]
        public void InitializationTests(int height, int width, double[] values, bool fill, bool fromDArr, bool expectedSuccess)
        {
            bool success = true;
            try
            {
                if (fromDArr)
                    Matrix.FromTwoDimArray(Common.ToTwoDimArray(values, width));
                else
                    Common.CompileMatrix(height, width, values, fill);
            }
            catch {success = false;}
            Assert.AreEqual(expectedSuccess, success);
        }

        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, 1, 1, 1.0, true)]
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, 0, 0, 1.0, true)]
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, -1, 1, 1.0, false)]
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, 1, -1, 1.0, false)]
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, 1, 2, 1.0, false)]
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, 2, 1, 1.0, false)]
        [TestCase(2, 2, new[] {1, double.Epsilon, 1, 1.0}, 0, 1, double.Epsilon, true)]
        public void IndexerTests(int height, int width, double[] values, int row, int column, double expectedValue,
            bool expectedSuccess)
        {
            bool success = true;
            try
            {
                Matrix matrix = Common.CompileMatrix(height, width, values, false);
                double value = matrix[row, column];
                Assert.AreEqual(expectedValue, value);
            }
            catch {success = false;}
            Assert.AreEqual(expectedSuccess, success);
        }

        [TestCase(2, 2, new[] {1, 0, 1, 1.0})]
        [TestCase(4, 2, new[] {1, 0, 1, 1.0, 0, 0, 0, 0})]
        [TestCase(1, 1, new[] {1.0})]
        public void EnumeratorTest(int height, int width, double[] values)
        {
            Matrix matrix = Common.CompileMatrix(height, width, values, false);
            List<double> result = new List<double>();
            foreach (double value in matrix)
                result.Add(value);
            Assert.AreEqual(values.Length, result.Count);
            for (int i = 0; i < values.Length; i++)
                Assert.AreEqual(values[i], result[i]);
        }

        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, true, 0, new[] {1.0, 0}, true)]
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, true, -1, new[] {1.0, 0}, false)]
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, true, 2, new[] {1.0, 0}, false)]
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, false, 0, new[] {1, 1.0}, true)]
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, false, -1, new[] {1.0, 0}, false)]
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, false, 2, new[] {1.0, 0}, false)]
        public void GetLineTests(int height, int width, double[] values, bool row, int index, double[] expectedLine,
            bool expectedSuccess)
        {
            bool success = true;
            try
            {
                Matrix matrix = Common.CompileMatrix(height, width, values, false);
                double[] actualLine = row ? matrix.GetRow(index) : matrix.GetColumn(index);
                Assert.AreEqual(expectedLine.Length, actualLine.Length);
                for (int i = 0; i < expectedLine.Length; i++)
                    Assert.AreEqual(expectedLine[i], actualLine[i]);
            }
            catch {success = false;}
            Assert.AreEqual(expectedSuccess, success);
        }
        
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, true, 0, new[] {2.0, 1}, true)]
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, true, -1, new[] {2.0, 1}, false)]
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, true, 2, new[] {2.0, 0}, false)]
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, false, 0, new[] {2, 1.0}, true)]
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, false, -1, new[] {2.0, 0}, false)]
        [TestCase(2, 2, new[] {1, 0, 1, 1.0}, false, 2, new[] {2.0, 0}, false)]
        public void SetLineTests(int height, int width, double[] values, bool row, int index, double[] line,
            bool expectedSuccess)
        {
            bool success = true;
            try
            {
                Matrix matrix = Common.CompileMatrix(height, width, values, false);
                double[,] cells = Common.ToTwoDimArray(values, width);
                if (row)
                    matrix.SetRow(index, line); 
                else
                    matrix.SetColumn(index, line);
                double[] actualLine = row ? matrix.GetRow(index) : matrix.GetColumn(index);
                Assert.AreEqual(line.Length, actualLine.Length);
                for (int i = 0; i < (row ? matrix.Size.Height : matrix.Size.Width); i++)
                    for (int j = 0; j < line.Length; j++)
                        if (i == index)
                            Assert.AreEqual(line[j], actualLine[j]);
                        else
                            Assert.AreEqual(cells[i, j], matrix[i, j]);
            }
            catch {success = false;}
            Assert.AreEqual(expectedSuccess, success);
        }

        [Test]
        public void GetArrayTests()
        {
            double[,] values = new[,] {{0, 1.0}, {2, 3}};
            Matrix matrix = Matrix.FromTwoDimArray(values);
            double[,] actual = matrix.GetDDimArray();
            Assert.AreEqual(values.GetLength(0), actual.GetLength(0));
            Assert.AreEqual(values.GetLength(1), actual.GetLength(1));
            int x = values.GetLength(1), y = values.GetLength(0);
            for (int i = 0; i < y; i++)
            for (int j = 0; j < x; j++)
                Assert.AreEqual(values[i, j], actual[i, j]);
        }

        [Test]
        public void SizeTest()
        {
            Matrix matrix = new Matrix(3, 2);
            Size expectedSize = new Size(2, 3);
            Assert.AreEqual(matrix.Size.Height, 3);
            Assert.AreEqual(matrix.Size.Width, 2);
            Assert.AreEqual(expectedSize, matrix.Size);
        }
    }
}