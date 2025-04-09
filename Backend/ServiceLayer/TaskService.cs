using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Board;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    class TaskService
    {
        private BoardFacade boardFacade;

        public TaskService(BoardFacade boardFacade)
        {
            this.boardFacade = boardFacade;
        }

        public string AddTask(string boardName, int taskId, string title, DateTime dueTime, string description)
        {
            throw new NotImplementedException();
        }

        public string EditTask(string boardName, int taskId, string title, DateTime dueTime, string description)
        {
            throw new NotImplementedException();
        }

        public string MoveTask(string boardName, int taskId, int dest)
        {
            throw new NotImplementedException();
        }

        public string GetTask(string boardName, int taskId)
        {
            throw new NotImplementedException();
        }

        public string GetAllTasks(string boardName)
        {
            throw new NotImplementedException();
        }
    }
}
