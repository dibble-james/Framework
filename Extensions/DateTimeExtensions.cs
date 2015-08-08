// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="James Dibble">
//    Copyright 2012 James Dibble
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace System
{
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Extensions of the <see cref="DateTime"/> class.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Get the ordinal string for the given <see cref="DateTime"/>.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <returns>
        /// An ordinal representation of the day.  <c>Eg: 15th</c>
        /// </returns>
        public static string Ordinal(this DateTime date)
        {
            string suffix;

            var ones = date.Day % 10;
            var tens = (int)Math.Floor(date.Day / 10M) % 10;

            if (tens == 1)
            {
                suffix = "th";
            }
            else
            {
                switch (ones)
                {
                    case 1:
                        suffix = "st";
                        break;

                    case 2:
                        suffix = "nd";
                        break;

                    case 3:
                        suffix = "rd";
                        break;

                    default:
                        suffix = "th";
                        break;
                }
            }

            return string.Format(CultureInfo.CurrentCulture, "{0}{1}", date.Day, suffix);
        }
    }
}