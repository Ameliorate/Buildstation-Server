using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Buildstation_Server.Class;

namespace Buildstation_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread NetworkListenerThread = new Thread(Class.NetworkThread.NetworkListenerThread);
            NetworkListenerThread.Start();

            Class.ClientNetworkSorters.InfoHandler InfoHandler = new Class.ClientNetworkSorters.InfoHandler();

            string CerrentName;
            int XPos = 0;
            int YPos = 0;

            while (true)
            {
                CerrentName = NameTools.GenerateName("Space");
                ObjectTools.SpawnObjectBuffer(XPos.ToString(), YPos.ToString(), CerrentName, "Space");
                XPos++;
                if (XPos == 15)
                {
                    XPos = 0;
                    YPos++;
                    if (YPos == 15)
                    {
                        break;
                    }
                }
            }

            ObjectTools.FlushPacketBuffer();

            Console.WriteLine("[Info] Finished Initalising");
        }
    }
}
