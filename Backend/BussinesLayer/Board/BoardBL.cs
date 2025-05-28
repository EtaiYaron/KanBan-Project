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

        /// <summary>
        /// This method is used to add a task to the board.
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="title"></param>
        /// <param name="dueDate"></param>
        /// <param name="description"></param>
        public void AddTask(string title, DateTime dueDate, string description)
        {
            columns[0].AddTask(new TaskBL(nextTaskId, boardId, title, dueDate, description));
            nextTaskId++;
        }

        /// <summary>
        /// Adds a user to the board's joined users set.
        /// </summary>
        /// <param name="email">The email of the user to join the board.</param>
        public void JoinUser(string email)
        {
            joinedUsers.Add(email);
        }

        /// <summary>
        /// Removes a user from the board's joined users set and unassigns them from any tasks they were assigned to.
        /// </summary>
        /// <param name="email">The email of the user to leave the board.</param>
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
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a user is currently joined to the board.
        /// </summary>
        /// <param name="email">The email of the user to check.</param>
        /// <returns>True if the user is in the board; otherwise, false.</returns>
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
        /// This method is used to move a task to a different column.
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="dest"></param>
        public void MoveTask(TaskBL task, int dest)
        {
            columns[dest - 1].RemoveTask(task.TaskId);
            columns[dest].AddTask(task);
        }
        
        public Dictionary<int, TaskBL> GetTasksOfColumn(int column)
        {
            return columns[column].Tasks;
        }

        public ColumnBL GetColumn(int columnOrdinal)
        {
            return columns[columnOrdinal];
        }
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
