using System;
using System.Net;

#region TestHeader
#if TESTS
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsMSUnit")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsNUnit")]
#endif
#endregion

namespace Eventer
{
    /// <summary>
    /// Device, or Instance of program, that should be used for synchronization.
    /// </summary>
    class SynchronizationDevice : IStorable
    {
        /// <summary>
        /// Name of this Device.
        /// </summary>
        public string Name { set; get; }

		/// <summary>
		/// Device Identification.
		/// </summary>
		public string DeviceID { get; private set; }

        /// <summary>
        /// Detemine if is this Device allowed synchronize local instance.
        /// </summary>
        public bool Allow { get; set; }

        /// <summary>
        /// Lst Date and Time of Synchronization.
        /// </summary>
        public DateTime LastSynchDate { get; set; }

        /// <summary>
        /// IP Adress of this device
        /// </summary>
        public IPAddress IP { get; set; }

        /// <summary>
        /// Group ID of this device
        /// </summary>
        public string GroupID { get; set; }

        /// <summary>
        /// Standart Consturstor for easy data set.
        /// </summary>
        /// <param name="name">Name of Device</param>
        /// <param name="deviceID">Device ID of Device</param>
        /// <param name="allow">Determine if is this Device allowed to synchronize local instance</param>
        /// <param name="lastSynchDate">Date and Time of Last synchronization</param>
        public SynchronizationDevice(string name, string deviceID, bool allow, DateTime lastSynchDate)
        {
            Name = name;
            DeviceID = deviceID;
            Allow = allow;
            LastSynchDate = lastSynchDate;
        }

        /// <summary>
        /// Standart Consturstor for easy data set.
        /// </summary>
        /// <param name="name">Name of Device</param>
        /// <param name="deviceID">Device ID of Device</param>
        /// <param name="allow">Determine if is this Device allowed to synchronize local instance</param>
        /// <param name="lastSynchDate">Date and Time of Last synchronization</param>
        /// <param name="ip">IP Adress of this Device</param>
        /// <param name="groupID">GroupID of this Device</param>
        public SynchronizationDevice(string name, string deviceID, bool allow, DateTime lastSynchDate, IPAddress ip, string groupID)
        {
            Name = name;
            DeviceID = deviceID;
            Allow = allow;
            LastSynchDate = lastSynchDate;
            IP = ip;
            GroupID = groupID;
        }

        /// <summary>
        /// Return string required for insertion into Database.
        /// </summary>
        /// <returns>String in specific format: "value1, value2, value3"</returns>
        public string GetSQLiteInsertString()
        {
            return
                "'" + Name + "', "
                + "'" + DeviceID + "', "
                + "" + ((Allow) ? "1" : "0") + ", "
                + "'" + LastSynchDate.ToString(Constants.DateTimeFormat) + "'";
        }

        /// <summary>
        /// String that is used to update this instance.
        /// </summary>
        /// <returns>String in format: "collumn1=value1, collumn2=value2, collumn3=value3"</returns>
        public string GetSQLiteUpdateString()
        {
            return
                "name='" + Name + "', "
                + "deviceID='" + DeviceID + "', "
                + "allow=" + ((Allow) ? "1" : "0") + ", "
                + "lastSynchDate='" + LastSynchDate.ToString(Constants.DateTimeFormat) + "'";
        }

        /// <summary>
        /// String that is prepered for shown in ListBox types continers. When is this instance inserted into them.
        /// </summary>
        public string ListBoxString
        {
            get
            {
                string lastsynch = LastSynchDate == DateTime.MinValue ? "Nikdy" : LastSynchDate.ToString(Constants.UserFriendlyDateTimeFormat);
                return
                    Name.ToString() + "\t" +
                    "Skupina=" + GroupID + ", \t"
                + "ID='" + DeviceID + "', "
                + "Synchronizace:" + ((Allow) ? (lastsynch) : "Nepovolena");
            }
        }
        
    }
}
