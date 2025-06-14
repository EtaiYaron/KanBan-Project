using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace IntroSE.Kanban.Frontend.Model
{
    internal class TaskModel
    {
        public string Title { get; }
        public DateTime CreationTime { get; }
        public DateTime DueDate { get; }

        public string Assignee { get; }

        internal TaskModel(TaskSL task)
        {
            Title = task.Title;
            CreationTime = task.CreationTime;
            DueDate = task.DueDate;
            Assignee = task.Assignee;
        }
    }
}
