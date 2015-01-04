using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildstation_Server.Class
{
    static class ObjectTools
    {
        static string[] Parameters = new string[3];
        static dynamic CreatedTile;

        /// <summary>
        /// Creates a tile at the intended coordanites with the intended name. Also handles all initalision and object handling logic.
        /// </summary>
        /// <param name="XPos">The X possition of the object you want to spawn.</param>
        /// <param name="YPos">The Y possition of the object you want to spawn.</param>
        /// <param name="ObjectName">The name of the object you want to spawn. This is the one that comes from nametools. EX: Space_ASDGT</param>
        /// <param name="ObjectType">The type of the object you want to spawn.</param>
        public static void SpawnObject(string XPos, string YPos, string ObjectName, string ObjectType)
        {
            CreatedTile = CreateObject(XPos, YPos, "No", ObjectName, ObjectType);       // Creates the object.
            CreatedTile.Initalise();            // Initalises it.
            // Interesting thaught, if an object is created in another thread and the rendering engine tries to render that object, the game will crash.
            Variables.PhysicalObjects.Add(ObjectName, CreatedTile);     // Adds a refrence to the object in the tile dictionary.
            NetworkThread.BroadcastMessage("PlaceTile", ObjectType + "," + ObjectName + "," + XPos + "," + YPos + "," + InitData);
        }

        private static string InitData;

        /// <summary>
        /// Creates a tile at the intended coordanites with the intended name. Also handles all initalision and object handling logic.
        /// </summary>
        /// <param name="XPos">The X possition of the object you want to spawn.</param>
        /// <param name="YPos">The Y possition of the object you want to spawn.</param>
        /// <param name="ZPos">The Z possition of the object you want to spawn.</param>
        /// <param name="ObjectName">The name of the object you want to spawn. This is the one that comes from nametools. EX: Space_ASDGT</param>
        /// <param name="ObjectType">The type of the object you want to spawn.</param>
        public static void SpawnObject(string XPos, string YPos, string ZPos, string ObjectName, string ObjectType)
        {
            CreatedTile = CreateObject(XPos, YPos, ZPos, ObjectName, ObjectType);       // Creates the object.
            CreatedTile.Initalise();            // Initalises it.
            // Interesting thaught, if an object is created in another thread and the rendering engine tries to render that object, the game will crash.
            Variables.PhysicalObjects.Add(ObjectName, CreatedTile);     // Adds a refrence to the object in the tile dictionary.
            InitData = CreatedTile.GetData();
            NetworkThread.BroadcastMessage("PlaceTile", ObjectType + "," + ObjectName + "," + XPos + "," + YPos + "," + ZPos + "," + InitData);
        }

        private static List<String> SpawnPacketBuffer = new List<string>();

        public static void SpawnObjectBuffer(string XPos, string YPos, string ZPos, string ObjectName, string ObjectType)
        {
            CreatedTile = CreateObject(XPos, YPos, ZPos, ObjectName, ObjectType);       // Creates the object.
            CreatedTile.Initalise();            // Initalises it.
            // Interesting thaught, if an object is created in another thread and the rendering engine tries to render that object, the game will crash.
            Variables.PhysicalObjects.Add(ObjectName, CreatedTile);     // Adds a refrence to the object in the tile dictionary.
            InitData = CreatedTile.GetData();
            SpawnPacketBuffer.Add(ObjectType + "," + ObjectName + "," + XPos + "," + YPos + "," + ZPos + "," + InitData);
        }

        public static void SpawnObjectBuffer(string XPos, string YPos, string ObjectName, string ObjectType)
        {
            CreatedTile = CreateObject(XPos, YPos, "No", ObjectName, ObjectType);       // Creates the object.
            CreatedTile.Initalise();            // Initalises it.
            // Interesting thaught, if an object is created in another thread and the rendering engine tries to render that object, the game will crash.
            Variables.PhysicalObjects.Add(ObjectName, CreatedTile);     // Adds a refrence to the object in the tile dictionary.
            SpawnPacketBuffer.Add(ObjectType + "," + ObjectName + "," + XPos + "," + YPos + "," + InitData);
        }

        public static void FlushPacketBuffer()
        {
            string[] SpawnObjectBufferAsArray;
            SpawnObjectBufferAsArray = SpawnPacketBuffer.ToArray();
            NetworkThread.BroadcastMessage("PlaceTile", SpawnObjectBufferAsArray);
        }


        /// <summary>
        /// Handles the turning of shorthand strings for objects into the full objects. Quite ugly, so it's it's own method.
        /// </summary>
        /// <param name="XPos">The X possition.</param>
        /// <param name="YPos">The Y possition</param>
        /// <param name="ZPos">The Z possition. Can be "No" if it is supposed to be at the top of the stack of tiles.</param>
        /// <param name="ObjectName">The name of the object. Comes from nametools. EX: Space_DSGHR.</param>
        /// <param name="ObjectType">The type of object. EX: Space.</param>
        /// <returns>Returns a raw object to be assigned to the object array after initalision is done.</returns>
        private static object CreateObject(string XPos, string YPos, string ZPos, string ObjectName, string ObjectType)
        {       // WARNING: Ugly code ahead, I do admit it is terrible, but there isn't a better way.

            // Begin new object
            if (ObjectType == "ObjectTemplate")
            {
                if (ZPos == "No")
                {
                    return new Objects.ObjectTemplate(ObjectName, XPos, YPos);
                }
                else
                {
                    return new Objects.ObjectTemplate(ObjectName, XPos, YPos, ZPos);
                }
            }
            // End new object

            // Begin new object
            if (ObjectType == "Space")
            {
                if (ZPos == "No")
                {
                    return new Objects.Space(ObjectName, XPos, YPos);
                }
                else
                {
                    return new Objects.Space(ObjectName, XPos, YPos, ZPos);
                }
            }
            // End new object


            else
            {
                throw new ArgumentException("The ObjectType \"" + ObjectType + "\" was not valid.", "ObjectType");
            }
        }


    }
}
