using System;
using System.Collections.Generic;
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
    /// Find devices in Local network by sending broadcast.
    /// </summary>
    class DeviceFinder
    {
        /// <summary>
        /// Reference to instance of Database.
        /// </summary>
        private Database db;

        /// <summary>
        /// Instance of UDP Client used for sending broadcast.
        /// </summary>
        private UdpClient client;

        /// <summary>
        /// Instance ID.
        /// </summary>
        private string instanceID;

        /// <summary>
        /// Determine if this instance should stop working.
        /// </summary>
        private volatile bool stopRequested;

        /// <summary>
        /// Standart constructor.
        /// </summary>
        /// <param name="db">Instance of Database.</param>
        public DeviceFinder(Database db)
        {
        	#if TESTS
            if (db == null)
                throw new ArgumentNullException("db");
        	#else
            if (db == null)
                throw new ArgumentNullException(nameof(db));
        	#endif
            this.db = db;
        }

        /// <summary>
        /// Reset this instance.
        /// </summary>
        /// <param name="instanceID">Instance ID.</param>
        public void Reset(string instanceID)
        {
            this.instanceID = instanceID;
        }

        /// <summary>
        /// Start Finding device in separate Thread.
        /// </summary>
        /// <param name="instancesData">Where should be enqued new devices.</param>
        public void FindDevices(Queue<SynchronizationDevice> instancesData)
        {
            stopRequested = false;
            Thread t = new Thread(findDevicesParallel);
            t.IsBackground = true; 
            t.Name = " - Instance Finder Thread";
            t.Start(instancesData);
        }

        /// <summary>
        /// Find devices, it should be called in separate Thread.
        /// </summary>
        /// <param name="data">Queue where new devices should be enqued.</param>
        private void findDevicesParallel(object data)
        {
            client = null;
            try
            {
                Queue<SynchronizationDevice> devicesData = (Queue<SynchronizationDevice>)data;
                HashSet<string> devices = new HashSet<string>();
                client = new UdpClient();
                var sendData = Encoding.UTF8.GetBytes("Send AppInfo");
                var deviceEp = new IPEndPoint(IPAddress.Any, 0);

                DateTime startFinding = DateTime.Now;

                client.EnableBroadcast = true;

                for (int i = 0; i < 4; i++)
                    client.Send(sendData, sendData.Length, new IPEndPoint(IPAddress.Broadcast, Constants.UDPPort));

                client.Client.ReceiveTimeout = 10000;
                while (startFinding.AddSeconds(8) > DateTime.Now)
                {
                    if (stopRequested)
                        return;
                    byte[] instanceResponseData;
                    try
                    {
                        instanceResponseData = client.Receive(ref deviceEp);
                    }
                    catch (SocketException)
                    {
                        continue;
                    }
                    var instanceResponse = Encoding.UTF8.GetString(instanceResponseData);
                    string[] responseParams = instanceResponse.Split(',', '=');
                    if (responseParams[3] == instanceID || devices.Contains(responseParams[3]))
                        continue;
                    devices.Add(responseParams[3]);
                    SynchronizationDevice device = db.GetSynchronizationDeviceInfo(responseParams[3]);
                    if (device == null)
                    {
                        device = new SynchronizationDevice(responseParams[1], responseParams[3], false, DateTime.MinValue, deviceEp.Address, responseParams[5]);
                        db.InsertSynchronizationDevice(device);
                    }
                    else
                    {
                        device.IP = deviceEp.Address;
                        device.GroupID = responseParams[5];
                        if (device.Name != responseParams[1])
                        {
                            device.Name = responseParams[1];
                            db.UpdateSynchronizationDevice(device);
                        }
                    }
                    lock (devicesData)
                        devicesData.Enqueue(device);
                }
                lock (devicesData)
                    devicesData.Enqueue(null);
            }
            catch (SocketException)
            {

            }
            finally
            {
                if (client != null)
                    lock (client)
                        client.Close();
            }
        }

        /// <summary>
        /// Request Stop of working.
        /// </summary>
        public void RequestStop()
        {
            stopRequested = true;

            if (client != null)
                lock (client)
                    client.Close();
        }
    }
}
