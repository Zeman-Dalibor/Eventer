using System;

#region TestHeader
#if TESTS
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsMSUnit")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsNUnit")]
#endif
#endregion

namespace Eventer
{
    /// <summary>
    /// Interval of time.
    /// </summary>
    struct Interval
    {
        /// <summary>
        /// Start of interval.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// End of interval.
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// Standart Constructor for easy data set.
        /// </summary>
        /// <param name="start">Start of interval.</param>
        /// <param name="end">End of interval.</param>
        public Interval(DateTime start, DateTime end) : this()
        {
            this.Start = start;
           	this.End = end;
        }
    }
}