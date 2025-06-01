using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class BoardDAL
    {
        private List<BoardsUsersDAL> boardUserDALs;
        private List<TaskDAL> allTasks;
        private string boardName;
        private int boardId;
        private string ownerEmail;
        private int backlog;
        private int inProgress;
        private int done;
        private BoardController boardController;
        private bool isPersistent;
        private int nextTaskId;
        private HashSet<string> joinedUsers;

        public BoardDAL(int boardId, string boardName, string ownerEmail, int maxTasks0, int maxTasks1, int maxTasks2, int nextTaskId=0)
        {
            this.boardId = boardId;
            this.boardName = boardName;
            this.ownerEmail = ownerEmail;
            this.backlog = maxTasks0;
            this.inProgress = maxTasks1;
            this.done = maxTasks2;
            this.nextTaskId = nextTaskId;
            this.boardUserDALs = new List<BoardsUsersDAL>();
            this.allTasks = new List<TaskDAL>();
            this.boardController = new BoardController();
            this.isPersistent = false;
            this.joinedUsers = new HashSet<string>();
        }


        public string BoardName
        {
            get { return boardName; }
            set
            {
                boardController.Update(this, "boardName", value);
                boardName = value;
            }
        }

        public int BoardId
        {
            get { return boardId; }
            set
            {
                boardController.Update(this, "boardId", value.ToString());
                boardId = value;
            }
        }

        public string OwnerEmail
        {
            get { return ownerEmail; }
            set
            {
                boardController.Update(this, "ownerEmail", value);
                ownerEmail = value;
            }
        }

        public int Backlog
        {
            get { return backlog; }
            set
            {
                boardController.Update(this, "backlog", value.ToString());
                backlog = value;
            }
        }

        public int InProgress
        {
            get { return inProgress; }
            set
            {
                boardController.Update(this, "inProgress", value.ToString());
                inProgress = value;
            }
        }

        public int Done
        {
            get { return done; }
            set
            {
                boardController.Update(this, "done", value.ToString());
                done = value;
            }
        }

        public int NextTaskId
        {
            get { return nextTaskId; }
            set
            {
                boardController.Update(this, "nextTaskId", value.ToString());
                nextTaskId = value;
            }
        }

        /// <summary>
        /// This method is used to delete the board from the database and clear all associated data.
        /// </summary>
        public void DeleteBoard()
        {
            boardController.Delete(this);
            isPersistent = false;
            allTasks.Clear();
            boardUserDALs.Clear();
            joinedUsers.Clear();
        }

        /// <summary>
        /// This method is used to join a user to the board.
        /// </summary>
        /// <param name="userEmail"></param>
        public void JoinBoard(string userEmail)
        {
            joinedUsers.Add(userEmail);
            BoardsUsersDAL boardUserDAL = new BoardsUsersDAL(this.boardId, userEmail, userEmail == this.ownerEmail);
            this.boardUserDALs.Add(boardUserDAL);
            boardUserDAL.BoardsUsersController.Insert(boardUserDAL);
        }

        /// <summary>
        /// This method is used to leave a user from the board.
        /// </summary>
        /// <param name="userEmail"></param>
        public void LeaveBoard(string userEmail)
        {
            joinedUsers.Remove(userEmail);
            BoardsUsersDAL boardUserDAL = new BoardsUsersDAL(this.boardId, userEmail, userEmail == this.ownerEmail);
            this.boardUserDALs.Remove(boardUserDAL);
            boardUserDAL.BoardsUsersController.Delete(boardUserDAL);
        }

        /// <summary>
        /// This method is used to add a task to the board.
        /// </summary>
        /// <param name="task"></param>
        public void AddTask(TaskDAL task)
        {
            if (!allTasks.Any(t => t.TaskId == task.TaskId))
            {
                allTasks.Add(task);
                NextTaskId++;
            }
        }

        /// <summary>
        /// This method is used to move a task to the next column in the board.
        /// </summary>
        /// <param name="taskId"></param>
        public void MoveTask(int taskId)
        {
            TaskDAL task = allTasks.FirstOrDefault(t => t.TaskId == taskId);
            task.MoveTask();
        }


        /// <summary>
        /// This method is used to change the owner of the board.
        /// </summary>
        /// <param name="newOwnerEmail"></param>
        /// <exception cref="Exception"></exception>

        public void ChangeOwner(string newOwnerEmail)
        {
            if (joinedUsers.Contains(newOwnerEmail))
            {
                this.OwnerEmail = newOwnerEmail;
                foreach (var boardUser in boardUserDALs)
                {
                    if (boardUser.UserEmail == newOwnerEmail)
                    {
                        boardUser.IsOwner = true;
                    }
                    else
                    {
                        boardUser.IsOwner = false;
                    }
                }
            }
            else
            {
                throw new Exception("New owner must be a member of the board.");
            }
        }


        /// <summary>
        /// This method is used to limit the number of tasks in a specific column of the board.
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="limit"></param>
        public void limitTasksColumn(int columnOrdinal, int limit)
        {
            if (columnOrdinal == 0)
            {
                this.Backlog = limit;
            }
            else if (columnOrdinal == 1)
            {
                this.InProgress = limit;
            }
            else if (columnOrdinal == 2)
            {
                this.Done = limit;
            }
        }

        public void UpdateLastBoardId(int lastBoardId)
        {
            boardController.UpdateLastBoardId(lastBoardId);
        }

        /// <summary>
        /// This method is used to persist the board to the database.
        /// </summary>
        public void persist()
        {
            if (!isPersistent)
            {
                boardController.Insert(this);
                isPersistent = true;
            }
        }


    }
}
