using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Yolk.Engine;

namespace Yolk.Geometry
{
    [DataContract]
    public struct Segment : IEquatable<Segment>
    {
        [DataMember]
        public Point PointA;

        [DataMember]
        public Point PointB;

        public Segment(Point a, Point b)
        {
            PointA = a;
            PointB = b;
        }

        public Segment(float x1, float y1, float x2, float y2)
        {
            PointA = new Point(x1, y1);
            PointB = new Point(x2, y2);
        }

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

        public readonly float Length => Point.Distance(PointA, PointB);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Segment left, Segment right)
        {
            return left.PointA == right.PointA
                && left.PointB == right.PointB;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Segment left, Segment right)
        {
            return !(left == right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float AngleBetween(Segment segment, Segment other)
        {
            if (Intersects(segment, other) is null)
            {
                return float.NaN;
            }

            float slope1 = segment.Slope;
            float slope2 = segment.Slope;
            float value = (slope1 - slope2) / (1 + (slope1 * slope2));
            return MathF.Atan(MathF.Abs(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point? Intersects(Segment segment, Segment other)
        {
            if (IsParallel(segment, other))
            {
                return null;
            }

            Point dc = other.PointB - other.PointA;
            Point ac = segment.PointA - other.PointA;
            Point ba = segment.PointB - segment.PointA;

            float t = ((dc.X * ac.Y) - (dc.Y * ac.X)) / ((dc.Y * ba.X) - (dc.X * ba.Y));
            float u = (ac.X + (ba.X * t)) / dc.X;

            return ValidDirection(other, u)
                ? Lerp(segment, t)
                : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCollinear(Segment segment, Segment other)
        {
            return Line.IsCollinear(
                new Line(segment.PointA, segment.PointB),
                new Line(other.PointA, other.PointB)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCongruent(Segment segment, Segment other)
        {
            return Floats.IsEqual(segment.Length, other.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsHorizontal(Segment segment)
        {
            return Floats.IsEqual(segment.Slope, 0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrthogonal(Segment segment, Segment other)
        {
            return Floats.IsEqual(segment.Slope * other.Slope, -1f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsParallel(Segment segment, Segment other)
        {
            return Floats.IsEqual(segment.Slope, other.Slope);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsVertical(Segment segment)
        {
            return float.IsInfinity(segment.Slope);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point? Lerp(Segment segment, float amount)
        {
            return ValidDirection(segment, amount)
                ? Point.Lerp(segment.PointA, segment.PointB, amount)
                : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Segment ReflectOverXAxis(Segment segment)
        {
            return new Segment(
                Point.ReflectOverXAxis(segment.PointA),
                Point.ReflectOverXAxis(segment.PointB)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Segment ReflectOverYAxis(Segment segment)
        {
            return new Segment(
                Point.ReflectOverYAxis(segment.PointA),
                Point.ReflectOverYAxis(segment.PointB)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidDirection(Segment segment, float direction)
        {
            bool greaterThanMin = (segment.PointA.Inclusive)
                ? direction >= 0
                : direction > 0;

            bool lessThanMax = (segment.PointB.Inclusive)
                ? direction <= 100
                : direction < 100;

            return greaterThanMin && lessThanMax;
        }

        public readonly Segment ClosedSegment()
        {
            return new Segment(PointA.ClosedPoint(), PointB.ClosedPoint());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals([NotNullWhen(true)] object obj)
        {
            return (obj is Segment other) && Equals(other);
        }

        public readonly bool Equals(Segment other)
        {
            return this == other;
        }

        public override readonly int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + PointA.GetHashCode();
                hash = (hash * 7) + PointB.GetHashCode();
                return hash;
            }
        }

        public readonly Segment HalfSegment(bool includePointA)
        {
            return includePointA
                 ? new Segment(PointA.ClosedPoint(), PointB.OpenPoint())
                 : new Segment(PointA.OpenPoint(), PointB.ClosedPoint());
        }

        public readonly bool IsIdentical(Segment other)
        {
            return PointA.IsIdentical(other.PointA)
                && PointB.IsIdentical(other.PointB);
        }

        public readonly Segment OpenSegment()
        {
            return new Segment(PointA.OpenPoint(), PointB.OpenPoint());
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
