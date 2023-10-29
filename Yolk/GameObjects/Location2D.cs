using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Yolk.Engine;

namespace Yolk.GameObjects
{
    public struct Location2D : IEquatable<Location2D>
    {
        [DataMember]
        public float X;

        [DataMember]
        public float Y;

        public Location2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Location2D location, Location2D other)
        {
            return location.X == other.X
                && location.Y == other.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Location2D location, Location2D other)
        {
            return !(location == other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals([NotNullWhen(true)] object obj)
        {
            return (obj is Location2D other) && Equals(other);
        }

        public readonly bool Equals(Location2D other)
        {
            return this == other;
        }

        public readonly bool IsIdentical(Location2D other)
        {
            return Floats.IsEqual(X, other.X)
                && Floats.IsEqual(Y, other.Y);
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

        public override readonly string ToString()
        {
            return $"{{X:{X}, Y:{Y}}}";
        }
    }
}
