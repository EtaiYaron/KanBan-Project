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

        public BoardBL CreateBoard(string boardname, int maxTasks = -1)
        {
            if (!authenticationFacade.isLoggedIn(currentUserEmail))
            {
                throw new Exception("User is not logged in");
            }
            if (string.IsNullOrEmpty(boardname))
            {
                throw new ArgumentNullException("boardname");
            }
            if (boards.ContainsKey(currentUserEmail) && !boards[currentUserEmail].ContainsKey(boardname))
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
            throw new NotImplementedException();
        }

        public BoardBL MoveTask(string boardname, int taskId, int dest)
        { 
            throw new NotImplementedException(); 
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
            throw new NotImplementedException();
        }

        public TaskBL GetTask(string boardname, int taskId)
        {
            throw new NotImplementedException();
        }

        public List<TaskBL> GetAllTasks(string boardname)
        {
            throw new NotImplementedException();
        }

        public BoardBL LimitTasks(string boardname, int newLimit)
        {
            throw new NotImplementedException();
        }
    }
}
