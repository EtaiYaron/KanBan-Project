using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;

namespace IntroSE.Kanban.Backend.BussinesLayer.Board
{
    internal class BoardFacade
    {
        private readonly Dictionary<string, Dictionary<string, BoardBL>> boards;
        private AuthenticationFacade authenticationFacade;
        private readonly string currentUserEmail;

        public BoardFacade(AuthenticationFacade authenticationFacade)
        {
            this.boards = new Dictionary<string, Dictionary<string, BoardBL>>();
            this.authenticationFacade = authenticationFacade;
        }

        public BoardBL CreateBoard(string boardname)
        {
            if (!authenticationFacade.isLoggedIn(currentUserEmail))
            {
                throw new Exception("User is not logged in");
            }
            if (string.IsNullOrEmpty(boardname))
            {
                throw new ArgumentNullException("boardname isn't valid");
            }
            if (boards.ContainsKey(currentUserEmail) && boards[currentUserEmail].ContainsKey(boardname))
            {
                throw new ArgumentNullException("boardname already exist under the same user");
            }
            if (!boards.ContainsKey(currentUserEmail))
            {
                boards.Add(currentUserEmail, new Dictionary<string, BoardBL>());
            }
            BoardBL curr = new BoardBL(boardname);
            boards[currentUserEmail].Add(boardname, curr);
            return curr;
        }

        public BoardBL DeleteBoard(string boardname)
        {
            if (!authenticationFacade.isLoggedIn(currentUserEmail))
            {
                throw new Exception("User is not logged in");
            }
            if (string.IsNullOrEmpty(boardname))
            {
                throw new ArgumentNullException("boardname isn't valid");
            }
            if (!boards.ContainsKey(currentUserEmail) || !boards[currentUserEmail].ContainsKey(boardname))
            {
                throw new ArgumentNullException("boardname doesn't exist under the same user");
            }
            BoardBL curr = boards[currentUserEmail][boardname];
            boards[currentUserEmail].Remove(boardname);
            return curr;
        }

        public BoardBL MoveTask(string boardname, int taskId, int destcolumn)
        {
            if (!authenticationFacade.isLoggedIn(currentUserEmail))
            {
                throw new Exception("User is not logged in");
            }
            if (string.IsNullOrEmpty(boardname))
            {
                throw new ArgumentNullException("boardname isn't valid");
            }
            if (!boards.ContainsKey(currentUserEmail) || !boards[currentUserEmail].ContainsKey(boardname))
            {
                throw new ArgumentNullException("boardname doesn't exist under the same user");
            }
            BoardBL board = boards[currentUserEmail][boardname];
            if (!(board.Tasks).Contains(taskId))
            {
                throw new ArgumentException("taskId doesn't exist under this board");
            }
            if (destcolumn < 0 || destcolumn > 2)
            {
                throw new ArgumentOutOfRangeException("dest must be between 0 and 2");
            }
            int fromcolumn = board.GetTask(taskId).State;
            if (destcolumn - fromcolumn != 1)
            {
                throw new ArgumentException("cannot move the task to this destination");
            }
            board.MoveTask(taskId, destcolumn);
            return board;
        }

        public BoardBL AddTask(string boardname, int taskId, string title, DateTime dueTime, string description)
        {
            throw new NotImplementedException();
        }

        public TaskBL EditTask(string boardname, int taskId, string title, DateTime dueTime, string description)
        {
            throw new NotImplementedException();
        }

        public BoardBL GetBoard(string boardname)
        {
            if (!authenticationFacade.isLoggedIn(currentUserEmail))
            {
                throw new Exception("User is not logged in");
            }
            if (string.IsNullOrEmpty(boardname))
            {
                throw new ArgumentNullException("boardname");
            }
            if (!boards.ContainsKey(currentUserEmail) || !boards[currentUserEmail].ContainsKey(boardname))
            {
                throw new ArgumentNullException("boardname doesn't exist under the same user");
            }
            return boards[currentUserEmail][boardname];
        }

        public TaskBL GetTask(string boardname, int taskId)
        {
            if (!authenticationFacade.isLoggedIn(currentUserEmail))
            {
                throw new Exception("User is not logged in");
            }
            if (string.IsNullOrEmpty(boardname))
            {
                throw new ArgumentNullException("boardname isn't valid");
            }
            if (!boards.ContainsKey(currentUserEmail) || !boards[currentUserEmail].ContainsKey(boardname))
            {
                throw new ArgumentNullException("boardname doesn't exist under the same user");
            }
            BoardBL board = boards[currentUserEmail][boardname];
            if (!(board.Tasks).Contains(taskId))
            {
                throw new ArgumentException("taskId doesn't exist under this board");
            }
            return board.GetTask(taskId);
        }

        public Dictionary<int, TaskBL> GetAllTasks(string boardname)
        {
            if (!authenticationFacade.isLoggedIn(currentUserEmail))
            {
                throw new Exception("User is not logged in");
            }
            if (string.IsNullOrEmpty(boardname))
            {
                throw new ArgumentNullException("boardname isn't valid");
            }
            if (!boards.ContainsKey(currentUserEmail) || !boards[currentUserEmail].ContainsKey(boardname))
            {
                throw new ArgumentNullException("boardname doesn't exist under the same user");
            }
            BoardBL board = boards[currentUserEmail][boardname];
            return board.Tasks;
        }

        public BoardBL LimitTasks(string boardname, int column, int newLimit)
        {
            throw new NotImplementedException();
        }
    }
}
