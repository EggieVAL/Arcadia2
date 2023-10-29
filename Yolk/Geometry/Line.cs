using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Yolk.Engine;

namespace Yolk.Geometry
{
    [DataContract]
    public struct Line : IEquatable<Line>
    {
        [DataMember]
        public Point PointA;

        [DataMember]
        public Point PointB;

        public Line(Point a, Point b)
        {
            PointA = a;
            PointB = b;
        }

        public Line(Point point, float deltaY, float deltaX)
        {
            PointA = point;
            PointB = point + new Point(deltaX, deltaY);
        }

        public Line(Point point, float slope) : this(point, slope, 1)
        {
        }

        public Line(Point point, Vector direction) : this(point, direction.Y, direction.X)
        {
        }

        public static Line Identity => new(Point.Origin, Vector.Unit);

        public static Line XAxis => new(Point.Origin, Vector.UnitX);

        public static Line YAxis => new(Point.Origin, Vector.UnitY);

        public readonly float Slope
        {
            get
            {
                Point delta = PointB - PointA;
                return Floats.IsEqual(delta.X, 0f)
                    ? float.PositiveInfinity
                    : delta.Y / delta.X;
            }
        }

        public readonly float XIntercept
        {
            get
            {
                return IsVertical(this)
                    ? PointA.X
                    : -YIntercept / Slope;
            }
        }

        public readonly float YIntercept
        {
            get
            {
                return IsVertical(this)
                    ? float.NaN
                    : PointA.Y - (Slope * PointA.X);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Line line, Line other)
        {
            return line.Slope == other.Slope
                && line.XIntercept == other.XIntercept
                && line.YIntercept == other.YIntercept;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Line line, Line other)
        {
            return !(line == other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float AngleBetween(Line line, Line other)
        {
            if (IsParallel(line, other))
            {
                return float.NaN;
            }

            float slope1 = line.Slope;
            float slope2 = other.Slope;
            float value = (slope1 - slope2) / (1 + (slope1 * slope2));
            return MathF.Atan(MathF.Abs(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point? Cast(Line line, Line other)
        {
            return Intersects(line, other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point? Cast(Line line, Ray ray)
        {
            return Ray.Cast(ray, line);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point? Cast(Line line, Segment segment)
        {
            if (Floats.IsEqual(line.Slope, segment.Slope))
            {
                return null;
            }

            Point dc = segment.PointB - segment.PointA;
            Point ac = line.PointA - segment.PointA;
            Point ba = line.PointB - line.PointA;

            float t = ((dc.X * ac.Y) - (dc.Y * ac.X)) / ((dc.Y * ba.X) - (dc.X * ba.Y));
            float u = (ac.X + (ba.X * t)) / dc.X;

            return Segment.ValidDirection(segment, u)
                ? Lerp(line, t)
                : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceBetween(Line line, Line other)
        {
            if (IsVertical(line))
            {
                return other.PointA.X - line.PointA.X;
            }

            float m = line.Slope;
            float b1 = line.YIntercept;
            float b2 = other.YIntercept;
            return (b1 - b2) / MathF.Sqrt((m*m) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceBetween(Line line, Point point)
        {
            if (IsVertical(line))
            {
                return point.X - line.PointA.X;
            }

            float x = point.X;
            float y = point.Y;
            float m = line.Slope;
            float b = line.YIntercept;
            return -((m*x) - y + b) / MathF.Sqrt((m*m) + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point? Intersects(Line line, Line other)
        {
            if (IsParallel(line, other))
            {
                return null;
            }

            Point dc = other.PointB - other.PointA;
            Point ac = line.PointA - other.PointA;
            Point ba = line.PointB - other.PointA;

            float t = ((dc.X * ac.Y) - (dc.Y * ac.X))
                    / ((dc.Y * ba.X) - (dc.X * ba.Y));

            return Lerp(line, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCollinear(Line line, Line other)
        {
            return Floats.IsEqual(line.Slope, other.Slope)
                && Floats.IsEqual(line.XIntercept, other.XIntercept)
                && Floats.IsEqual(line.YIntercept, other.YIntercept);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsHorizontal(Line line)
        {
            return Floats.IsEqual(line.Slope, 0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrthogonal(Line line, Line other)
        {
            return Floats.IsEqual(line.Slope * other.Slope, -1f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsParallel(Line line, Line other)
        {
            return Floats.IsEqual(line.Slope, other.Slope);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsVertical(Line line)
        {
            return float.IsInfinity(line.Slope);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point Lerp(Line line, float amount)
        {
            return Point.Lerp(line.PointA, line.PointB, amount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Line ReflectOverXAxis(Line line)
        {
            return new Line(
                Point.ReflectOverXAxis(line.PointA),
                Point.ReflectOverXAxis(line.PointB)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Line ReflectOverYAxis(Line line)
        {
            return new Line(
                Point.ReflectOverYAxis(line.PointA),
                Point.ReflectOverYAxis(line.PointB)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals([NotNullWhen(true)] object obj)
        {
            return (obj is Line other) && Equals(other);
        }

        public readonly bool Equals(Line other)
        {
            return this == other;
        }

        public override readonly int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + Slope.GetHashCode();
                hash = (hash * 7) + XIntercept.GetHashCode();
                hash = (hash * 7) + YIntercept.GetHashCode();
                return hash;
            }
        }

        public readonly bool IsIdentical(Line other)
        {
            return IsCollinear(this, other);
        }

        public override readonly string ToString()
        {
            return $"{{PointA:{PointA}, PointB:{PointB}}}";
        }

        public void Translate(Point delta)
        {
            PointA.Translate(delta);
            PointB.Translate(delta);
        }

        public void Translate(float dx, float dy)
        {
            PointA.Translate(dx, dy);
            PointB.Translate(dx, dy);
        }
    }
}
