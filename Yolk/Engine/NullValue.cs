using System;

namespace Yolk.Engine
{
    public static class NullValue
    {
        public static T Convert<T>(T? value) where T : struct
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value), "Cannot convert null to a value type.");
            }
            return (T) value;
        }
    }
}
