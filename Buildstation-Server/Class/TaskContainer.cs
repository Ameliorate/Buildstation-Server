using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildstation_Server.Class
{
    class TaskContainer
    {
        public TaskContainer() { }
        
        public TaskContainer(Task task, DateTime nextExecute)
        {
            taskClass = task;
            nextExecuteTime = nextExecute;
        }

        public TaskContainer(Task task, DateTime nextExecute, int repeatTime)
        {
            taskClass = task;
            nextExecuteTime = nextExecute;
            repeatDuration = repeatTime;
        }

        public dynamic taskClass
        {
            get
            {
                return taskClass;
            }
            set
            {
                try
                {
                    value.isValidTask();
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// An estimite of how long it takes to execute that task. int.MaxValue indicates that it is not known.
        /// </summary>
        public int executeDuration = int.MaxValue;

        /// <summary>
        /// When the task should be executed next.
        /// </summary>
        public DateTime nextExecuteTime = DateTime.Now;

        /// <summary>
        /// The ammount of time inbetween when the task should be executed. In miliseconds.
        /// </summary>
        public int repeatDuration = 0;

        public void execute()
        {
            taskClass.execute();
        }
    }
}
