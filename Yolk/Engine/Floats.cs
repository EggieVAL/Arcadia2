using System;

namespace Yolk.Engine
{
    public static class Floats
    {
        public static bool IsEqual(float a, float b, float epsilon = float.Epsilon)
        {
            float difference = MathF.Abs(a - b);
            return a == b
                || difference / (MathF.Abs(a) + MathF.Abs(b)) < epsilon;
        }

        public static bool IsEqual(double a, double b, double epsilon = float.Epsilon)
        {
            double difference = Math.Abs(a - b);
            return a == b
                || difference / (Math.Abs(a) + Math.Abs(b)) < epsilon;
        }

        public static bool IsEqual(decimal a, decimal b, decimal epsilon = (decimal) float.Epsilon)
        {
            decimal difference = Math.Abs(a - b);
            return a == b
                || difference / (Math.Abs(a) + Math.Abs(b)) < epsilon;
        }
    }
}
