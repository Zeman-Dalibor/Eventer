using System.Net;
using System.Net.Sockets;
using System.Text;
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
    /// Response to broadcast and requests to connection.
    /// </summary>
    class Responder
    {
        /// <summary>
        /// Backend Synchronization instance.
        /// </summary>
        private Synchronization synch;

        /// <summary>
        /// Determine if this instance should stop working
        /// </summary>
        private volatile bool stopRequested;

        /// <summary>
        /// Determine if it should responding.
        /// </summary>
        public bool Responding
        {
            get { return responding; }
            set
            {
                if (value)
                {
                    lock (responseLock)
                    {
                        responding = true;
                        if (allreadyResponding)
                            return;
                        allreadyResponding = true;
                    }
                    startResponding();
                }
                else
                {
                    lock (responseLock)
                    {
                        responding = false;

                        if (responder != null)
                            responder.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Determine Allready responding, and so it is not necessary to start it again.
        /// </summary>
        private bool allreadyResponding = false;

        /// <summary>
        /// Determine if responding.
        /// </summary>
        private bool responding = false;

        /// <summary>
        /// UDP client instance, the responder.
        /// </summary>
        private UdpClient responder;

        /// <summary>
        /// Thread of responser.
        /// </summary>
        private Thread responseThread = null;

        /// <summary>
        /// Lock for responsing.
        /// </summary>
        private object responseLock = new object();

        /// <summary>
        /// Standart constructor.
        /// </summary>
        /// <param name="synch">Instance of Synchronization.</param>
        public Responder(Synchronization synch)
        {
            this.synch = synch;
        }

        /// <summary>
        /// Start responding.
        /// </summary>
        private void startResponding()
        {
            Thread t = new Thread(respondingParallel);
            t.IsBackground = true; 
            t.Name = " - Broadcast Responder";
            t.Start();
        }

        /// <summary>
        /// Start responding, it should be called in separate Thread.
        /// </summary>
        private void respondingParallel()
        {
            lock (responseLock)
            {
                if (responseThread != null)
                    responseThread.Join();
                responseThread = Thread.CurrentThread;
            }
            responder = null;
            try
            {
                responder = new UdpClient(Constants.UDPPort);

                while (true)
                {
                    if (stopRequested)
                        return;
                    lock (responseLock)
                        if (!responding)
                        {
                            allreadyResponding = false;
                            responseThread = null;
                            responder.Close();
                            break;
                        }

                    var searcherEp = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedBytes = null;
                    try
                    {
                        receivedBytes = responder.Receive(ref searcherEp);
                        if (receivedBytes == null || receivedBytes.Length == 0)
                            return;
                    }
                    catch (SocketException)
                    {
                        continue;
                    }
                    if (receivedBytes == null)
                        continue;

                    var clientRequest = Encoding.UTF8.GetString(receivedBytes);


                    if (clientRequest == "Send AppInfo")
                    {
                        var responseData = Encoding.UTF8.GetBytes("name=" + synch.Name + ",id=" + synch.InstanceID + ",groupid=" + synch.GroupID);
                        responder.Send(responseData, responseData.Length, searcherEp);
                    }
                    else if (clientRequest == "ConnectToMe")
                        synch.ConnectSenderTo(searcherEp.Address);
                }
            }
            catch (SocketException)
            {
                synch.CallbackError(Synchronization.ErrorType.AllreadyBindUDPPort);
                allreadyResponding = false;
                responding = false;
                responseThread = null;
                return;
            }
            finally
            {
                if (responder != null)
                    lock (responder)
                        responder.Close();
            }
        }

        /// <summary>
        /// Request Stop of working.
        /// </summary>
        public void RequestStop()
        {
            stopRequested = true;

            if (responder != null)
                lock (responder)
                    responder.Close();
        }
    }
}
