using System;

namespace MDPSingle
{
    public class MDPoint
    {
        public int Dim { get; }
        private double[] _coordinates;

        public MDPoint(double[] coords)
        {
            _coordinates = coords;
            Dim = _coordinates.Length;
        }

        public double this[int index]
        {
            get => _coordinates[index];
            set => _coordinates[index] = value;
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

        public override string ToString() =>
             $"({string.Join(", ", _coordinates)})";
    }
}