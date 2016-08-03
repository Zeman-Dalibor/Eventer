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
    /// Change of TimeEvent. Del, Add or Edit change.
    /// </summary>
    class Change : IStorable
    {
        /// <summary>
        /// Types of Change.
        /// </summary>
        public enum ChangeType { Add, Edit, Del };

        /// <summary>
        /// Hash of TimeEvent which this change influenced.
        /// </summary>
        public string TimeEventHash { get; set; }

        /// <summary>
        /// TimeEvent that was influenced by this change. Null if this change is Delete type.
        /// </summary>
        public TimeEvent TimeEvent { get; set; }

        /// <summary>
        /// Date and Time when this change occurs.
        /// </summary>
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// Type of this Change.
        /// </summary>
        public ChangeType Type { get; set; }

        /// <summary>
        /// Standart Constructor for easy data set.
        /// </summary>
        /// <param name="eventHash">Hash of <see cref="TimeEvent"/>.</param>
        /// <param name="type">Type of this change: Add, Edit or Del.</param>
        /// <param name="changeDate">DateTime when this happened.</param>
        public Change(string eventHash, ChangeType type, DateTime changeDate)
        {
            TimeEventHash = eventHash;
            Type = type;
            ChangeDate = changeDate;
        }

        /// <summary>
        /// Parse Connection Record(packet) into <see cref="Change"/>.
        /// </summary>
        /// <param name="record">String that contain data for <see cref="Change"/></param>
        /// <returns><see cref="Change"/> with data contained in string.</returns>
        public static Change Parse(string record)
        {
            string[] cParams = record.Split(';');
            return new Change(cParams[0], (ChangeType)(long.Parse(cParams[1])), DateTime.Parse(cParams[2]));
        }

        /// <summary>
        /// Return string required for insertion into Database.
        /// </summary>
        /// <returns>String in specific format: "value1, value2, value3"</returns>
        public string GetSQLiteInsertString()
        {
            return
                "'" + TimeEventHash.ToString() + "', "
                + "'" + ((long)Type).ToString() + "', "
                + "'" + ChangeDate.ToString(Constants.DateTimeFormat) + "'";
        }

        /// <summary>
        /// String that is used to update this instance.
        /// </summary>
        /// <returns>String in format: "collumn1=value1, collumn2=value2, collumn3=value3"</returns>
        public string GetSQLiteUpdateString()
        {
            return
                "eventHash='" + TimeEventHash.ToString() + "', "
                + "type='" + ((long)Type).ToString() + "', "
                + "changeDate='" + ChangeDate.ToString(Constants.DateTimeFormat) + "'";
        }

        /// <summary>
        /// String that is used for synchronization through network.
        /// </summary>
        /// <returns>String, where data values are delimited by ';'.</returns>
        public string GetSynchronizationString()
        {
            return
                "" + TimeEventHash.ToString() + ";"
                + "" + ((long)Type).ToString() + ";"
                + "" + ChangeDate.ToString(Constants.DateTimeFormat) + "";
        }

        /// <summary>
        /// String that is prepered for shown in ListBox types continers. When is this instance inserted into them.
        /// </summary>
        public string ListBoxString
        {
            get
            {
                string retStr = "";
                switch (Type)
                {
                    case ChangeType.Add:
                        retStr = "Přidána událost: " + TimeEvent.Name
                           + " [" + TimeEventHash.ToString() + "]"
                           + " " + ChangeDate.ToString(Constants.UserFriendlyDateTimeFormat) + "";
                        break;
                    case ChangeType.Edit:
                        retStr = "Editována událost: " + TimeEvent.Name
                           + " [" + TimeEventHash.ToString() + "]"
                           + " " + ChangeDate.ToString(Constants.UserFriendlyDateTimeFormat) + "";
                        break;
                    case ChangeType.Del:
                        retStr = "Smazána událost: "
                           + " [" + TimeEventHash.ToString() + "]"
                           + " " + ChangeDate.ToString(Constants.UserFriendlyDateTimeFormat) + "";
                        break;
                    default:
                        break;
                }
                return retStr;
            }
        }
    }
}
