using MatrixCalculator;
using Microsoft.VisualBasic.CompilerServices;

namespace MDPSingle
{
    public interface IProjectableModel
    {
        public int Dim { get; }
        public Matrix ToMatrix();

        public Matrix Project(Matrix transformationMatrix) =>
            transformationMatrix * ToMatrix();
    }
}