using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

#region TestHeader
#if TESTS
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsMSUnit")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsNUnit")]
#endif
#endregion

namespace Eventer
{
    /// <summary>
    /// Synchronization and all useful methods and other things for it.
    /// </summary>
    internal class Synchronization
    {
        /// <summary>
        /// Name of this program used in Synchronizing.
        /// </summary>
        public string Name { get; set; }

		/// <summary>
		/// Instance ID of this program.
		/// </summary>
		public string InstanceID { get; private set; }

        /// <summary>
        /// Group ID of this program.
        /// </summary>
        public string GroupID { get; private set; }

        /// <summary>
        /// Instance of backend Database.
        /// </summary>
        private Database db;

        #region Broadcast

        /// <summary>
        /// Error Type that can happened and should be recognized.
        /// </summary>
        public enum ErrorType { AllreadyBindUDPPort, AllreadyBindTCPPort, ConnectionProblem, ConnectionLost };

        /// <summary>
        /// Error Method, delegate for errors handlers.
        /// </summary>
        /// <param name="type"></param>
        public delegate void ErrorMethod(ErrorType type);

        /// <summary>
        /// CallBack called, when error occurs.
        /// </summary>
        public ErrorMethod CallbackError;

        /// <summary>
        /// Indicate if program is actually responding. To broadcast and to connection request.
        /// </summary>
        public bool Responding
        {
            get { return responder.Responding; }
            set { responder.Responding = value; }
        }

        /// <summary>
        /// Responder instance, it response to requests.
        /// </summary>
        private Responder responder;

        /// <summary>
        /// Device finder, it broadcast local net and discover devices.
        /// </summary>
        private DeviceFinder deviceFinder;

        #endregion Broadcast

        #region Synch

        /// <summary>
        /// Local changes for fast access. They are listed in SynchForm.
        /// </summary>
        public Dictionary<string, Change> LocalChanges = new Dictionary<string, Change>();

        /// <summary>
        /// Data for Local ListBox.
        /// </summary>
        public Queue<Change> LocalListBoxData = new Queue<Change>();

        /// <summary>
        /// Local changes for fast access. They are listed in SynchForm.
        /// </summary>
        public Dictionary<string, Change> RemoteChanges = new Dictionary<string, Change>();

        /// <summary>
        /// Data for Remote ListBox.
        /// </summary>
        public Queue<Change> RemoteListBoxData = new Queue<Change>();

        /// <summary>
        /// Side of change, what should happened with change if it is synchronizing.
        /// </summary>
        public enum ChangeSide { Local, LocalDisabled, Remote, RemoteDisabled, NotDecided, BothSideDisabled, LocalSideDisabled, RemoteSideDisabled, NotNecessaryUpdate }

        /// <summary>
        /// Result changes. It contain hashs and which side should be used.
        /// </summary>
        private Dictionary<string, ChangeSide> resultChanges = new Dictionary<string, ChangeSide>();

        /// <summary>
        /// It is filling local changes: Data for listBox and LocalChanges cache.
        /// </summary>
        private LocalChangeFinder localChangeFinder;

        /// <summary>
        /// Thread of localChangeFinder.
        /// </summary>
        private Thread localChangeFinderThread;

        /// <summary>
        /// Receiver it receive change and send a new one.
        /// </summary>
        private ChangeReceiver receiver;

        /// <summary>
        /// It send old one and receive new one.
        /// </summary>
        private ChangeSender sender;

        /// <summary>
        /// If it is actually sinchronizing.
        /// </summary>
        private bool synchronizing;

        /// <summary>
        /// Count of changes that has been synchronized.
        /// </summary>
        public int Synchronized;

        /// <summary>
        /// Count of total changes that must be synchronized.
        /// </summary>
        public int SynchronizeTotal;

        /// <summary>
        /// If LocalChange already load all changes.
        /// </summary>
        public bool LocalChangeFull = false;

        /// <summary>
        /// If RemoteChange already load all changes.
        /// </summary>
        public bool RemoteChangeFull = false;

        /// <summary>
        /// If synchronization is completed
        /// </summary>
        public bool SynchronizationCompleted = false;

        /// <summary>
        /// Maximal local changes.
        /// </summary>
        public int MaxLocal { get { return localChangeFinder.MaxChangeFind; } }

        /// <summary>
        /// Maximal Remote Changes.
        /// </summary>
        public int MaxRemote { get { return receiver.MaxReceived; } }

        #endregion Synch

        /// <summary>
        /// Constructor for creating instance with connection to Database.
        /// </summary>
        /// <param name="db">Reference to database.</param>
        public Synchronization(Database db)
        {
            deviceFinder = new DeviceFinder(db);
            responder = new Responder(this);

            ApplicationInfo appInfo = db.GetApplicationInfo();
            this.db = db;
            InstanceID = appInfo.InstanceID;
            GroupID = appInfo.GroupID;
            Name = appInfo.Name;
            Responding = appInfo.Visible;

            localChangeFinder = new LocalChangeFinder(db, this);
        }

        /// <summary>
        /// Save Application Info.
        /// </summary>
        /// <param name="notifyEvents">If notify events should be save as True or False.</param>
        public void SaveApplicationInfo(bool notifyEvents)
        {
            ApplicationInfo appInfo = new ApplicationInfo(Name, InstanceID, GroupID, Responding, notifyEvents);
            db.UpdateApplicationInfo(appInfo);
        }

        /// <summary>
        /// Set Event group ID to given string.
        /// </summary>
        /// <param name="id">New group ID.</param>
        public void SetEventsGroupID(string id)
        {
            GroupID = id;
        }

        /// <summary>
        /// It shuld be called when MainForm is closing.
        /// </summary>
        /// <param name="notifyEvents">If it is notify events true or false.</param>
        public void MainFormClosing(bool notifyEvents)
        {
            SaveApplicationInfo(notifyEvents);
            Responding = false;
            if (responder != null)
                responder.RequestStop();

            if (deviceFinder != null)
                deviceFinder.RequestStop();
        }

        #region Broadcasting By UDP
        //----------------------------------- Broadcasting By UDP  ---------------------------------

        /// <summary>
        /// Find devices in local network.
        /// </summary>
        /// <param name="devices">Qeue with not processed device.</param>
        public void FindDevices(Queue<SynchronizationDevice> devices)
        {
            deviceFinder.Reset(InstanceID);
            deviceFinder.FindDevices(devices);
        }

        /// <summary>
        /// Connect Sender to given ip.
        /// </summary>
        /// <param name="ip">Where should Sender connect to.</param>
        public void ConnectSenderTo(IPAddress ip)
        {
            if (sender != null)
                sender.RequestStop();
            sender = new ChangeSender(db, this, ip);
        }

        //----------------------------------- End of Broadcasting By UDP ---------------------------------
        #endregion Broadcasting By UDP

        #region SynchBy TCP
        //----------------------------------- SynchBy TCP  ---------------------------------

        /// <summary>
        /// Send Connect Request to ip.
        /// </summary>
        /// <param name="ip">Who should connect to.</param>
        public void SendConnectRequest(IPAddress ip)
        {
            var client = new UdpClient();
            var msg = Encoding.UTF8.GetBytes("ConnectToMe");
            client.Send(msg, msg.Length, new IPEndPoint(ip, Constants.UDPPort));
            client.Close();
        }

        /// <summary>
        /// Send Changes to writer.
        /// </summary>
        /// <param name="writer">Where should be chnage send through.</param>
        /// <param name="c">Which change should be send.</param>
        /// <param name="te">Which TimeEvent should be send.</param>
        public void SendChange(TextWriter writer, Change c, TimeEvent te)
        {
            if (c.Type == Change.ChangeType.Del)
                writer.WriteLine(c.GetSynchronizationString() + "*" + "null" + "*?");
            else
                writer.WriteLine(c.GetSynchronizationString() + "*" + te.GetSynchronizationString() + "*?");
        }

        /// <summary>
        /// Clear data structures used for synchronization and reset and stop workers.
        /// </summary>
        private void clearAndResetDatas()
        {
            lock (resultChanges)
                resultChanges.Clear();
            lock (RemoteChanges)
                RemoteChanges.Clear();
            lock (RemoteListBoxData)
                RemoteListBoxData.Clear();
            lock (LocalChanges)
                LocalChanges.Clear();
            lock (LocalListBoxData)
                LocalListBoxData.Clear();

            if (receiver != null)
                receiver.RequestStop();

            synchronizing = false;

            LocalChangeFull = false;
            RemoteChangeFull = false;

            Synchronized = 0;
            SynchronizeTotal = 0;

            SynchronizationCompleted = false;
        }

        /// <summary>
        /// Start synchronizing with device.
        /// </summary>
        /// <param name="device">Which device it is going to synchronize.</param>
        public void SynchronizeWith(SynchronizationDevice device)
        {
            clearAndResetDatas();
            receiver = new ChangeReceiver(device, this, resultChanges);

            SendConnectRequest(device.IP);

            localChangeFinder.Reset(device.LastSynchDate, resultChanges);

            localChangeFinderThread = new Thread(localChangeFinder.DoWork);
            localChangeFinderThread.IsBackground = true;
            localChangeFinderThread.Name = " - Local change Adder Thread";
            localChangeFinderThread.Start(LocalListBoxData);

            DialogResult res;
            using (SynchForm form = new SynchForm(device, this))
            {
                res = form.ShowDialog();
            }

            if (res == DialogResult.OK)
            {
                device.LastSynchDate = DateTime.Now.AddSeconds(5);
                db.UpdateSynchronizationDevice(device);
            }
        }

        /// <summary>
        /// Start Synchronizing.
        /// </summary>
        public void StartSynchronizing()
        {
            synchronizing = true;
            receiver.StartExecutingNewChanges();
        }

        /// <summary>
        /// Stop synchronizing.
        /// </summary>
        public void StopSynchronizing()
        {
            synchronizing = false;
            SynchronizationCompleted = true;
        }

        /// <summary>
        /// Should be called when SynchForm is closing.
        /// </summary>
        /// <returns>True if it can be safely closed. False otherwise.</returns>
        public bool SychFormClosing()
        {
            if (synchronizing)
                return false;

            if (sender != null)
                sender.RequestStop();

            if (receiver != null)
                receiver.RequestStop();

            if (localChangeFinder != null)
                localChangeFinder.RequestStop();

            lock (resultChanges)
                resultChanges.Clear();
            lock (RemoteChanges)
                RemoteChanges.Clear();
            lock (RemoteListBoxData)
                RemoteListBoxData.Clear();
            lock (LocalChanges)
                LocalChanges.Clear();
            lock (LocalListBoxData)
                LocalListBoxData.Clear();

            return true;
        }

        /// <summary>
        /// Parse Record received by connection into Chnage and TimeEvent.
        /// </summary>
        /// <param name="record">String that should be parsed.</param>
        /// <param name="c">Change, that was parsed.</param>
        /// <param name="te">TimeEvent, that was parsed</param>
        public void ParseRecord(string record, out Change c, out TimeEvent te)
        {
            var items = record.Split('*');
            c = Change.Parse(items[0]);
            if (items[1] == "null")
                te = null;
            else
                te = TimeEvent.Parse(items[1]);
        }

        /// <summary>
        /// Add Remote Change into data structures.
        /// </summary>
        /// <param name="record">String with change that should be added.</param>
        public void AddRemoteChange(string record)
        {
            Change c;
            TimeEvent te;

            ParseRecord(record, out c, out te);
            c.TimeEvent = te;

            if (c.Type != Change.ChangeType.Del && c.TimeEvent == null)
                throw new ArgumentNullException();

            lock (RemoteChanges)
                RemoteChanges.Add(c.TimeEventHash, c);

            lock (resultChanges)
                if (resultChanges.ContainsKey(c.TimeEventHash))
                    lock (LocalChanges)
                        AddToResultsAndReturnIfItIsNecessary(LocalChanges[c.TimeEventHash], c);
                else
                    resultChanges.Add(c.TimeEventHash, ChangeSide.Remote);

            lock (RemoteListBoxData)
                RemoteListBoxData.Enqueue(c);
        }

        /// <summary>
        /// Add to result and change its side if it's necessary.
        /// </summary>
        /// <param name="local">Local change.</param>
        /// <param name="remote">Remote change.</param>
        /// <returns>True if it is neccesary to decide, False otherwise.</returns>
        public bool AddToResultsAndReturnIfItIsNecessary(Change local, Change remote)
        {
            lock (resultChanges)
                if (local.Type == Change.ChangeType.Del && (remote.Type == Change.ChangeType.Add || remote.Type == Change.ChangeType.Edit))
                {
                    resultChanges[local.TimeEventHash] = ChangeSide.NotDecided;
                    return true;
                }
                else if ((local.Type == Change.ChangeType.Add || local.Type == Change.ChangeType.Edit) && remote.Type == Change.ChangeType.Del)
                {
                    resultChanges[local.TimeEventHash] = ChangeSide.NotDecided;
                    return true;
                }
                else if (local.Type == Change.ChangeType.Del && remote.Type == Change.ChangeType.Del)
                {
                    resultChanges[local.TimeEventHash] = ChangeSide.NotNecessaryUpdate;
                    return false;
                }
                else if (local.TimeEvent.HasSameDataAs(remote.TimeEvent))
                {
                    resultChanges[local.TimeEventHash] = ChangeSide.NotNecessaryUpdate;
                    return false;
                }
                else
                {
                    resultChanges[local.TimeEventHash] = ChangeSide.NotDecided;
                    return true;
                }
        }



        /// <summary>
        /// Execute new change, send them, or execute localy, or nothing of that.
        /// </summary>
        /// <param name="client">Where potencial new changes should be sent.</param>
        /// <param name="hash">Hash of change that should be executed.</param>
        /// <param name="side">Side of </param>
        /// <param name="sendChange">Determine if it should be send new changes to client.</param>
        public void ExecuteNewChange(TextWriter client, string hash, ChangeSide side, bool sendChange, Queue<Change> changeCache, Queue<TimeEvent> timeEventCache)
        {
            switch (side)
            {
                case ChangeSide.Local:
                case ChangeSide.RemoteSideDisabled:
                    if (sendChange)
                        lock (LocalChanges)
                            SendChange(client, LocalChanges[hash], LocalChanges[hash].TimeEvent);
                    break;
                case ChangeSide.Remote:
                case ChangeSide.LocalSideDisabled:
                    lock (RemoteChanges)
                        ExecuteChange(RemoteChanges[hash], timeEventCache, changeCache);
                    break;
                case ChangeSide.NotDecided:
                    using (SideChooseForm choose = new SideChooseForm(LocalChanges[hash], RemoteChanges[hash]))
                    {
                        DialogResult isLocalOne = choose.ShowDialog();

                        if (isLocalOne == DialogResult.Yes)
                            SendChange(client, LocalChanges[hash], LocalChanges[hash].TimeEvent);

                        else if (isLocalOne == DialogResult.No)
                            ExecuteChange(RemoteChanges[hash], timeEventCache, changeCache);
                        else
                            return;
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Enable local change.
        /// </summary>
        /// <param name="hash">Hash of enabling change.</param>
        public void EnableLocalChange(string hash)
        {
            lock (resultChanges)
                switch (resultChanges[hash])
                {
                    case ChangeSide.NotNecessaryUpdate:
                        return;
                    case ChangeSide.LocalDisabled:
                        resultChanges[hash] = ChangeSide.Local;
                        break;
                    case ChangeSide.BothSideDisabled:
                        resultChanges[hash] = ChangeSide.RemoteSideDisabled;
                        break;
                    case ChangeSide.Local:
                        resultChanges[hash] = ChangeSide.Local;
                        break;
                    case ChangeSide.LocalSideDisabled:
                        resultChanges[hash] = ChangeSide.NotDecided;
                        break;
                }
        }

        /// <summary>
        /// Disable local change.
        /// </summary>
        /// <param name="hash">Hash of disabling change.</param>
        public void DisableLocalChange(string hash)
        {
            lock (resultChanges)
                switch (resultChanges[hash])
                {
                    case ChangeSide.NotNecessaryUpdate:
                        return;
                    case ChangeSide.Local:
                        resultChanges[hash] = ChangeSide.LocalDisabled;
                        break;
                    case ChangeSide.NotDecided:
                        resultChanges[hash] = ChangeSide.LocalSideDisabled;
                        break;
                    case ChangeSide.RemoteSideDisabled:
                        resultChanges[hash] = ChangeSide.BothSideDisabled;
                        break;
                    case ChangeSide.LocalDisabled:
                        resultChanges[hash] = ChangeSide.LocalDisabled;
                        break;
                }
        }

        /// <summary>
        /// Enable remote change.
        /// </summary>
        /// <param name="hash">Hash of enabling change.</param>
        public void EnableRemoteChange(string hash)
        {
            lock (resultChanges)
                switch (resultChanges[hash])
                {
                    case ChangeSide.NotNecessaryUpdate:
                        return;
                    case ChangeSide.RemoteDisabled:
                        resultChanges[hash] = ChangeSide.Remote;
                        break;
                    case ChangeSide.BothSideDisabled:
                        resultChanges[hash] = ChangeSide.LocalSideDisabled;
                        break;
                    case ChangeSide.Remote:
                        resultChanges[hash] = ChangeSide.Remote;
                        break;
                    case ChangeSide.RemoteSideDisabled:
                        resultChanges[hash] = ChangeSide.NotDecided;
                        break;
                }
        }

        /// <summary>
        /// Disable remote change.
        /// </summary>
        /// <param name="hash">Hash of disabling change.</param>
        public void DisableRemoteChange(string hash)
        {
            lock (resultChanges)
                switch (resultChanges[hash])
                {
                    case ChangeSide.NotNecessaryUpdate:
                        return;
                    case ChangeSide.Remote:
                        resultChanges[hash] = ChangeSide.RemoteDisabled;
                        break;
                    case ChangeSide.NotDecided:
                        resultChanges[hash] = ChangeSide.RemoteSideDisabled;
                        break;
                    case ChangeSide.LocalSideDisabled:
                        resultChanges[hash] = ChangeSide.BothSideDisabled;
                        break;
                    case ChangeSide.RemoteDisabled:
                        resultChanges[hash] = ChangeSide.RemoteDisabled;
                        break;
                }
        }

        /// <summary>
        /// Execute new change in local.
        /// </summary>
        /// <param name="c">Change that should be executed.</param>
        /// <param name="te">TimeEvent that should be executed.</param>
        public void ExecuteChange(Change c, Queue<TimeEvent> newTimeEvents, Queue<Change> newChanges)
        {
            lock (db)
                switch (c.Type)
                {
                    case Change.ChangeType.Add:
                    case Change.ChangeType.Edit:
                        if (c.TimeEvent == null)
                            throw new NullReferenceException();
                        lock (newTimeEvents)
                            newTimeEvents.Enqueue(c.TimeEvent);
                        break;
                    case Change.ChangeType.Del:
                        db.ExecuteDelChange(c.TimeEventHash);
                        break;
                }
            lock (newChanges)
                newChanges.Enqueue(c);
        }

        /// <summary>
        /// Write content of change cache into Database.
        /// </summary>
        public void WriteCaches(Queue<TimeEvent> newTimeEvents, Queue<Change> newChanges)
        {
            lock (newTimeEvents)
            {
                TimeEvent[] timeEvents = new TimeEvent[newTimeEvents.Count];
                int i = 0;
                while (true)
                {
                    if (newTimeEvents.Count == 0)
                        break;
                    timeEvents[i] = newTimeEvents.Dequeue();
                    i++;
                }
                lock (db)
                    if (timeEvents.Length != 0)
                        db.ExecuteAddEditChanges(timeEvents);
                newTimeEvents.Clear();
            }
            lock (newChanges)
            {
                Change[] changes = new Change[newChanges.Count];
                int i = 0;
                while (true)
                {
                    if (newChanges.Count == 0)
                        break;

                    Change c = newChanges.Dequeue();
                    c.ChangeDate = DateTime.Now;
                    changes[i] = c;
                    i++;
                }

                lock (db)
                    if (changes.Length != 0)
                        db.InsertChange(changes);
                newChanges.Clear();
            }
        }

        //----------------------------------- End of SynchBy TCP  ---------------------------------
        #endregion SynchBy TCP
    }
}
