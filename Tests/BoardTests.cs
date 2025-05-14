using IntroSE.Kanban.Backend.BussinesLayer.Board;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;
using IntroSE.Kanban.Backend.BussinesLayer.User;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Text.Json;
using NUnit.Framework;


namespace Tests
{
    [TestFixture]
    class BoardTests
    {
        private UserService us;
        private BoardService b;
        private TaskService t;
        private int cnt;

        [OneTimeSetUp]
        public void SetUpTests()
        {
            ServiceFactory sf = new ServiceFactory();
            this.us = sf.UserService;
            this.b = sf.BoardService;
            this.t = sf.TaskService;
            cnt = 0;
            us.Register("yaronet@post.bgu.ac.il", "Admin1");
            us.Register("Shauli@gmail.com", "Haparlament1");
        }

        [Test]
        public void TestCreateBoardPositiveCase()
        {
            // This test checks if a board can be created successfully (Requirement 8)
            Response res = JsonSerializer.Deserialize<Response>(b.CreateBoard("yaronet@post.bgu.ac.il", "name"));
            Assert.Equals(res.ErrorMessage == null, true);

        }

        [Test]
        public void TestCreateBoardPositiveCase1()
        {
            // This test checks if a board can be created successfully by a different user (Requirement 8)
            Response res = JsonSerializer.Deserialize<Response>(b.CreateBoard("Shauli@gmail.com", "name1"));
            cnt++;
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestCreateBoardNegativeCase()
        {
            // This test checks if creating a board with the same name for the same user fails (Requirement 10)
            Response res = JsonSerializer.Deserialize<Response>(b.CreateBoard("yaronet@post.bgu.ac.il", "name"));
            Assert.Equals(res.ErrorMessage == null, false);

        }

        [Test]
        public void TestCreateBoardNegativeCase1()
        {
            // This test checks if creating a board with an invalid user fails (Requirement 8)
            Response res = JsonSerializer.Deserialize<Response>(b.CreateBoard("hauli@gmail.com", "name2"));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestDeleteBoardPositiveCase()
        {
            // This test checks if a board can be deleted successfully (Requirement 8)
            Response res = JsonSerializer.Deserialize<Response>(b.DeleteBoard("yaronet@post.bgu.ac.il", "name"));
            iAssert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestDeleteBoardPositiveCase1()
        {
            // This test checks if a board can be deleted successfully by a different user (Requirement 8)
            Response res = JsonSerializer.Deserialize<Response>(b.DeleteBoard("Shauli@gmail.com", "name1"));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestDeleteBoardNegativeCase()
        {
            // This test checks if deleting a non-existent board fails (Requirement 8)
            Response res = JsonSerializer.Deserialize<Response>(b.DeleteBoard("yaronet@post.bgu.ac.il", "name"));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestDeleteBoardNegativeCase1()
        {
            // This test checks if deleting a board with mismatched names fails (Requirement 8)
            b.CreateBoard("Shauli@gmail.com", "name1");
            cnt++;
            Response res = JsonSerializer.Deserialize<Response>(b.DeleteBoard("Shauli@gmail.com", "name"));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestGetBoardNegativeCase()
        {
            // This test checks if retrieving a non-existent board fails (Requirement 8)
            Response res = JsonSerializer.Deserialize<Response>(b.GetBoard("yaronet@post.bgu.ac.il", "name"));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestGetBoardNegativeCase1()
        {
            // This test checks if retrieving a board with an invalid user fails (Requirement 8)
            Response res = JsonSerializer.Deserialize<Response>(b.GetBoard("ya", "name"));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestGetBoardPositiveCase()
        {
            // This test checks if an existing board can be retrieved successfully (Requirement 8)
            b.CreateBoard("yaronet@post.bgu.ac.il", "name");
            cnt++;
            Response res = JsonSerializer.Deserialize<Response>(b.GetBoard("yaronet@post.bgu.ac.il", "name"));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestGetBoardPositiveCase1()
        {
            // This test checks if another existing board can be retrieved successfully (Requirement 8)
            b.CreateBoard("yaronet@post.bgu.ac.il", "name50");
            cnt++;
            Response res = JsonSerializer.Deserialize<Response>(b.GetBoard("yaronet@post.bgu.ac.il", "name50"));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestLimitTasksNegativeCase()
        {
            // This test checks if limiting tasks in a column to zero and adding a task fails (Requirement 11)
            b.LimitTasks("yaronet@post.bgu.ac.il", "name", 0, 0);
            Response res = JsonSerializer.Deserialize<Response>(t.AddTask("yaronet@post.bgu.ac.il", "name", "task1", new DateTime(2026, 4, 10), "test limis tasks"));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestLimitTasksNegativeCase1()
        {
            // This test checks if setting an invalid task limit fails (Requirement 11)
            Response res = JsonSerializer.Deserialize<Response>(b.LimitTasks("yaronet@post.bgu.ac.il", "name", 3, 0));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestLimitTasksPositiveCase()
        {
            // This test checks if setting a valid task limit and adding a task succeeds (Requirement 11)
            b.LimitTasks("yaronet@post.bgu.ac.il", "name", 0, 10);
            Response res = JsonSerializer.Deserialize<Response>(t.AddTask("yaronet@post.bgu.ac.il", "name", "task1", new DateTime(2026, 4, 10), "test limis tasks"));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestLimitTasksPositiveCase1()
        {
            // This test checks if setting a high task limit succeeds (Requirement 11)
            Response res = JsonSerializer.Deserialize<Response>(b.LimitTasks("yaronet@post.bgu.ac.il", "name", 2, 100));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestGetUserBoardsPositiveCase()
        {
            b.CreateBoard("yaronet@post.bgu.ac.il", "Milestone2");
            cnt++;
            Response res = JsonSerializer.Deserialize<Response>(b.GetUserBoards("yaronet@post.bgu.ac.il"));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestGetUserBoardsPositiveCase1()
        {
            us.Register("Kobe2424@gmail.com" , "Kobe1" );
            b.JoinBoard("Kobe2424@gmail.com", cnt);
            cnt++;
            Response res = JsonSerializer.Deserialize<Response>(b.GetUserBoards("Kobe2424@gmail.com"));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestGetUserBoardsNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.GetUserBoards("LebronJames@gmail.com"));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestDeleteBoardNegativeCase2()
        {
            b.CreateBoard("Shauli@gmail.com", "Mile2");
            cnt++;
            Response res = JsonSerializer.Deserialize<Response>(b.DeleteBoard("yaronet@post.bgu.ac.il", "Mile2"));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestJoinBoardPositiveCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.JoinBoard("yaronet@post.bgu.ac.il", cnt));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestJoinBoardPositiveCase1()
        {
            us.Register("DonaldTrump@gmail.com", "UsaPresident2025");
            Response res = JsonSerializer.Deserialize<Response>(b.JoinBoard("DonaldTrump@gmail.com", cnt));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestJoinBoardNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.JoinBoard("yaronet@post.bgu.ac.il", cnt+1));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestJoinBoardNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.JoinBoard("DonaldTrump@gmail.co", cnt ));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestLeaveBoardPositiveCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.LeaveBoard("yaronet@post.bgu.ac.il", cnt));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestLeaveBoardPositiveCase1()
        {
            b.CreateBoard("yaronet@post.bgu.ac.il", "newBoard");
            cnt++;
            b.JoinBoard("DonaldTrump@gmail.com", cnt);
            b.ChangeOwner("yaronet@post.bgu.ac.il", "DonaldTrump@gmail.com", "newBoard");
            Response res = JsonSerializer.Deserialize<Response>(b.LeaveBoard("yaronet@post.bgu.ac.il", cnt));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestLeaveBoardNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.LeaveBoard("DonaldTrump@gmail.com", cnt));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestLeaveBoardNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.LeaveBoard("yaronet@post.bgu.ac.il", cnt));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestChangeOwnerPositiveCase()
        {
            b.JoinBoard("yaronet@post.bgu.ac.il", cnt);
            Response res = JsonSerializer.Deserialize<Response>(b.ChangeOwner("Shauli@gmail.com" ,"yaronet@post.bgu.ac.il", "Mile2"));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestChangeOwnerPositiveCase1()
        {
            b.CreateBoard("yaronet@post.bgu.ac.il", "newBoard2");
            cnt++;
            b.JoinBoard("DonaldTrump@gmail.com", cnt);
            Response res = JsonSerializer.Deserialize<Response>(b.ChangeOwner("yaronet@post.bgu.ac.il", "DonaldTrump@gmail.com", "newBoard2"));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestChangeOwnerNegativeCase()
        { 
            Response res = JsonSerializer.Deserialize<Response>(b.ChangeOwner("Shauli@gmail.com", "yaronet@post.bgu.ac.il", "Mile2"));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestChangeOwnerNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.ChangeOwner("DonaldTrump@gmail.com", "DonaldTrump@gmail.com", "newBoard2"));
            Assert.Equals(res.ErrorMessage == null, false);
        }
    }
}
