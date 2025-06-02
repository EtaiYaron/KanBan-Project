using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
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

        BoardDAL boardDAL;
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
            boardDAL = new BoardDAL(boardId, name, owner, columns[0].MaxTasks, columns[1].MaxTasks, columns[2].MaxTasks);

        }

        public BoardBL(BoardDAL boardDAL)
        {
            this.boardId = boardDAL.BoardId;
            this.name = boardDAL.BoardName;
            this.owner = boardDAL.OwnerEmail;
            this.joinedUsers = new HashSet<string>();

            this.nextTaskId = boardDAL.NextTaskId;
            columns = new ColumnBL[NumOfColumns];
            columns[0] = new ColumnBL(0, columnNames[0], boardDAL.Backlog);
            columns[1] = new ColumnBL(1, columnNames[1], boardDAL.InProgress);
            columns[2] = new ColumnBL(2, columnNames[2], boardDAL.Done);
            this.boardDAL = boardDAL;

        }

        /// <summary>
        /// Adds a new task to the board's backlog column.
        /// </summary>
        /// <param name="title">The title of the task.</param>
        /// <param name="dueDate">The due date of the task.</param>
        /// <param name="description">The description of the task.</param>
        public void AddTask(string title, DateTime dueDate, string description)
        {
            columns[0].AddTask(new TaskBL(nextTaskId, boardId, title, dueDate, description));
            nextTaskId++;
        }

        /// <summary>
        /// Adds a user to the set of users who have joined the board.
        /// </summary>
        /// <param name="email">The email of the user to add.</param>
        public void JoinUser(string email)
        {
            joinedUsers.Add(email);
        }

        /// <summary>
        /// Removes a user from the board and unassigns them from any tasks.
        /// </summary>
        /// <param name="email">The email of the user to remove.</param>
        public void LeaveUser(string email)
        {
            joinedUsers.Remove(email);
            for (int i = 0; i < NumOfColumns; i++)
            {
                foreach (var task in columns[i].Tasks.Values)
                {
                    if (task.Assignee == email)
                    {
                        task.Assignee = null;
                        task.TaskDAL.AssignTaskToUser(null);  
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a user is a member of the board.
        /// </summary>
        /// <param name="email">The email of the user to check.</param>
        /// <returns>True if the user is in the board; otherwise, false.</returns>
        public bool IsUserInBoard(string email)
        {
            return joinedUsers.Contains(email);
        }


        /// <summary>
        /// Edits the details of an existing task.
        /// </summary>
        /// <param name="taskId">The ID of the task to edit.</param>
        /// <param name="title">The new title, or null to leave unchanged.</param>
        /// <param name="dueDate">The new due date, or null to leave unchanged.</param>
        /// <param name="description">The new description, or null to leave unchanged.</param>
        public void EditTask(int taskId, string title, DateTime? dueDate, string description)
        {
            TaskBL task = GetTask(taskId);
            if (title != null)
            {
                task.Title = title;
            }
            if (dueDate != null)
            {
                task.DueDate = dueDate;
            }
            if (description != null)
            {
                task.Description = description;
            }
            task.TaskDAL.EditTask(title, dueDate, description);
        }

        /// <summary>
        /// Moves a task to a different column.
        /// </summary>
        /// <param name="task">The task to move.</param>
        /// <param name="dest">The destination column index.</param>
        public void MoveTask(TaskBL task, int dest)
        {
            columns[dest - 1].RemoveTask(task.TaskId);
            columns[dest].AddTask(task);
        }

        /// <summary>
        /// Gets all tasks in the specified column.
        /// </summary>
        /// <param name="column">The column index.</param>
        /// <returns>A dictionary of task IDs to TaskBL objects.</returns>
        public Dictionary<int, TaskBL> GetTasksOfColumn(int column)
        {
            return columns[column].Tasks;
        }

        /// <summary>
        /// Gets the column object by its index.
        /// </summary>
        /// <param name="columnOrdinal">The column index.</param>
        /// <returns>The ColumnBL object.</returns>
        public ColumnBL GetColumn(int columnOrdinal)
        {
            return columns[columnOrdinal];
        }

        /// <summary>
        /// Finds the column containing the specified task.
        /// </summary>
        /// <param name="task">The task to search for.</param>
        /// <returns>The column containing the task, or null if not found.</returns>
        public ColumnBL GetTaskOrdinal(TaskBL task)
        {
            foreach (var column in columns)
            {
                if (column.Tasks.ContainsKey(task.TaskId))
                {
                    return column;
                }
            }
            return null;
        }

        /// <summary>
        /// Sets the maximum number of tasks allowed in a column.
        /// </summary>
        /// <param name="columnOrdinal">The column index.</param>
        /// <param name="newLimit">The new task limit.</param>
        public void setColumnLimit(int columnOrdinal, int newLimit)
        {
            this.columns[columnOrdinal].MaxTasks = newLimit;
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
        /// Retrieves a task by its ID from any column in the board.
        /// </summary>
        /// <param name="taskId">The ID of the task to retrieve.</param>
        /// <returns>The TaskBL object if found; otherwise, null.</returns>
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

        public BoardDAL BoardDAL
        {
            get { return boardDAL; }
        }

        public HashSet<string> JoinedUsers
        {
            get { return joinedUsers; }
        }
    }
}
