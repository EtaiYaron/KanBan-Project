using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class BoardTests
    {
        public bool TestDeleteBoardNegativeCase()
        {
            BoardService b = new BoardService();
            Response res = b.deleteBoard("name");
            if(res.ErrorMsg == null)
            {
                return false;
            }
            return true;

        }

        public bool TestDeleteBoardPositiveCase()
        {
            BoardService b = new BoardService();
            b.createBoard("name");
            Response res = b.deleteBoard("name");
            if (res.ErrorMsg != null)
            {
                return false;
            }
            return true;
        }


        public bool TestCreateBoardNegativeCase()
        {
            BoardService b = new BoardService();
            b.createBoard("name");
            Response res = b.createBoard("name");
            if (res.ErrorMsg == null)
            {
                return false;
            }
            return true;
        }

        public bool TestCreateBoardPositiveCase()
        {
            BoardService b = new BoardService();
            b.createBoard("name");
            Response res = b.getBoard();
            if (res.ErrorMsg == null)
            {
                return false;
            }
            return true;
        }

        public bool TestGetBoardNegativeCase()
        {
            BoardService b = new BoardService();
            b.createBoard("name");
            Response res = b.getBoard();

            if (res.ErrorMsg == null)
            {
                return false;
            }
            return true;
        }

        public bool TestGetBoardPositiveCase()
        {
            BoardService b = new BoardService();
            b.createBoard("name");
            Response res = b.getBoard();

            if (res.ErrorMsg != null)
            {
                return false;
            }
            return true;
        }

        public bool TestLimitTasksNegativeCase()
        {
            BoardService b = new BoardService();
            b.limitTasks("name", 0);

            Response res = b.createBoard("name");

            if (res.ErrorMsg == null)
            {
                return false;
            }
            return true;
        }

        public bool TestLimitTasksPositiveCase()
        {
            BoardService b = new BoardService();
            b.limitTasks("name", 0);

            Response res = b.createBoard("name");

            if (res.ErrorMsg != null)
            {
                return false;
            }
            return true;
        }
    }
}
