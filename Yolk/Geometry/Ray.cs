using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Yolk.Engine;

namespace Yolk.Geometry
{
    [DataContract]
    public struct Ray : IEquatable<Ray>
    {
        [DataMember]
        public PointF Point;

        [DataMember]
        public Vector Direction;

        public Ray(PointF point, Vector direction)
        {
            Point = point;
            Direction = direction;
        }

        public Ray(PointF from, PointF to)
        {
            Point = from;
            Direction = (to - from).ToVector();
        }

        public readonly float Slope
        {
            get
            {
                return Floats.IsEqual(Direction.X, 0f)
                    ? float.PositiveInfinity
                    : Direction.Y / Direction.X;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray operator -(Ray ray)
        {
            return new Ray(ray.Point, -ray.Direction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Ray ray, Ray other)
        {
            return ray.Point == other.Point
                && ray.Direction.Normalize() == other.Direction.Normalize();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Ray ray, Ray other)
        {
            return !(ray == other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float AngleBetween(Ray ray, Ray other)
        {
            if (Intersects(ray, other) is null)
            {
                return float.NaN;
            }

            float slope1 = ray.Slope;
            float slope2 = other.Slope;
            float value = (slope1 - slope2) / (1 + (slope1 * slope2));
            return MathF.Atan(MathF.Abs(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF? Cast(Ray ray, Line line)
        {
            if (Floats.IsEqual(ray.Slope, line.Slope))
            {
                return null;
            }

            PointF dc = line.PointB - line.PointA;
            PointF ac = ray.Point - line.PointA;
            PointF ba = ray.Direction.ToPointF();

            float t = ((dc.X * ac.Y) - (dc.Y * ac.X)) / ((dc.Y * ba.X) - (dc.X * ba.Y));
            return Lerp(ray, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF? Cast(Ray ray, Ray other)
        {
            return Intersects(ray, other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF? Cast(Ray ray, Segment segment)
        {
            if (Floats.IsEqual(ray.Slope, segment.Slope))
            {
                return null;
            }

            PointF dc = segment.PointB - segment.PointA;
            PointF ac = ray.Point - segment.PointA;
            PointF ba = ray.Direction.ToPointF();

            float t = ((dc.X * ac.Y) - (dc.Y * ac.X)) / ((dc.Y * ba.X) - (dc.X * ba.Y));
            float u = (ac.X + (ba.X * t)) / dc.X;

            return Segment.ValidDirection(segment, u)
                ? Lerp(ray, t)
                : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF? Intersects(Ray ray, Ray other)
        {
            if (Vector.IsCollinear(ray.Direction, other.Direction))
            {
                return null;
            }

            PointF dc = ray.Point + ray.Direction.ToPointF() - other.Point;
            PointF ac = ray.Point - other.Point;
            PointF ba = ray.Direction.ToPointF();

            float t = ((dc.X * ac.Y) - (dc.Y * ac.X)) / ((dc.Y * ba.X) - (dc.X * ba.Y));
            float u = (ac.X + (ba.X * t)) / dc.X;

            return ValidDirection(other, u)
                ? Lerp(ray, t)
                : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAntiParallel(Ray ray, Ray other)
        {
            return Vector.IsAntiParallel(ray.Direction, other.Direction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCollinear(Ray ray, Ray other)
        {
            Vector direction = new(ray.Point, other.Point);
            return Vector.IsCollinear(direction, ray.Direction)
                && Vector.IsCollinear(direction, other.Direction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsHorizontal(Ray ray)
        {
            return Floats.IsEqual(ray.Slope, 0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrthogonal(Ray ray, Ray other)
        {
            return Vector.IsOrthogonal(ray.Direction, other.Direction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsParallel(Ray ray, Ray other)
        {
            return Vector.IsParallel(ray.Direction, other.Direction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsVertical(Ray ray)
        {
            return float.IsInfinity(ray.Slope);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF? Lerp(Ray ray, float amount)
        {
            return ValidDirection(ray, amount)
                ? PointF.Lerp(ray.Point, ray.Point + ray.Direction.ToPointF(), amount)
                : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray Negate(Ray ray)
        {
            return -ray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray PointEast(PointF point)
        {
            return new Ray(point, Vector.UnitX);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray PointEast(float x, float y)
        {
            return PointEast(new PointF(x, y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray PointNorth(PointF point)
        {
            return new Ray(point, -Vector.UnitY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray PointNorth(float x, float y)
        {
            return PointNorth(new PointF(x, y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray PointNortheast(PointF point)
        {
            return new Ray(point, new Vector(1f, -1f));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray PointNortheast(float x, float y)
        {
            return PointNortheast(new PointF(x, y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray PointNorthwest(PointF point)
        {
            return new Ray(point, -Vector.Unit);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray PointNorthwest(float x, float y)
        {
            return PointNorthwest(new PointF(x, y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray PointSouth(PointF point)
        {
            return new Ray(point, Vector.UnitY);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray PointSouth(float x, float y)
        {
            return PointSouth(new PointF(x, y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray PointSoutheast(PointF point)
        {
            return new Ray(point, Vector.Unit);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray PointSoutheast(float x, float y)
        {
            return PointSoutheast(new PointF(x, y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray PointSouthwest(PointF point)
        {
            return new Ray(point, new Vector(-1f, 1f));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray PointSouthwest(float x, float y)
        {
            return PointSouthwest(new PointF(x, y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray PointWest(PointF point)
        {
            return new Ray(point, -Vector.UnitX);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray PointWest(float x, float y)
        {
            return PointWest(new PointF(x, y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray Reflect(Ray ray, Vector normal)
        {
            return new Ray(ray.Point, Vector.Reflect(ray.Direction, normal));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray ReflectOverXAxis(Ray ray)
        {
            return new Ray(
                PointF.ReflectOverXAxis(ray.Point),
                Vector.ReflectOverXAxis(ray.Direction)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ray ReflectOverYAxis(Ray ray)
        {
            return new Ray(
                PointF.ReflectOverYAxis(ray.Point),
                Vector.ReflectOverYAxis(ray.Direction)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidDirection(Ray ray, float direction)
        {
            return (ray.Point.Inclusive)
                ? direction >= 0f
                : direction > 0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals([NotNullWhen(true)] object obj)
        {
            return (obj is Ray other) && Equals(other);
        }

        public readonly bool Equals(Ray other)
        {
            return this == other;
        }

        public override readonly int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + Point.GetHashCode();
                hash = (hash * 7) + Direction.Normalize().GetHashCode();
                return hash;
            }
        }

        public readonly bool IsIdentical(Ray other)
        {
            return Point.IsIdentical(other.Point)
                && Direction.IsIdentical(other.Direction);
        }

        public override readonly string ToString()
        {
            return $"{{Point:{Point}, Direction:{Direction}}}";
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
