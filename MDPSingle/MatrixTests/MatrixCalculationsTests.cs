using System;
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
    }
}