using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class TaskDAL
    {
        private int taskId;
        private string title;
        private readonly DateTime creationTime;
        private DateTime? dueDate;
        private string description;
        private int state;
        private long boardId;
        private string? assigneeEmail;
        private TaskController TaskController;
        private bool isPersistent;

        /// <summary>
        /// Constructor for the TaskDAL class.
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="boardId"></param>
        /// <param name="title"></param>
        /// <param name="dueDate"></param>
        /// <param name="description"></param>
        public TaskDAL(int taskId,long boardId, string title, DateTime dueDate, string description)
        {
            this.taskId = taskId;
            this.boardId = boardId;
            this.title = title;
            this.creationTime = DateTime.Now;
            this.dueDate = dueDate;
            this.description = description;
            this.state = 0;
            this.TaskController = new TaskController();
            this.isPersistent = false;
            this.assigneeEmail = null;
        }


        /// <summary>
        /// Constructor for the TaskDAL class.
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="boardId"></param>
        /// <param name="title"></param>
        /// <param name="dueDate"></param>
        /// <param name="creationTime"></param>
        /// <param name="description"></param>
        public TaskDAL(int taskId, long boardId, string title, DateTime dueDate, DateTime creationTime, string description)
        {
            this.taskId = taskId;
            this.boardId = boardId;
            this.title = title;
            this.creationTime = creationTime;
            this.dueDate = dueDate;
            this.description = description;
            this.state = 0;
            this.TaskController = new TaskController();
            this.isPersistent = false;
            this.assigneeEmail = null;
        }

        public int TaskId
        {
            get { return this.taskId; }
            set { this.taskId = value; }
        }

        public string Title
        {
            get { return this.title; }
            set {
                if (isPersistent)
                {
                    title = value; 
                    TaskController.Update(this, "title", value);
                }
            }
        }

        public DateTime? DueDate
        {
            get { return this.dueDate; }
            set
            {
                if (isPersistent)
                {
                    dueDate = value; 
                    TaskController.Update(this, "dueDate", "#" + dueDate.ToString() + "#");
                }
            }
        }

        public string Description
        {
            get { return this.description; }
            set
            {
                if (isPersistent)
                {
                    title = value; 
                    TaskController.Update(this, "description", value);
                }
            }
        }

        public int State
        {
            get { return this.state; }
            set
            {
                if (isPersistent)
                {
                    this.state = value;
                    TaskController.Update(this, "state", value.ToString());
                }
            }
        }

        public long BoardId
        {
            get { return this.boardId; }
            set
            {
                if (isPersistent)
                {
                    this.boardId = value;
                }
            }
        }

        public DateTime CreationTime
        {
            get { return this.creationTime; }
        }

        public string AssigneeEmail
        {
            get { return this.assigneeEmail; }
            set
            {
                if (isPersistent)
                {
                    this.assigneeEmail = value;
                    TaskController.Update(this, "assigneeEmail", value);
                }
            }
        }

        /// <summary>
        /// This method is used to edit the task.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dueDate"></param>
        /// <param name="description"></param>
        public void EditTask(string title, DateTime dueDate, string description)
        {
            this.Title = title;
            this.DueDate = dueDate;
            this.Description = description;
        }


        /// <summary>
        /// This method is used to move the task to the next column.
        /// </summary>
        public void MoveTask()
        {
            this.State = this.state + 1;
        }

        /// <summary>
        /// This method is used to assign the task to a user.
        /// </summary>
        /// <param name="email"></param>
        public void AssignTaskToUser(string email)
        {
            this.AssigneeEmail = email;            
        }

        /// <summary>
        /// This method is used to check if the task is persistent in the database.
        /// </summary>
        public void persist()
        {
            if (!isPersistent)
            {
                TaskController.Insert(this);
                isPersistent = true;
            }
        }

        
        public void persist(long boardId)
        {
            if (!isPersistent)
            {
                throw new Exception("cannot save persisted object.");
            }
            this.boardId = boardId;
            TaskController.Insert(this);
            this.isPersistent = true;
        }

    }
}
