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
        private readonly string assignee;

        internal TaskSL(TaskBL tbl)
        {
            this.taskId = tbl.TaskId;
            this.creationTime = tbl.CreationTime;
            this.title = tbl.Title;
            this.description = tbl.Description;
            this.dueDate = (DateTime)tbl.DueDate;
            this.assignee = tbl.Assignee;
        }

        [JsonConstructor]
        public TaskSL(int taskId, DateTime creationTime, string title, string description, DateTime dueDate, string assignee)
        {
            this.taskId = taskId;
            this.title = title;
            this.creationTime = creationTime;
            this.dueDate = dueDate;
            this.description = description;
            this.assignee = assignee;
        }

        public TaskSL() { }

        // Properties for serialization
        public int TaskId
        {
            get { return this.taskId; }
        }

        public string Assignee
        {
            get { return this.assignee; }
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
