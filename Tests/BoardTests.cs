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

            // Test: Creating a board successfully (Requirement 8)
            bool tests = TestCreateBoardPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestCreateBoardPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestCreateBoardPositiveCase: Failed");
            }

            // Test: Creating a board with a different user (Requirement 8)
            tests = TestCreateBoardPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestCreateBoardPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestCreateBoardPositiveCase1: Failed");
            }

            // Test: Creating a board with the same name for the same user (Requirement 10)
            tests = TestCreateBoardNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestCreateBoardNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestCreateBoardNegativeCase: Failed");
            }

            // Test: Creating a board with an invalid user (Requirement 8)
            tests = TestCreateBoardNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestCreateBoardNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestCreateBoardNegativeCase1: Failed");
            }

            // Test: Deleting a board successfully (Requirement 8)
            tests = TestDeleteBoardPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestDeleteBoardPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestDeleteBoardPositiveCase: Failed");
            }

            // Test: Deleting a board with a different user (Requirement 8)
            tests = TestDeleteBoardPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestDeleteBoardPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestDeleteBoardPositiveCase1: Failed");
            }

            // Test: Deleting a non-existent board (Requirement 8)
            tests = TestDeleteBoardNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestDeleteBoardNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestDeleteBoardNegativeCase: Failed");
            }

            // Test: Deleting a board with mismatched names (Requirement 8)
            tests = TestDeleteBoardNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestDeleteBoardNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestDeleteBoardNegativeCase1: Failed");
            }

            // Test: Retrieving a non-existent board (Requirement 8)
            tests = TestGetBoardNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestGetBoardNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetBoardNegativeCase: Failed");
            }

            // Test: Retrieving a board with invalid user (Requirement 8)
            tests = TestGetBoardNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestGetBoardNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestGetBoardNegativeCase1: Failed");
            }

            // Test: Retrieving an existing board (Requirement 8)
            tests = TestGetBoardPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestGetBoardPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetBoardPositiveCase: Failed");
            }

            // Test: Retrieving another existing board (Requirement 8)
            tests = TestGetBoardPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestGetBoardPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestGetBoardPositiveCase1: Failed");
            }

            // Test: Limiting tasks in a column to zero and adding a task (Requirement 11)
            tests = TestLimitTasksNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestLimitTasksNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestLimitTasksNegativeCase: Failed");
            }

            // Test: Setting an invalid task limit (Requirement 11)
            tests = TestLimitTasksNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestLimitTasksNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestLimitTasksNegativeCase1: Failed");
            }

            // Test: Setting a valid task limit and adding a task (Requirement 11)
            tests = TestLimitTasksPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestLimitTasksPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestLimitTasksPositiveCase: Failed");
            }

            // Test: Setting a high task limit (Requirement 11)
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
            // This method registers two users for testing purposes
            us.Register("yaronet@post.bgu.ac.il", "Admin1");
            us.Register("Shauli@gmail.com", "Haparlament1");
        }

        public bool TestCreateBoardPositiveCase()
        {
            // This test checks if a board can be created successfully (Requirement 8)
            Response res = JsonSerializer.Deserialize<Response>(b.CreateBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        public bool TestCreateBoardPositiveCase1()
        {
            // This test checks if a board can be created successfully by a different user (Requirement 8)
            Response res = JsonSerializer.Deserialize<Response>(b.CreateBoard("Shauli@gmail.com", "name1"));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        public bool TestCreateBoardNegativeCase()
        {
            // This test checks if creating a board with the same name for the same user fails (Requirement 10)
            Response res = JsonSerializer.Deserialize<Response>(b.CreateBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        public bool TestCreateBoardNegativeCase1()
        {
            // This test checks if creating a board with an invalid user fails (Requirement 8)
            Response res = JsonSerializer.Deserialize<Response>(b.CreateBoard("hauli@gmail.com", "name2"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        public bool TestDeleteBoardPositiveCase()
        {
            // This test checks if a board can be deleted successfully (Requirement 8)
            Response res = JsonSerializer.Deserialize<Response>(b.DeleteBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        public bool TestDeleteBoardPositiveCase1()
        {
            // This test checks if a board can be deleted successfully by a different user (Requirement 8)
            Response res = JsonSerializer.Deserialize<Response>(b.DeleteBoard("Shauli@gmail.com", "name1"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        public bool TestDeleteBoardNegativeCase()
        {
            // This test checks if deleting a non-existent board fails (Requirement 8)
            Response res = JsonSerializer.Deserialize<Response>(b.DeleteBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        public bool TestDeleteBoardNegativeCase1()
        {
            // This test checks if deleting a board with mismatched names fails (Requirement 8)
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
            // This test checks if retrieving a non-existent board fails (Requirement 8)
            Response res = JsonSerializer.Deserialize<Response>(b.GetBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        public bool TestGetBoardNegativeCase1()
        {
            // This test checks if retrieving a board with an invalid user fails (Requirement 8)
            Response res = JsonSerializer.Deserialize<Response>(b.GetBoard("ya", "name"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        public bool TestGetBoardPositiveCase()
        {
            // This test checks if an existing board can be retrieved successfully (Requirement 8)
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
            // This test checks if another existing board can be retrieved successfully (Requirement 8)
            b.CreateBoard("yaronet@post.bgu.ac.il", "name50");
            Response res = JsonSerializer.Deserialize<Response>(b.GetBoard("yaronet@post.bgu.ac.il", "name50"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        public bool TestLimitTasksNegativeCase()
        {
            // This test checks if limiting tasks in a column to zero and adding a task fails (Requirement 11)
            b.LimitTasks("yaronet@post.bgu.ac.il", "name", 0, 0);
            Response res = JsonSerializer.Deserialize<Response>(t.AddTask("yaronet@post.bgu.ac.il", "name", "task1", new DateTime(2026, 4, 10), "test limis tasks"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        public bool TestLimitTasksNegativeCase1()
        {
            // This test checks if setting an invalid task limit fails (Requirement 11)
            Response res = JsonSerializer.Deserialize<Response>(b.LimitTasks("yaronet@post.bgu.ac.il", "name", 3, 0));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        public bool TestLimitTasksPositiveCase()
        {
            // This test checks if setting a valid task limit and adding a task succeeds (Requirement 11)
            b.LimitTasks("yaronet@post.bgu.ac.il", "name", 0, 10);
            Response res = JsonSerializer.Deserialize<Response>(t.AddTask("yaronet@post.bgu.ac.il", "name", "task1", new DateTime(2026, 4, 10), "test limis tasks"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        public bool TestLimitTasksPositiveCase1()
        {
            // This test checks if setting a high task limit succeeds (Requirement 11)
            Response res = JsonSerializer.Deserialize<Response>(b.LimitTasks("yaronet@post.bgu.ac.il", "name", 2, 100));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }
    }
}
