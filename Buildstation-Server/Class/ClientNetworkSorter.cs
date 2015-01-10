using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildstation_Server.Class
{
    abstract class ClientNetworkSorter
    {
        /*
        public ClientNetworkSorter()
        {
            NetworkThread.RegisterNetworkSorter("foo", this);
        }
        */      // This shouldnt be uncommented, it is just here as a reminder.
        protected string[] DataSplit;

        public virtual void NewTrafic(string Data, ClientTracker Client)
        {
            throw new NotImplementedException();
        }
    }
}
