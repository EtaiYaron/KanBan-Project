using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Board;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardService
    {
        private BoardFacade boardFacade;

        public BoardService(BoardFacade boardFacade)
        {
            this.boardFacade = boardFacade;
        }

        public Response CreateBoard(string name)
        {
            throw new NotImplementedException();
        }

        public Response DeleteBoard(string name)
        {
            throw new NotImplementedException();
        }

        public Response GetBoard(string name)
        {
            throw new NotImplementedException();
        }

        public Response LimitTasks(string name, int newLimit)
        {
            throw new NotImplementedException();
        }

        public Response GetId(string name)
        {
            throw new NotImplementedException();
        }
    }
}
