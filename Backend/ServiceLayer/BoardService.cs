using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Board;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    class BoardService
    {
        private BoardFacade boardFacade;

        public BoardService(BoardFacade boardFacade)
        {
            this.boardFacade = boardFacade;
        }

        public string CreateBoard(string name)
        {
            throw new NotImplementedException();
        }

        public string DeleteBoard(string name)
        {
            throw new NotImplementedException();
        }

        public string GetBoard(string name)
        {
            throw new NotImplementedException();
        }

        public string LimitTasks(string name, int newLimit)
        {
            throw new NotImplementedException();
        }
    }
}
