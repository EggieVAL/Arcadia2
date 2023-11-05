using System.Collections.Generic;

namespace Yolk.Geometry.Shapes
{
    public static class ShapeFactory
    {
        public static List<Segment> CreateShape(List<PointF> map)
        {
            if (map.Count < 3)
            {
                throw new ShapeFactoryError("A map needs at least three points");
            }

            if (map[0] != map[^1])
            {
                throw new ShapeFactoryError("The start and end point of a map must be the same");
            }

            List<Segment> edges = new();
            int iterate = map.Count - 1;

            for (int i = 0; i < iterate; ++i)
            {
                edges.Add(new Segment(map[i], map[i+1]));
            }
            return edges;
        }
    }
}
