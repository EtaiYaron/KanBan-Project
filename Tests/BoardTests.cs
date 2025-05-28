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
            /*
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

            // Test: Retrieving a user's own boards (Requirement 8, 9)
            tests = TestGetUserBoardsPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestGetUserBoardsPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetUserBoardsPositiveCase: Failed");
            }

            // Test: Retrieving boards a user has joined (Requirement 8, 12)
            tests = TestGetUserBoardsPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestGetUserBoardsPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestGetUserBoardsPositiveCase1: Failed");
            }

            // Test: Retrieving boards for a non-existent user (Requirement 8, 9)
            tests = TestGetUserBoardsNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestGetUserBoardsNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetUserBoardsNegativeCase: Failed");
            }

            // Test: Non-owner cannot delete a board (Requirement 11, 13)
            tests = TestDeleteBoardNegativeCase2();
            if (tests)
            {
                Console.WriteLine("TestDeleteBoardNegativeCase2: Passed");
            }
            else
            {
                Console.WriteLine("TestDeleteBoardNegativeCase2: Failed");
            }

            // Test: User can join an existing board (Requirement 12)
            tests = TestJoinBoardPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestJoinBoardPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestJoinBoardPositiveCase: Failed");
            }

            // Test: Another user can join an existing board (Requirement 12)
            tests = TestJoinBoardPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestJoinBoardPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestJoinBoardPositiveCase1: Failed");
            }

            // Test: Joining a non-existent board fails (Requirement 12)
            tests = TestJoinBoardNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestJoinBoardNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestJoinBoardNegativeCase: Failed");
            }

            // Test: Joining a board with an invalid user fails (Requirement 12)
            tests = TestJoinBoardNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestJoinBoardNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestJoinBoardNegativeCase1: Failed");
            }

            // Test: User can leave a board they joined (Requirement 12, 15)
            tests = TestLeaveBoardPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestLeaveBoardPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestLeaveBoardPositiveCase: Failed");
            }

            // Test: Board owner can transfer ownership and then leave (Requirement 13, 14)
            tests = TestLeaveBoardPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestLeaveBoardPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestLeaveBoardPositiveCase1: Failed");
            }

            // Test: User who is not a member cannot leave a board (Requirement 12)
            tests = TestLeaveBoardNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestLeaveBoardNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestLeaveBoardNegativeCase: Failed");
            }

            // Test: Board owner cannot leave the board (Requirement 14)
            tests = TestLeaveBoardNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestLeaveBoardNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestLeaveBoardNegativeCase1: Failed");
            }

            // Test: Board owner can transfer ownership to another member (Requirement 13)
            tests = TestChangeOwnerPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestChangeOwnerPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestChangeOwnerPositiveCase: Failed");
            }

            // Test: Board owner can transfer ownership to a newly joined member (Requirement 13)
            tests = TestChangeOwnerPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestChangeOwnerPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestChangeOwnerPositiveCase1: Failed");
            }

            // Test: Non-owner cannot transfer board ownership (Requirement 13)
            tests = TestChangeOwnerNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestChangeOwnerNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestChangeOwnerNegativeCase: Failed");
            }

            // Test: User cannot transfer ownership to themselves if not the owner (Requirement 13)
            tests = TestChangeOwnerNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestChangeOwnerNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestChangeOwnerNegativeCase1: Failed");
            }

            */
        }

        /// <summary>
        /// Registers two users for testing purposes.
        /// </summary>
        public void CreatingUser()
        {
            us.Register("yaronet@post.bgu.ac.il", "Admin1");
            us.Register("Shauli@gmail.com", "Haparlament1");
        }

        /// <summary>
        /// Checks if a board can be created successfully.
        /// Requirement: 8 (board creation)
        /// </summary>
        public bool TestCreateBoardPositiveCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.CreateBoard("yaronet@post.bgu.ac.il", "name"));
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task", new DateTime(2026, 10, 10), "try to add task");
            //t.EditTask("yaronet@post.bgu.ac.il", "name", 0, "TASK", null, null);
            b.JoinBoard("Shauli@gmail.com", 0);
            //t.AssignTaskToUser("yaronet@post.bgu.ac.il", "name", 0, "Shauli@gmail.com");
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a board can be created successfully by a different user.
        /// Requirement: 8 (board creation)
        /// </summary>
        public bool TestCreateBoardPositiveCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.CreateBoard("Shauli@gmail.com", "name1"));
            cnt++;
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if creating a board with the same name for the same user fails.
        /// Requirement: 10 (unique board names per user)
        /// </summary>
        public bool TestCreateBoardNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.CreateBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if creating a board with an invalid user fails.
        /// Requirement: 8 (board creation)
        /// </summary>
        public bool TestCreateBoardNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.CreateBoard("hauli@gmail.com", "name2"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if a board can be deleted successfully.
        /// Requirement: 8 (board deletion)
        /// </summary>
        public bool TestDeleteBoardPositiveCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.DeleteBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if a board can be deleted successfully by a different user.
        /// Requirement: 8 (board deletion)
        /// </summary>
        public bool TestDeleteBoardPositiveCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.DeleteBoard("Shauli@gmail.com", "name1"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if deleting a non-existent board fails.
        /// Requirement: 8 (board deletion)
        /// </summary>
        public bool TestDeleteBoardNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.DeleteBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if deleting a board with mismatched names fails.
        /// Requirement: 8 (board deletion)
        /// </summary>
        public bool TestDeleteBoardNegativeCase1()
        {
            b.CreateBoard("Shauli@gmail.com", "name1");
            cnt++;
            Response res = JsonSerializer.Deserialize<Response>(b.DeleteBoard("Shauli@gmail.com", "name"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if retrieving a non-existent board fails.
        /// Requirement: 8 (board retrieval)
        /// </summary>
        public bool TestGetBoardNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.GetBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if retrieving a board with an invalid user fails.
        /// Requirement: 8 (board retrieval)
        /// </summary>
        public bool TestGetBoardNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.GetBoard("ya", "name"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if an existing board can be retrieved successfully.
        /// Requirement: 8 (board retrieval)
        /// </summary>
        public bool TestGetBoardPositiveCase()
        {
            b.CreateBoard("yaronet@post.bgu.ac.il", "name");
            cnt++;
            Response res = JsonSerializer.Deserialize<Response>(b.GetBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if another existing board can be retrieved successfully.
        /// Requirement: 8 (board retrieval)
        /// </summary>
        public bool TestGetBoardPositiveCase1()
        {
            b.CreateBoard("yaronet@post.bgu.ac.il", "name50");
            cnt++;
            Response res = JsonSerializer.Deserialize<Response>(b.GetBoard("yaronet@post.bgu.ac.il", "name50"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if limiting tasks in a column to zero and adding a task fails.
        /// Requirement: 11 (task limit)
        /// </summary>
        public bool TestLimitTasksNegativeCase()
        {
            b.LimitTasks("yaronet@post.bgu.ac.il", "name", 0, 0);
            Response res = JsonSerializer.Deserialize<Response>(t.AddTask("yaronet@post.bgu.ac.il", "name", "task1", new DateTime(2026, 4, 10), "test limis tasks"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if setting an invalid task limit fails.
        /// Requirement: 11 (task limit)
        /// </summary>
        public bool TestLimitTasksNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.LimitTasks("yaronet@post.bgu.ac.il", "name", 3, 0));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if setting a valid task limit and adding a task succeeds.
        /// Requirement: 11 (task limit)
        /// </summary>
        public bool TestLimitTasksPositiveCase()
        {
            b.LimitTasks("yaronet@post.bgu.ac.il", "name", 0, 10);
            Response res = JsonSerializer.Deserialize<Response>(t.AddTask("yaronet@post.bgu.ac.il", "name", "task1", new DateTime(2026, 4, 10), "test limis tasks"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if setting a high task limit succeeds.
        /// Requirement: 11 (task limit)
        /// </summary>
        public bool TestLimitTasksPositiveCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.LimitTasks("yaronet@post.bgu.ac.il", "name", 2, 100));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if a user can retrieve their boards.
        /// Requirements: 8, 9 (board retrieval)
        /// </summary>
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

        /// <summary>
        /// Checks if a user can retrieve boards they joined.
        /// Requirements: 8, 12 (board retrieval, join)
        /// </summary>
        public bool TestGetUserBoardsPositiveCase1()
        {
            us.Register("Kobe2424@gmail.com", "Kobe1");
            b.JoinBoard("Kobe2424@gmail.com", cnt);
            cnt++;
            Response res = JsonSerializer.Deserialize<Response>(b.GetUserBoards("Kobe2424@gmail.com"));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if retrieving boards for a non-existent user fails.
        /// Requirements: 8, 9 (board retrieval)
        /// </summary>
        public bool TestGetUserBoardsNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.GetUserBoards("LebronJames@gmail.com"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a non-owner cannot delete a board.
        /// Requirements: 11, 13 (ownership, permissions)
        /// </summary>
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

        /// <summary>
        /// Checks if a user can join an existing board.
        /// Requirement: 12 (join board)
        /// </summary>
        public bool TestJoinBoardPositiveCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.JoinBoard("yaronet@post.bgu.ac.il", cnt));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if another user can join an existing board.
        /// Requirement: 12 (join board)
        /// </summary>
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

        /// <summary>
        /// Checks if joining a non-existent board fails.
        /// Requirement: 12 (join board)
        /// </summary>
        public bool TestJoinBoardNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.JoinBoard("yaronet@post.bgu.ac.il", cnt + 1));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if joining a board with an invalid user fails.
        /// Requirement: 12 (join board)
        /// </summary>
        public bool TestJoinBoardNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.JoinBoard("DonaldTrump@gmail.co", cnt));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a user can leave a board they joined.
        /// Requirements: 12, 15 (leave board)
        /// </summary>
        public bool TestLeaveBoardPositiveCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.LeaveBoard("yaronet@post.bgu.ac.il", cnt));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a board owner can transfer ownership and then leave.
        /// Requirements: 13, 14 (ownership transfer, leave)
        /// </summary>
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

        /// <summary>
        /// Checks if a user who is not a member cannot leave a board.
        /// Requirement: 12 (leave board)
        /// </summary>
        public bool TestLeaveBoardNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.LeaveBoard("DonaldTrump@gmail.com", cnt));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a board owner cannot leave the board.
        /// Requirement: 14 (owner leave restriction)
        /// </summary>
        public bool TestLeaveBoardNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.LeaveBoard("yaronet@post.bgu.ac.il", cnt));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a board owner can transfer ownership to another member.
        /// Requirement: 13 (ownership transfer)
        /// </summary>
        public bool TestChangeOwnerPositiveCase()
        {
            b.JoinBoard("yaronet@post.bgu.ac.il", cnt);
            Response res = JsonSerializer.Deserialize<Response>(b.ChangeOwner("Shauli@gmail.com", "yaronet@post.bgu.ac.il", "Mile2"));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a board owner can transfer ownership to a newly joined member.
        /// Requirement: 13 (ownership transfer)
        /// </summary>
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

        /// <summary>
        /// Checks if a non-owner cannot transfer board ownership.
        /// Requirement: 13 (ownership transfer)
        /// </summary>
        public bool TestChangeOwnerNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.ChangeOwner("Shauli@gmail.com", "yaronet@post.bgu.ac.il", "Mile2"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a user cannot transfer ownership to themselves if not the owner.
        /// Requirement: 13 (ownership transfer)
        /// </summary>
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
