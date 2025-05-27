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
        private int maxTasks0;
        private int maxTasks1;
        private int maxTasks2;
        private BoardController boardController;
        private bool isPersistent;
        private int nextTaskId;
        private HashSet<string> joinedUsers;

        public BoardDAL(int boardId, string boardName, string ownerEmail, int maxTasks0, int maxTasks1, int maxTasks2, int nextTaskId=0)
        {
            this.boardId = boardId;
            this.boardName = boardName;
            this.ownerEmail = ownerEmail;
            this.maxTasks0 = maxTasks0;
            this.maxTasks1 = maxTasks1;
            this.maxTasks2 = maxTasks2;
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

        public int MaxTasks0
        {
            get { return maxTasks0; }
            set
            {
                boardController.Update(this, "maxTasks0", value.ToString());
                maxTasks0 = value;
            }
        }

        public int MaxTasks1
        {
            get { return maxTasks1; }
            set
            {
                boardController.Update(this, "maxTasks1", value.ToString());
                maxTasks1 = value;
            }
        }

        public int MaxTasks2
        {
            get { return maxTasks2; }
            set
            {
                boardController.Update(this, "maxTasks2", value.ToString());
                maxTasks2 = value;
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
        /// This method is used to join a user to the board.
        /// </summary>
        /// <param name="userEmail"></param>
        public void JoinBoard(string userEmail)
        {
            joinedUsers.Add(userEmail);
            this.boardUserDALs.Add(new BoardsUsersDAL(this.boardId, userEmail, this.ownerEmail==userEmail));
        }

        /// <summary>
        /// This method is used to leave a user from the board.
        /// </summary>
        /// <param name="userEmail"></param>
        public void LeaveBoard(string userEmail)
        {
            joinedUsers.Remove(userEmail);
            this.boardUserDALs.RemoveAll(bu => bu.UserEmail == userEmail && bu.BoardId == this.boardId);
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
                task.persist(this.boardId);
            }
        }
        
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

        public BoardController BoardController
        {
            get { return boardController; }
        }

    }
}
