using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Yolk.Engine;

namespace Yolk.GameObjects
{
    [DataContract]
    public struct Location3D : IEquatable<Location3D>
    {
        [DataMember]
        public float X;

        [DataMember]
        public float Y;

        [DataMember]
        public float Z;

        public Location3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Location3D(Location2D location)
        {
            X = location.X;
            Y = location.Y;
            Z = 0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Location3D location, Location3D other)
        {
            return location.X == other.X
                && location.Y == other.Y
                && location.Z == other.Z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Location3D location, Location3D other)
        {
            return !(location == other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals([NotNullWhen(true)] object obj)
        {
            return (obj is Location3D location) && Equals(location);
        }

        public readonly bool Equals(Location3D other)
        {
            return this == other;
        }

        public readonly bool IsIdentical(Location3D other)
        {
            return Floats.IsEqual(X, other.X)
                && Floats.IsEqual(Y, other.Y)
                && Floats.IsEqual(Z, other.Z);
        }

        public override readonly int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + X.GetHashCode();
                hash = (hash * 7) + Y.GetHashCode();
                hash = (hash * 7) + Z.GetHashCode();
                return hash;
            }
        }

        public override readonly string ToString()
        {
            return $"{{X:{X}, Y:{Y}, Z:{Z}}}";
        }
    }
}
