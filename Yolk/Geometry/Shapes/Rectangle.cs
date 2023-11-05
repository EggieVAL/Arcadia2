using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Yolk.Engine;

namespace Yolk.Geometry.Shapes
{
    [DataContract]
    public struct Rectangle : IEquatable<Rectangle>
    {
        [DataMember]
        public PointF Point;

        [DataMember]
        public float Height;

        [DataMember]
        public float Width;

        public Rectangle(PointF point, float width, float height)
        {
            Width = width;
            Height = height;
            Point = point;
        }

        public Rectangle(float x, float y, float width, float height)
            : this(new PointF(x, y), width, height)
        {
        }

        public Rectangle(float width, float height)
            : this(PointF.Origin, width, height)
        {
        }

        public readonly float Area => Width * Height;

        public readonly List<Segment> Edges => ShapeFactory.CreateShape(Map);

        public readonly List<PointF> Map
        {
            get
            {
                float x = Point.X;
                float y = Point.Y;

                return new List<PointF>()
                {
                    new PointF(x, y),
                    new PointF(x + Width, y),
                    new PointF(x + Width, y + Height),
                    new PointF(x, y + Height),
                    new PointF(x, y)
                };
            }
        }

        public readonly float Perimeter => (Width + Height) * 2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Rectangle rectangle, Rectangle other)
        {
            return rectangle.Point == other.Point
                && rectangle.Height == other.Height
                && rectangle.Width == other.Width;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Rectangle rectangle, Rectangle other)
        {
            return !(rectangle == other);
        }

        public readonly bool ContainsPoint(PointF point)
        {
            return ContainsPoint(point.X, point.Y);
        }

        public readonly bool ContainsPoint(float x, float y)
        {
            Ray ray = Ray.PointEast(x, y);
            int edgesPassed = 0;

            foreach (Segment edge in Edges)
            {
                PointF? intersect = Ray.Cast(ray, edge);
                edgesPassed += (intersect is null) ? 0 : 1;
            }
            return edgesPassed % 2 == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals([NotNullWhen(true)] object obj)
        {
            return (obj is Rectangle other) && Equals(other);
        }

        public readonly bool Equals(Rectangle other)
        {
            return this == other;
        }

        public override readonly int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + Point.GetHashCode();
                hash = (hash * 7) + Height.GetHashCode();
                hash = (hash * 7) + Width.GetHashCode();
                return hash;
            }
        }

        public readonly bool IsIdentical(Rectangle other)
        {
            return Point.IsIdentical(other.Point)
                && Floats.IsEqual(Height, other.Height)
                && Floats.IsEqual(Width, other.Width);
        }

        public readonly bool IsIdenticalBySize(Rectangle other)
        {
            return Floats.IsEqual(Height, other.Height)
                && Floats.IsEqual(Width, other.Width);
        }

        public override readonly string ToString()
        {
            return $"{{Width:{Width}, Height:{Height}, Point:{Point}}}";
        }

        public void Translate(PointF delta)
        {
            Point.Translate(delta);
        }

        public void Translate(float dx, float dy)
        {
            Point.Translate(dx, dy);
        }
    }
}
