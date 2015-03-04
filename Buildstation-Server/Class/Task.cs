using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildstation_Server.Class
{
    class Task
    {
        /// <summary>
        /// Executes the task.
        /// </summary>
        public virtual void execute()
        {

        }

        /// <summary>
        /// Checks internally if it is a valid task.
        /// </summary>
        /// <returns>Always true.</returns>
        public bool isValidTask()
        {
            return true;
        }
    }
}
