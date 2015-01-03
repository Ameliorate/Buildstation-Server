using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildstation_Server.Class
{
    abstract class ClientNetworkSorter
    {
        public ClientNetworkSorter()
        {
            NetworkThread.RegisterNetworkSorter("foo", this);
        }

        protected string[] DataSplit;

        public virtual void NewTrafic(string Data)
        {
            throw new NotImplementedException();
        }
    }
}
