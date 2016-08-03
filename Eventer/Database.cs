using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

#if Mono 
using Mono.Data.Sqlite;
#else
using System.Data.SQLite;
#endif

#region TestHeader
#if TESTS
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsMSUnit")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsNUnit")]
#endif
#endregion

namespace Eventer
{
    /// <summary>
    /// Database Wrapper and easy access to database class. It store all datas and configuration for Database.
    /// </summary>
    public class Database
    {
        /// <summary>
        /// Database name.
        /// </summary>
        string databaseName;

        /// <summary>
        /// Database Connection handle.
        /// </summary>
#if Mono
        SqliteConnection dbConnection;
#else
        SQLiteConnection dbConnection;
#endif

        /// <summary>
        /// Database with given name, that store datas like <see cref="TimeEvent"/>s, <see cref="Change"/>s,...
        /// </summary>
        /// <param name="name">Name of Database and Database's file.</param>
        public Database(string name)
        {
            databaseName = name;
            if (!File.Exists(databaseName + ".sqlite"))
                createNewDatabase();
            connectToDatabase();
        }

        /// <summary>
        /// Connect to database
        /// </summary>
        void connectToDatabase()
        {
            if (dbConnection != null)
                return;
#if Mono
            dbConnection = new SqliteConnection("Data Source=" + databaseName + ".sqlite;Version=3;");
#else
            dbConnection = new SQLiteConnection("Data Source=" + databaseName + ".sqlite;Version=3;");
#endif
            dbConnection.Open();
        }

        /// <summary>
        /// Create database with local databaseName.
        /// </summary>
        void createNewDatabase()
        {
#if Mono
            SqliteConnection.CreateFile(databaseName + ".sqlite");
#else
            SQLiteConnection.CreateFile(databaseName + ".sqlite");
#endif
            connectToDatabase();
            createTimeEventTable();
            createChangeTable();
            createSynchronizationDeviceTable();
            createApplicationInfoTable();
            InsertApplicationInfo(
                new ApplicationInfo("",
                    SupportMethods.GetMd5Hash(DateTime.Now.Ticks.ToString()).Substring(24),
                    "",
                    false,
                    true));
        }
#if Mono
        /// <summary>
        /// Gets SQL command for executing in Database.
        /// </summary>
        /// <param name="sql">String with SQL command.</param>
        /// <returns>SqliteCommand for Mono.</returns>
        private SqliteCommand getCommand(string sql)
        {
            return new SqliteCommand(sql, dbConnection);
        }
        
        /// <summary>
        /// Get null reader of type that is specific for Mono.Data.Sqlite (Linux like systems)
        /// </summary>
        /// <returns>Always null.</returns>
        private SqliteDataReader getNullReader()
        {
            return null;
        }
#else
        /// <summary>
        /// Gets SQL command for executing in Database.
        /// </summary>
        /// <param name="sql">String with SQL command.</param>
        /// <returns>SQLiteCommand for System.</returns>
        private SQLiteCommand getCommand(string sql)
        {
            return new SQLiteCommand(sql, dbConnection);
        }

        /// <summary>
        /// Get null reader of type that is specific for System.Data.SQLite (Windows)
        /// </summary>
        /// <returns>Always null.</returns>
        private SQLiteDataReader getNullReader()
        {
            return null;
        }
#endif

        /// <summary>
        /// Create table for <see cref="TimeEvent"/>.
        /// </summary>
        private void createTimeEventTable()
        {
            string sql = "CREATE TABLE event (hash VARCHAR(32) PRIMARY KEY, name VARCHAR(128), description TEXT, startDate DATETIME, endDate DATETIME, notification BOOLEAN, notificationDate DATETIME)";
            var command = getCommand(sql);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Create table for <see cref="Change"/>.
        /// </summary>
        private void createChangeTable()
        {
            string sql = "CREATE TABLE change (eventHash VARCHAR(32), type INTEGER, changeDate DATETIME)";
            var command = getCommand(sql);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Create table for <see cref="SynchronizationDevice"/>.
        /// </summary>
        private void createSynchronizationDeviceTable()
        {
            string sql = "CREATE TABLE synchronizationDevice (name VARCHAR(128), deviceID VARCHAR(32) PRIMARY KEY, allow BOOLEAN, lastSynchDate DATETIME)";
            var command = getCommand(sql);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Create table for <see cref="ApplicationInfo"/>.
        /// </summary>
        private void createApplicationInfoTable()
        {
            string sql = "CREATE TABLE applicationInfo (name VARCHAR(128), instanceID VARCHAR(8), groupID TEXT, visible BOOLEAN, notifyEvents BOOLEAN)";
            var command = getCommand(sql);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Insert <see cref="TimeEvent"/>s into Database. And Log It.
        /// </summary>
        /// <param name="tEvent"><see cref="TimeEvent"/>s that should be inserted.</param>
        internal void InsertTimeEvent(params TimeEvent[] tEvent)
        {
            insertTimeEvent(tEvent);
            ChangeAdd(tEvent);
        }

        /// <summary>
        /// Insert <see cref="TimeEvent"/>s into Database.
        /// </summary>
        /// <param name="tEvent"><see cref="TimeEvent"/>s that should be inserted.</param>
        private void insertTimeEvent(params TimeEvent[] tEvent)
        {
            string sql = getInsertString("event", "hash, name, description, startDate, endDate, notification, notificationDate", tEvent);
            getCommand(sql).ExecuteNonQuery();
        }

        /// <summary>
        /// Insert <see cref="Change"/> or <see cref="Change"/>s into Database.
        /// </summary>
        /// <param name="change"><see cref="Change"/>s that should be inserted.</param>
        internal void InsertChange(params Change[] change)
        {
            string sql = getInsertString("change", "eventHash, type, changeDate", change);
            getCommand(sql).ExecuteNonQuery();
        }

        /// <summary>
        /// Insert <see cref="SynchronizationDevice"/> or <see cref="SynchronizationDevice"/>s into Database.
        /// </summary>
        /// <param name="device"><see cref="SynchronizationDevice"/>s that should be inserted.</param>
        internal void InsertSynchronizationDevice(params SynchronizationDevice[] device)
        {
            string sql = getInsertString("synchronizationDevice", "name, deviceID, allow, lastSynchDate", device);
            getCommand(sql).ExecuteNonQuery();
        }

        /// <summary>
        /// Insert <see cref="ApplicationInfo"/> into Database.
        /// </summary>
        /// <param name="info"><see cref="ApplicationInfo"/> that should be inserted.</param>
        internal void InsertApplicationInfo(ApplicationInfo info)
        {
            string sql = getInsertString("applicationInfo", "name, instanceID, groupID, visible, notifyEvents", info);
            getCommand(sql).ExecuteNonQuery();
        }

        /// <summary>
        /// Generic method for returns specific inserion string. 
        /// </summary>
        /// <param name="tableName">Name of table there data should be inserted.</param>
        /// <param name="collums">Clomuns separated by ',' in one string, whitout leading or traling ','.</param>
        /// <param name="values">Data that should be inserted into datbase.</param>
        /// <returns>String in format INSERT INTO "TableName" columns VALUES (), (), ...;</returns>
        private string getInsertString(string tableName, string collums, params IStorable[] values)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO ");
            sb.Append(tableName);
            sb.Append("(");
            sb.Append(collums);
            sb.Append(") values ");

            for (int i = 0; i < values.Length; i++)
            {
                sb.Append("(");
                sb.Append(values[i].GetSQLiteInsertString());
                if (i + 1 == values.Length)
                    sb.Append(")");
                else
                    sb.Append("), ");
            }
            sb.Append(";");

            return sb.ToString();
        }

        /// <summary>
        /// Gets Events that is in given Time Interval.
        /// </summary>
        /// <param name="from">Start of interval.</param>
        /// <param name="to">End of interval.</param>
        /// <param name="limit">How many <see cref="TimeEvent"/>s should be returned as maximal value.</param>
        /// <returns>Enumerable <see cref="TimeEvent"/>s ascending by StarDate. And happen in given interval.</returns>
        internal IEnumerable<TimeEvent> GetTimeEventsInRange(DateTime from, DateTime to, int limit = int.MaxValue)
        {
            var reader = getNullReader();
            try
            {
                string sql = "SELECT * FROM event WHERE endDate >= datetime('" + from.ToString(Constants.DateTimeFormat)
                    + "') AND startDate <= datetime('" + to.ToString(Constants.DateTimeFormat)
                    + "') ORDER BY startDate ASC";
                if (limit != int.MaxValue)
                    sql += " LIMIT " + limit.ToString();
                var command = getCommand(sql);
                reader = command.ExecuteReader();
                command.Dispose();

                foreach (var item in reader)
                {
                    yield return TimeEvent.LoadTimeEvent((string)reader["hash"], (string)reader["name"], (string)reader["description"], (DateTime)reader["startDate"], (DateTime)reader["endDate"], (bool)reader["notification"], (DateTime)reader["notificationDate"]);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
        }

        /// <summary>
        /// Gets all changes that is added since given DateTime.
        /// </summary>
        /// <param name="since">At least this DateTime should have been change added, or after this DateTime.</param>
        /// <returns>Enumerable <see cref="Change"/>s which has been added after or in given DateTime.</returns>
        internal IEnumerable<Change> GetChangesSinceDateTime(DateTime since)
        {
            var reader = getNullReader();
            try
            {
                string sql = "SELECT * FROM change WHERE changeDate >= datetime('" + since.ToString(Constants.DateTimeFormat)
                    + "') ORDER BY changeDate ASC";
                var command = getCommand(sql);
                reader = command.ExecuteReader();
                foreach (var item in reader)
                {
                    yield return new Change((string)reader["eventHash"], (Change.ChangeType)(long)reader["type"], (DateTime)reader["changeDate"]);
                }
                command.Dispose();
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
        }

        /// <summary>
        /// Append From clause if its requested.
        /// </summary>
        /// <param name="sb">String builder that shoulde potencial clause appended.</param>
        /// <param name="addAnd">Indicates if this mehod should "AND" before apending clause. Alway return True.</param>
        /// <param name="from">Start of interval. <see cref="DateTime.MinValue"/> if this parameter should not be take into account.</param>
        private void AppendClauseFromIfNotDefault(StringBuilder sb, ref bool addAnd, DateTime from)
        {
            if (from != DateTime.MinValue)
            {
                if (addAnd)
                    sb.Append(" AND");
                else
                    sb.Append(" WHERE");
                sb.Append(" endDate >= datetime('");
                sb.Append(from.ToString(Constants.DateTimeFormat));
                sb.Append("')");
                addAnd = true;
            }
        }

        /// <summary>
        /// Append To clause if its requested.
        /// </summary>
        /// <param name="sb">String builder that shoulde potencial clause appended.</param>
        /// <param name="addAnd">Indicates if this mehod should "AND" before apending clause. Alway return True.</param>
        /// <param name="to">End of interval. <see cref="DateTime.MinValue"/> if this parameter should not be take into account.</param>
        private void AppendClauseToIfNotDefault(StringBuilder sb, ref bool addAnd, DateTime to)
        {
            if (to != DateTime.MinValue)
            {
                if (addAnd)
                    sb.Append(" AND");
                else
                    sb.Append(" WHERE");
                sb.Append(" startDate <= datetime('");
                sb.Append(to.ToString(Constants.DateTimeFormat));
                sb.Append("')");
                addAnd = true;
            }
        }

        /// <summary>
        /// Append Notification Name if its requested.
        /// </summary>
        /// <param name="sb">String builder that shoulde potencial clause appended.</param>
        /// <param name="addAnd">Indicates if this mehod should "AND" before apending clause. Alway return True.</param>
        /// <param name="name">String contained in record in collumn name. Blank string if this parameter should not be take into account.</param>
        private void AppendClauseNameIfNotDefault(StringBuilder sb, ref bool addAnd, string name)
        {
            if (name != "")
            {
                if (addAnd)
                    sb.Append(" AND");
                else
                    sb.Append(" WHERE");
                sb.Append(" name LIKE '%");
                sb.Append(name);
                sb.Append("%'");
                addAnd = true;
            }
        }

        /// <summary>
        /// Append Description clause if its requested.
        /// </summary>
        /// <param name="sb">String builder that shoulde potencial clause appended.</param>
        /// <param name="addAnd">Indicates if this mehod should "AND" before apending clause. Alway return True.</param>
        /// <param name="description">String contained in record in collumn name. Blank string if this parameter should not be take into account.</param>
        private void AppendClauseDescriptionIfNotDefault(StringBuilder sb, ref bool addAnd, string description)
        {
            if (description != "")
            {
                if (addAnd)
                    sb.Append(" AND");
                else
                    sb.Append(" WHERE");
                sb.Append(" description LIKE '%");
                sb.Append(description);
                sb.Append("%'");
                addAnd = true;
            }
        }

        /// <summary>
        /// Append Notification clause if its requested.
        /// </summary>
        /// <param name="sb">String builder that shoulde potencial clause appended.</param>
        /// <param name="addAnd">Indicates if this mehod should "AND" before apending clause. Alway return True.</param>
        /// <param name="notificationDate">Date, the day where notification is set. <see cref="DateTime.MinValue"/> if this parameter should not be take into account.</param>
        private void AppendClauseNotificationDateIfNotDefault(StringBuilder sb, ref bool addAnd, DateTime notificationDate)
        {
            if (notificationDate != DateTime.MinValue)
            {
                if (addAnd)
                    sb.Append(" AND");
                else
                    sb.Append(" WHERE");
                sb.Append(" notificationDate >= datetime('" + notificationDate.Date.ToString(Constants.DateTimeFormat) + "')");
                sb.Append(" AND notificationDate < datetime('" + notificationDate.Date.AddDays(1).ToString(Constants.DateTimeFormat) + "')");
                addAnd = true;
            }
        }

        /// <summary>
        /// Gets <see cref="TimeEvent"/>s which contain string <paramref name="name"/> in Name parameter, <paramref name="description"/> in Description parameter,
        /// they happen sometimes after <paramref name="from"/> and before <paramref name="to"/> dates. And does notify in <paramref name="notificationDate"/>
        /// </summary>
        /// <param name="name">String contained in record in collumn name. Blank string if this parameter should not be take into account.</param>
        /// <param name="description">String contained in record in collumn name. Blank string if this parameter should not be take into account.</param>
        /// <param name="from">Start of interval. <see cref="DateTime.MinValue"/> if this parameter should not be take into account.</param>
        /// <param name="to">End of interval. <see cref="DateTime.MinValue"/> if this parameter should not be take into account.</param>
        /// <param name="notificationDate">Date, the day where notification is set. <see cref="DateTime.MinValue"/> if this parameter should not be take into account.</param>
        /// <returns>Enumerable <see cref="TimeEvent"/>s which have specific parameters.</returns>
        internal IEnumerable<TimeEvent> GetTimeEvents(string name, string description, DateTime from, DateTime to, DateTime notificationDate)
        {
            var reader = getNullReader();
            try
            {
                bool addAnd = false;
                StringBuilder sb = new StringBuilder("SELECT * FROM event");

                AppendClauseFromIfNotDefault(sb, ref addAnd, from);
                AppendClauseToIfNotDefault(sb, ref addAnd, to);
                AppendClauseNameIfNotDefault(sb, ref addAnd, name);
                AppendClauseDescriptionIfNotDefault(sb, ref addAnd, description);
                AppendClauseNotificationDateIfNotDefault(sb, ref addAnd, notificationDate);

                sb.Append(" ORDER BY startDate ASC LIMIT 50000");

                string sql = sb.ToString();

                var command = getCommand(sql);
                reader = command.ExecuteReader();
                command.Dispose();

                foreach (var item in reader)
                {
                    yield return TimeEvent.LoadTimeEvent((string)reader["hash"], (string)reader["name"], (string)reader["description"], (DateTime)reader["startDate"], (DateTime)reader["endDate"], (bool)reader["notification"], (DateTime)reader["notificationDate"]);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
        }

        /// <summary>
        /// Gets <see cref="TimeEvent"/>s which contain string <paramref name="name"/> in Name parameter, <paramref name="description"/> in Description parameter,
        /// they happen sometimes after <paramref name="from"/> and before <paramref name="to"/> dates. And does notify in <paramref name="notificationDate"/>
        /// </summary>
        /// <param name="sParams">Searching parameters, which must be pass to return TimeEvent record.</param>
        /// <returns>Enumerable events that complain given parameters</returns>
        internal IEnumerable<TimeEvent> GetTimeEvents(SearchParams sParams)
        {
            string name = sParams.Name;
            string description = sParams.Description;
            DateTime from = sParams.From;
            DateTime to = sParams.To;
            DateTime notificationDate = sParams.NotificationDate;

            return GetTimeEvents(name, description, from, to, notificationDate);
        }

        /// <summary>
        /// Gets <see cref="TimeEvent"/>s for notify purpose. It returns only TimeEvents with enabled notification and NotificationDate in given interval from notifyFrom to notifyTo.
        /// </summary>
        /// <param name="notifyFrom">Start of interval of notificationDate.</param>
        /// <param name="notifyTo">End of interval of notificationDate.</param>
        /// <returns>Enumerable <see cref="TimeEvent"/>s for notification.</returns>
        internal IEnumerable<TimeEvent> GetTimeEventsForNotification(DateTime notifyFrom, DateTime notifyTo)
        {
            var reader = getNullReader();
            try
            {
                string sql = "SELECT * FROM event WHERE notification = 1 AND notificationDate > datetime('" + notifyFrom.ToString(Constants.DateTimeFormat) + "')"
                    + " AND notificationDate <= datetime('" + notifyTo.ToString(Constants.DateTimeFormat) + "')"
                    + " ORDER BY notificationDate ASC";

                var command = getCommand(sql);
                reader = command.ExecuteReader();
                command.Dispose();

                foreach (var item in reader)
                {
                    yield return TimeEvent.LoadTimeEvent((string)reader["hash"], (string)reader["name"], (string)reader["description"], (DateTime)reader["startDate"], (DateTime)reader["endDate"], (bool)reader["notification"], (DateTime)reader["notificationDate"]);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
        }

        /// <summary>
        /// Check if exist <see cref="TimeEvent"/> with specific hash in Database.
        /// </summary>
        /// <param name="hash">Hash of TimeEvent.</param>
        /// <returns>True if in database exist record with hash, false otherwise</returns>
        internal bool ExistTimeEventWithHash(string hash)
        {
            var reader = getNullReader();
            try
            {
                string sql = "SELECT hash FROM event WHERE hash = '" + hash + "' LIMIT 1;";
                var command = getCommand(sql);
                reader = command.ExecuteReader();
                command.Dispose();

                if (reader.Read())
                    return true;
                else
                    return false;

            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
        }

        /// <summary>
        /// Gets <see cref="TimeEvent"/>'s data from database.
        /// </summary>
        /// <param name="hash"></param>
        /// <returns>One <see cref="TimeEvent"/> with specific hash or null if record with given hash does not exist.</returns>
        internal TimeEvent GetTimeEvent(string hash)
        {
            var reader = getNullReader();
            try
            {
                string sql = "SELECT * FROM event WHERE hash = '" + hash + "' LIMIT 1;";
                var command = getCommand(sql);
                reader = command.ExecuteReader();
                command.Dispose();

                if (reader.Read())
                    return TimeEvent.LoadTimeEvent((string)reader["hash"], (string)reader["name"],
                        (string)reader["description"], (DateTime)reader["startDate"],
                        (DateTime)reader["endDate"], (bool)reader["notification"], (DateTime)reader["notificationDate"]);
                else
                    return null;
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
        }

        /// <summary>
        /// Check if exist <see cref="SynchronizationDevice"/> with specific ID in Database.
        /// </summary>
        /// <param name="deviceID">Id of <see cref="SynchronizationDevice"/></param>
        /// <returns>True if in database exist record with deviceID, false otherwise</returns>
        internal bool ExistDeviceWithID(string deviceID)
        {
            var reader = getNullReader();
            try
            {
                string sql = "SELECT * FROM synchronizationDevice WHERE deviceID = '" + deviceID + "' LIMIT 1;";
                var command = getCommand(sql);
                reader = command.ExecuteReader();
                command.Dispose();

                if (reader.Read())
                    return true;
                else
                    return false;
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
        }

        /// <summary>
        /// Gets <see cref="SynchronizationDevice"/> by specific deviceID.
        /// </summary>
        /// <param name="deviceID">ID of device which datas should be returned.</param>
        /// <returns>Datas stored in Database about device with given deviceID</returns>
        internal SynchronizationDevice GetSynchronizationDeviceInfo(string deviceID)
        {
            var reader = getNullReader();
            try
            {
                string sql = "SELECT * FROM synchronizationDevice WHERE deviceID = '" + deviceID + "' LIMIT 1;";
                var command = getCommand(sql);
                reader = command.ExecuteReader();
                command.Dispose();

                if (reader.Read())
                {
                    return new SynchronizationDevice((string)reader["name"], (string)reader["deviceID"], (bool)reader["allow"], (DateTime)reader["lastSynchDate"]);
                }
                else
                    return null;
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
        }

        /// <summary>
        /// Return current <seealso cref="ApplicationInfo"/> stored in Database.
        /// </summary>
        /// <returns>Application info about this current program. <seealso cref="ApplicationInfo"/></returns>
        internal ApplicationInfo GetApplicationInfo()
        {
            var reader = getNullReader();
            try
            {
                string sql = "SELECT * FROM applicationInfo LIMIT 1;";
                var command = getCommand(sql);
                reader = command.ExecuteReader();
                command.Dispose();

                if (reader.Read())
                    return new ApplicationInfo((string)reader["name"], (string)reader["instanceID"], (string)reader["groupID"], (bool)reader["visible"], (bool)reader["notifyEvents"]);
                else
                    throw new ArgumentException("Application Info do not exist!!");
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
        }

        /// <summary>
        /// Update event with specific hash and Log that change. It change all datas of event.
        /// </summary>
        /// <param name="hash">Original hash of event.</param>
        /// <param name="name">New name for event.</param>
        /// <param name="description">New description of event.</param>
        /// <param name="startDate">New starting date of this event.</param>
        /// <param name="endDate">New eding date of this event.</param>
        /// <param name="notification">New notification boolean.</param>
        /// <param name="notificationDate">New date for notification.</param>
        /// <returns>True if datas has been updated, false otherwise.</returns>
        internal bool UpdateTimeEvent(string hash, string name, string description, DateTime startDate, DateTime endDate, bool notification, DateTime notificationDate)
        {
            ChangeEdit(hash);
            return updateTimeEvent(TimeEvent.LoadTimeEvent(hash, name, description, startDate, endDate, notification, notificationDate));

        }

        /// <summary>
        /// Update event and Log that change. It is not necessary more info. <seealso cref="TimeEvent"/> already contain hash of event.
        /// </summary>
        /// <param name="te">Event that has been changed. It contain old hash and new datas.</param>
        /// <returns>True if datas has been updated, false otherwise.</returns>
        internal bool UpdateTimeEvent(TimeEvent te)
        {
            ChangeEdit(te);
            return updateTimeEvent(te);
        }

        /// <summary>
        /// Update event and Log that change. It is not necessary more info. <seealso cref="TimeEvent"/> already contain hash of event.
        /// </summary>
        /// <param name="te">Event that has been changed. It contain old hash and new datas.</param>
        /// <returns>True if datas has been updated, false otherwise.</returns>
        private bool updateTimeEvent(TimeEvent te)
        {
            string sql = getUpdateString("event", te, "hash='" + te.Hash + "'");
            var command = getCommand(sql);
            int count = command.ExecuteNonQuery();
            command.Dispose();
            if (count == 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Update given Application info, current program.
        /// </summary>
        /// <param name="appInfo"></param>
        /// <returns>True if datas has been updated, false otherwise.</returns>
        internal bool UpdateApplicationInfo(ApplicationInfo appInfo)
        {
            string sql = getUpdateString("applicationInfo", appInfo, "instanceID='" + appInfo.InstanceID + "'");
            var command = getCommand(sql);
            int count = command.ExecuteNonQuery();
            command.Dispose();
            if (count == 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Update given <see cref="SynchronizationDevice"/>. <see cref="SynchronizationDevice"/> already contain DeviceID.
        /// </summary>
        /// <param name="device"><see cref="SynchronizationDevice"/> with new datas.</param>
        /// <returns>True if datas has been updated, false otherwise.</returns>
        internal bool UpdateSynchronizationDevice(SynchronizationDevice device)
        {
            string sql = getUpdateString("synchronizationDevice", device, "deviceID='" + device.DeviceID + "'");
            var command = getCommand(sql);
            int count = command.ExecuteNonQuery();
            command.Dispose();
            if (count == 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Generic generator for Updating strings.
        /// </summary>
        /// <param name="tableName">Name of table where update will be applied.</param>
        /// <param name="values">Values, that is new. In format "ValueName=Value"</param>
        /// <param name="where">Condition for changing values to parameter value.</param>
        /// <returns>String that will be used for execution as SQL string.</returns>
        private string getUpdateString(string tableName, IStorable values, string where)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE ");
            sb.Append(tableName);
            sb.Append(" SET ");
            sb.Append(values.GetSQLiteUpdateString());
            sb.Append(" WHERE ");
            sb.Append(where);
            sb.Append(";");
            return sb.ToString();
        }

        /// <summary>
        /// Insert <see cref="TimeEvent"/>s into Database.
        /// </summary>
        /// <param name="tEvent"><see cref="TimeEvent"/>s that should be inserted.</param>
        private void replaceTimeEvent(params TimeEvent[] tEvent)
        {
            string sql = getReplaceString("event", "hash, name, description, startDate, endDate, notification, notificationDate", tEvent);
            getCommand(sql).ExecuteNonQuery();
        }

        /// <summary>
        /// Generic method for returns specific INSERT OR REPLACE string. Values's string is same as Insert string.
        /// </summary>
        /// <param name="tableName">Name of table there data should be inserted.</param>
        /// <param name="collums">Clomuns separated by ',' in one string, whitout leading or traling ','.</param>
        /// <param name="values">Data that should be inserted into datbase.</param>
        /// <returns>String in format INSERT OR REPLACE INTO INTO "TableName" columns VALUES (), (), ...;</returns>
        private string getReplaceString(string tableName, string collums, params IStorable[] values)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT OR REPLACE INTO ");
            sb.Append(tableName);
            sb.Append("(");
            sb.Append(collums);
            sb.Append(") values ");

            for (int i = 0; i < values.Length; i++)
            {
                sb.Append("(");
                sb.Append(values[i].GetSQLiteInsertString());
                if (i + 1 == values.Length)
                    sb.Append(")");
                else
                    sb.Append("), ");
            }
            sb.Append(";");

            return sb.ToString();
        }

        /// <summary>
        /// Delete <see cref="TimeEvent"/>s and Log that changes.
        /// </summary>
        /// <param name="events"><see cref="TimeEvent"/>s that should be deleted.</param>
        internal void DeleteTimeEvents(params TimeEvent[] events)
        {
            deleteTimeEvents(events);
            ChangeDel(events);
        }

        /// <summary>
        /// Delete <see cref="TimeEvent"/>s and Do NOT Log that changes.
        /// </summary>
        /// <param name="events"><see cref="TimeEvent"/>s that should be deleted.</param>
        internal void DeleteTimeEventsWithoutLoging(params TimeEvent[] events)
        {
            deleteTimeEvents(events);
        }

        /// <summary>
        /// Delete <see cref="TimeEvent"/>s.
        /// </summary>
        /// <param name="events"><see cref="TimeEvent"/>s that should be deleted.</param>
        private void deleteTimeEvents(params TimeEvent[] events)
        {
            string sql = deleteTimeEventsString(events);
            var command = getCommand(sql);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        /// <summary>
        /// Get string that is used for deleting <see cref="TimeEvent"/>s.
        /// </summary>
        /// <param name="events"><see cref="TimeEvent"/>s that should be deleted.</param>
        /// <returns>string that is used to delete given events.</returns>
        private string deleteTimeEventsString(params TimeEvent[] events)
        {
            StringBuilder sb = new StringBuilder();
            
            for (int i = 0; i < events.Length; i++)
            {
                sb.Append("DELETE FROM event WHERE hash = '");
                sb.Append(events[i].Hash);
                sb.Append("';");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Delete <see cref="TimeEvent"/> with specific hash and Log that change.
        /// </summary>
        /// <param name="hash">Hash of <see cref="TimeEvent"/> that should be deleted.</param>
        public void DeleteTimeEvent(string hash)
        {
            deleteTimeEvent(hash);
            ChangeDel(hash);
        }

        /// <summary>
        /// Delete <see cref="TimeEvent"/> with specific hash.
        /// </summary>
        /// <param name="hash">Hash of <see cref="TimeEvent"/> that should be deleted.</param>
        private void deleteTimeEvent(string hash)
        {
            string sql = "DELETE FROM event WHERE hash = '" + hash + "';";
            var command = getCommand(sql);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        /// <summary>
        /// Log Changes as adding <see cref="TimeEvent"/>s.
        /// </summary>
        /// <param name="events"><see cref="TimeEvent"/>s, that was added.</param>
        private void ChangeAdd(params TimeEvent[] events)
        {
            Change[] changes = new Change[events.Length];
            for (int i = 0; i < events.Length; i++)
            {
                changes[i] = new Change(events[i].Hash, Change.ChangeType.Add, DateTime.Now);
            }
            InsertChange(changes);
        }

        /// <summary>
        /// Log Change as adding <see cref="TimeEvent"/> with hash.
        /// </summary>
        /// <param name="hash">Hash of the <see cref="TimeEvent"/> that was edited.</param>
        private void ChangeAdd(string hash)
        {
            InsertChange(new Change(hash, Change.ChangeType.Add, DateTime.Now));
        }

        /// <summary>
        /// Log Changes as editing <see cref="TimeEvent"/>s.
        /// </summary>
        /// <param name="events"><see cref="TimeEvent"/>s, that was edited.</param>
        private void ChangeEdit(params TimeEvent[] events)
        {
            for (int i = 0; i < events.Length; i++)
            {
                ChangeAdd(events[i].Hash);
            }
        }

        /// <summary>
        /// Log Change as editing <see cref="TimeEvent"/> with hash.
        /// </summary>
        /// <param name="hash">Hash of the <see cref="TimeEvent"/> that was edited.</param>
        private void ChangeEdit(string hash)
        {
            InsertChange(new Change(hash, Change.ChangeType.Edit, DateTime.Now));
        }
        
        /// <summary>
        /// Log Change as deleting Event with hash.
        /// </summary>
        /// <param name="hash">Hash of the <see cref="TimeEvent"/> that was deteled.</param>
        private void ChangeDel(string hash)
        {
            InsertChange(new Change(hash, Change.ChangeType.Del, DateTime.Now));
        }

        /// <summary>
        /// Log change as deleting <see cref="TimeEvent"/>s.
        /// </summary>
        /// <param name="events"><see cref="TimeEvent"/>s, that was deleted.</param>
        private void ChangeDel(params TimeEvent[] events)
        {
            Change[] changes = new Change[events.Length];
            for (int i = 0; i < events.Length; i++)
            {
                changes[i] = new Change(events[i].Hash, Change.ChangeType.Del, DateTime.Now);
            }
            InsertChange(changes);
        }

        /// <summary>
        /// Execute Del Change. It delete <see cref="TimeEvent"/> with given hash from database.
        /// </summary>
        /// <param name="hash">Hash of <see cref="TimeEvent"/> that should be deleted.</param>
        internal void ExecuteDelChange(string hash)
        {
            deleteTimeEvent(hash);
        }

        /// <summary>
        /// Execute Edit Change. It edit <see cref="TimeEvent"/> from database and insert there new datas.
        /// </summary>
        /// <param name="te"><see cref="TimeEvent"/> that should be executed to Database.</param>
        internal void ExecuteEditChanges(params TimeEvent[] te)
        {
            ExecuteAddEditChanges(te);
        }

        /// <summary>
        /// Execute Add Change. It Add TimeEvent to database.
        /// </summary>
        /// <param name="te"><see cref="TimeEvent"/> that should be executed to Database.</param>
        internal void ExecuteAddChanges(params TimeEvent[] te)
        {
            ExecuteAddEditChanges(te);
        }

        /// <summary>
        /// Execute Add or Edit change. It depends on if given <see cref="TimeEvent"/> allready exist.
        /// </summary>
        /// <param name="te"><see cref="TimeEvent"/> that should be executed to Database.</param>
        internal void ExecuteAddEditChanges(params TimeEvent[] te)
        {
            replaceTimeEvent(te);
        }


        /// <summary>
        /// Close Database. After this method no more methods from this instance should be called.
        /// </summary>
        public void CloseDatabase()
        {
            dbConnection.Close();
        }
    }
}
