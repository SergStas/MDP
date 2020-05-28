using System;
using System.Drawing;
using MatrixCalculator;
using NUnit.Framework;

namespace MatrixTests
{
    [TestFixture]
    public class MatrixCalculationsTests
    {
        [TestCase(new[] {1.0}, new[] {2.0, 0}, 2.0, false)]
        [TestCase(new[] {1.0}, new[] {2.0}, 2.0, true)]
        [TestCase(new[] {1.0, 5}, new[] {2.0, 3}, 17.0, true)]
        [TestCase(new[] {1.0, 5}, new[] {2.0, 0}, 2.0, true)]
        [TestCase(new[] {1.0, 5, 3}, new[] {2.0, 3, 1}, 20.0, true)]
        [TestCase(new[] {-1.5, 5}, new[] {2.0, 3}, 12.0, true)]
        [TestCase(new[] {1.0, 5}, new[] {-2.0, 0}, -2.0, true)]
        [TestCase(new[] {-1.0, 1.5, 3}, new[] {1.5, 3, -1}, 0.0, true)]
        [TestCase(new[] {1.0, 5}, new double[0], 17.0, false)]
        public void DotProductTests(double[] v1, double[] v2, double expectedResult, bool expectedSuccess)
        {
            bool success = true;
            try
            {
                double actualResult = Calculations.GetDotProduct(v1, v2);
                Assert.AreEqual(expectedResult, actualResult, 1e-7);
            }
            catch
            {
                success = false;
            }
            Assert.AreEqual(expectedSuccess, success);
        }

        [TestCase(2, 1, new[] {2.0, 1}, 2, new[] {4.0, 2})]
        [TestCase(2, 1, new[] {0.0, 1}, 2.7, new[] {0, 2.7})]
        [TestCase(2, 2, new[] {2.0, 1, 4, 5}, 0.5, new[] {1, 0.5, 2, 2.5})]
        [TestCase(2, 1, new[] {2.0, 1}, -2, new[] {-4.0, -2})]
        [TestCase(1, 1, new[] {1.0}, -1, new[] {-1.0})]
        public void MpByConstTests(int height, int width, double[] values, double k, double[] expectedValues)
        {
            Matrix matrix = Common.CompileMatrix(height, width, values);
            Matrix result0 = matrix * k;
            Matrix result1 = k * matrix;
            Matrix expectedResult = Matrix.FromTwoDimArray(Common.ToTwoDimArray(expectedValues, width));
            bool equality0 = result0.Equals(expectedResult);
            bool equality1 = result1.Equals(expectedResult);
            Assert.True(equality0);
            Assert.True(equality1);
        }

        [TestCase(2, 1, new[] {2.0, 1}, 2, new[] {1, 0.5})]
        [TestCase(2, 1, new[] {0.0, 1}, 2.5, new[] {0, 0.4})]
        [TestCase(2, 2, new[] {2.0, 1, 4, 5}, 0.5, new[] {4.0, 2, 8, 10})]
        [TestCase(2, 1, new[] {2.0, 1}, -2, new[] {-1, -0.5})]
        [TestCase(1, 1, new[] {1.0}, -1, new[] {-1.0})]
        [TestCase(1, 1, new[] {1.0}, 0, new[] {-1.0})]
        public void DivByConstTests(int height, int width, double[] values, double k, double[] expectedValues)
        {
            Matrix matrix = Common.CompileMatrix(height, width, values);
            try
            {
                Matrix result = matrix / k;
                Matrix expectedResult = Matrix.FromTwoDimArray(Common.ToTwoDimArray(expectedValues, width));
                bool equality = result.Equals(expectedResult);
                Assert.True(equality);
            }
            catch {Assert.True(Math.Abs(k) < double.Epsilon);}
        }

        [TestCase(2, new[] {2.0, 1}, new[] {1.0, 2}, new[] {3.0, 3})]
        [TestCase(2, new[] {2.0, 1}, new[] {0.0, 0}, new[] {2.0, 1})]
        [TestCase(2, new[] {1.0, 0, 0, 1}, new[] {0.0, 1, 1, 0}, new[] {1.0, 1, 1, 1})]
        [TestCase(2, new[] {1.0, 0, 0, 1}, new[] {0.0, -1, -1, 0}, new[] {1.0, -1, -1, 1})]
        [TestCase(1, new[] {4.0}, new[] {-8.0}, new[] {-4.0})]
        [TestCase(1, new[] {2.0, 1}, new[] {1.0, 2}, new[] {3.0, 3})]
        [TestCase(1, new[] {2.0, 1}, new[] {0.0, 0}, new[] {2.0, 1})]
        [TestCase(1, new[] {1.0, 0, 0, 1}, new[] {0.0, 1, 1, 0}, new[] {1.0, 1, 1, 1})]
        [TestCase(1, new[] {1.0, 0, 0, 1}, new[] {0.0, -1, -1, 0}, new[] {1.0, -1, -1, 1})]
        public void SumMatricesTest(int width, double[] v0, double[] v1, double[] expectedValues)
        {
            Matrix m0 = Common.CompileMatrix(width, v0);
            Matrix m1 = Common.CompileMatrix(width, v1);
            Matrix expectedResult = Common.CompileMatrix(width, expectedValues);
            Matrix result0 = m0 + m1;
            Matrix result1 = m1 + m0;
            bool equality0 = expectedResult.Equals(result0);
            bool equality1 = expectedResult.Equals(result1);
            Assert.True(equality0);
            Assert.True(equality1);
        }

        [TestCase(1, new[]{0.0, 0}, 2, new[] {0.0, 0})]
        [TestCase(2, new[]{0.0, 0}, 2, new[] {0.0, 0, 0, 0})]
        [TestCase(2, new[]{0.0, 0}, 4, new[] {0.0, 0, 0, 0})]
        [TestCase(2, new[]{0.0, 0}, 1, new double[0])]
        public void SumMatricesInvalidCases(int w0, double[] v0, int w1, double[] v1)
        {
            try
            {
                Matrix m0 = Common.CompileMatrix(w0, v0);
                Matrix m1 = Common.CompileMatrix(w1, v1);
                Matrix result0 = m1 + m0;
                Matrix result1 = m1 + m0;
                Assert.Fail();
            }
            catch {Assert.Pass();}
        }
        
        [TestCase(2, new[] {2.0, 1}, new[] {1.0, 2}, new[] {1.0, -1})]
        [TestCase(2, new[] {2.0, 1}, new[] {0.0, 0}, new[] {2.0, 1})]
        [TestCase(2, new[] {1.0, 0, 0, 1}, new[] {0.0, 1, 1, 0}, new[] {1.0, -1, -1, 1})]
        [TestCase(2, new[] {1.0, 0, 0, 1}, new[] {0.0, -1, -1, 0}, new[] {1.0, 1, 1, 1})]
        [TestCase(1, new[] {4.0}, new[] {-8.0}, new[] {12.0})]
        [TestCase(1, new[] {2.0, 1}, new[] {1.0, 2}, new[] {1.0, -1})]
        [TestCase(1, new[] {2.0, 1}, new[] {0.0, 0}, new[] {2.0, 1})]
        [TestCase(1, new[] {1.0, 0, 0, 1}, new[] {0.0, 1, 1, 0}, new[] {1.0, -1, -1, 1})]
        [TestCase(1, new[] {1.0, 0, 0, 1}, new[] {0.0, -1, -1, 0}, new[] {1.0, 1, 1, 1})]
        public void SubMatricesTest(int width, double[] v0, double[] v1, double[] expectedValues)
        {
            Matrix m0 = Common.CompileMatrix(width, v0);
            Matrix m1 = Common.CompileMatrix(width, v1);
            Matrix expectedResult = Common.CompileMatrix(width, expectedValues);
            Matrix result = m0 - m1;
            bool equality = expectedResult.Equals(result);
            Assert.True(equality);
        }

        [TestCase(1, new[]{0.0, 0}, 2, new[] {0.0, 0})]
        [TestCase(2, new[]{0.0, 0}, 2, new[] {0.0, 0, 0, 0})]
        [TestCase(2, new[]{0.0, 0}, 4, new[] {0.0, 0, 0, 0})]
        [TestCase(2, new[]{0.0, 0}, 1, new double[0])]
        public void SubMatricesInvalidCases(int w0, double[] v0, int w1, double[] v1)
        {
            try
            {
                Matrix m0 = Common.CompileMatrix(w0, v0);
                Matrix m1 = Common.CompileMatrix(w1, v1);
                Matrix result = m0 - m1;
                Assert.Fail();
            }
            catch {Assert.Pass();}
        }

        [TestCase(2, new[] {1.0, 2, 3, 4}, new[] {2.0, 1, 0, 3}, new[]{2.0, 7, 6, 15})]
        [TestCase(2, new[] {2.0, 3, 1, 0, 1, 1}, new[] {4.0, 2}, new[]{14.0, 4, 6})]
        [TestCase(3, new[] {1.0, 0, 0, 0, 1, 0, 0, 0, 1}, new[] {4, 3, 0, 2, 2, 0.2}, new[] {4, 3, 0, 2, 2, 0.2})]
        [TestCase(2, new[] {-1.0, 0, -1, 1}, new[] {3.0, -2}, new[]{-3.0, -5})]
        [TestCase(3, new[] {1.0, 2, 3}, new[] {4.0, 2, 0, 3, 2, 2, -1, 0, 1}, new[]{7.0, 6, 7})]
        [TestCase(1, new[] {-1.0}, new[] {-1.0}, new[]{1.0})]
        [TestCase(3, new[] {0.0, 0, 0, 0, 0, 0}, new[] {0.0, 0, 0, 0, 0, 0}, new[] {0.0, 0, 0, 0})]
        [TestCase(2, new[] {1.0, -1, -1, 1}, new[] {-1.0, 1, 1, -1}, new[]{-2.0, 2, 2, -2})]
        public void MatrixMultiplicationTests(int width0, double[] v0, double[] v1, double[] expectedValues)
        {
            Matrix m0 = Common.CompileMatrix(width0, v0);
            Matrix m1 = Common.CompileMatrix(v1.Length / width0, v1);
            Matrix expectedResult = Common.CompileMatrix(m1.Size.Width, expectedValues);
            Matrix actualResult = m0 * m1;
            Assert.AreEqual(actualResult.Size.Width, m1.Size.Width, "Sizes wasn't equal!");
            Assert.AreEqual(actualResult.Size.Height, m0.Size.Height, "Sizes wasn't equal!");
            bool equality = expectedResult.Equals(actualResult);
            Assert.True(equality);
        }
    }
}