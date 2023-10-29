using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Yolk.Geometry.Shapes
{
    [DataContract]
    public struct Polygon : IEquatable<Polygon>
    {
        private List<Point> map;

        public Polygon(List<Point> map)
        {
            this.map = new List<Point>(map);
        }

        public readonly float Area
        {
            get
            {
                float area = 0f;
                int iterate = map.Count - 1;

                for (int i = 0; i < iterate; ++i)
                {
                    area += Point.CrossProduct(map[i], map[i+1]);
                }
                return 0.5f * MathF.Abs(area);
            }
        }

        public readonly List<Segment> Edges => ShapeFactory.CreateShape(map);

        public readonly List<Point> Map => new(map);

        public readonly float Perimeter
        {
            get
            {
                float perimeter = 0f;
                foreach (Segment edge in Edges)
                {
                    perimeter += edge.Length;
                }
                return perimeter;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Polygon polygon, Polygon other)
        {
            if (polygon.map.Count != other.map.Count)
            {
                return false;
            }

            for (int i = 0; i < polygon.map.Count; ++i)
            {
                if (polygon.map[i] != other.map[i])
                {
                    return false;
                }
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Polygon polygon, Polygon other)
        {
            return !(polygon == other);
        }

        public readonly bool ContainsPoint(Point point)
        {
            return ContainsPoint(point.X, point.Y);
        }

        public readonly bool ContainsPoint(float x, float y)
        {
            Ray ray = Ray.PointEast(x, y);
            int edgesPassed = 0;

            foreach (Segment edge in Edges)
            {
                Point? intersect = Ray.Cast(ray, edge);
                edgesPassed += (intersect is null) ? 0 : 1;
            }
            return edgesPassed % 2 == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals([NotNullWhen(true)] object obj)
        {
            return (obj is Polygon other) && Equals(other);
        }

        public readonly bool Equals(Polygon other)
        {
            return this == other;
        }

        public override readonly int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                foreach (Point point in map)
                {
                    hash = (hash * 7) + point.GetHashCode();
                }
                return hash;
            }
        }

        public readonly bool IsIdentical(Polygon other)
        {
            if (map.Count != other.map.Count)
            {
                return false;
            }

            for (int i = 0; i < map.Count; ++i)
            {
                if (!map[i].IsIdentical(other.map[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public void Translate(Point delta)
        {
            for (int i = 0; i < map.Count; ++i)
            {
                map[i].Translate(delta);
            }
        }

        public void Translate(float dx, float dy)
        {
            Translate(new Point(dx, dy));
        }
    }
}
