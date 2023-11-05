using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Yolk.Geometry.Shapes
{
    [DataContract]
    public struct Ellipse : IEquatable<Ellipse>
    {
        [DataMember]
        public PointF Center;

        [DataMember]
        public PointF Axis;

        public Ellipse(PointF axis, PointF center)
        {
            Axis = axis;
            Center = center;
        }

        public Ellipse(PointF axis) : this(axis, PointF.Origin)
        {
        }

        public readonly float ApproximationError
        {
            get
            {
                return MathF.Pow(Lambda, 10);
            }
        }

        public readonly float Area => MathF.PI * Axis.X * Axis.Y;

        public readonly float Circumference
        {
            get
            {
                float a = SemiMajorAxis;
                float b = SemiMinorAxis;
                float t = Lambda;
                return MathF.PI * (a + b) * (1 + (3*t / (10 + MathF.Sqrt(4 - 3*t))));
            }
        }

        public readonly float Eccentricity
        {
            get
            {
                float a = SemiMajorAxis;
                float b = SemiMinorAxis;
                return MathF.Sqrt(1 - (b * b / (a * a)));
            }
        }

        private readonly float Lambda
        {
            get
            {
                float a = SemiMajorAxis;
                float b = SemiMinorAxis;
                float c = a - b;
                float d = a + b;
                return (c * c) / (d * d);
            }
        }

        public readonly float SemiMajorAxis => MathF.Max(Axis.X, Axis.Y);

        public readonly float SemiMinorAxis => MathF.Min(Axis.X, Axis.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Ellipse ellipse, Ellipse other)
        {
            return ellipse.Center == other.Center
                && ellipse.Axis == other.Axis;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Ellipse ellipse, Ellipse other)
        {
            return !(ellipse == other);
        }

        public readonly bool ContainsPoint(PointF point)
        {
            point -= Center / Axis;
            return PointF.DotProduct(point, point) <= 1;
        }

        public readonly bool ContainsPoint(float x, float y)
        {
            return ContainsPoint(new PointF(x, y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals([NotNullWhen(true)] object obj)
        {
            return (obj is Ellipse other) && Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(Ellipse other)
        {
            return this == other;
        }

        public override readonly int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + Axis.GetHashCode();
                hash = (hash * 7) + Center.GetHashCode();
                return hash;
            }
        }

        public readonly bool IsIdentical(Ellipse other)
        {
            return Axis.IsIdentical(other.Axis)
                && Center.IsIdentical(other.Center);
        }

        public readonly bool IsIdenticalBySize(Ellipse other)
        {
            return Axis.IsIdentical(other.Axis);
        }

        public override readonly string ToString()
        {
            return $"{{Axis:{Axis}, Center:{Center}}}";
        }

        public void Translate(PointF delta)
        {
            Center.Translate(delta);
        }

        public void Translate(float dx, float dy)
        {
            Center.Translate(dx, dy);
        }
    }
}
