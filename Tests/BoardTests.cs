using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class BoardTests
    {
        private readonly BoardService b = new BoardService();
        public static void RunTests()
        {
            // Add your test cases here
            Console.WriteLine("Running Tests...");
            bool tests = TestDeleteBoardNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestDeleteBoardNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestDeleteBoardNegativeCase: Failed");
            }

            bool tests = TestDeleteBoardPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestDeleteBoardPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestDeleteBoardPositiveCase: Failed");
            }

            bool tests = TestCreateBoardNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestCreateBoardNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestCreateBoardNegativeCase: Failed");
            }

            bool tests = TestCreateBoardPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestCreateBoardPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestCreateBoardPositiveCase: Failed");
            }

            bool tests = TestGetBoardNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestGetBoardNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetBoardNegativeCase: Failed");
            }

            bool tests = TestGetBoardPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestGetBoardPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetBoardPositiveCase: Failed");
            }

            bool tests = TestLimitTasksNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestLimitTasksNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestLimitTasksNegativeCase: Failed");
            }

            bool tests = TestLimitTasksPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestLimitTasksPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestLimitTasksPositiveCase: Failed");
            }
            
        }

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
            Response res = b.createBoard("name");
            if (res.ErrorMsg == null)
            {
                return true;
            }
            return false;
        }

        public bool TestGetBoardNegativeCase()
        {
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
