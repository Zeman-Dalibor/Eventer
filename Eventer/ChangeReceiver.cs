using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
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
    /// Receive Change and send back a new one.
    /// </summary>
    class ChangeReceiver : IDisposable
    {
        /// <summary>
        /// Determine if this instance should stop working.
        /// </summary>
        private volatile bool stopRequested;

        /// <summary>
        /// Thread that used to listen and receive changes.
        /// </summary>
        private Thread listenerThread;

        /// <summary>
        /// Instance of listener.
        /// </summary>
        private TcpListener listener = null;

        /// <summary>
        /// Instance of receiver.
        /// </summary>
        private TcpClient receiver = null;

        /// <summary>
        /// Stream that is used to communite.
        /// </summary>
        private NetworkStream receiverStream;

        /// <summary>
        /// Instance of backend Synchronization.
        /// </summary>
        private Synchronization synch;

        /// <summary>
        /// Lock for waiting until continue will be requested.
        /// </summary>
        private object sendNewChangesRequest = new object();

        /// <summary>
        /// Maximal number of changes that will be received.
        /// </summary>
        public int MaxReceived { private set; get; }

        /// <summary>
        /// For atomic check of waiting.
        /// </summary>
        private object waitingLock = new object();

        /// <summary>
        /// Indicate if should bypass waiting for countinueRequest.
        /// </summary>
        private volatile bool bypassWaiting;

        /// <summary>
        /// Indicate if connection was closed, and that is should not be closed twice.
        /// </summary>
        private volatile bool closed = false;

        /// <summary>
        /// Lock for changing state. For example stopping.
        /// </summary>
        private object stateChangeLock = new object();

        /// <summary>
        /// Reference to resultChange to index this chnages properly.
        /// </summary>
        private Dictionary<string, Synchronization.ChangeSide> resultChanges;

        /// <summary>
        /// Cache for faster insert new changes
        /// </summary>
        private Queue<Change> newChanges = new Queue<Change>();

        /// <summary>
        /// Cache for faster insert new TimeEvents or Edit TimeEvents
        /// </summary>
        private Queue<TimeEvent> newTimeEvents = new Queue<TimeEvent>();

        /// <summary>
        /// Standart Constructor for easy data set.
        /// </summary>
        /// <param name="device">Device with which one this should be synchronized.</param>
        /// <param name="synch">Reference to synchronization.</param>
        public ChangeReceiver(SynchronizationDevice device, Synchronization synch, Dictionary<string, Synchronization.ChangeSide> resultChanges)
        {
            this.synch = synch;
            bypassWaiting = false;
            closed = false;
            this.resultChanges = resultChanges;

            listenerThread = new Thread(receivingParallel);
            listenerThread.IsBackground = true;
            listenerThread.Name = " - Listener Thread";
            listenerThread.Start(device.LastSynchDate);
        }

        /// <summary>
        /// Receiving changes and response. It should be called in separate Thread.
        /// </summary>
        /// <param name="sinceObject">Date after which changes are received.</param>
        private void receivingParallel(object sinceObject)
        {
            DateTime since = (DateTime)sinceObject;

            lock (stateChangeLock)
            {
                listener = null;
                receiverStream = null;
                receiver = null;
            }
            try
            {
                listener = new TcpListener(IPAddress.Any, Constants.TCPPort);
                listener.Start();

                if (!waitUntillConnectionOrEnd(listener))
                    return;

                receiver = listener.AcceptTcpClient();

                receiverStream = receiver.GetStream();

                using (StreamReader cReader = new StreamReader(receiverStream))
                using (StreamWriter cWriter = new StreamWriter(receiverStream))
                {

                    cWriter.WriteLine("SendMeYourChanges;" + since.ToString(Constants.DateTimeFormat) + ";" + synch.InstanceID + ";?");
                    if (stopRequested)
                        return;
                    cWriter.Flush();

                    string remoteResponse = cReader.ReadLine();
                    if (remoteResponse == "TerminateConnection;?")
                    {
                        synch.CallbackError(Synchronization.ErrorType.ConnectionLost);
                        return;
                    }

                    if (!tryReceiveMaxCountChanges(cReader))
                        return;

                    if (!receiveAllChanges(cReader))
                        return;

                    synch.RemoteChangeFull = true;
                    
                    lock (sendNewChangesRequest)
                    { if (!bypassWaiting) Monitor.Wait(sendNewChangesRequest); }
                    
                    if (!decideAndExecuteNewChanges(cReader, cWriter))
                        return;

                    cWriter.WriteLine("EndOfConnection;?");
                    if (stopRequested)
                        return;
                    cWriter.Flush();

                    if (!waitUntillConnectionEnd(cReader))
                        return;

                    synch.StopSynchronizing();
                }
            }
            catch (SocketException)
            {
                //Closed Connection due to closing of synch Form
                synch.CallbackError(Synchronization.ErrorType.ConnectionLost);
            }
            catch (IOException)
            {
                //Closed Streame due to closing of synch Form
                synch.CallbackError(Synchronization.ErrorType.ConnectionLost);
            }
            catch (ObjectDisposedException)
            {
                //Closed object due to closing of synch Form
                synch.CallbackError(Synchronization.ErrorType.ConnectionLost);
            }
            catch (InvalidOperationException)
            {
                //Closed object due to closing of synch Form
                synch.CallbackError(Synchronization.ErrorType.ConnectionLost);
            }
            finally
            {
                lock (stateChangeLock)
                {
                    if (receiver != null)
                        if (!closed)
                        {
                            closed = true;
                            if (receiver.Connected && receiver.GetStream() != null)
                                receiver.GetStream().Close();

                            receiver.Close();
                        }
                    receiver = null;
                    receiverStream = null;
                }
                if (listener != null)
                    listener.Stop();
            }
        }

        /// <summary>
        /// Try receive Max count that have remote instance, and save it into MaxReceived properties.
        /// </summary>
        /// <param name="cReader"></param>
        /// <returns>True if all was received without problem, False otherwise.</returns>
        private bool tryReceiveMaxCountChanges(TextReader cReader)
        {
            string remoteCount = cReader.ReadLine();
            int count = 0;
            if (int.TryParse(remoteCount, out count))
                MaxReceived = count;
            else
                return false;
            return true;

        }

        /// <summary>
        /// Wait untill Connection End request will be send by client.
        /// </summary>
        /// <param name="cReader">Where wait request.</param>
        /// <returns>True if End request was received without problem, False otherwise.</returns>
        private bool waitUntillConnectionEnd(TextReader cReader)
        {
            string endLine;
            while ((endLine = cReader.ReadLine()) != "EndOfConnection;?")
                if (stopRequested || endLine == null)
                {
                    synch.CallbackError(Synchronization.ErrorType.ConnectionLost);
                    return false;
                }
            return true;
        }

        /// <summary>
        /// Execute new changes, execute localy or send them to cWriter.
        /// </summary>
        /// <param name="cReader">From where should receive if new changes should be send or not.</param>
        /// <param name="cWriter">Where potencialy changes should be send.</param>
        /// <returns>True if all was read and send without problem, False otherwise.</returns>
        private bool decideAndExecuteNewChanges(TextReader cReader, TextWriter cWriter)
        {
            if (stopRequested)
                return false;

            string line = cReader.ReadLine();

            if (line == null || line[line.Length - 1] != '?')
            {
                synch.CallbackError(Synchronization.ErrorType.ConnectionProblem);
                return false;
            }

            if (line == "SendChanges;?")
                executeNewChanges(cWriter, true);
            else
                executeNewChanges(cWriter, false);
            return true;
        }

        /// <summary>
        /// Execute new changes.
        /// </summary>
        /// <param name="client">Client's writer where potencial change should be send</param>
        /// <param name="sendChanges">Determine if it should be send new changes to client.</param>
        public void executeNewChanges(TextWriter client, bool sendChanges)
        {
            lock (resultChanges)
                synch.SynchronizeTotal = resultChanges.Count + 1;
            
            lock (resultChanges)
                foreach (var item in resultChanges)
                {
                    if (stopRequested)
                    {
                        client.WriteLine("end;?");
                        return;
                    }

                    synch.ExecuteNewChange(client, item.Key, item.Value, sendChanges, newChanges, newTimeEvents);
                    synch.Synchronized++;
                }
            synch.WriteCaches(newTimeEvents, newChanges);
            synch.Synchronized++;
            client.WriteLine("end;?");
        }

        /// <summary>
        /// Wait until new connection appear. Return imidiatelly false, if connection problem or timeout happened.
        /// </summary>
        /// <param name="listener">Reference to listener that is waiting for connection.</param>
        /// <returns>True if new connection wants to connect. False if timeout is over or stop has been requested.</returns>
        private bool waitUntillConnectionOrEnd(TcpListener listener)
        {
            DateTime thisTime = DateTime.Now;
            while (stopRequested || !listener.Pending())
            {
                if (stopRequested)
                    return false;
                if (thisTime.AddSeconds(3) < DateTime.Now)
                {
                    synch.CallbackError(Synchronization.ErrorType.ConnectionProblem);
                    return false;
                }
                Thread.Sleep(10);
            }
            return true;
        }

        /// <summary>
        /// Receive all changes and process them.
        /// </summary>
        /// <param name="cReader">Reader where should be changes read.</param>
        /// <returns>True if all was read without problem, False otherwise.</returns>
        private bool receiveAllChanges(TextReader cReader)
        {
            while (true)
            {
                if (stopRequested)
                    return false;

                string record = cReader.ReadLine();

                if (record == null || record[record.Length - 1] != '?')
                {
                    synch.CallbackError(Synchronization.ErrorType.ConnectionProblem);
                    return false;
                }

                if (record == "end;?")
                    break;

                synch.AddRemoteChange(record);
            }
            return true;
        }

        /// <summary>
        /// Continue next part of work. It will execute new changes.
        /// </summary>
        public void StartExecutingNewChanges()
        {
            lock (sendNewChangesRequest)
            {
                bypassWaiting = true;
                Monitor.Pulse(sendNewChangesRequest);
            }
        }

        /// <summary>
        /// Request Stop of working.
        /// </summary>
        public void RequestStop()
        {
            lock (stateChangeLock)
            {
                stopRequested = true;

                lock (sendNewChangesRequest)
                    Monitor.Pulse(sendNewChangesRequest);

                if (receiver != null)
                    if (!closed)
                    {
                        closed = true;
                        if (receiver.Connected && receiver.GetStream() != null)
                            receiver.GetStream().Close();
                        receiver.Close();
                    }
                receiver = null;
                receiverStream = null;
            }
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
