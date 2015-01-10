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
            Class.ClientNetworkSorters.TileGetHandler TileGetHandler = new Class.ClientNetworkSorters.TileGetHandler();
            Class.ClientNetworkSorters.GetAllHandler GetAllHandler = new Class.ClientNetworkSorters.GetAllHandler();

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

            Thread TileUpdateThread = new Thread(Class.TileUpdateThread.PhysicalUpdateLoop);
            TileUpdateThread.Start();
            

            Console.WriteLine("[Info] Finished Initalising");
        }
    }
}
