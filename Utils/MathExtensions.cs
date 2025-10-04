using System;

namespace PraticaPoo.Utils
{
    /// <summary>
    /// Provides extension methods for numeric operations. Extension methods extend existing
    /// types without modifying their definitions.
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        /// Returns <paramref name="value"/> squared. Demonstrates a simple static extension.
        /// </summary>
        public static int Square(this int value) => value * value;

        /// <summary>
        /// Clamps <paramref name="value"/> between the provided <paramref name="min"/> and <paramref name="max"/> values.
        /// Thread‑safe by virtue of being pure (no shared state).
        /// </summary>
        public static int Clamp(this int value, int min, int max)
        {
            if (min > max)
                throw new ArgumentException($"{nameof(min)} cannot be greater than {nameof(max)}.");
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}
