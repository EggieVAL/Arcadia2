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
        public Point Center;

        [DataMember]
        public float Height;

        [DataMember]
        public float Width;

        public Rectangle(float width, float height, Point center)
        {
            Width = width;
            Height = height;
            Center = center;
        }

        public Rectangle(float width, float height)
            : this(width, height, Point.Origin)
        {
        }

        public readonly float Area => Width * Height;

        public readonly List<Segment> Edges => ShapeFactory.CreateShape(Map);

        public readonly List<Point> Map
        {
            get
            {
                float x = Center.X - (Width * 0.5f);
                float y = Center.Y - (Height * 0.5f);

                return new List<Point>()
                {
                    new Point(x, y),
                    new Point(x + Width, y),
                    new Point(x + Width, y + Height),
                    new Point(x, y + Height),
                    new Point(x, y)
                };
            }
        }

        public readonly float Perimeter => (Width + Height) * 2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Rectangle rectangle, Rectangle other)
        {
            return rectangle.Center == other.Center
                && rectangle.Height == other.Height
                && rectangle.Width == other.Width;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Rectangle rectangle, Rectangle other)
        {
            return !(rectangle == other);
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
                hash = (hash * 7) + Center.GetHashCode();
                hash = (hash * 7) + Height.GetHashCode();
                hash = (hash * 7) + Width.GetHashCode();
                return hash;
            }
        }

        public readonly bool IsIdentical(Rectangle other)
        {
            return Center.IsIdentical(other.Center)
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
            return $"{{Width:{Width}, Height:{Height}, Center:{Center}}}";
        }

        public void Translate(Point delta)
        {
            Center.Translate(delta);
        }

        public void Translate(float dx, float dy)
        {
            Center.Translate(dx, dy);
        }
    }
}
