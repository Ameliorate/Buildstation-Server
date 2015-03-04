using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildstation_Server.Class
{
    /// <summary>
    /// Allows you to call requests to the gamemode without knowing wich gamemode is being used.
    /// </summary>
    static class Gamemode
    {
        /// <summary>
        /// Generate a tile at that spot.
        /// </summary>
        /// <param name="X">The X position of that tile</param>
        /// <param name="Y">The Y pos of that tile.</param>
        public static void Generate(int X, int Y)
        {
            // TODO: Actually implament gamemodes.
            ObjectTools.SpawnObject(X.ToString(), Y.ToString(), NameTools.GenerateName("Space"), "Space");      // Just spawns a space tile as a filler.
        }
    }
}
