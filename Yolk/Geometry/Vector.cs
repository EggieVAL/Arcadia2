using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Yolk.Engine;

namespace Yolk.Geometry
{
    [DataContract]
    public struct Vector : IEquatable<Vector>
    {
        [DataMember]
        public float X;

        [DataMember]
        public float Y;

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector(float value) : this(value, value)
        {
        }

        public Vector(PointF from, PointF to)
        {
            PointF difference = from - to;
            X = difference.X;
            Y = difference.Y;
        }

        public static Vector Zero => default;

        public static Vector Unit => new(1f);

        public static Vector UnitX => new(1f, 0f);

        public static Vector UnitY => new(0f, 1f);

        public readonly float Magnitude => MathF.Sqrt(DotProduct(this, this));

        public static implicit operator Microsoft.Xna.Framework.Vector2(Vector vector)
        {
            return new Microsoft.Xna.Framework.Vector2(vector.X, vector.Y);
        }

        public static implicit operator System.Numerics.Vector2(Vector vector)
        {
            return new System.Numerics.Vector2(vector.X, vector.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector operator +(Vector addend1, Vector addend2)
        {
            return new Vector(
                addend1.X + addend2.X,
                addend1.Y + addend2.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector operator +(Vector addend1, float addend2)
        {
            return addend1 + new Vector(addend2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector operator +(float addend1, Vector addend2)
        {
            return addend2 + new Vector(addend1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector operator -(Vector minuend, Vector subtrahend)
        {
            return new Vector(
                minuend.X - subtrahend.X,
                minuend.Y - subtrahend.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector operator -(Vector minuend, float subtrahend)
        {
            return minuend - new Vector(subtrahend);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector operator -(float minuend, Vector subtrahend)
        {
            return new Vector(minuend) - subtrahend;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector operator *(Vector multiplicand, Vector multiplier)
        {
            return new Vector(
                multiplicand.X * multiplier.X,
                multiplicand.Y * multiplier.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector operator -(Vector vector)
        {
            return Zero - vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector operator *(Vector multiplicand, float multiplier)
        {
            return multiplicand * new Vector(multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector operator *(float multiplicand, Vector multiplier)
        {
            return multiplier * new Vector(multiplicand);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector operator /(Vector dividend, Vector divisor)
        {
            return new Vector(
                dividend.X / divisor.X,
                dividend.Y / divisor.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector operator /(Vector dividend, float divisor)
        {
            return dividend / new Vector(divisor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector operator /(float dividend, Vector divisor)
        {
            return new Vector(dividend) / divisor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector vector, Vector other)
        {
            return vector.X == other.X
                && vector.Y == other.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector vector, Vector other)
        {
            return !(vector == other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Abs(Vector vector)
        {
            return new Vector(
                MathF.Abs(vector.X),
                MathF.Abs(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Acos(Vector vector)
        {
            return new Vector(
                MathF.Acos(vector.X),
                MathF.Acos(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Acosh(Vector vector)
        {
            return new Vector(
                MathF.Acosh(vector.X),
                MathF.Acosh(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Add(Vector addend1, Vector addend2)
        {
            return addend1 + addend2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Add(Vector addend1, float addend2)
        {
            return addend1 + addend2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Add(float addend1, Vector addend2)
        {
            return addend1 + addend2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Asin(Vector vector)
        {
            return new Vector(
                MathF.Asin(vector.X),
                MathF.Asin(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Asinh(Vector vector)
        {
            return new Vector(
                MathF.Asinh(vector.X),
                MathF.Asinh(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Atan(Vector vector)
        {
            return new Vector(
                MathF.Atan(vector.X),
                MathF.Atan(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Atan2(Vector y, Vector x)
        {
            return new Vector(
                MathF.Atan2(y.X, x.X),
                MathF.Atan2(y.Y, x.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Atanh(Vector vector)
        {
            return new Vector(
                MathF.Atanh(vector.X),
                MathF.Atanh(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Cbrt(Vector vector)
        {
            return new Vector(
                MathF.Cbrt(vector.X),
                MathF.Cbrt(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Ceiling(Vector vector)
        {
            return new Vector(
                MathF.Ceiling(vector.X),
                MathF.Ceiling(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Clamp(Vector vector, Vector min, Vector max)
        {
            return Min(Max(vector, min), max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Cos(Vector vector)
        {
            return new Vector(
                MathF.Cos(vector.X),
                MathF.Cos(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Cosh(Vector vector)
        {
            return new Vector(
                MathF.Cosh(vector.X),
                MathF.Cosh(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CrossProduct(Vector vector, Vector other)
        {
            return (vector.X * other.Y) - (vector.Y * other.X);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vector vector, Vector other)
        {
            return MathF.Sqrt(DistanceSquared(vector, other));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceSquared(Vector vector, Vector other)
        {
            Vector difference = vector - other;
            return DotProduct(difference, difference);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Divide(Vector dividend, Vector divisor)
        {
            return dividend / divisor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Divide(Vector dividend, float divisor)
        {
            return dividend / divisor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Divide(float dividend, Vector divisor)
        {
            return dividend / divisor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DotProduct(Vector vector, Vector other)
        {
            return (vector.X * other.X) + (vector.Y * other.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Exp(Vector vector)
        {
            return new Vector(
                MathF.Exp(vector.X),
                MathF.Exp(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Floor(Vector vector)
        {
            return new Vector(
                MathF.Floor(vector.X),
                MathF.Floor(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector IEEERemainder(Vector x, Vector y)
        {
            return new Vector(
                MathF.IEEERemainder(x.X, y.X),
                MathF.IEEERemainder(x.Y, y.X)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector ILogB(Vector vector)
        {
            return new Vector(
                MathF.ILogB(vector.X),
                MathF.ILogB(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAntiParallel(Vector vector, Vector other)
        {
            vector = vector.Normalize();
            other = other.Normalize();
            return IsCollinear(vector, other)
                && !vector.IsIdentical(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCollinear(Vector vector, Vector other)
        {
            return Floats.IsEqual(CrossProduct(vector, other), 0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOrthogonal(Vector vector, Vector other)
        {
            return Floats.IsEqual(DotProduct(vector, other), 0f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsParallel(Vector vector, Vector other)
        {
            vector = vector.Normalize();
            other = other.Normalize();
            return IsCollinear(vector, other)
                && vector.IsIdentical(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Lerp(Vector vector, Vector other, float amount)
        {
            return vector + ((other - vector) * amount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Log(Vector vector, Vector @base)
        {
            return new Vector(
                MathF.Log(vector.X, @base.X),
                MathF.Log(vector.Y, @base.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Log(Vector vector)
        {
            return new Vector(
                MathF.Log(vector.X),
                MathF.Log(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Log10(Vector vector)
        {
            return new Vector(
                MathF.Log10(vector.X),
                MathF.Log10(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Log2(Vector vector)
        {
            return new Vector(
                MathF.Log2(vector.X),
                MathF.Log2(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Max(Vector vector, Vector other)
        {
            return new Vector(
                (vector.X > other.X) ? vector.X : other.X,
                (vector.Y > other.Y) ? vector.Y : other.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Min(Vector vector, Vector other)
        {
            return new Vector(
                (vector.X < other.X) ? vector.X : other.X,
                (vector.Y < other.Y) ? vector.Y : other.Y
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Multiply(Vector multiplicand, Vector multiplier)
        {
            return multiplicand * multiplier;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Multiply(Vector multiplicand, float multiplier)
        {
            return multiplicand * multiplier;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Multiply(float multiplicand, Vector multiplier)
        {
            return multiplicand * multiplier;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Negate(Vector vector)
        {
            return -vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Pow(Vector @base, Vector power)
        {
            return new Vector(
                MathF.Pow(@base.X, power.X),
                MathF.Pow(@base.Y, power.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Reflect(Vector vector, Vector normal)
        {
            float dotProduct = DotProduct(vector, normal);
            return vector - (2 * dotProduct * normal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector ReflectOverXAxis(Vector vector)
        {
            return new Vector(vector.X, -vector.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector ReflectOverYAxis(Vector vector)
        {
            return new Vector(-vector.X, vector.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Round(Vector vector)
        {
            return new Vector(
                MathF.Round(vector.X),
                MathF.Round(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Round(Vector vector, int digits)
        {
            return new Vector(
                MathF.Round(vector.X, digits),
                MathF.Round(vector.Y, digits)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Round(Vector vector, MidpointRounding mode)
        {
            return new Vector(
                MathF.Round(vector.X, mode),
                MathF.Round(vector.Y, mode)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Round(Vector vector, int digits, MidpointRounding mode)
        {
            return new Vector(
                MathF.Round(vector.X, digits, mode),
                MathF.Round(vector.Y, digits, mode)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector ScaleB(Vector vector, int n)
        {
            return new Vector(
                MathF.ScaleB(vector.X, n),
                MathF.ScaleB(vector.Y, n)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Sin(Vector vector)
        {
            return new Vector(
                MathF.Sin(vector.X),
                MathF.Sin(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Sinh(Vector vector)
        {
            return new Vector(
                MathF.Sinh(vector.X),
                MathF.Sinh(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Sqrt(Vector vector)
        {
            return new Vector(
                MathF.Sqrt(vector.X),
                MathF.Sqrt(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Subtract(Vector minuend, Vector subtrahend)
        {
            return minuend - subtrahend;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Subtract(Vector minuend, float subtrahend)
        {
            return minuend - subtrahend;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Subtract(float minuend, Vector subtrahend)
        {
            return minuend - subtrahend;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Tan(Vector vector)
        {
            return new Vector(
                MathF.Tan(vector.X),
                MathF.Tan(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Tanh(Vector vector)
        {
            return new Vector(
                MathF.Tanh(vector.X),
                MathF.Tanh(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector Truncate(Vector vector)
        {
            return new Vector(
                MathF.Truncate(vector.X),
                MathF.Truncate(vector.Y)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals([NotNullWhen(true)] object obj)
        {
            return (obj is Vector other) && Equals(other);
        }

        public readonly bool Equals(Vector other)
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
                return hash;
            }
        }

        public readonly bool IsIdentical(Vector other)
        {
            return Floats.IsEqual(X, other.X)
                && Floats.IsEqual(Y, other.Y);
        }

        public readonly Vector Normalize()
        {
            return this / Magnitude;
        }

        public readonly Point ToPoint()
        {
            return new Point((int) X, (int) Y);
        }

        public readonly Point ToPoint(bool inclusive)
        {
            return new Point((int) X, (int) Y, inclusive);
        }

        public readonly PointF ToPointF()
        {
            return new PointF(X, Y);
        }

        public readonly PointF ToPointF(bool inclusive)
        {
            return new PointF(X, Y, inclusive);
        }

        public override readonly string ToString()
        {
            return $"{{X:{X}, Y:{Y}}}";
        }

        public readonly System.Drawing.Point ToSystemPoint()
        {
            return new System.Drawing.Point((int) X, (int) Y);
        }

        public readonly System.Drawing.PointF ToSystemPointF()
        {
            return new System.Drawing.PointF(X, Y);
        }

        public readonly System.Numerics.Vector2 ToSystemVector()
        {
            return this;
        }

        public readonly Microsoft.Xna.Framework.Point ToXnaPoint()
        {
            return new Microsoft.Xna.Framework.Point((int) X, (int) Y);
        }

        public readonly Microsoft.Xna.Framework.Vector2 ToXnaVector()
        {
            return this;
        }

        public void Translate(float dx, float dy)
        {
            unchecked
            {
                X += dx;
                Y += dy;
            }
        }

        public void Translate(Vector delta)
        {
            Translate(delta.X, delta.Y);
        }
    }
}
