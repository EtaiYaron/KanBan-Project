using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;

namespace IntroSE.Kanban.Backend.BussinesLayer.Board
{
    class BoardFacade
    {
        private Dictionary<string, Dictionary<string, BoardBL>> boards;
        private AuthenticationFacade authenticationFacade;

        public BoardFacade(AuthenticationFacade authenticationFacade)
        {
            this.boards = new Dictionary<string, Dictionary<string, BoardBL>>();
            this.authenticationFacade = authenticationFacade;
        }

        public BoardBL CreateBoard(string name, int maxTasks = -1)
        {
            throw new NotImplementedException();
        }

        public BoardBL DeleteBoard(string name)
        {
            throw new NotImplementedException();
        }

        public BoardBL MoveTask(string name, int taskId, int dest)
        { 
            throw new NotImplementedException(); 
        }

        public BoardBL AddTask(string name, int taskId, string title, DateTime dueTime, string description)
        {
            throw new NotImplementedException();
        }

        public TaskBL EditTask(string name, int taskId, string title, DateTime dueTime, string description)
        {
            throw new NotImplementedException();
        }

        public BoardBL GetBoard(string name)
        {
            throw new NotImplementedException();
        }

        public TaskBL GetTask(string name, int taskId)
        {
            throw new NotImplementedException();
        }

        public List<TaskBL> GetAllTasks(string name)
        {
            throw new NotImplementedException();
        }

        public BoardBL LimitTasks(string name, int newLimit)
        {
            throw new NotImplementedException();
        }
    }
}
