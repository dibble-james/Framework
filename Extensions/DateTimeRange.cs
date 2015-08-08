// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeRange.cs" company="James Dibble">
//   Copyright 2012 James Dibble
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace System
{
    using System.Collections.Generic;

    /// <summary>
    /// An object representing a pair of points in time.
    /// </summary>
    public struct DateTimeRange
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DateTimeRange"/> struct. 
        /// </summary>
        /// <param name="start">
        /// The <see cref="DateTime"/> this <see cref="DateTimeRange"/> began.
        /// </param>
        /// <param name="finish">
        /// The <see cref="DateTime"/> this <see cref="DateTimeRange"/> ended.
        /// </param>
        public DateTimeRange(DateTime start, DateTime finish) : this()
        {
            this.Start = start;
            this.Finish = finish;
        }

        /// <summary>
        /// Gets the <see cref="DateTime"/> this <see cref="DateTimeRange"/> began.
        /// </summary>
        public DateTime Start { get; private set; }

        /// <summary>
        /// Gets the <see cref="DateTime"/> this <see cref="DateTimeRange"/> ended.
        /// </summary>
        public DateTime Finish { get; private set; }

        /// <summary>
        /// Check whether the <paramref name="date"/> is within this <see cref="DateTimeRange"/>.
        /// </summary>
        /// <param name="date">
        /// The date to check.
        /// </param>
        /// <returns>
        /// A value indicating whether the <paramref name="date"/> is within this <see cref="DateTimeRange"/>.
        /// </returns>
        public bool IsInRange(DateTime date)
        {
            return date <= this.Finish && date >= this.Start;
        }

        /// <summary>
        /// Get all the days in the date range.
        /// </summary>
        /// <returns>The days in this <see cref="DateTimeRange"/>.</returns>
        public IEnumerable<DateTime> DatesInRange()
        {
            var currentDate = this.Start.Date;

            do
            {
                yield return currentDate;

                currentDate = currentDate.AddDays(1);
            } while (currentDate <= this.Finish.Date);
        }
    }
}