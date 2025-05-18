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
        private int state;
        private TaskDAL taskDAL;
        private bool isPersistent;
        private string assigneeEmail;
        private long boardId;
        

        public TaskBL(int taskId, string title, DateTime dueDate, string description)
        {
            this.taskId = taskId;
            this.title = title;
            this.creationTime = DateTime.Now;
            this.dueDate = dueDate;
            this.description = description;
            this.state = 0;
        }

        public void EditTask(string title, DateTime dueDate, string description)
        {
            this.title = title;
            this.dueDate = dueDate;
            this.description = description;
        }

        public void moveTask(int dest)
        { 
            this.taskId = dest; 
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
        public int State
        {
            get { return this.state; }
            set { this.state = value; }
        }





    }
}
