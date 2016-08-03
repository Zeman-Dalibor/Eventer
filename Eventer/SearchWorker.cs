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
    /// Search events and enque it and its Dates into queues.
    /// </summary>
    class SearchWorker
    {
        /// <summary>
        /// Reference to instance of Database.
        /// </summary>
        private Database db;

        /// <summary>
        /// Thread of searcher.
        /// </summary>
        private Thread searchWorkerThread;

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
        private Queue<TimeEvent> listboxSearchData;

        /// <summary>
        /// Reference to bolded Dates Datas.
        /// </summary>
        private Queue<DateTime> searchBoldedDates;

        /// <summary>
        /// Lock for State change.
        /// </summary>
        private object searchStateChangeLock = new object();

        /// <summary>
        /// Last searching parameters.
        /// </summary>
        SearchParams searchSearchingParams;

        /// <summary>
        /// Determinate if this instance is actually working or not.
        /// </summary>
        public bool Working { get; private set; }

        /// <summary>
        /// Standart constructor.
        /// </summary>
        /// <param name="db">Reference to Database instance.</param>
        /// <param name="form">Reference to MainForm.</param>
        /// <param name="listboxSearchData">Events datas.</param>
        /// <param name="searchBoldedDates">Bolded Dates datas.</param>
        public SearchWorker(Database db, MainForm form, Queue<TimeEvent> listboxSearchData, Queue<DateTime> searchBoldedDates)
        {
            this.db = db;
            this.form = form;
            this.listboxSearchData = listboxSearchData;
            this.searchBoldedDates = searchBoldedDates;
        }

        /// <summary>
        /// Show, enqueue events into Datas.
        /// </summary>
        /// <param name="name">String that should contain Name.</param>
        /// <param name="description">String that should contain Description.</param>
        /// <param name="from">Start of interval when event happen.</param>
        /// <param name="to">End of interval when event happen.</param>
        /// <param name="notificationDate">Date when notification should happen.</param>
        public void SearchShowEvents(string name, string description, DateTime from, DateTime to, DateTime notificationDate)
        {
            SearchParams searchParams;
            lock (searchStateChangeLock)
            {
                searchParams = new SearchParams(name, description, from, to, notificationDate);
                searchSearchingParams = searchParams;
                temporaryStopRequested = true;
            }
            Working = true;

            form.ClearSearchListBox();

            lock (listboxSearchData)
                listboxSearchData.Clear();
            lock (searchBoldedDates)
                searchBoldedDates.Clear();

            Thread t = new Thread(searchFindEventsParallel);
            t.IsBackground = true; 
            t.Name = " - Search Worker";
            t.Start(searchParams);

            form.EnableSearchTimer();
        }

        /// <summary>
        /// Start finding events and enquing them in Datas. It should be called in separate Thread.
        /// </summary>
        /// <param name="data">Starting params.</param>
        private void searchFindEventsParallel(object data)
        {
            SearchParams sParams = (SearchParams)data;
            try
            {
                if (searchWorkerThread != null && searchWorkerThread.IsAlive)
                    searchWorkerThread.Join();
                searchWorkerThread = Thread.CurrentThread;

                temporaryStopRequested = false;

                if (invokeStopSearchQueing(sParams))
                    return;

                IEnumerable<TimeEvent> events;
                lock (db)
                    events = db.GetTimeEvents(sParams);

                if (invokeStopSearchQueing(sParams))
                    return;

                insertEventsAndBoldedDates(events, sParams);

                lock (searchStateChangeLock)
                {
                    if (invokeStopSearchQueing(sParams))
                        return;

                    Working = false;
                    listboxSearchData.Enqueue(null);
                    searchBoldedDates.Enqueue(DateTime.MinValue);
                }
            }
            finally
            {
                Working = false;
                searchWorkerThread = null;
            }
        }

        /// <summary>
        /// Insert new Events and Bolded Dates into queues.
        /// </summary>
        /// <param name="events">Source of events, that should be inserted.</param>
        /// <param name="sParams">Local paramater with which this was started.</param>
        private void insertEventsAndBoldedDates(IEnumerable<TimeEvent> events, SearchParams sParams)
        {
            foreach (var item in events)
            {
                if (invokeStopSearchQueing(sParams))
                    return;
                lock (listboxSearchData)
                {
                    while (listboxSearchData.Count > 500)
                    {
                        Monitor.Wait(listboxSearchData, 250);
                        if (invokeStopSearchQueing(sParams))
                            return;
                    }
                    listboxSearchData.Enqueue(item);
                }

                lock (searchBoldedDates)
                {
                    if (invokeStopSearchQueing(sParams))
                        return;
                    SupportMethods.EnqueInterval(searchBoldedDates, item.StartDate, item.EndDate);
                }
            }

            
        }

        /// <summary>
        /// Determine if work should stop.
        /// </summary>
        /// <param name="sParams">Starting parameters.</param>
        /// <returns>True if it should stop, False otherwise.</returns>
        private bool invokeStopSearchQueing(SearchParams sParams)
        {
            lock (searchStateChangeLock)
            {
                if (searchSearchingParams != sParams)
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
        /// <param name="searchSearchingParams">New searching parameters.</param>
        public void ChangePreviewSearchingData(SearchParams searchSearchingParams)
        {
            lock (searchStateChangeLock)
            {
                this.searchSearchingParams = searchSearchingParams;
            }
        }
    }
}
