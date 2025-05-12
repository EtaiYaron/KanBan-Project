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
        private int cnt;

        public BoardTests(UserService us, BoardService b, TaskService t)
        {
            this.us = us;
            this.b = b;
            this.t = t;
            cnt = 0;
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
            // MileStone 2 Tests:

            tests = TestGetUserBoardsPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestGetUserBoardsPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetUserBoardsPositiveCase: Failed");
            }

            tests = TestGetUserBoardsPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestGetUserBoardsPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestGetUserBoardsPositiveCase1: Failed");
            }

            tests = TestGetUserBoardsNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestGetUserBoardsNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetUserBoardsNegativeCase: Failed");
            }

            tests = TestDeleteBoardNegativeCase2();
            if (tests)
            {
                Console.WriteLine("TestDeleteBoardNegativeCase2: Passed");
            }
            else
            {
                Console.WriteLine("TestDeleteBoardNegativeCase2: Failed");
            }

            tests = TestJoinBoardPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestJoinBoardPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestJoinBoardPositiveCase: Failed");
            }

            tests = TestJoinBoardPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestJoinBoardPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestJoinBoardPositiveCase1: Failed");
            }

            tests = TestJoinBoardNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestJoinBoardNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestJoinBoardNegativeCase: Failed");
            }

            tests = TestJoinBoardNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestJoinBoardNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestJoinBoardNegativeCase1: Failed");
            }

            tests = TestLeaveBoardPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestLeaveBoardPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestLeaveBoardPositiveCase: Failed");
            }

            tests = TestLeaveBoardPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestLeaveBoardPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestLeaveBoardPositiveCase1: Failed");
            }

            tests = TestLeaveBoardNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestLeaveBoardNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestLeaveBoardNegativeCase: Failed");
            }

            tests = TestLeaveBoardNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestLeaveBoardNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestLeaveBoardNegativeCase1: Failed");
            }

            tests = TestChangeOwnerPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestChangeOwnerPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestChangeOwnerPositiveCase: Failed");
            }

            tests = TestChangeOwnerPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestChangeOwnerPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestChangeOwnerPositiveCase1: Failed");
            }

            tests = TestChangeOwnerNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestChangeOwnerNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestChangeOwnerNegativeCase: Failed");
            }

            tests = TestChangeOwnerNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestChangeOwnerNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestChangeOwnerNegativeCase1: Failed");
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
            cnt++;
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
            cnt++;
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
            cnt++;
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
            cnt++;
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

        public bool TestGetUserBoardsPositiveCase()
        {
            b.CreateBoard("yaronet@post.bgu.ac.il", "Milestone2");
            cnt++;
            Response res = JsonSerializer.Deserialize<Response>(b.GetUserBoards("yaronet@post.bgu.ac.il"));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        public bool TestGetUserBoardsPositiveCase1()
        {
            us.Register("Kobe2424@gmail.com" , "Kobe1" );
            b.JoinBoard("Kobe2424@gmail.com", cnt);
            cnt++;
            Response res = JsonSerializer.Deserialize<Response>(b.GetUserBoards("Kobe2424@gmail.com"));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        public bool TestGetUserBoardsNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.GetUserBoards("LebronJames@gmail.com"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        public bool TestDeleteBoardNegativeCase2()
        {
            b.CreateBoard("Shauli@gmail.com", "Mile2");
            cnt++;
            Response res = JsonSerializer.Deserialize<Response>(b.DeleteBoard("yaronet@post.bgu.ac.il", "Mile2"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        public bool TestJoinBoardPositiveCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.JoinBoard("yaronet@post.bgu.ac.il", cnt));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        public bool TestJoinBoardPositiveCase1()
        {
            us.Register("DonaldTrump@gmail.com", "UsaPresident2025");
            Response res = JsonSerializer.Deserialize<Response>(b.JoinBoard("DonaldTrump@gmail.com", cnt));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        public bool TestJoinBoardNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.JoinBoard("yaronet@post.bgu.ac.il", cnt+1));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        public bool TestJoinBoardNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.JoinBoard("DonaldTrump@gmail.co", cnt ));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        public bool TestLeaveBoardPositiveCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.LeaveBoard("yaronet@post.bgu.ac.il", cnt));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        public bool TestLeaveBoardPositiveCase1()
        {
            b.CreateBoard("yaronet@post.bgu.ac.il", "newBoard");
            cnt++;
            b.JoinBoard("DonaldTrump@gmail.com", cnt);
            b.ChangeOwner("yaronet@post.bgu.ac.il", "DonaldTrump@gmail.com", "newBoard");
            Response res = JsonSerializer.Deserialize<Response>(b.LeaveBoard("yaronet@post.bgu.ac.il", cnt));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        public bool TestLeaveBoardNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.LeaveBoard("DonaldTrump@gmail.com", cnt));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        public bool TestLeaveBoardNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.LeaveBoard("yaronet@post.bgu.ac.il", cnt));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        public bool TestChangeOwnerPositiveCase()
        {
            b.JoinBoard("yaronet@post.bgu.ac.il", cnt);
            Response res = JsonSerializer.Deserialize<Response>(b.ChangeOwner("Shauli@gmail.com" ,"yaronet@post.bgu.ac.il", "Mile2"));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        public bool TestChangeOwnerPositiveCase1()
        {
            b.CreateBoard("yaronet@post.bgu.ac.il", "newBoard2");
            cnt++;
            b.JoinBoard("DonaldTrump@gmail.com", cnt);
            Response res = JsonSerializer.Deserialize<Response>(b.ChangeOwner("yaronet@post.bgu.ac.il", "DonaldTrump@gmail.com", "newBoard2"));

            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        public bool TestChangeOwnerNegativeCase()
        { 
            Response res = JsonSerializer.Deserialize<Response>(b.ChangeOwner("Shauli@gmail.com", "yaronet@post.bgu.ac.il", "Mile2"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        public bool TestChangeOwnerNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.ChangeOwner("DonaldTrump@gmail.com", "DonaldTrump@gmail.com", "newBoard2"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }
    }
}
