using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Threading;


namespace Buildstation_Server.Class
{
    static class NetworkThread
    {

        private static TcpClient NewConnection;
        private static Dictionary<string, ClientTracker> ClientTrackers = new Dictionary<string, ClientTracker>();
        private static Dictionary<string, Thread> ClientThreads = new Dictionary<string, Thread>();
        public static List<string> ClientTrackerKeys = new List<string>();

        public static void NetworkListenerThread()
        {
            // Some needed varibles for network connections.
            TcpListener Listener = new TcpListener(System.Net.IPAddress.Parse("127.0.0.1"), 25565);
            Listener.Start();
            string NewConnectionString;

            while (true)
            {
                NewConnection = Listener.AcceptTcpClient();
                NewConnectionString = NewConnection.ToString();

                ClientTrackers.Add(NewConnectionString, new ClientTracker(NewConnection));
                ClientThreads.Add(NewConnectionString, new Thread(ClientTrackers[NewConnectionString].Connect));
                ClientTrackerKeys.Add(NewConnectionString);
                ClientThreads[NewConnectionString].Start();     // Creates an instance of the client tracking class, creates a new thread of it, then starts the thread. Also it adds the key to a list for use in broadcasting.
            }
        }

        public static Dictionary<string, dynamic> NetworkSorters = new Dictionary<string, dynamic>();

        public static void RegisterNetworkSorter(string SorterName, dynamic NetworkSorter)
        {
            NetworkSorters.Add(SorterName, NetworkSorter);
        }

        public static void BroadcastMessage(string Sorter, string Message)
        {
            int Progress = 0;
            string Key;

            while (Progress <= ClientTrackerKeys.Count)
            {
                Key = ClientTrackerKeys[Progress];
                ClientTrackers[Key].SendMessage(Sorter, Message);
                Progress++;
            }
        }
    }
}
