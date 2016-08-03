using System;
using System.Collections.Generic;
using System.Threading;

#region TestHeader
#if TESTS
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsMSUnit")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EventerTestsNUnit")]
#endif
#endregion

namespace Eventer
{
    /// <summary>
    /// Add Notification it is neccessary.
    /// </summary>
    class NotificationAdder : IDisposable
    {
        /// <summary>
        /// Determine if this instance should stop working
        /// </summary>
        private volatile bool stopRequested;

        /// <summary>
        /// Thread for Adder and its job.
        /// </summary>
        private Thread adderThread;

        /// <summary>
        /// Last DateTime when last notification was added.
        /// </summary>
        private DateTime lastNotificationAddedDate;

        /// <summary>
        /// Reference for queue that hold notification TimeEvents.
        /// </summary>
        private Queue<TimeEvent> notificationTimeEvents;

        /// <summary>
        /// Determine if this instance should stop working
        /// </summary>
        private Database db;

        /// <summary>
        /// Lock for adding next TimeEvents if any.
        /// </summary>
        private object adderLock = new object();

        /// <summary>
        /// Standart Constructor
        /// </summary>
        /// <param name="db">Reference to Database.</param>
        /// <param name="notificationEvents">Queue where new TimeEvents should ne enqueued.</param>
        public NotificationAdder(Database db, Queue<TimeEvent> notificationEvents)
        {
            this.db = db;
            this.notificationTimeEvents = notificationEvents;
            lastNotificationAddedDate = DateTime.Now.AddMinutes(-5);

            adderThread = new Thread(addingEvents);
            adderThread.IsBackground = true;
            adderThread.Name = " - Notification event Adder";
            adderThread.Start();
        }

        /// <summary>
        /// Request add new Events into queue.
        /// </summary>
        public void AddNewEventsRequest()
        {
            lock (adderLock)
                Monitor.Pulse(adderLock);
        }

        /// <summary>
        /// It will be adding new events that will happe in next 30 sec. It should be called in separate Thread.
        /// </summary>
        private void addingEvents()
        {
            while (true)
            {
                lock (adderLock)
                    Monitor.Wait(adderLock);

                if (stopRequested)
                    return;

                DateTime end = DateTime.Now.AddSeconds(30);
                IEnumerable<TimeEvent> notifyTeDB = db.GetTimeEventsForNotification(lastNotificationAddedDate, end);

                foreach (var item in notifyTeDB)
                {
                    lock (notificationTimeEvents)
                        notificationTimeEvents.Enqueue(item);

                    if (stopRequested)
                        return;
                }
                lastNotificationAddedDate = end;
            }
        }

        /// <summary>
        /// Request Stop of working.
        /// </summary>
        public void RequestStop()
        {
            stopRequested = true;

            lock (adderLock)
                Monitor.Pulse(adderLock);
        }

        /// <summary>
        /// Dispose, Close, Request Stop.
        /// </summary>
        public void Dispose()
        {
            RequestStop();
        }
    }
}
