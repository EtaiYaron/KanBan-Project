using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Board;


namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class TaskService
    {
        private BoardFacade boardFacade;

        public TaskService(BoardFacade boardFacade)
        {
            this.boardFacade = boardFacade;
        }

        public Response AddTask(string boardName, int taskId, string title, DateTime dueTime, string description)
        {
            throw new NotImplementedException();
        }

        public Response EditTask(string boardName, int taskId, string title, DateTime dueTime, string description)
        {
            throw new NotImplementedException();
        }

        public Response MoveTask(string boardName, int taskId, int dest)
        {
            throw new NotImplementedException();
        }

        public Response GetTask(string boardName, int taskId)
        {
            throw new NotImplementedException();
        }

        public Response GetAllTasks(string boardName)
        {
            throw new NotImplementedException();
        }
    }
}
