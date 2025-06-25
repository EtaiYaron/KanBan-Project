using IntroSE.Kanban.Backend.BussinesLayer.Board;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.BussinesLayer.User;
using System.Text.Json;

namespace UnitTesting
{
    public class BoardTests
    {
        private BoardFacade b;
        private UserFacade us;
        private int cnt;

        [OneTimeSetUp]
        [Order(1)]
        public void Setup()
        {
            ServiceFactory service = new ServiceFactory();
            this.us = service.UserFacade;
            this.b = service.BoardFacade;
            us.DeleteAllUsers();
            b.DeleteAllBoards();
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
            bool passed = false;
            try
            {
                b.CreateBoard("yaronet@post.bgu.ac.il", "name");
                passed = true;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            if (passed)
                Assert.Pass("CreateBoard_ValidUserAndName passed");
        }

        /// <summary>
        /// Checks if a board can be created successfully by a different user.
        /// Requirement: 8 (board creation)
        /// </summary>
        [Test]
        [Order(3)]
        public void CreateBoard_ValidOtherUserAndName()
        {
            bool passed = false;  
            try
            {
                b.CreateBoard("shauli@gmail.com", "name1");
                passed = true;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            if (passed)
                Assert.Pass("CreateBoard_ValidOtherUserAndName passed");
        }

        /// <summary>
        /// Checks if creating a board with the same name for the same user fails.
        /// Requirement: 10 (unique board names per user)
        /// </summary>
        [Test]
        [Order(4)]
        public void CreateBoard_DuplicateNameForUser()
        {
            try
            {
                b.CreateBoard("yaronet@post.bgu.ac.il", "name");
                Assert.Fail("There is alredy board under the same name.");
            }
            catch (Exception)
            {
                Assert.Pass("CreateBoard_DuplicateNameForUser passed");
            }
        }

        /// <summary>
        /// Checks if creating a board with an invalid user fails.
        /// Requirement: 8 (board creation)
        /// </summary>
        [Test]
        [Order(5)]
        public void CreateBoard_InvalidUser()
        {
            try
            {
                b.CreateBoard("hauli@gmail.com", "name2");
                Assert.Fail("There is no user under this name.");
            }
            catch (Exception)
            {
                Assert.Pass("CreateBoard_InvalidUser passed");
            }
        }

        /// <summary>
        /// Checks if a board can be deleted successfully.
        /// Requirement: 8 (board deletion)
        /// </summary>
        [Test]
        [Order(6)]
        public void DeleteBoard_ValidUserAndName()
        {
            try
            {
                b.DeleteBoard("yaronet@post.bgu.ac.il", "name");
                Assert.Pass("DeleteBoard_ValidUserAndName passed");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Checks if a board can be deleted successfully by a different user.
        /// Requirement: 8 (board deletion)
        /// </summary>
        [Test]
        [Order(7)]
        public void DeleteBoard_ValidOtherUserAndName()
        {
            try
            {
                b.DeleteBoard("shauli@gmail.com", "name1");
                Assert.Pass("DeleteBoard_ValidOtherUserAndName passed");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Checks if deleting a non-existent board fails.
        /// Requirement: 8 (board deletion)
        /// </summary>
        [Test]
        [Order(8)]
        public void DeleteBoard_NonExistentBoard()
        {
            try
            {
                b.DeleteBoard("yaronet@post.bgu.ac.il", "name");
                Assert.Fail("Board alredy been deleted");
            }
            catch (Exception)
            {
                Assert.Pass("DeleteBoard_NonExistentBoard passed");
            }
        }

        /// <summary>
        /// Checks if deleting a board with mismatched names fails.
        /// Requirement: 8 (board deletion)
        /// </summary>
        [Test]
        [Order(9)]
        public void DeleteBoard_MismatchedName()
        {
            try
            {
                b.CreateBoard("shauli@gmail.com", "name1");
                cnt++;
                b.DeleteBoard("shauli@gmail.com", "name");
                Assert.Fail("Board name is not correct");
            }
            catch (Exception)
            {
                Assert.Pass("DeleteBoard_MismatchedName passed");
            }
        }

        /// <summary>
        /// Checks if retrieving a non-existent board fails.
        /// Requirement: 8 (board retrieval)
        /// </summary>
        [Test]
        [Order(10)]
        public void GetBoard_NonExistentBoard()
        {
            try
            {
                b.GetBoard("yaronet@post.bgu.ac.il", "name");
                Assert.Fail("There is no Board under this name");
            }
            catch (Exception)
            {
                Assert.Pass("GetBoard_NonExistentBoard passed");
            }
        }

        /// <summary>
        /// Checks if retrieving a board with an invalid user fails.
        /// Requirement: 8 (board retrieval)
        /// </summary>
        [Test]
        [Order(11)]
        public void GetBoard_InvalidUser()
        {
            try
            {
                b.GetBoard("ya", "name");
                Assert.Fail("There is no such email");
            }
            catch (Exception)
            {
                Assert.Pass("GetBoard_InvalidUser passed");
            }
        }

        /// <summary>
        /// Checks if an existing board can be retrieved successfully.
        /// Requirement: 8 (board retrieval)
        /// </summary>
        [Test]
        [Order(12)]
        public void GetBoard_ExistingBoard()
        {
            try
            {
                b.CreateBoard("yaronet@post.bgu.ac.il", "name");
                cnt++;
                b.GetBoard("yaronet@post.bgu.ac.il", "name");
                Assert.Pass("GetBoard_ExistingBoard passed");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Checks if another existing board can be retrieved successfully.
        /// Requirement: 8 (board retrieval)
        /// </summary>
        [Test]
        [Order(13)]
        public void GetBoard_ExistingOtherBoard()
        {
            try
            {
                b.CreateBoard("yaronet@post.bgu.ac.il", "name50");
                cnt++;
                b.GetBoard("yaronet@post.bgu.ac.il", "name50");
                Assert.Pass("GetBoard_ExistingOtherBoard passed");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        

        /// <summary>
        /// Checks if limiting tasks in a column to one and adding 2 tasks fails.
        /// Requirement: 11 (task limit)
        /// </summary>
        [Test]
        [Order(14)]
        public void LimitTasks_ExceedLimit()
        {
            try
            {
                b.LimitTasks("yaronet@post.bgu.ac.il", "name", 0, 1);
                b.AddTask("yaronet@post.bgu.ac.il", "name", "task1", new DateTime(2026, 4, 10), "test limis tasks");
                b.AddTask("yaronet@post.bgu.ac.il", "name", "task2", new DateTime(2026, 4, 10), "test limis tasks");
                Assert.Fail("There is limit 1 for tasks in backlog in this board");
            }
            catch (Exception)
            {
                Assert.Pass("LimitTasks_ExceedLimit passed");
            }
        }

        /// <summary>
        /// Checks if setting an invalid task limit fails.
        /// Requirement: 11 (task limit)
        /// </summary>
        [Test]
        [Order(15)]
        public void LimitTasks_InvalidLimit()
        {
            try
            {
                b.LimitTasks("yaronet@post.bgu.ac.il", "name", 1, 0);
                Assert.Fail("Limit can't be 0");
            }
            catch (Exception)
            {
                Assert.Pass("LimitTasks_InvalidLimit passed");
            }
        }

        /// <summary>
        /// Checks if setting a valid task limit and adding a task succeeds.
        /// Requirement: 11 (task limit)
        /// </summary>
        [Test]
        [Order(16)]
        public void LimitTasks_ValidLimitAndAddTask()
        {
            try
            {
                b.LimitTasks("yaronet@post.bgu.ac.il", "name", 0, 10);
                b.AddTask("yaronet@post.bgu.ac.il", "name", "task1", new DateTime(2026, 4, 10), "test limis tasks");
                Assert.Pass("LimitTasks_ValidLimitAndAddTask passed");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Checks if setting a high task limit succeeds.
        /// Requirement: 11 (task limit)
        /// </summary>
        [Test]
        [Order(17)]
        public void LimitTasks_HighLimit()
        {
            try
            {
                b.LimitTasks("yaronet@post.bgu.ac.il", "name", 2, 100);
                Assert.Pass("LimitTasks_HighLimit passed");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Checks if a user can retrieve their boards.
        /// Requirements: 8, 9 (board retrieval)
        /// </summary>
        [Test]
        [Order(18)]
        public void GetUserBoards_ExistingUser()
        {
            try
            {
                b.CreateBoard("yaronet@post.bgu.ac.il", "Milestone2");
                cnt++;
                b.GetUserBoards("yaronet@post.bgu.ac.il");
                Assert.Pass("GetUserBoards_ExistingUser passed");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Checks if a user can retrieve boards they joined.
        /// Requirements: 8, 12 (board retrieval, join)
        /// </summary>
        [Test]
        [Order(19)]
        public void GetUserBoards_JoinedUser()
        {
            try
            {
                us.Register("Kobe2424@gmail.com", "Kobe24");
                b.JoinBoard("Kobe2424@gmail.com", cnt);
                b.GetUserBoards("Kobe2424@gmail.com");
                Assert.Pass("GetUserBoards_JoinedUser passed");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Checks if retrieving boards for a non-existent user fails.
        /// Requirements: 8, 9 (board retrieval)
        /// </summary>
        [Test]
        [Order(20)]
        public void GetUserBoards_NonExistentUser()
        {
            try
            {
                b.GetUserBoards("LebronJames@gmail.com");
                Assert.Fail("There is no such email");
            }
            catch (Exception)
            {
                Assert.Pass("GetUserBoards_NonExistentUser passed");
            }
        }

        /// <summary>
        /// Checks if a non-owner cannot delete a board.
        /// Requirements: 11, 13 (ownership, permissions)
        /// </summary>
        [Test]
        [Order(21)]
        public void DeleteBoard_NonOwner()
        {
            try
            {
                b.CreateBoard("shauli@gmail.com", "Mile2");
                cnt++;
                b.DeleteBoard("yaronet@post.bgu.ac.il", "Mile2");
                Assert.Fail("user tried to delete is not owner");
            }
            catch (Exception)
            {
                Assert.Pass("DeleteBoard_NonOwner passed");
            }
        }

        /// <summary>
        /// Checks if a user can join an existing board.
        /// Requirement: 12 (join board)
        /// </summary>
        [Test]
        [Order(22)]
        public void JoinBoard_ExistingBoard()
        {
            try
            {
                cnt++;
                b.JoinBoard("yaronet@post.bgu.ac.il", cnt);
                Assert.Pass("JoinBoard_ExistingBoard passed");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Checks if another user can join an existing board.
        /// Requirement: 12 (join board)
        /// </summary>
        [Test]
        [Order(23)]
        public void JoinBoard_ExistingOtherBoard()
        {
            try
            {
                us.Register("DonaldTrump@gmail.com", "UsaPresident2025");
                b.JoinBoard("DonaldTrump@gmail.com", cnt);
                Assert.Pass("JoinBoard_ExistingOtherBoard passed");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Checks if joining a non-existent board fails.
        /// Requirement: 12 (join board)
        /// </summary>
        [Test]
        [Order(24)]
        public void JoinBoard_NonExistentBoard()
        {
            try
            {
                b.JoinBoard("yaronet@post.bgu.ac.il", cnt + 1);
                Assert.Fail("There is no such board id");
            }
            catch (Exception)
            {
                Assert.Pass("JoinBoard_NonExistentBoard passed");
            }
        }

        /// <summary>
        /// Checks if joining a board with an invalid user fails.
        /// Requirement: 12 (join board)
        /// </summary>
        [Test]
        [Order(25)]
        public void JoinBoard_InvalidUser()
        {
            try
            {
                b.JoinBoard("DonaldTrump@gmail.co", cnt);
                Assert.Fail("User alredy in board");
            }
            catch (Exception)
            {
                Assert.Pass("TestJoinBoardNegativeCase1 passed");
            }
        }

        /// <summary>
        /// Checks if a user can leave a board they joined.
        /// Requirements: 12, 15 (leave board)
        /// </summary>
        [Test]
        [Order(26)]
        public void LeaveBoard_JoinedUser()
        {
            try
            {
                b.LeaveBoard("yaronet@post.bgu.ac.il", cnt);
                Assert.Pass("LeaveBoard_JoinedUser passed");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Checks if a board owner can transfer ownership and then leave.
        /// Requirements: 13, 14 (ownership transfer, leave)
        /// </summary>
        [Test]
        [Order(27)]
        public void LeaveBoard_OwnerTransferAndLeave()
        {
            try
            {
                b.CreateBoard("yaronet@post.bgu.ac.il", "newBoard");
                cnt++;
                b.JoinBoard("DonaldTrump@gmail.com", cnt);
                b.ChangeOwner("yaronet@post.bgu.ac.il", "DonaldTrump@gmail.com", "newBoard");
                b.LeaveBoard("yaronet@post.bgu.ac.il", cnt);
                Assert.Pass("LeaveBoard_OwnerTransferAndLeave passed");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Checks if a user who is not a member cannot leave a board.
        /// Requirement: 12 (leave board)
        /// </summary>
        [Test]
        [Order(28)]
        public void LeaveBoard_NotMember()
        {
            try
            {
                b.LeaveBoard("DonaldTrump@gmail.com", cnt);
                Assert.Fail("There is no such user in board");
            }
            catch (Exception)
            {
                Assert.Pass("LeaveBoard_NotMember passed");
            }            
        }

        /// <summary>
        /// Checks if a board owner cannot leave the board.
        /// Requirement: 14 (owner leave restriction)
        /// </summary>
        [Test]
        [Order(29)]
        public void LeaveBoard_Owner()
        {
            try
            {
                b.LeaveBoard("yaronet@post.bgu.ac.il", cnt);
                Assert.Fail("Owner can't leave board");
            }
            catch (Exception)
            {
                Assert.Pass("LeaveBoard_Owner passed");
            }
        }

        /// <summary>
        /// Checks if a board owner can transfer ownership to another member.
        /// Requirement: 13 (ownership transfer)
        /// </summary>
        [Test]
        [Order(30)]
        public void ChangeOwner_TransferToMember()
        {
            try
            {
                b.JoinBoard("yaronet@post.bgu.ac.il", cnt - 1);
                b.ChangeOwner("shauli@gmail.com", "yaronet@post.bgu.ac.il", "Mile2");
                Assert.Pass("ChangeOwner_TransferToMember passed");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Checks if a board owner can transfer ownership to a newly joined member.
        /// Requirement: 13 (ownership transfer)
        /// </summary>
        [Test]
        [Order(31)]
        public void ChangeOwner_TransferToNewlyJoinedMember()
        {
            try
            {
                b.CreateBoard("yaronet@post.bgu.ac.il", "newBoard2");
                cnt++;
                b.JoinBoard("DonaldTrump@gmail.com", cnt);
                b.ChangeOwner("yaronet@post.bgu.ac.il", "DonaldTrump@gmail.com", "newBoard2");
                Assert.Pass("ChangeOwner_TransferToNewlyJoinedMember passed");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Checks if a non-owner cannot transfer board ownership.
        /// Requirement: 13 (ownership transfer)
        /// </summary>
        [Test]
        [Order(32)]
        public void ChangeOwner_NonOwner()
        {
            try
            {
                b.ChangeOwner("shauli@gmail.com", "yaronet@post.bgu.ac.il", "Mile2");
                Assert.Fail("Non-owner cannot transfer board ownership.");
            }
            catch (Exception)
            {
                Assert.Pass("ChangeOwner_NonOwner passed");
            }            
        }

        /// <summary>
        /// Checks if the member of the boatd cannot transfer ownership to a user that is not in the board.
        /// Requirement: 13 (ownership transfer)
        /// </summary>
        [Test]
        [Order(33)]
        public void ChangeOwner_TransferToNonMember()
        {
            try
            {
                b.ChangeOwner("DonaldTrump@gmail.com", "shauli@gmail.com", "newBoard2");
                Assert.Fail("Cannot transfer board to user that isn't in board");
            }
            catch (Exception)
            {
                Assert.Pass("ChangeOwner_TransferToNonMember passed");
            }           
        }
    }
}