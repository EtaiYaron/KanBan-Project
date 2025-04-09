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
            bool tests = TestCreateBoardPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestCreateBoardPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestCreateBoardPositiveCase: Failed");
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

            bool tests = TestDeleteBoardPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestDeleteBoardPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestDeleteBoardPositiveCase: Failed");
            }

            bool tests = TestDeleteBoardNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestDeleteBoardNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestDeleteBoardNegativeCase: Failed");
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


        public bool TestCreateBoardPositiveCase()
        {
            Response res = b.CreateBoard("name");
            if (res.ErrorMsg == null)
            {
                return true;
            }
            return false;
        }

        public bool TestCreateBoardNegativeCase()
        {
            Response res = b.CreateBoard("name");
            if (res.ErrorMsg == null)
            {
                return false;
            }
            return true;
        }


        public bool TestDeleteBoardPositiveCase()
        {
            Response res = b.DeleteBoard("name");
            if (res.ErrorMsg != null)
            {
                return false;
            }
            return true;
        }

        public bool TestDeleteBoardNegativeCase()
        {
            Response res = b.DeleteBoard("name");
            if(res.ErrorMsg == null)
            {
                return false;
            }
            return true;

        }

        

        public bool TestGetBoardNegativeCase()
        {
            Response res = b.GetBoard("name");
            if (res.ErrorMsg != null)
            {
                return true;
            }
            return false;
        }

        public bool TestGetBoardPositiveCase()
        {
            b.CreateBoard("name");
            Response res = b.GetBoard("name");

            if (res.ErrorMsg != null)
            {
                return false;
            }
            return true;
        }

        public bool TestLimitTasksNegativeCase()
        {
            b.LimitTasks("name", 0);

            Response res = b.AddTask(1, 09/04/2025, "task1", "test limis tasks");

            if (res.ErrorMsg == null)
            {
                return false;
            }
            return true;
        }

        public bool TestLimitTasksPositiveCase()
        {
            b.LimitTasks("name", 0);

            Response res = b.AddTask(1, 09 / 04 / 2025, "task1", "test limis tasks");

            if (res.ErrorMsg != null)
            {
                return false;
            }
            return true;
        }
    }
}
