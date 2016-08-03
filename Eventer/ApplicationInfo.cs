
#region TestHeader
#if TESTS
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsMSUnit")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsNUnit")]
#endif
#endregion

namespace Eventer
{
    /// <summary>
    /// struct for store Application Info and configuration.
    /// </summary>
    class ApplicationInfo : IStorable
    {
        /// <summary>
        /// User defined name of this program. (e.g.: "PC in kitchen")
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Uniq instance ID, that is generated with first run of program.
        /// </summary>
        public string InstanceID { set; get; }

        /// <summary>
        /// Group ID for synchronization. (e.g.: "Family Smith")
        /// </summary>
        public string GroupID { set; get; }

        /// <summary>
        /// Set App visible or Hide, it determine if app will be listening on port 8888.
        /// </summary>
        public bool Visible { set; get; }

        /// <summary>
        /// Set App nofity events when its notify DateTime come.
        /// </summary>
        public bool NotifyEvents { set; get; }

        /// <summary>
        /// Constructor that explicitly set all variables to its default values.
        /// </summary>
        public ApplicationInfo()
        {
			Name = default(string);
            InstanceID = default(string);
            GroupID = default(string);
            Visible = default(bool);
            NotifyEvents = default(bool);
        }
        
        /// <summary>
        /// Standart constructor for easy creation.
        /// </summary>
        /// <param name="name">Name of program.</param>
        /// <param name="instanceID">Id of program.</param>
        /// <param name="groupID">Identificator of program.</param>
        public ApplicationInfo(string name, string instanceID, string groupID, bool visible, bool notifyEvents)
        {
            Name = name;
            InstanceID = instanceID;
            GroupID = groupID;
            Visible = visible;
            NotifyEvents = notifyEvents;
        }

        /// <summary>
        /// Return string required for insertion into Database.
        /// </summary>
        /// <returns>String in specific format: "value1, value2, value3"</returns>
        public string GetSQLiteInsertString()
        {
            return
                "'" + Name + "', " +
                "'" + InstanceID + "', " +
                "'" + GroupID + "', " +
                "" + ((Visible) ? "1" : "0") + ", " +
                "" + ((NotifyEvents) ? "1" : "0") + "";
        }

        /// <summary>
        /// String that is used to update this instance.
        /// </summary>
        /// <returns>String in format: "collumn1=value1, collumn2=value2, collumn3=value3"</returns>
        public string GetSQLiteUpdateString()
        {
            return
                "name='" + Name + "', " +
                "instanceID='" + InstanceID + "', " +
                "groupID='" + GroupID + "', " +
                "visible=" + ((Visible) ? "1" : "0") + ", " +
                "notifyEvents=" + ((NotifyEvents) ? "1" : "0") + "";
        }
    }
}
