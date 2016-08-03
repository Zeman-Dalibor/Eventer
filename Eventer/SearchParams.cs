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
    /// Searching parameters.
    /// </summary>
    class SearchParams
    {
        /// <summary>
        /// Parameter From.
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// Parameter To.
        /// </summary>
        public DateTime To { get; set; }

        /// <summary>
        /// Parameter Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Parameter Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Parameter NotificationDate.
        /// </summary>
        public DateTime NotificationDate { get; set; }

        /// <summary>
        /// Constructor for explicit set default variables.
        /// </summary>
        public SearchParams()
        {
        	Name = default(string);
            Description = default(string);
            From = default(DateTime);
            To = default(DateTime);
            NotificationDate = default(DateTime);
        }
        
        /// <summary>
        /// Standart Constructor for easy data set.
        /// </summary>
        /// <param name="name">Name for searching.</param>
        /// <param name="description">Description for searching.</param>
        /// <param name="from">From for searching.</param>
        /// <param name="to">To for searching.</param>
        /// <param name="notificationDate">Notification Date for searching.</param>
        public SearchParams(string name, string description, DateTime from, DateTime to, DateTime notificationDate)
        {
            Name = name;
            Description = description;
            From = from;
            To = to;
            NotificationDate = notificationDate;
        }

        /// <summary>
        /// Compare datas set in these two instances of <see cref="SearchParams"/>.
        /// </summary>
        /// <param name="a">First instance.</param>
        /// <param name="b">Second instance.</param>
        /// <returns>True if datas stored are equal, False otherwise.</returns>
        public static bool operator ==(SearchParams a, SearchParams b)
        {
            if (System.Object.ReferenceEquals(a, b))
                return true;
            
            if (((object)a == null) || ((object)b == null))
                return false;
            
            return 
                a.Name == b.Name && 
                a.Description == b.Description &&
                a.From == b.From &&
                a.To == b.To &&
                a.NotificationDate == b.NotificationDate;
        }

        /// <summary>
        /// Compare datas set in these two instances of <see cref="SearchParams"/>.
        /// </summary>
        /// <param name="a">First instance.</param>
        /// <param name="b">Second instance.</param>
        /// <returns>False if datas stored are equal, True otherwise.</returns>
        public static bool operator !=(SearchParams a, SearchParams b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Standart equals of instances.
        /// </summary>
        /// <param name="obj">Second instance to compare.</param>
        /// <returns>True if equal, False otherwise.</returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Standart hashCode of instances.
        /// </summary>
        /// <returns>Standart HashCode.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
