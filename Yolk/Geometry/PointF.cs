using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Yolk.Engine;

namespace Yolk.Geometry
{
    [DataContract]
    public struct PointF : IEquatable<PointF>
    {
        [DataMember]
        public float X;

        [DataMember]
        public float Y;

        [DataMember]
        public bool Inclusive;

        public PointF(float x, float y, bool inclusive)
        {
            X = x;
            Y = y;
            Inclusive = inclusive;
        }

        public PointF(float x, float y) : this(x, y, true)
        {
        }

        public PointF(float value, bool inclusive) : this(value, value, inclusive)
        {
        }

        public PointF(float value) : this(value, true)
        {
        }

        public static PointF Origin => new(0);

        public static explicit operator Microsoft.Xna.Framework.Point(PointF point)
        {
            return new Microsoft.Xna.Framework.Point((int) point.X, (int) point.Y);
        }

        public static explicit operator Point(PointF point)
        {
            return new Point((int) point.X, (int) point.Y, point.Inclusive);
        }

        public static explicit operator System.Drawing.Point(PointF point)
        {
            return new System.Drawing.Point((int) point.X, (int) point.Y);
        }

        public static explicit operator System.Drawing.PointF(PointF point)
        {
            return new System.Drawing.PointF(point.X, point.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF operator +(PointF addend1, PointF addend2)
        {
            return new PointF(
                addend1.X + addend2.X,
                addend1.Y + addend2.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF operator +(PointF addend1, float addend2)
        {
            return addend1 + new PointF(addend2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF operator +(float addend1, PointF addend2)
        {
            return addend2 + addend1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF operator -(PointF minuend, PointF subtrahend)
        {
            return new PointF(
                minuend.X - subtrahend.X,
                minuend.Y - subtrahend.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF operator -(PointF minuend, float subtrahend)
        {
            return minuend - new PointF(subtrahend);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF operator -(float minuend, PointF subtrahend)
        {
            return new PointF(minuend) - subtrahend;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF operator -(PointF point)
        {
            return Origin - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF operator *(PointF multiplicand, PointF multiplier)
        {
            return new PointF(
                multiplicand.X * multiplier.X,
                multiplicand.Y * multiplier.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF operator *(PointF multiplicand, float multiplier)
        {
            return multiplicand * new PointF(multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF operator *(float multiplicand, PointF multiplier)
        {
            return multiplier * multiplicand;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF operator /(PointF dividend, PointF divisor)
        {
            return new PointF(
                dividend.X / divisor.X,
                dividend.Y / divisor.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF operator /(PointF dividend, float divisor)
        {
            return dividend / new PointF(divisor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF operator /(float dividend, PointF divisor)
        {
            return new PointF(dividend) / divisor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(PointF point, PointF other)
        {
            return point.X == other.X
                && point.Y == other.Y
                && point.Inclusive == other.Inclusive;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(PointF point, PointF other)
        {
            return !(point == other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Add(PointF addend1, PointF addend2)
        {
            return addend1 + addend2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CrossProduct(PointF point, PointF other)
        {
            return (point.X * other.Y) - (point.Y * other.X);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(PointF point, PointF other)
        {
            return MathF.Sqrt(DistanceSquared(point, other));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceSquared(PointF point, PointF other)
        {
            PointF difference = point - other;
            return DotProduct(difference, difference);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Divide(PointF dividend, PointF divisor)
        {
            return dividend / divisor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DotProduct(PointF point, PointF other)
        {
            return (point.X * other.Y) + (point.Y * other.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Lerp(PointF point, PointF other, float amount)
        {
            return point + ((other - point) * amount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Multiply(PointF multiplicand, PointF multiplier)
        {
            return multiplicand * multiplier;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Subtract(PointF minuend, PointF subtrahend)
        {
            return minuend - subtrahend;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF Negate(PointF point)
        {
            return -point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF ReflectOverXAxis(PointF point)
        {
            return new PointF(point.X, -point.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF ReflectOverYAxis(PointF point)
        {
            return new PointF(-point.X, point.Y);
        }

        public readonly PointF ClosedPoint()
        {
            return new PointF(X, Y, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals([NotNullWhen(true)] object obj)
        {
            return (obj is PointF other) && Equals(other);
        }

        public readonly bool Equals(PointF other)
        {
            return this == other;
        }

        public override readonly int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + X.GetHashCode();
                hash = (hash * 7) + Y.GetHashCode();
                hash = (hash * 7) + Inclusive.GetHashCode();
                return hash;
            }
        }

        public readonly bool IsIdentical(PointF other)
        {
            return Floats.IsEqual(X, other.X)
                && Floats.IsEqual(Y, other.Y)
                && Inclusive == other.Inclusive;
        }

        public readonly PointF OpenPoint()
        {
            return new PointF(X, Y, false);
        }

        public readonly Point ToPoint()
        {
            return (Point) this;
        }

        public override readonly string ToString()
        {
            return $"{{X:{X}, Y:{Y}}}";
        }

        public readonly System.Drawing.Point ToSystemPoint()
        {
            return (System.Drawing.Point) this;
        }

        public readonly System.Drawing.PointF ToSystemPointF()
        {
            return (System.Drawing.PointF) this;
        }

        public readonly System.Numerics.Vector2 ToSystemVector()
        {
            return new System.Numerics.Vector2(X, Y);
        }

        public readonly Vector ToVector()
        {
            return new Vector(X, Y);
        }

        public readonly Microsoft.Xna.Framework.Point ToXnaPoint()
        {
            return (Microsoft.Xna.Framework.Point) this;
        }

        public readonly Microsoft.Xna.Framework.Vector2 ToXnaVector()
        {
            return new Microsoft.Xna.Framework.Vector2(X, Y);
        }

        public void Translate(PointF delta)
        {
            Translate(delta.X, delta.Y);
        }

        public void Translate(float dx, float dy)
        {
            unchecked
            {
                X += dx;
                Y += dy;
            }
        }
    }
}
