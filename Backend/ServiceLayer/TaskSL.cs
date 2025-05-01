using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Board;

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

        public TaskSL(TaskBL tbl)
        {
            this.taskId = tbl.TaskId;
            this.title = tbl.Title;
            this.creationTime = tbl.CreationTime;
            this.dueDate = tbl.DueDate;
            this.description = tbl.Description; 
            this.state = tbl.State;
        }
    }
}
