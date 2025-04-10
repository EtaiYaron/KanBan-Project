using IntroSE.Kanban.Backend.BussinesLayer.Board;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;
using IntroSE.Kanban.Backend.BussinesLayer.User;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace Tests
{
    class BoardTests
    {
        private BoardService b;
        private TaskService t;

        public BoardTests(BoardService b, TaskService t)
        {
            this.b = b;
            this.t = t;
        }

        public void BoardRunTests()
        {
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

            tests = TestCreateBoardNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestCreateBoardNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestCreateBoardNegativeCase: Failed");
            }

            tests = TestDeleteBoardPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestDeleteBoardPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestDeleteBoardPositiveCase: Failed");
            }

            tests = TestDeleteBoardNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestDeleteBoardNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestDeleteBoardNegativeCase: Failed");
            }

            tests = TestGetBoardNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestGetBoardNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetBoardNegativeCase: Failed");
            }

            tests = TestGetBoardPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestGetBoardPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetBoardPositiveCase: Failed");
            }

            tests = TestLimitTasksNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestLimitTasksNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestLimitTasksNegativeCase: Failed");
            }

            tests = TestLimitTasksPositiveCase();
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
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        public bool TestCreateBoardNegativeCase()
        {
            Response res = b.CreateBoard("name");
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }


        public bool TestDeleteBoardPositiveCase()
        {
            Response res = b.DeleteBoard("name");
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        public bool TestDeleteBoardNegativeCase()
        {
            Response res = b.DeleteBoard("name");
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;

        }



        public bool TestGetBoardNegativeCase()
        {
            Response res = b.GetBoard("name");
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        public bool TestGetBoardPositiveCase()
        {
            b.CreateBoard("name");
            Response res = b.GetBoard("name");

            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        public bool TestLimitTasksNegativeCase()
        {
            b.LimitTasks("name", 0);

            Response res = t.AddTask("name", 1, "task1", new DateTime(2025, 4, 10), "test limis tasks");

            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        public bool TestLimitTasksPositiveCase()
        {
            b.LimitTasks("name", 0);

            Response res = t.AddTask("name", 1, "task1", new DateTime(2025, 4, 10), "test limis tasks");

            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }
    }
}
