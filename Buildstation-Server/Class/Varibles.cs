﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildstation_Server.Class
{
    static class Variables
    {
        // This class contains public varibles ment to be used in many places.

        /// <summary>
        /// The map. Mostly is used by other objects to see what is under them and to interface with those objects. X and Y are self explanatory, Z is the height of the objects in a pile of objects. 0 is normally the floor or space.
        /// </summary>
        public static Dictionary<Coordinate, string> Map = new Dictionary<Coordinate, string>();


        /// <summary>
        /// Every single possible reasonable charactor on your keyboard. Could include lowercases for almost twice as many, but thats getting unreasonable. Also too much efort to type.
        /// </summary>
        static public char[] AllPossibleCharactors = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

        public static Dictionary<string, dynamic> PhysicalObjects = new Dictionary<string, dynamic>();      // A dictionary of objects. Every single object in the game. Probably not optimal, but I cant find another way.
        public static List<string> AllTiles = new List<string>();       // Allows tiles to update every object without scaning through the map array, the disadvantage being that they cant access the tiles in a spific order exept when it was created.

    }
}
