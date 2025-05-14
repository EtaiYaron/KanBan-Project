using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Board;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;
using IntroSE.Kanban.Backend.ServiceLayer;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    class TaskServiceTests
    {
        private UserService us;
        private BoardService b;
        private TaskService t;
        private Response res;
        private int id;
        private int cnt;

        [OneTimeSetUp]
        public void SetUpTaskServiceTests()
        {
            ServiceFactory sf = new ServiceFactory();
            this.us = sf.UserService;
            this.b = sf.BoardService;
            this.t = sf.TaskService;
            us.Register("yaronet@post.bgu.ac.il", "Admin1");
            b.CreateBoard("yaronet@post.bgu.ac.il", "name");
            res = JsonSerializer.Deserialize<Response>(t.AddTask("yaronet@post.bgu.ac.il", "name", "task0", new DateTime(2026, 4, 10), "checking if task is created"));
            id = 0;
            cnt = 0;
        }

        [Test]
        public void TestAddTaskPositiveCase()
        {
            // This test checks if a task can be added successfully to the backlog column (Requirement 13, 5)
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestAddTaskNegativeCase()
        {
            // This test checks if adding a task with invalid data (e.g., past due date) fails (Requirement 5)
            res = JsonSerializer.Deserialize<Response>(t.AddTask("yaronet@post.bgu.ac.il", "name", "task1", new DateTime(2025, 4, 10), "checking if task isn't created"));
            id++;
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestEditTaskPositiveCase()
        {
            // This test checks if a task can be edited successfully (Requirement 15, 16)
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task2", new DateTime(2026, 4, 10), "task is created");
            id++;
            res = JsonSerializer.Deserialize<Response>(t.EditTask("yaronet@post.bgu.ac.il", "name", id, "task2", new DateTime(2027, 4, 10), "checking if task is edited"));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestEditTaskNegativeCase()
        {
            // This test checks if editing a non-existent task fails (Requirement 15, 16)
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task3", new DateTime(2026, 4, 10), "task is created");
            id++;
            res = JsonSerializer.Deserialize<Response>(t.EditTask("yaronet@post.bgu.ac.il", "name", id + 1, "task3", new DateTime(2026, 4, 10), "checking if task is edited"));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestMoveTaskPositiveCase()
        {
            // This test checks if a task can be moved between valid columns (Requirement 14)
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task4", new DateTime(2026, 4, 10), "task is created");
            id++;
            t.EditTask("yaronet@post.bgu.ac.il", "name", id, "task4", new DateTime(2026, 4, 10), "checking if task is edited");
            res = JsonSerializer.Deserialize<Response>(t.MoveTask("yaronet@post.bgu.ac.il", "name", id, 1));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestMoveTaskNegativeCase()
        {
            // This test checks if moving a task to an invalid column fails (Requirement 14)
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task5", new DateTime(2026, 4, 10), "task is created");
            id++;
            res = JsonSerializer.Deserialize<Response>(t.MoveTask("yaronet@post.bgu.ac.il", "name", id, 2));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestGetTaskPositiveCase()
        {
            // This test checks if a task can be retrieved successfully (Requirement 5)
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task6", new DateTime(2026, 4, 10), "task is created");
            id++;
            res = JsonSerializer.Deserialize<Response>(t.GetTask("yaronet@post.bgu.ac.il", "name", id));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestGetTaskNegativeCase()
        {
            // This test checks if retrieving a non-existent task fails (Requirement 5)
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task7", new DateTime(2026, 4, 10), "task is created");
            id++;
            res = JsonSerializer.Deserialize<Response>(t.GetTask("yaronet@post.bgu.ac.il", "name", id + 1));
            Assert.Equals(res.ErrorMessage == null, false);
        }
        
        [Test]
        public void TestGetAllTasksPositiveCase()
        {
            // This test checks if all tasks can be retrieved successfully from a board (Requirement 17)
            res = JsonSerializer.Deserialize<Response>(t.GetAllTasks("yaronet@post.bgu.ac.il", "name"));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestGetAllTasksNegativeCase()
        {
            // This test checks if retrieving tasks from a non-existent board fails (Requirement 17)
            res = JsonSerializer.Deserialize<Response>(t.GetAllTasks("yaronet@post.bgu.ac.il", "name2"));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestAssignTaskToUserPositiveCase()
        {
            us.Register("Shauli@gmail.com", "Haparlament1");
            b.CreateBoard("Shauli@gmail.com", "ABCD");
            t.AddTask("Shauli@gmail.com", "ABCD", "tasktome", new DateTime(2026, 4, 10), "task is created");
            id++;
            cnt++;
            b.JoinBoard("yaronet@post.bgu.ac.il", cnt);
            Response res = JsonSerializer.Deserialize<Response>(b.AssignTaskToUser("Shauli@gmail.com", "ABCD", id, "yaronet@post.bgu.ac.il"));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestAssignTaskToUserPositiveCase1()
        {
            us.Register("DonaldTrump@gmail.com", "UsaPresident2025");
            t.AddTask("Shauli@gmail.com", "USA", "task for trump", new DateTime(2026, 4, 10), "task is for donald");
            id++;
            b.JoinBoard("DonaldTrump@gmail.com", cnt);
            Response res = JsonSerializer.Deserialize<Response>(b.AssignTaskToUser("Shauli@gmail.com", "USA", id, "DonaldTrump@gmail.com"));
            Assert.Equals(res.ErrorMessage == null, true);
        }
        
        [Test]
        public void TestAssignTaskToUserNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.AssignTaskToUser("Shauli@gmail.com", "ABCD", id+1, "yaronet@post.bgu.ac.il"));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestAssignTaskToUserNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(b.AssignTaskToUser("Shauli@gmail.co", "USA", id, "DonaldTrump@gmail.com"));
            Assert.Equals(res.ErrorMessage == null, false);
        }
    }
}
