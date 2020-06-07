using System.Collections.Generic;
using MatrixCalculator;
using Microsoft.VisualBasic.CompilerServices;

namespace MDPSingle
{
    public interface IProjectableModel
    {
        public IEnumerable<Matrix> ToMatrices();

        public IEnumerable<Matrix> Project(Matrix transformationMatrix);
    }
}