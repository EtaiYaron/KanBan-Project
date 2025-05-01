using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class TaskSL
    {
        private readonly int taskId;
        private readonly string title;
        private readonly DateTime creationTime;
        private readonly DateTime dueDate;
        private readonly string description;
        private readonly int state;

        public TaskSL(int taskId, string title, DateTime creationTime, DateTime dueDate, string description, int state)
        {
            this.taskId = taskId;
            this.title = title;
            this.creationTime = creationTime;
            this.dueDate = dueDate;
            this.description = description;
            this.state = state;
        }
    }
}
