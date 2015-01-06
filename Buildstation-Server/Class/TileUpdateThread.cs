using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Buildstation_Server.Class
{
    static class TileUpdateThread
    {
        static int Progress = 0;
        static int SleepTime;
        static string TileAtPoint;

        /// <summary>
        /// Updates every tile in the game once per 100 miliseconds, or 10 times a second. 1/10 of a second is equal to 1 Physical Tick.
        /// </summary>
        public static void PhysicalUpdateLoop()
        {
            Stopwatch StopWatch = new Stopwatch();
            long TimeSinceLoopBegin;;
            string UpdateData;
            List<String> UpdatePackets = new List<string>();
            

            while (true)
            {
                StopWatch.Restart();
                try
                {
                    while (Progress <= Variables.AllTiles.Count)
                    {
                        TileAtPoint = Variables.AllTiles[Progress];     // Gets what tile it will try to update.

                        UpdateData = Variables.PhysicalObjects[TileAtPoint].Update();       // Trys to update it.
                        if (UpdateData != "")
                            UpdatePackets.Add(UpdateData);
                        Progress++;
                    }
                }
                catch (ArgumentOutOfRangeException e) { }

                string[] UpdateArray;
                UpdateArray = UpdatePackets.ToArray();

                NetworkThread.BroadcastMessage("TileUpdate", UpdateArray);

                TimeSinceLoopBegin = StopWatch.ElapsedMilliseconds;
                SleepTime = Math.Abs((int)TimeSinceLoopBegin - 100);
                if (TimeSinceLoopBegin <= 100)      // Basically waits untill 100 miliseconds has passed, so a tick is always 100 miliseconds or more.
                    Thread.Sleep(SleepTime);
                
            }
        }

        public static void SimulationUpdateLoop()
        {       // This is detacated for computation intensive things like atmospherics that aren't the highest priority.       
            // I'm thinking this should run something like every 1/2 of a second or something. Though that may be a bit much since atmos should rush out of an airlock as soon as it's opened.
            throw new NotImplementedException();
        }
    }
}
