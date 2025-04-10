using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Board;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardService
    {
        private BoardFacade boardFacade;
        /// <summary>
        /// Empty Constructor for the BoardService class just for now.
        /// </summary>
        public BoardService()
        {
            this.boardFacade = new BoardFacade(new AuthenticationFacade());
        }
        internal BoardService(BoardFacade boardFacade)
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
