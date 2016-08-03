using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Eventer
{
    /// <summary>
    /// It delete TimeEvents in separate thread.
    /// </summary>
    class TimeEventDeleter
    {
        /// <summary>
        /// Reference to instance of Database.
        /// </summary>
        private Database db;

        /// <summary>
        /// Determine if this instance should stop working.
        /// </summary>
        private volatile bool stopRequested;

        /// <summary>
        /// Events that should be deleted.
        /// </summary>
        private TimeEvent[] eventsToDelete;

        /// <summary>
        /// Count of allready deleted Timevents.
        /// </summary>
        public int DeletedCount { get; private set; }

        /// <summary>
        /// Total count in selection, that should be deleted.
        /// </summary>
        private int totalCount;

        /// <summary>
        /// Start deleting given events.
        /// </summary>
        /// <param name="eventsToDelete">Events that will be deleted.</param>
        public TimeEventDeleter(Database db, int totalCount, TimeEvent[] eventsToDelete)
        {
            this.db = db;
            this.eventsToDelete = eventsToDelete;
            this.totalCount = totalCount;

            DeletedCount = 0;

            Thread t = new Thread(deleteEventsParallel);
            t.IsBackground = true;
            t.Name = " - TimeEvent Deleter";
            t.Start();
        }

        /// <summary>
        /// Delete given selection of <see cref="TimeEvent"/>s.
        /// </summary>
        private void deleteEventsParallel()
        {
            int len = totalCount / 20;
            List<TimeEvent> block = new List<TimeEvent>();
            foreach (TimeEvent item in eventsToDelete)
            {
                if (stopRequested)
                    return;
                block.Add(item);
                if (block.Count >= len)
                {
                    db.DeleteTimeEvents(block.ToArray());
                    DeletedCount += block.Count;
                    block.Clear();
                }
            }

            if (block.Count > 0)
            {
                db.DeleteTimeEvents(block.ToArray());
                DeletedCount += block.Count;
            }
        }

        /// <summary>
        /// Request Stop working.
        /// </summary>
        public void RequestStop()
        {
            stopRequested = true;
        }
    }
}
