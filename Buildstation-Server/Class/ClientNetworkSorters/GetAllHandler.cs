using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildstation_Server.Class.ClientNetworkSorters
{
    class GetAllHandler : ClientNetworkSorter
    {
        public GetAllHandler()
        {
            NetworkThread.RegisterNetworkSorter("GetAll", this);
        }

        private int CerrentXPoint;
        private int CerrentYPoint;
        private int CerrentZPoint;
        private int XGoal;
        private int YGoal;

        public override void NewTrafic(string Data, ClientTracker Client)
        {       // Data comes in  as [Xpos],[YPos] representing the top left corner of the 15*15 square that is the displayport.
            DataSplit = Data.Split(',');
            CerrentXPoint = Convert.ToInt32(DataSplit[0]);
            CerrentYPoint = Convert.ToInt32(DataSplit[1]);
            CerrentZPoint = 0;
            XGoal = CerrentXPoint + 15;
            YGoal = CerrentYPoint + 15;
            List<string> PacketBuffer = new List<string>();
            bool TileIsEmpty;
            string CerrentTileName;
            string CerrentTileType;
            string InitData;

            Console.WriteLine("GetAll Start");

            while (true)
            {
                // Data will be [TileType],[TileName],[XPos],[YPos],[ZPos],[InitData] to spawn an object on clientside.

                CerrentTileName = Variables.Map[CerrentXPoint, CerrentYPoint, CerrentZPoint];
                CerrentTileType = CerrentTileName.Split('_')[0];
                InitData = Variables.PhysicalObjects[CerrentTileName].GetData();

                PacketBuffer.Add(CerrentTileType + "," + CerrentTileName + "," + CerrentXPoint + "," + CerrentYPoint + "," + CerrentZPoint + "," + InitData);

                CerrentZPoint++;
                TileIsEmpty = string.IsNullOrEmpty(Variables.Map[CerrentXPoint, CerrentYPoint, CerrentZPoint]);

                if (TileIsEmpty == true)
                {
                    CerrentZPoint = 0;
                    CerrentXPoint++;

                    if (CerrentXPoint == XGoal)
                    {
                        CerrentXPoint = 0;
                        CerrentYPoint++;

                        Console.WriteLine(CerrentYPoint);

                        if (CerrentYPoint == YGoal)
                        {
                            Console.WriteLine("Finished Getall");

                            Client.SendMessage("PlaceTile", PacketBuffer.ToArray());
                            Console.WriteLine("Sent Packet");
                            Client.SendMessage("Finished", "GetAll");
                            Console.WriteLine("Sent finish");

                            break;      // I have a large dislike for 3 dimentional loops. Too many things to keep track of.
                        }
                    }
                }
            }

            //Client.SendMessage("PlaceTile", PacketBuffer.ToArray());

            //Client.SendMessage("Finished", "GetAll");       // Tell the client it's finished reciveing all the data it needs.
        }
    }
}
