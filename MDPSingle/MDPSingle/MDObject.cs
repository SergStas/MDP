using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MDPSingle
{
    public class MDObject : IProjectableModel
    {
        public int Dim { get; }
        public List<MDPoint> Points { get; }
        public int PointsCount => 
            Points.Count;

        public MDObject(IEnumerable<MDPoint> pts) =>
            Points = pts.ToList();
    }
}