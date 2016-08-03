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
    /// Represents Events with all necessary parameter like Start and End Dates, Name, Description and Notification.
    /// </summary>
    class TimeEvent : IStorable
    {
        /// <summary>
        /// Hash of this TimeEvent, generated with creation of TimeEvent and still same all the time.
        /// </summary>
        public string Hash { get; protected set; }

        /// <summary>
        /// Name of this TimeEvent.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of this TimeEvent.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Beginning Date and Time of this TimeEvent.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Ending Date and Time of this TimeEvent.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Determine if Notification of this Time Events should be shown.
        /// </summary>
        public bool Notification
        {
            get { return notification; }
            set
            {
                if (value)
                    notification = true;
                else
                {
                    notification = false;
                    notificationDate = DateTime.MinValue;
                }
            }
        }

        /// <summary>
        /// private notification value.
        /// </summary>
        private bool notification;

        /// <summary>
        /// Date and Time when notification should be shown.
        /// </summary>
        public DateTime NotificationDate
        {
            get { return notificationDate; }
            set
            {
                notificationDate = value;
                Notification = true;
            }
        }

        /// <summary>
        /// private notificationDate value.
        /// </summary>
        private DateTime notificationDate;

        /// <summary>
        /// Private Constructor that create Time event and easy set all it's datas.
        /// </summary>
        /// <param name="hash">Hash of <see cref="TimeEvent"/></param>
        /// <param name="name">Name of <see cref="TimeEvent"/></param>
        /// <param name="description">Description of <see cref="TimeEvent"/></param>
        /// <param name="startDate">StartDate of <see cref="TimeEvent"/></param>
        /// <param name="endDate">EndDate of <see cref="TimeEvent"/></param>
        /// <param name="notification">Notification of <see cref="TimeEvent"/></param>
        /// <param name="notificationDate">NotificationDate of <see cref="TimeEvent"/></param>
        private TimeEvent(string hash, string name, string description, DateTime startDate, DateTime endDate, bool notification, DateTime notificationDate)
        {
        	#if TESTS
            if (name == null)
                throw new ArgumentNullException("name");
            if (description == null)
                throw new ArgumentNullException("description");
        	#else
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (description == null)
                throw new ArgumentNullException(nameof(description));
        	#endif

            Hash = hash;
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            this.notification = notification;
            this.notificationDate = notificationDate;
        }

        /// <summary>
        /// Parse Connection Record(packet) into <see cref="TimeEvent"/>.
        /// </summary>
        /// <param name="record">String that contain data for <see cref="TimeEvent"/></param>
        /// <returns><see cref="TimeEvent"/> with data contained in string.</returns>
        public static TimeEvent Parse(string record)
        {
            string[] eParams = record.Split(';');
            return new TimeEvent(eParams[0], eParams[1], eParams[2].Replace('^', '\n'), DateTime.Parse(eParams[3]), DateTime.Parse(eParams[4]),
                bool.Parse(eParams[5]), DateTime.Parse(eParams[6]));
        }

        /// <summary>
        /// Load TimeEvent from Database, if it's hash is already known.
        /// </summary>
        /// <param name="hash">Hash of <see cref="TimeEvent"/></param>
        /// <param name="name">Name of <see cref="TimeEvent"/></param>
        /// <param name="description">Description of <see cref="TimeEvent"/></param>
        /// <param name="startDate">StartDate of <see cref="TimeEvent"/></param>
        /// <param name="endDate">EndDate of <see cref="TimeEvent"/></param>
        /// <param name="notification">Notification of <see cref="TimeEvent"/></param>
        /// <param name="notificationDate">NotificationDate of <see cref="TimeEvent"/></param>
        /// <returns></returns>
        public static TimeEvent LoadTimeEvent(string hash, string name, string description, DateTime startDate, DateTime endDate, bool notification, DateTime notificationDate)
        {
            return new TimeEvent(hash, name, description, startDate, endDate, notification, notificationDate);
        }

        /// <summary>
        /// Create new <see cref="TimeEvent"/> and calculate hash by inserted Datas and other value.
        /// </summary>
        /// <param name="hash">Hash of <see cref="TimeEvent"/></param>
        /// <param name="name">Name of <see cref="TimeEvent"/></param>
        /// <param name="description">Description of <see cref="TimeEvent"/></param>
        /// <param name="startDate">StartDate of <see cref="TimeEvent"/></param>
        /// <param name="endDate">EndDate of <see cref="TimeEvent"/></param>
        /// <param name="notification">Notification of <see cref="TimeEvent"/></param>
        /// <param name="notificationDate">NotificationDate of <see cref="TimeEvent"/></param>
        /// <returns><see cref="TimeEvent"/> with given parameters.</returns>
        public static TimeEvent CreateTimeEvent(string name, string description, DateTime startDate, DateTime endDate, bool notification, DateTime notificationDate)
        {
            Random rand = new Random();
            string hash = SupportMethods.GetMd5Hash(DateTime.Now + rand.NextDouble().ToString() + name + description + startDate + endDate + notification + notificationDate);
            return new TimeEvent(hash, name, description, startDate, endDate, notification, notificationDate);
        }

        /// <summary>
        /// Calculate new hash and return new <see cref="TimeEvent"/> with same data but newly generated hash.
        /// </summary>
        /// <param name="te">Already existing TimeEvent with good datas, but wrong hash.</param>
        /// <returns></returns>
        public static TimeEvent CreateTimeEventWithDuplicitHash(TimeEvent te)
        {
            Random rand = new Random();
            string hash = SupportMethods.GetMd5Hash(DateTime.Now + rand.NextDouble().ToString() + te.Name + te.Description + te.StartDate + te.EndDate + te.Notification + te.NotificationDate);
            return new TimeEvent(hash, te.Name, te.Description, te.StartDate, te.EndDate, te.Notification, te.NotificationDate);
        }

        /// <summary>
        /// Disable notification for this event.
        /// </summary>
        public void DisableNotification()
        {
            Notification = false;
        }

        /// <summary>
        /// Return string required for insertion into Database.
        /// </summary>
        /// <returns>String in specific format: "value1, value2, value3"</returns>
        public string GetSQLiteInsertString()
        {
            return
                "'" + Hash.ToString() + "', "
                + "'" + Name.ToString() + "', "
                + "'" + Description.ToString() + "', "
                + "'" + StartDate.ToString(Constants.DateTimeFormat) + "', "
                + "'" + EndDate.ToString(Constants.DateTimeFormat) + "', "
                + "" + ((Notification) ? "1" : "0") + ", "
                + "'" + NotificationDate.ToString(Constants.DateTimeFormat) + "'";
        }

        /// <summary>
        /// String that is used to update this instance.
        /// </summary>
        /// <returns>String in format: "collumn1=value1, collumn2=value2, collumn3=value3"</returns>
        public string GetSQLiteUpdateString()
        {
            return
                "name='" + Name.ToString() + "', "
                + "description='" + Description.ToString() + "', "
                + "startDate='" + StartDate.ToString(Constants.DateTimeFormat) + "', "
                + "endDate='" + EndDate.ToString(Constants.DateTimeFormat) + "', "
                + "notification=" + ((Notification) ? "1" : "0") + ", "
                + "notificationDate='" + NotificationDate.ToString(Constants.DateTimeFormat) + "'";
        }

        /// <summary>
        /// String that is prepered for shown in ListBox types continers. When is this instance inserted into them.
        /// </summary>
        public string ListBoxString
        {
            get
            {
                return Name.ToString() + ", "
                     + " \t " + StartDate.ToString(Constants.UserFriendlyDateTimeFormat) + " - "
                     + EndDate.ToString(Constants.UserFriendlyDateTimeFormat)
                     + ",\t'" + Description.ToString() + "'";
            }
        }

        /// <summary>
        /// String that is used for synchronization through network.
        /// </summary>
        /// <returns>String, where data values are delimited by ';' and newlines anre repleced with '^'</returns>
        public string GetSynchronizationString()
        {
            return
                "" + Hash.ToString() + ";"
                + "" + Name.ToString() + ";"
                + "" + Description.Replace('\n', '^').ToString() + ";"
                + "" + StartDate.ToString(Constants.DateTimeFormat) + ";"
                + "" + EndDate.ToString(Constants.DateTimeFormat) + ";"
                + "" + Notification.ToString() + ";"
                + "" + NotificationDate.ToString(Constants.DateTimeFormat) + "";
        }

        /// <summary>
        /// Standart Into string Convertion.
        /// </summary>
        /// <returns>String with important datas from <see cref="TimeEvent"/></returns>
        public override string ToString()
        {
            return Name.ToString() + " "
                + "\t'" + Description.ToString() + "', "
                + "\t (" + StartDate.ToString(Constants.DateTimeFormat) + ") -\t"
                + "\t (" + EndDate.ToString(Constants.DateTimeFormat) + ")";
        }

        /// <summary>
        /// Determine if this instance and instance given in parameter has same data values.
        /// </summary>
        /// <param name="te">Second TimeEvent, which this instance should be compared to equality.</param>
        /// <returns>True if has same data, false otherwise.</returns>
        public bool HasSameDataAs(TimeEvent te)
        {
            if (te == null)
                return false;
            if (this.Name == te.Name &&
                this.Description == te.Description &&
                this.StartDate == te.StartDate &&
                this.EndDate == te.EndDate &&
                this.Notification == te.Notification &&
                this.NotificationDate == te.NotificationDate)
                return true;
            else
                return false;
        }
    }
}
