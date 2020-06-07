#nullable enable
using System;
using System.Collections.Generic;
using MatrixCalculator;

namespace MDPSingle
{
    public class MDPoint : IProjectableModel
    {
        public int Dim { get; }
        private double[] _coordinates;

        public MDPoint(double[] coords)
        {
            _coordinates = coords;
            Dim = _coordinates.Length;
        }

        public IEnumerable<Matrix> ToMatrices()
        {
            Matrix result = new Matrix(Dim, 1);
            for (int i = 0; i < Dim; i++)
                result[i, 0] = _coordinates[i];
            yield return result;
        }


        public double this[int index]
        {
            get => _coordinates[index];
            set => _coordinates[index] = value;
        }

        public IEnumerable<Matrix> Project(Matrix transformationMatrix)
        {
            foreach (Matrix matrix in ToMatrices())
                yield return transformationMatrix * matrix;
        }

        public override bool Equals(object? obj) =>
            obj is MDPoint point && Equals(point);

        bool Equals(MDPoint point)
        {
            if (point.Dim != Dim)
                return false;
            for (int i=0;i<Dim;i++)
                if (Math.Abs(point[i] - _coordinates[i]) > 1e-7)
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Dim.GetHashCode();
                foreach (double value in _coordinates)
                    result = (result ^ value.GetHashCode()) * 511;
                return result;
            }
        }

        public override string ToString() =>
             $"({string.Join(", ", _coordinates)})";
    }
}