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

            tests = TestCreateBoardPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestCreateBoardPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestCreateBoardPositiveCase1: Failed");
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

            tests = TestCreateBoardNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestCreateBoardNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestCreateBoardNegativeCase1: Failed");
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
            tests = TestDeleteBoardPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestDeleteBoardPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestDeleteBoardPositiveCase1: Failed");
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

            tests = TestDeleteBoardNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestDeleteBoardNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestDeleteBoardNegativeCase1: Failed");
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

            tests = TestGetBoardNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestGetBoardNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestGetBoardNegativeCase1: Failed");
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

            tests = TestGetBoardPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestGetBoardPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestGetBoardPositiveCase1: Failed");
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

            tests = TestLimitTasksNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestLimitTasksNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestLimitTasksNegativeCase1: Failed");
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

            tests = TestLimitTasksPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestLimitTasksPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestLimitTasksPositiveCase1: Failed");
            }

        }

        public void CreatingUser()
        {
            us.Register("yaronet@post.bgu.ac.il", "Admin1");
            us.Register("Shauli@gmail.com", "Haparlament1");
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

        public bool TestCreateBoardPositiveCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.CreateBoard("Shauli@gmail.com", "name1"));
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

        public bool TestCreateBoardNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.CreateBoard("hauli@gmail.com", "name2"));
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

        public bool TestDeleteBoardPositiveCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.DeleteBoard("Shauli@gmail.com", "name1"));
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

        public bool TestDeleteBoardNegativeCase1()
        {
            b.CreateBoard("Shauli@gmail.com", "name1");
            Response res = JsonSerializer.Deserialize<Response>(b.DeleteBoard("Shauli@gmail.com", "name"));
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

        public bool TestGetBoardNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.GetBoard("ya", "name"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        public bool TestGetBoardPositiveCase()
        {
            b.CreateBoard("yaronet@post.bgu.ac.il", "name");
            Response res = JsonSerializer.Deserialize<Response>(b.GetBoard("yaronet@post.bgu.ac.il", "name"));

            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        public bool TestGetBoardPositiveCase1()
        {
            b.CreateBoard("yaronet@post.bgu.ac.il", "name50");
            Response res = JsonSerializer.Deserialize < Response > (b.GetBoard("yaronet@post.bgu.ac.il", "name50"));

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

        public bool TestLimitTasksNegativeCase1()
        {

            Response res = JsonSerializer.Deserialize<Response>(b.LimitTasks("yaronet@post.bgu.ac.il", "name", 3, 0));

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

        public bool TestLimitTasksPositiveCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.LimitTasks("yaronet@post.bgu.ac.il", "name", 2, 100));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }
    }
}
