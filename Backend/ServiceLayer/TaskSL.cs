using System;
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

        public TaskSL(TaskBL tbl)
        {
            this.taskId = tbl.TaskId;
            this.title = tbl.Title;
            this.creationTime = tbl.CreationTime;
            this.dueDate = (DateTime)tbl.DueDate;
            this.description = tbl.Description;
        }

        // Properties for serialization
        public int Id
        {
            get { return this.taskId; }
        }

        public string Title
        {
            get { return this.title; }
        }

        public DateTime CreationTime
        {
            get { return this.creationTime; }
        }

        public DateTime DueDate
        {
            get { return this.dueDate; }
        }

        public string Description
        {
            get { return this.description; }
        }
    }
}
