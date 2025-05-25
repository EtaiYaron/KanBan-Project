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
        private ColumnBL[] columns;
        private const int NumOfColumns = 3;
        private readonly string[] columnNames = { "Backlog", "In Progress", "Done" };

        public BoardBL(int boardId, string name, string owner)
        {
            this.boardId = boardId;
            this.name = name;
            this.owner = owner;
            this.joinedUsers = new HashSet<string>();

            this.nextTaskId = 0;
            columns = new ColumnBL[NumOfColumns];
            for (int i = 0; i < NumOfColumns; i++)
                columns[i] = new ColumnBL(i, columnNames[i]);
            
        }

        /// <summary>
        /// This method is used to add a task to the board.
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="title"></param>
        /// <param name="dueDate"></param>
        /// <param name="description"></param>
        public void AddTask(string title, DateTime dueDate, string description)
        {
            columns[0].AddTask(new TaskBL(nextTaskId, title, dueDate, description));
            nextTaskId++;
        }

        public void JoinUser(string email)
        {
            joinedUsers.Add(email);
        }

        public void LeaveUser(string email)
        {
            joinedUsers.Remove(email);
        }

        public bool IsUserInBoard(string email)
        {
            return joinedUsers.Contains(email);
        }


        /// <summary>
        /// This method is used to edit an existing task in the board.
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="title"></param>
        /// <param name="dueDate"></param>
        /// <param name="description"></param>
        public void EditTask(int taskId, string title, DateTime? dueDate, string description)
        {
            TaskBL task = GetTask(taskId);
            if (title != null)
                task.Title = title;
            if (dueDate != null)
                task.DueDate = dueDate;
            if (description != null)
                task.Description = description;
        }

        /// <summary>
        /// This method is used to move a task to a different column.
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="dest"></param>
        public void MoveTask(TaskBL task, int dest)
        {
            columns[task.State].RemoveTask(task.TaskId);
            columns[dest].AddTask(task);
        }
        

        /// <summary>
        /// This method is used to retrieve all tasks in the specified column.
        /// </summary>
        /// <param name="column">The column index (0, 1, or 2)</param>
        /// <returns>A list of TaskBL objects in the given column.</returns>
        public Dictionary<int, TaskBL> GetTasksOfColumn(int column)
        {
            return columns[column].Tasks;
        }

        public ColumnBL GetColumn(int columnOrdinal)
        {
            return columns[columnOrdinal];
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

        /// <summary>
        /// This method is used to retrieve a task by its ID.
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public TaskBL GetTask(int taskId)
        {

            foreach (var column in columns)
            {
                if (column.Tasks.ContainsKey(taskId))
                {
                    return column.Tasks[taskId];
                }
            }
            return null;
        }

        public string Name{ get { return name; }}
    }
}
