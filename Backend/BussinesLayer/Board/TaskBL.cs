using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BussinesLayer.Board
{
    internal class TaskBL
    {
        private int taskId;
        private string title;
        private readonly DateTime creationTime;
        private DateTime? dueDate;
        private string description;
        private TaskDAL taskDAL;
        private long boardId;
        private string assignee;

        public TaskBL(int taskId, long boardId ,string title, DateTime dueDate, string description)
        {
            this.taskId = taskId;
            this.title = title;
            this.creationTime = DateTime.Now;
            this.dueDate = dueDate;
            this.description = description;
            this.assignee = null;
            this.taskDAL = new TaskDAL(taskId, boardId, title, dueDate, description);
        }

        public string Assignee
        {
            get { return this.assignee; }
            set { this.assignee = value; }
        }


        public int TaskId
        {
            get { return this.taskId; }
            set { this.taskId = value; }
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public DateTime? DueDate
        {
            get { return this.dueDate; }
            set { this.dueDate = value; }
        }

        public DateTime CreationTime
        {
            get { return this.creationTime; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public TaskDAL TaskDAL
        {
            get { return this.taskDAL; }
        }
    }
}
