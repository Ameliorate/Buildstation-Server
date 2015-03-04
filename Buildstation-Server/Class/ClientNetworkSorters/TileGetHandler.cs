using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildstation_Server.Class.ClientNetworkSorters
{
    class TileGetHandler : ClientNetworkSorter
    {
        public TileGetHandler()
        {
            NetworkThread.RegisterNetworkSorter("GetTile", this);
        }

        private string TileTypeAtSpace;
        private string TileAtSpace;
        private string[] TileNameSplit;
        private string TileInitData;
        private int XPos;
        private int YPos;
        private int ZPos;

        public void NewTrafic(string Data, ClientTracker Client)
        {       // Data comes in as [XPos],[YPos],[ZPos]
            DataSplit = Data.Split(',');

            XPos = Convert.ToInt32(DataSplit[0]);
            YPos = Convert.ToInt32(DataSplit[1]);
            ZPos = Convert.ToInt32(DataSplit[2]);
            try
            {
                TileAtSpace = Variables.Map[new Coordinate(XPos, YPos, ZPos, "Default")];
            }
            catch (Exception)
            {
                Gamemode.Generate(XPos, YPos);
                TileAtSpace = Variables.Map[new Coordinate(XPos, YPos, ZPos, "Default")];   // Once the tile has generated, continue on with what your doing.
            }
            TileNameSplit = TileTypeAtSpace.Split('_');
            TileTypeAtSpace = TileNameSplit[0];
            TileInitData = Variables.PhysicalObjects[TileAtSpace].GetData();

            Client.SendMessage("PlaceTile", TileTypeAtSpace + "," + TileAtSpace + "," + DataSplit[0] + "," + DataSplit[1] + "," + DataSplit[2] + "," + TileInitData);
        }
    }
}
