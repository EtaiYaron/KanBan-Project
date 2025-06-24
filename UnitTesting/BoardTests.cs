using IntroSE.Kanban.Backend.ServiceLayer;
using System.Text.Json;
namespace UnitTesting
{
    public class BoardTests
    {
        private UserService us;
        private BoardService b;
        private TaskService t;
        private int cnt;

        [OneTimeSetUp]
        [Order(1)]
        public void Setup()
        {  
            ServiceFactory s = new ServiceFactory();
            this.us = s.UserService;
            this.b = s.BoardService;
            this.t = s.TaskService;
            b.DeleteAllBoards();
            us.DeleteAllUsers();
            cnt = 0;
            us.Register("yaronet@post.bgu.ac.il", "Admin1");
            us.Register("shauli@gmail.com", "Haparlament1");
        }

        /// <summary>
        /// Checks if a board can be created successfully.
        /// Requirement: 8 (board creation)
        /// </summary>
        [Test]
        [Order(2)]
        public void CreateBoard_ValidUserAndName()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.CreateBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage == null)
            {
                Assert.Pass("TestCreateBoardPositiveCase passed");
            }
            Assert.Fail(res.ErrorMessage);
        }

        /// <summary>
        /// Checks if a board can be created successfully by a different user.
        /// Requirement: 8 (board creation)
        /// </summary>
        [Test]
        [Order(3)]
        public void CreateBoard_ValidOtherUserAndName()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.CreateBoard("shauli@gmail.com", "name1"));
            cnt++;
            if (res.ErrorMessage == null)
            {
                Assert.Pass("TestCreateBoardPositiveCase1 passed");
            }
            Assert.Fail(res.ErrorMessage);
        }

        /// <summary>
        /// Checks if creating a board with the same name for the same user fails.
        /// Requirement: 10 (unique board names per user)
        /// </summary>
        [Test]
        [Order(4)]
        public void CreateBoard_DuplicateNameForUser()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.CreateBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage == null)
            {
                Assert.Fail("There is alredy board under the same name.");
            }
            Assert.Pass("TestCreateBoardNegativeCase passed");
        }

        /// <summary>
        /// Checks if creating a board with an invalid user fails.
        /// Requirement: 8 (board creation)
        /// </summary>
        [Test]
        [Order(5)]
        public void CreateBoard_InvalidUser()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.CreateBoard("hauli@gmail.com", "name2"));
            if (res.ErrorMessage == null)
            {
                Assert.Fail("There is no user this name.");
            }
            Assert.Pass("TestCreateBoardNegativeCase1 passed");
        }

        /// <summary>
        /// Checks if a board can be deleted successfully.
        /// Requirement: 8 (board deletion)
        /// </summary>
        [Test]
        [Order(6)]
        public void DeleteBoard_ValidUserAndName()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.DeleteBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage != null)
            {
                Assert.Fail(res.ErrorMessage);
            }
            Assert.Pass("TestDeleteBoardPositiveCase passed");
        }

        /// <summary>
        /// Checks if a board can be deleted successfully by a different user.
        /// Requirement: 8 (board deletion)
        /// </summary>
        [Test]
        [Order(7)]
        public void DeleteBoard_ValidOtherUserAndName()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.DeleteBoard("shauli@gmail.com", "name1"));
            if (res.ErrorMessage != null)
            {
                Assert.Fail(res.ErrorMessage);
            }
            Assert.Pass("TestDeleteBoardPositiveCase1 passed");
        }

        /// <summary>
        /// Checks if deleting a non-existent board fails.
        /// Requirement: 8 (board deletion)
        /// </summary>
        [Test]
        [Order(8)]
        public void DeleteBoard_NonExistentBoard()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.DeleteBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage == null)
            {
                Assert.Fail("Board alredy been deleted");
            }
            Assert.Pass("TestDeleteBoardNegativeCase passed");
        }

        /// <summary>
        /// Checks if deleting a board with mismatched names fails.
        /// Requirement: 8 (board deletion)
        /// </summary>
        [Test]
        [Order(9)]
        public void DeleteBoard_MismatchedName()
        {
            b.CreateBoard("shauli@gmail.com", "name1");
            cnt++;
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.DeleteBoard("shauli@gmail.com", "name"));
            if (res.ErrorMessage == null)
            {
                Assert.Fail("Board name is not correct");
            }
            Assert.Pass("TestDeleteBoardNegativeCase1 passed");
        }

        /// <summary>
        /// Checks if retrieving a non-existent board fails.
        /// Requirement: 8 (board retrieval)
        /// </summary>
        [Test]
        [Order(10)]
        public void GetBoard_NonExistentBoard()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.GetBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestGetBoardNegativeCase passed");
            }
            Assert.Fail("There is no Board under this name"); ;
        }

        /// <summary>
        /// Checks if retrieving a board with an invalid user fails.
        /// Requirement: 8 (board retrieval)
        /// </summary>
        [Test]
        [Order(11)]
        public void GetBoard_InvalidUser()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.GetBoard("ya", "name"));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestGetBoardNegativeCase1 passed");
            }
            Assert.Fail("There is no such email");
        }

        /// <summary>
        /// Checks if an existing board can be retrieved successfully.
        /// Requirement: 8 (board retrieval)
        /// </summary>
        [Test]
        [Order(12)]
        public void GetBoard_ExistingBoard()
        {
            b.CreateBoard("yaronet@post.bgu.ac.il", "name");
            cnt++;
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.GetBoard("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage != null)
            {
                Assert.Fail(res.ErrorMessage);
            }
            Assert.Pass("TestGetBoardPositiveCase passed");
        }

        /// <summary>
        /// Checks if another existing board can be retrieved successfully.
        /// Requirement: 8 (board retrieval)
        /// </summary>
        [Test]
        [Order(13)]
        public void GetBoard_ExistingOtherBoard()
        {
            b.CreateBoard("yaronet@post.bgu.ac.il", "name50");
            cnt++;
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.GetBoard("yaronet@post.bgu.ac.il", "name50"));
            if (res.ErrorMessage != null)
            {
                Assert.Fail(res.ErrorMessage);
            }
            Assert.Pass("TestGetBoardPositiveCase1 passed");
        }

        /// <summary>
        /// Checks if limiting tasks in a column to one and adding 2 tasks fails.
        /// Requirement: 11 (task limit)
        /// </summary>
        [Test]
        [Order(14)]
        public void LimitTasks_ExceedLimit()
        {
            b.LimitTasks("yaronet@post.bgu.ac.il", "name", 0, 1);
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task1", new DateTime(2026, 4, 10), "test limis tasks");
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(t.AddTask("yaronet@post.bgu.ac.il", "name", "task2", new DateTime(2026, 4, 10), "test limis tasks"));
            if (res.ErrorMessage == null)
            {
                Assert.Fail("There is limit 1 for tasks in this board");
            }
            Assert.Pass("TestLimitTasksNegativeCase passed");
        }

        /// <summary>
        /// Checks if setting an invalid task limit fails.
        /// Requirement: 11 (task limit)
        /// </summary>
        [Test]
        [Order(15)]
        public void LimitTasks_InvalidLimit()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.LimitTasks("yaronet@post.bgu.ac.il", "name", 1, 0));
            if (res.ErrorMessage == null)
            {
                Assert.Fail("Limit can't be 0");
            }
            Assert.Pass("TestLimitTasksNegativeCase1 passed");
        }

        /// <summary>
        /// Checks if setting a valid task limit and adding a task succeeds.
        /// Requirement: 11 (task limit)
        /// </summary>
        [Test]
        [Order(16)]
        public void LimitTasks_ValidLimitAndAddTask()
        {
            b.LimitTasks("yaronet@post.bgu.ac.il", "name", 0, 10);
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(t.AddTask("yaronet@post.bgu.ac.il", "name", "task1", new DateTime(2026, 4, 10), "test limis tasks"));
            if (res.ErrorMessage != null)
            {
                Assert.Fail(res.ErrorMessage);
            }
            Assert.Pass("TestLimitTasksPositiveCase passed");
        }

        /// <summary>
        /// Checks if setting a high task limit succeeds.
        /// Requirement: 11 (task limit)
        /// </summary>
        [Test]
        [Order(17)]
        public void LimitTasks_HighLimit()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.LimitTasks("yaronet@post.bgu.ac.il", "name", 2, 100));
            if (res.ErrorMessage != null)
            {
                Assert.Fail(res.ErrorMessage);
            }
            Assert.Pass("TestLimitTasksPositiveCase1 passed");
        }

        /// <summary>
        /// Checks if a user can retrieve their boards.
        /// Requirements: 8, 9 (board retrieval)
        /// </summary>
        [Test]
        [Order(18)]
        public void GetUserBoards_ExistingUser()
        {
            b.CreateBoard("yaronet@post.bgu.ac.il", "Milestone2");
            cnt++;
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.GetUserBoards("yaronet@post.bgu.ac.il"));
            if (res.ErrorMessage == null)
            {
                Assert.Pass("TestGetUserBoardsPositiveCase passed");
            }
            Assert.Fail(res.ErrorMessage);
        }

        /// <summary>
        /// Checks if a user can retrieve boards they joined.
        /// Requirements: 8, 12 (board retrieval, join)
        /// </summary>
        [Test]
        [Order(19)]
        public void GetUserBoards_JoinedUser()
        {
            us.Register("Kobe2424@gmail.com", "Kobe24");
            b.JoinBoard("Kobe2424@gmail.com", cnt);
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.GetUserBoards("Kobe2424@gmail.com"));
            if (res.ErrorMessage == null)
            {
                Assert.Pass("TestGetUserBoardsPositiveCase1 passed");
            }
            Assert.Fail(res.ErrorMessage);
        }

        /// <summary>
        /// Checks if retrieving boards for a non-existent user fails.
        /// Requirements: 8, 9 (board retrieval)
        /// </summary>
        [Test]
        [Order(20)]
        public void GetUserBoards_NonExistentUser()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.GetUserBoards("LebronJames@gmail.com"));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestGetUserBoardsNegativeCase passed");
            }
            Assert.Fail("There is no such user");
        }

        /// <summary>
        /// Checks if a non-owner cannot delete a board.
        /// Requirements: 11, 13 (ownership, permissions)
        /// </summary>
        [Test]
        [Order(21)]
        public void DeleteBoard_NonOwner()
        {
            b.CreateBoard("shauli@gmail.com", "Mile2");
            cnt++;
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.DeleteBoard("yaronet@post.bgu.ac.il", "Mile2"));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestDeleteBoardNegativeCase2 passed");
            }
            Assert.Fail("user tried to delete is not owner");
        }

        /// <summary>
        /// Checks if a user can join an existing board.
        /// Requirement: 12 (join board)
        /// </summary>
        [Test]
        [Order(22)]
        public void JoinBoard_ExistingBoard()
        {
            cnt++;
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.JoinBoard("yaronet@post.bgu.ac.il", cnt));
            if (res.ErrorMessage == null)
            {
                Assert.Pass("TestJoinBoardPositiveCase passed");
            }
            Assert.Fail(res.ErrorMessage);
        }

        /// <summary>
        /// Checks if another user can join an existing board.
        /// Requirement: 12 (join board)
        /// </summary>
        [Test]
        [Order(23)]
        public void JoinBoard_ExistingOtherBoard()
        {
            us.Register("DonaldTrump@gmail.com", "UsaPresident2025");
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.JoinBoard("DonaldTrump@gmail.com", cnt));
            if (res.ErrorMessage == null)
            {
                Assert.Pass("TestJoinBoardPositiveCase1 passed");
            }
            Assert.Fail(res.ErrorMessage);
        }

        /// <summary>
        /// Checks if joining a non-existent board fails.
        /// Requirement: 12 (join board)
        /// </summary>
        [Test]
        [Order(24)]
        public void JoinBoard_NonExistentBoard()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.JoinBoard("yaronet@post.bgu.ac.il", cnt + 1));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestJoinBoardNegativeCase passed");
            }
            Assert.Pass("There is no such board id");
        }

        /// <summary>
        /// Checks if joining a board with an invalid user fails.
        /// Requirement: 12 (join board)
        /// </summary>
        [Test]
        [Order(25)]
        public void JoinBoard_InvalidUser()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.JoinBoard("DonaldTrump@gmail.co", cnt));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestJoinBoardNegativeCase1 passed");
            }
            Assert.Fail("User alredy in board");
        }

        /// <summary>
        /// Checks if a user can leave a board they joined.
        /// Requirements: 12, 15 (leave board)
        /// </summary>
        [Test]
        [Order(26)]
        public void LeaveBoard_JoinedUser()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.LeaveBoard("yaronet@post.bgu.ac.il", cnt));
            if (res.ErrorMessage == null)
            {
                Assert.Pass("TestLeaveBoardPositiveCase passed");
            }
            Assert.Fail(res.ErrorMessage);
        }

        /// <summary>
        /// Checks if a board owner can transfer ownership and then leave.
        /// Requirements: 13, 14 (ownership transfer, leave)
        /// </summary>
        [Test]
        [Order(27)]
        public void LeaveBoard_OwnerTransferAndLeave()
        {
            b.CreateBoard("yaronet@post.bgu.ac.il", "newBoard");
            cnt++;
            b.JoinBoard("DonaldTrump@gmail.com", cnt);
            b.ChangeOwner("yaronet@post.bgu.ac.il", "DonaldTrump@gmail.com", "newBoard");
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.LeaveBoard("yaronet@post.bgu.ac.il", cnt));
            if (res.ErrorMessage == null)
            {
                Assert.Pass("TestLeaveBoardPositiveCase1 passed");
            }
            Assert.Fail(res.ErrorMessage);
        }

        /// <summary>
        /// Checks if a user who is not a member cannot leave a board.
        /// Requirement: 12 (leave board)
        /// </summary>
        [Test]
        [Order(28)]
        public void LeaveBoard_NotMember()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.LeaveBoard("DonaldTrump@gmail.com", cnt));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestLeaveBoardNegativeCase passed");
            }
            Assert.Fail("Owner can't leave board");
        }

        /// <summary>
        /// Checks if a board owner cannot leave the board.
        /// Requirement: 14 (owner leave restriction)
        /// </summary>
        [Test]
        [Order(29)]
        public void LeaveBoard_Owner()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.LeaveBoard("yaronet@post.bgu.ac.il", cnt));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestLeaveBoardNegativeCase1 passed");
            }
            Assert.Fail("There is no such user in board");
        }

        /// <summary>
        /// Checks if a board owner can transfer ownership to another member.
        /// Requirement: 13 (ownership transfer)
        /// </summary>
        [Test]
        [Order(30)]
        public void ChangeOwner_TransferToMember()
        {
            b.JoinBoard("yaronet@post.bgu.ac.il", cnt - 1);
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.ChangeOwner("shauli@gmail.com", "yaronet@post.bgu.ac.il", "Mile2"));
            if (res.ErrorMessage == null)
            {
                Assert.Pass("TestChangeOwnerPositiveCase passed");
            }
            Assert.Fail(res.ErrorMessage);
        }

        /// <summary>
        /// Checks if a board owner can transfer ownership to a newly joined member.
        /// Requirement: 13 (ownership transfer)
        /// </summary>
        [Test]
        [Order(31)]
        public void ChangeOwner_TransferToNewlyJoinedMember()
        {
            b.CreateBoard("yaronet@post.bgu.ac.il", "newBoard2");
            cnt++;
            b.JoinBoard("DonaldTrump@gmail.com", cnt);
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.ChangeOwner("yaronet@post.bgu.ac.il", "DonaldTrump@gmail.com", "newBoard2"));

            if (res.ErrorMessage == null)
            {
                Assert.Pass("TestChangeOwnerPositiveCase1 passed");
            }
            Assert.Fail(res.ErrorMessage);
        }

        /// <summary>
        /// Checks if a non-owner cannot transfer board ownership.
        /// Requirement: 13 (ownership transfer)
        /// </summary>
        [Test]
        [Order(32)]
        public void ChangeOwner_NonOwner()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.ChangeOwner("shauli@gmail.com", "yaronet@post.bgu.ac.il", "Mile2"));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestChangeOwnerNegativeCase passed");
            }
            Assert.Fail("Non-owner cannot transfer board ownership.");
        }

        /// <summary>
        /// Checks if the member of the boatd cannot transfer ownership to a user that is not in the board.
        /// Requirement: 13 (ownership transfer)
        /// </summary>
        [Test]
        [Order(33)]
        public void ChangeOwner_TransferToNonMember()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(b.ChangeOwner("DonaldTrump@gmail.com", "shauli@gmail.com", "newBoard2"));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestChangeOwnerNegativeCase1 passed");
            }
            Assert.Fail("Cannot transfer board to user that isn't in board");
        }
    }
}