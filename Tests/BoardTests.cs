using IntroSE.Kanban.Backend.BussinesLayer.Board;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;
using IntroSE.Kanban.Backend.BussinesLayer.User;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Text.Json;

namespace Tests
{
    class BoardTests
    {
        private UserService us;
        private BoardService b;
        private TaskService t;

        public BoardTests(UserService us, BoardService b, TaskService t)
        {
            this.us = us;
            this.b = b;
            this.t = t;
        }

        public void BoardRunTests()
        {
            Console.WriteLine("Running Tests...");

            CreatingUser();

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

        public void CreatingUser()
        {
            us.Register("yaronet@post.bgu.ac.il", "Admin1");
        }   
        public bool TestCreateBoardPositiveCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.CreateBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        public bool TestCreateBoardNegativeCase()
        {
            Response res = JsonSerializer.Deserialize < Response > (b.CreateBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }


        public bool TestDeleteBoardPositiveCase()
        {
            Response res = JsonSerializer.Deserialize < Response > (b.DeleteBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        public bool TestDeleteBoardNegativeCase()
        {
            Response res = JsonSerializer.Deserialize < Response > (b.DeleteBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;

        }

        public bool TestGetBoardNegativeCase()
        {
            Response res = JsonSerializer.Deserialize < Response > (b.GetBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        public bool TestGetBoardPositiveCase()
        {
            b.CreateBoard("yaronet@post.bgu.ac.il", "name");
            Response res = JsonSerializer.Deserialize < Response > (b.GetBoard("yaronet@post.bgu.ac.il", "name"));

            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        public bool TestLimitTasksNegativeCase()
        {
            b.LimitTasks("yaronet@post.bgu.ac.il", "name", 0, 0);

            Response res = JsonSerializer.Deserialize < Response > (t.AddTask("yaronet@post.bgu.ac.il", "name", "task1", new DateTime(2026, 4, 10), "test limis tasks"));

            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        public bool TestLimitTasksPositiveCase()
        {
            b.LimitTasks("yaronet@post.bgu.ac.il", "name", 0, 10);

            Response res = JsonSerializer.Deserialize < Response > (t.AddTask("yaronet@post.bgu.ac.il", "name", "task1", new DateTime(2026, 4, 10), "test limis tasks"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }
    }
}
