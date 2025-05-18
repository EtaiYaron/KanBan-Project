using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BussinesLayer.Board
{
    internal class BoardBL
    {
        private readonly int boardId;
        private string name;
        private string owner;
        private HashSet<string> joinedUsers;

        private int nextTaskId;
        private Dictionary<int, TaskBL> tasks;
        private int maxTasks0;
        private int maxTasks1;
        private int maxTasks2;
        private int numTasks0;
        private int numTasks1;
        private int numTasks2;

        public BoardBL(int boardId, string name, string owner)
        {
            this.boardId = boardId;
            this.name = name;
            this.owner = owner;
            this.joinedUsers = new HashSet<string>();

            this.nextTaskId = 0;
            this.tasks = new Dictionary<int, TaskBL>();
            this.maxTasks0 = -1;
            this.maxTasks1 = -1;
            this.maxTasks2 = -1;
        }

        public void AddTask(string title, DateTime dueDate, string description)
        {
            tasks.Add(nextTaskId, new TaskBL(nextTaskId, title, dueDate, description));
            nextTaskId++;
        }

        public void JoinUser(string email)
        {
            joinedUsers.Add(email);
        }

        public void UnjoinUser(string email)
        {
            joinedUsers.Remove(email);
        }

        public bool IsUserInBoard(string email)
        {
            return joinedUsers.Contains(email);
        }

        public void EditTask(int taskId, string title, DateTime? dueDate, string description)
        {
            if (title != null)
                tasks[taskId].Title = title;
            if (dueDate != null)
                tasks[taskId].DueDate = dueDate;
            if (description != null)
                tasks[taskId].Description = description;
        }

        public void MoveTask(int taskId, int dest)
        {
            tasks[taskId].State = dest;
        }

        public TaskBL GetTask(int taskId)
        {
            return tasks[taskId];
        }

        public List<TaskBL> GetTasksOfColumn(int column)
        {
            List<TaskBL> l = new List<TaskBL>();
            foreach (int k in tasks.Keys)
            {
                if (tasks[k].State == column)
                    l.Add(tasks[k]);
            }
            return l;
        }

        public int GetColumnLimit(int columnOrdinal)
        {
            if (columnOrdinal == 0) return MaxTasks0;
            if (columnOrdinal == 1) return MaxTasks1;
            return MaxTasks2;
        }

        public List<TaskBL> GetAllTasks()
        {
            return tasks.Values.ToList();
        }

        public void LimitTasks(int col, int newLimit)
        {
            if (col == 0)
            {
                this.maxTasks0 = newLimit;
            }
            else if (col == 1)
            {
                this.maxTasks1 = newLimit;
            }
            else if (col == 2)
            {
                this.maxTasks2 = newLimit;
            }
        }

        public string Owner
        {
            get { return owner; }
            set { this.owner = value; }
        }

        public int NextTaskId
        {
            get { return nextTaskId; }
        }

        public int BoardId
        {
            get { return boardId; }
        }

        public int MaxTasks0
            { get { return maxTasks0; } }
        public int MaxTasks1
            { get { return maxTasks1; } }
        public int MaxTasks2
            { get { return maxTasks2; } }
        public int NumTasks0
        { 
            get { return numTasks0; }
            set { this.numTasks0 = value; }
        }

        public int NumTasks1
        {
            get { return numTasks1; }
            set { this.numTasks1 = value; }
        }

        public int NumTasks2
        {
            get { return numTasks2; }
            set { this.numTasks2 = value; }
        }
        public Dictionary<int, TaskBL> Tasks
            { get { return tasks; } }
        public string Name
            { get { return name; }}
    }
}
