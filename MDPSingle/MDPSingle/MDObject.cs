using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using MatrixCalculator;

namespace MDPSingle
{
    public class MDObject : IProjectableModel
    {
        public int Dim { get; }

        public List<MDPoint> Points { get; }
        public int PointsCount => 
            Points.Count;

        public IEnumerable<Matrix> ToMatrices()
        {
            foreach (MDPoint point in Points)
            foreach (Matrix matrix in point.ToMatrices())
                yield return matrix;
        }

        public IEnumerable<Matrix> Project(Matrix transformationMatrix)
        {
            foreach (Matrix matrix in ToMatrices())
                yield return transformationMatrix * matrix;
        }

        public MDPoint this[int index]
        {
            get => Points[index];
            set => Points[index] = value;
        }

        public override bool Equals(object? obj) =>
            obj is MDObject mdObject && Equals(mdObject);

        private bool Equals(MDObject obj)
        {
            if (Dim != obj.Dim || PointsCount != obj.PointsCount)
                return false;
            for (int i=0;i<PointsCount;i++)
                if (!this[i].Equals(obj[i]))
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Dim.GetHashCode() * PointsCount.GetHashCode();
                foreach (MDPoint point in Points)
                    result = (result ^ point.GetHashCode()) * 511;
                return result;
            }
        }

        public MDObject(IEnumerable<MDPoint> pts) =>
            Points = pts.ToList();
    }
}