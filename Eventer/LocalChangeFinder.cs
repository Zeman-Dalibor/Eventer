using System;
using System.Collections.Generic;

#region TestHeader
#if TESTS
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsMSUnit")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsNUnit")]
#endif
#endregion

namespace Eventer
{
    /// <summary>
    /// Find and queue local changes.
    /// </summary>
    class LocalChangeFinder
    {
        /// <summary>
        /// Determine if this instance should stop working
        /// </summary>
        private volatile bool stopRequested;

        /// <summary>
        /// Determine if this instance should stop working
        /// </summary>
        private Database db;

        /// <summary>
        /// Since which time should be changes to added.
        /// </summary>
        private DateTime since;

        /// <summary>
        /// Reference to resultChange to index this chnages properly.
        /// </summary>
        private Dictionary<string, Synchronization.ChangeSide> resultChange;

        /// <summary>
        /// Instance of backend Synchronization.
        /// </summary>
        private Synchronization synch;

        /// <summary>
        /// Max Changes find in Database for given params.
        /// </summary>
        public int MaxChangeFind { private set; get; }
        
        /// <summary>
        /// Constructor for LocalChangeFinder
        /// </summary>
        /// <param name="db">Database reference.</param>
        /// <param name="synch">Syncrhonization reference.</param>
        public LocalChangeFinder(Database db, Synchronization synch)
        {
            this.db = db;
            this.synch = synch;
        }

        /// <summary>
        /// Reset working.
        /// </summary>
        /// <param name="since">Since this time the change must happen to be in result.</param>
        /// <param name="resultChange">Reference to result index. Changes will be inserted properly</param>
        public void Reset(DateTime since, Dictionary<string, Synchronization.ChangeSide> resultChange)
        {
            stopRequested = false;
            this.since = since;
            this.resultChange = resultChange;
        }

        /// <summary>
        /// Start Working. It will adding changes into given reference queue. It should be called in separate Thread.
        /// </summary>
        /// <param name="data">Queue where local changes should be inserted.</param>
        public void DoWork(object data)
        {
            Queue<Change> localChangesData = (Queue<Change>)data;

            IEnumerable<Change> changes = db.GetChangesSinceDateTime(since);

            Dictionary<string, Change> localChanges = new Dictionary<string, Change>();

            foreach (var item in changes)
            {
                if (stopRequested)
                    return;

                lock (localChanges)
                    if (localChanges.ContainsKey(item.TimeEventHash))
                        localChanges[item.TimeEventHash] = item;
                    else
                        localChanges.Add(item.TimeEventHash, item);
            }

            if (stopRequested)
                return;

            MaxChangeFind = localChanges.Count;

            getUniqChanges(localChangesData, localChanges);

            synch.LocalChangeFull = true;
        }

        /// <summary>
        /// Enqueue unique changes into desctination queue.
        /// </summary>
        /// <param name="destination">Where changes should be enqueued</param>
        /// <param name="source">Changes, that should be enqueued.</param>
        private void getUniqChanges(Queue<Change> destination, Dictionary<string, Change> source)
        {
            foreach (var item in source)
            {
                if (stopRequested)
                    return;

                Change c = item.Value;
                c.TimeEvent = db.GetTimeEvent(c.TimeEventHash);
                if (c.Type != Change.ChangeType.Del && c.TimeEvent == null)
                    throw new ArgumentNullException();

                lock (synch.LocalChanges)
                    synch.LocalChanges.Add(c.TimeEventHash, c);

                if (stopRequested)
                    return;

                lock (resultChange)
                {
                    if (resultChange.ContainsKey(c.TimeEventHash))
                        lock (synch.RemoteChanges)
                            synch.AddToResultsAndReturnIfItIsNecessary(c, synch.RemoteChanges[c.TimeEventHash]);
                    else
                        resultChange.Add(c.TimeEventHash, Synchronization.ChangeSide.Local);
                }

                lock (destination)
                    destination.Enqueue(c);
            }
        }

        /// <summary>
        /// Request Stop of working.
        /// </summary>
        public void RequestStop()
        {
            stopRequested = true;
        }
    }
}
