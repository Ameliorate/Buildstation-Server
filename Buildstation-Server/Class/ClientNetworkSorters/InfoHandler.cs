using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildstation_Server.Class.ClientNetworkSorters
{
    class InfoHandler : ClientNetworkSorter
    {
        public InfoHandler()
        {
            NetworkThread.RegisterNetworkSorter("Info", this);
        }

        public void NewTrafic(string Data, ClientTracker Client)
        {
            if (Data == "Connected")
                Console.WriteLine("[Info] A New Client Has Connected.");
        }
    }
}
