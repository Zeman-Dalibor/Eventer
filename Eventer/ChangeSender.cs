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
    /// Send all change since given time and receive new changes.
    /// </summary>
    class ChangeSender
    {
        /// <summary>
        /// Determine if this instance should stop working
        /// </summary>
        private volatile bool stopRequested;

        /// <summary>
        /// Reference to instance of Database.
        /// </summary>
        private Database db;

        /// <summary>
        /// Instance of backend Synchronization.
        /// </summary>
        private Synchronization synch;

        /// <summary>
        /// Thread of sender.
        /// </summary>
        private Thread senderThread;

        /// <summary>
        /// Instance of TCP client as sender.
        /// </summary>
        private TcpClient senderClient;

        /// <summary>
        /// Indicate if connection was closed, and that is should not be closed twice.
        /// </summary>
        private volatile bool closed = false;

        /// <summary>
        /// Lock for changing state. For example stopping.
        /// </summary>
        private object stateChangeLock = new object();

        /// <summary>
        /// Cache for faster insert new changes
        /// </summary>
        private Queue<Change> newChanges = new Queue<Change>();

        /// <summary>
        /// Cache for faster insert new TimeEvents or Edit TimeEvents
        /// </summary>
        private Queue<TimeEvent> newTimeEvents = new Queue<TimeEvent>();

        /// <summary>
        /// Standart constructor for easy data set and connect to instance and send Changes.
        /// </summary>
        /// <param name="db">Database reference, where change should be get.</param>
        /// <param name="synch">Synchronization instance where quest and dictionaries are located.</param>
        /// <param name="ip">Where to connect and send changes.</param>
        public ChangeSender(Database db, Synchronization synch, IPAddress ip)
        {
            this.db = db;
            this.synch = synch;
            closed = false;
            senderThread = new Thread(connectForSynchronizeParallel);
            senderThread.IsBackground = true;
            senderThread.Name = " - Sender Client Thread";
            senderThread.Start(ip);
        }

        /// <summary>
        /// Conect to given ip adress and send changes. It should be called in separate Thread.
        /// </summary>
        /// <param name="ipObject">IP adress, where to connect.</param>
        private void connectForSynchronizeParallel(object ipObject)
        {
            //Init
            IPAddress ip = (IPAddress)ipObject;
            NetworkStream stream = null;
            try
            {
                //Connect And set streams.
                senderClient = new TcpClient();
                senderClient.Connect(ip, Constants.TCPPort);
                stream = senderClient.GetStream();
                using (StreamReader sReader = new StreamReader(stream))
                using (StreamWriter sWriter = new StreamWriter(stream))
                {

                    string line = sReader.ReadLine();

                    if (line == null || line[line.Length - 1] != '?')
                    {
                        synch.CallbackError(Synchronization.ErrorType.ConnectionProblem);
                        return;
                    }

                    string[] synchParams = line.Split(';');

                    if (synchParams[2] == synch.InstanceID)
                    {
                        sWriter.WriteLine("TerminateConnection;?");
                        sWriter.WriteLine("EndOfConnection;?");
                        if (stopRequested)
                            return;
                        sWriter.Flush();
                        return;
                    }
                    else
                    {
                        sWriter.WriteLine("Continue;?");
                        if (stopRequested)
                            return;
                        sWriter.Flush();
                    }

                    SendAllChanges(sWriter, DateTime.Parse(synchParams[1]));

                    SynchronizationDevice device;
                    lock (db)
                        device = db.GetSynchronizationDeviceInfo(synchParams[2]);

                    if (device != null && device.Allow)
                    {
                        sWriter.WriteLine("SendChanges;?");
                        if (stopRequested)
                            return;
                        sWriter.Flush();
                        receiveAndExecuteNewChanges(sReader);
                    }
                    else
                        sWriter.WriteLine("DoNotSend;?");

                    sWriter.WriteLine("NowItIs;" + DateTime.Now.ToString(Constants.DateTimeFormat) + ";?");

                    sWriter.WriteLine("EndOfConnection;?");
                    if (stopRequested)
                        return;
                    sWriter.Flush();

                    if (!waitUntillConnectionEnd(sReader))
                        return;

                    if (device != null && device.Allow)
                    {
                        device.LastSynchDate = DateTime.Now.AddSeconds(5);
                        lock (db)
                            db.UpdateSynchronizationDevice(device);
                    }
                }
            }
            catch (SocketException)
            {
                //Connection closed
            }
            catch (IOException)
            {
                //Closed Streame due to closing of synch Form
            }
            catch (ObjectDisposedException)
            {
                //Closed Streame due to closing of synch Form
            }
            catch (NullReferenceException)
            {
                //Closed Streame due to closing connection
            }
            finally
            {

                lock (stateChangeLock)
                {
                    if (senderClient != null)
                        lock (senderClient)
                        {
                            if (!closed)
                            {
                                closed = true;
                                if (senderClient.Connected && senderClient.GetStream() != null) //TODO:Add connected check
                                    senderClient.GetStream().Close();

                                senderClient.Close();
                            }
                        }
                    senderClient = null;
                }
            }
        }

        /// <summary>
        /// Wait untill End Connection Request is read.
        /// </summary>
        /// <param name="sReader">Where should read EndConnection Request.</param>
        /// <returns>True if EndConnectionRefuest was read. </returns>
        private bool waitUntillConnectionEnd(TextReader sReader)
        {
            string endLine;
            while ((endLine = sReader.ReadLine()) != "EndOfConnection;?")
                if (stopRequested || endLine == null)
                    return false;
            return true;
        }

        /// <summary>
        /// Receive and execute new changes.
        /// </summary>
        /// <param name="sReader">Where new changes should be read.</param>
        /// <returns>True if changes have been read without problem, False otherwise.</returns>
        private bool receiveAndExecuteNewChanges(TextReader sReader)
        {
            while (true)
            {
                if (stopRequested)
                    return false;

                string record = sReader.ReadLine();

                if (record == null || record[record.Length - 1] != '?')
                {
                    synch.CallbackError(Synchronization.ErrorType.ConnectionLost);
                    return false;
                }

                if (record == "end;?")
                    break;

                Change c;
                TimeEvent te;
                synch.ParseRecord(record, out c, out te);
                c.TimeEvent = te;
                synch.ExecuteChange(c, newTimeEvents, newChanges);
            }
            synch.WriteCaches(newTimeEvents, newChanges);
            return true;
        }

        /// <summary>
        /// Send all changes.
        /// </summary>
        /// <param name="server">Where it should be send.</param>
        /// <param name="since">Which changes should be send.</param>
        private void SendAllChanges(TextWriter server, DateTime since)
        {
            IEnumerable<Change> changes;
            lock (db)
                changes = db.GetChangesSinceDateTime(since);
            Dictionary<string, Change> lastChanges = new Dictionary<string, Change>();
            foreach (var item in changes)
            {
                if (lastChanges.ContainsKey(item.TimeEventHash))
                    lastChanges[item.TimeEventHash] = item;
                else
                    lastChanges.Add(item.TimeEventHash, item);
            }
            server.WriteLine(lastChanges.Count);
            server.Flush();
            SendAllChanges(lastChanges, server);
        }

        /// <summary>
        /// Send All given changes.
        /// </summary>
        /// <param name="changes">Changes that should be send.</param>
        /// <param name="writer">Where it should be send.</param>
        private void SendAllChanges(Dictionary<string, Change> changes, TextWriter writer)
        {
            foreach (var item in changes)
            {
                Change c = item.Value;
                TimeEvent te;
                lock (db)
                    te = db.GetTimeEvent(item.Key);
                synch.SendChange(writer, c, te);
            }
            writer.WriteLine("end;?");
            writer.Flush();
        }

        /// <summary>
        /// Request Stop of working.
        /// </summary>
        public void RequestStop()
        {
            stopRequested = true;

            lock (stateChangeLock)
            {
                if (senderClient != null)
                {
                    lock (senderClient)
                        if (!closed)
                        {
                            closed = true;
                            if (senderClient.Connected && senderClient.GetStream() != null)
                                senderClient.GetStream().Close();
                            senderClient.Close();
                        }
                }
            }
        }
    }
}
