using System;

namespace Yolk.Geometry.Shapes
{
    public sealed class ShapeFactoryError : Exception
    {
        public ShapeFactoryError(string message) : base(message)
        {
        }
    }
}
