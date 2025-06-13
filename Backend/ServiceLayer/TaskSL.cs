using System;
using System.Text.Json.Serialization;
using IntroSE.Kanban.Backend.BussinesLayer.Board;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class TaskSL
    {
        private readonly int taskId;
        private readonly DateTime creationTime;
        private readonly string title;
        private readonly string description;
        private readonly DateTime dueDate;

        internal TaskSL(TaskBL tbl)
        {
            this.taskId = tbl.TaskId;
            this.creationTime = tbl.CreationTime;
            this.title = tbl.Title;
            this.description = tbl.Description;
            this.dueDate = (DateTime)tbl.DueDate;
        }

        [JsonConstructor]
        public TaskSL(int taskId, DateTime creationTime, string title, string description, DateTime dueDate)
        {
            this.taskId = taskId;
            this.title = title;
            this.creationTime = creationTime;
            this.dueDate = dueDate;
            this.description = description;
        }

        public TaskSL() { }

        // Properties for serialization
        public int TaskId
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
