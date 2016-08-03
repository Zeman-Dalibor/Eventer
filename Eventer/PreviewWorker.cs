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
    /// Search and enqueue Events and its bolded Dates into given datas, queues.
    /// </summary>
    class PreviewWorker
    {
        /// <summary>
        /// Reference to instance of Database.
        /// </summary>
        private Database db;

        /// <summary>
        /// Thread of Preview worker.
        /// </summary>
        private Thread previewWorkerThread;

        /// <summary>
        /// Reference to MainForm
        /// </summary>
        private MainForm form;

        /// <summary>
        /// Determine if this instance should stop working
        /// </summary>
        private volatile bool stopRequested;

        /// <summary>
        /// Determine if this instance should temporary stop working
        /// </summary>
        private volatile bool temporaryStopRequested;

        /// <summary>
        /// Refernce to Events Datas.
        /// </summary>
        private Queue<TimeEvent> listboxPreviewData;

        /// <summary>
        /// Reference to bolded Dates Datas.
        /// </summary>
        private Queue<DateTime> previewBoldedDates;

        /// <summary>
        /// Lock for State change.
        /// </summary>
        private object previewStateChangeLock = new object();

        /// <summary>
        /// Last searching From parameter.
        /// </summary>
        private DateTime previewSearchingFrom;
        /// <summary>
        /// Last searching To parameter.
        /// </summary>
        private DateTime previewSearchingTo;

        /// <summary>
        /// Determinate if this instance is actually working or not.
        /// </summary>
        public bool Working { get; private set; }

        /// <summary>
        /// Standart constructor.
        /// </summary>
        /// <param name="db">Reference to Database instance.</param>
        /// <param name="form">Reference to MainForm.</param>
        /// <param name="listboxPreviewData">Events datas.</param>
        /// <param name="previewBoldedDates">Bolded Dates datas.</param>
        public PreviewWorker(Database db, MainForm form, Queue<TimeEvent> listboxPreviewData, Queue<DateTime> previewBoldedDates)
        {
            this.db = db;
            this.form = form;
            this.listboxPreviewData = listboxPreviewData;
            this.previewBoldedDates = previewBoldedDates;
        }

        /// <summary>
        /// Show, enqueue events into Datas.
        /// </summary>
        /// <param name="from">Start of interval.</param>
        /// <param name="to">End of interval.</param>
        public void PreviewShowEvents(DateTime from, DateTime to)
        {
            lock (previewStateChangeLock)
            {
                if (from == previewSearchingFrom && to == previewSearchingTo)
                    return;

                temporaryStopRequested = true;
                ChangePreviewSearchingData(from, to);
            }
            Working = true;
            form.ShowPreviewLoadingMsg();

            Interval sr = new Interval(from, to);

            form.ClearPreview();

            lock (listboxPreviewData)
                listboxPreviewData.Clear();
            lock (previewBoldedDates)
                previewBoldedDates.Clear();

            Thread t = new Thread(previewFindEventsParallel);
            t.Name = " - Preview Worker";
            t.IsBackground = true; 
            t.Start(sr);

            form.EnablePreviewTimer();
        }

        /// <summary>
        /// Start finding events and enquing them in Datas. It should be called in separate Thread.
        /// </summary>
        /// <param name="interval">Starting interval.</param>
        private void previewFindEventsParallel(object interval)
        {
            try
            {
                if (previewWorkerThread != null && previewWorkerThread.IsAlive)
                    previewWorkerThread.Join();
                previewWorkerThread = Thread.CurrentThread;
                temporaryStopRequested = false;

                DateTime from = ((Interval)interval).Start;
                DateTime to = ((Interval)interval).End;

                IEnumerable<TimeEvent> events;

                if (invokeStopPreviewQueing(from, to))
                    return;

                lock (db)
                    events = db.GetTimeEventsInRange(from, to, 50000);

                if (invokeStopPreviewQueing(from, to))
                    return;

                insertEventsAndBoldedDates(events, from, to);

                lock (previewStateChangeLock)
                {
                    if (invokeStopPreviewQueing(from, to))
                        return;

                    Working = false;
                    listboxPreviewData.Enqueue(null);
                    previewBoldedDates.Enqueue(DateTime.MinValue);
                }
            }
            finally
            {
                Working = false;
                previewWorkerThread = null;
            }
        }

        /// <summary>
        /// Insert new events and Bolded Dates into queues.
        /// </summary>
        /// <param name="events">Source of events, that should be inserted.</param>
        /// <param name="from">Local paramater from with which this was started.</param>
        /// <param name="to">Local paramater to with which this was started.</param>
        private void insertEventsAndBoldedDates(IEnumerable<TimeEvent> events, DateTime from, DateTime to)
        {
            foreach (var item in events)
            {
                if (invokeStopPreviewQueing(from, to))
                    return;
                lock (listboxPreviewData)
                {
                    while (listboxPreviewData.Count > 500)
                    {
                        Monitor.Wait(listboxPreviewData, 250);
                        if (invokeStopPreviewQueing(from, to))
                            return;
                    }

                    listboxPreviewData.Enqueue(item);
                }

                lock (previewBoldedDates)
                {
                    if (invokeStopPreviewQueing(from, to))
                        return;
                    SupportMethods.EnqueInterval(previewBoldedDates, item.StartDate, item.EndDate);
                }
            }
        }

        /// <summary>
        /// Determine if work should stop.
        /// </summary>
        /// <param name="originFrom">Starting parameter from.</param>
        /// <param name="originTo">Starting parameter to.</param>
        /// <returns>True if it should stop, False otherwise.</returns>
        private bool invokeStopPreviewQueing(DateTime originFrom, DateTime originTo)
        {
            lock (previewStateChangeLock)
            {
                if (originFrom != previewSearchingFrom)
                    return true;
                if (originTo != previewSearchingTo)
                    return true;
                if (stopRequested)
                    return true;
                if (temporaryStopRequested)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// Request Stop of working.
        /// </summary>
        public void RequestStop()
        {
            stopRequested = true;
        }

        /// <summary>
        /// Request Temporary stop of working.
        /// </summary>
        public void TemporaryStopRequest()
        {
            temporaryStopRequested = true;
        }

        /// <summary>
        /// Chnage searching parameter to new one.
        /// </summary>
        /// <param name="from">New searching parameter from.</param>
        /// <param name="to">New searching parameter to.</param>
        public void ChangePreviewSearchingData(DateTime from, DateTime to)
        {
            lock (previewStateChangeLock)
            {
                previewSearchingFrom = from;
                previewSearchingTo = to;
            }
        }
    }
}
