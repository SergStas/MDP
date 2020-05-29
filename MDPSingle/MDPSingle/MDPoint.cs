#nullable enable
using System;
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

        public Matrix ToMatrix()
        {
            Matrix result = new Matrix(Dim, 1);
            for (int i = 0; i < Dim; i++)
                result[i, 0] = _coordinates[i];
            return result;
        }
            

        public double this[int index]
        {
            get => _coordinates[index];
            set => _coordinates[index] = value;
        }

        public Matrix Project(Matrix transformationMatrix) =>
            transformationMatrix * ToMatrix();

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

        public override int GetHashCode() //TODO: this
        {
            return base.GetHashCode();
        }

        public override string ToString() =>
             $"({string.Join(", ", _coordinates)})";
    }
}