using System;

namespace TWYK.Core.Domain
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class RoundingHelper
    {
        /// <summary>
        /// Round with default round type: Rounding001
        /// </summary>
        /// <param name="value">Value to round</param>
        /// <returns>Rounded value</returns>
        public static decimal Round(this decimal value) {
            
            //default round (Rounding001)
            var rez = Math.Round(value, 2);
            decimal t;

            t = (rez - Math.Truncate(rez)) * 10;
            t = (t - Math.Truncate(t)) * 10;

            t = t >= 5 ? 10 - t : 5 - t;
            rez += t / 100;

            return rez;
        }
    }
}