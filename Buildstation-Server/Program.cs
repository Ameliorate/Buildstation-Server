using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Buildstation_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread NetworkListenerThread = new Thread(Class.NetworkThread.NetworkListenerThread);
            NetworkListenerThread.Start();

            Class.ClientNetworkSorters.InfoHandler InfoHandler = new Class.ClientNetworkSorters.InfoHandler();
        }
    }
}
