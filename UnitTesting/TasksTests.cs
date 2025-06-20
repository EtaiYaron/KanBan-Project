using IntroSE.Kanban.Backend.ServiceLayer;
using System.Text.Json;
namespace UnitTesting
{
    public class TasksTests
    {
        private UserService us;
        private BoardService b;
        private TaskService t;
        private int cnt;
        private int id;
        Response res;

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
            us.Register("shay.klein11@gmail.com", "Admin1");
            b.CreateBoard("shay.klein11@gmail.com", "name");
            t.AddTask("shay.klein11@gmail.com", "name", "task0", new DateTime(2026, 4, 10), "checking if task is created");
            cnt = 0;
            id = 0;
        }

        /// <summary>
        /// This test checks if a task can be added successfully to the backlog column.
        /// Requirements: 13 (board member can add), 5 (task attributes)
        /// </summary>
        [Test]
        [Order(2)]
        public void AddTask_ValidTaskToBacklog()
        {
            res = JsonSerializer.Deserialize<Response>(t.AddTask("shay.klein11@gmail.com", "name", "task0", new DateTime(2026, 6, 10), "checking if task isn't created"));
            id++;
            if (res.ErrorMessage == null)
            {
                Assert.Pass("TestAddTaskPositiveCase passed.");
            }
            Assert.Fail(res.ErrorMessage);
        }

        /// <summary>
        /// This test checks if adding a task with invalid data (e.g., past due date) fails.
        /// Requirement: 5 (task attributes)
        /// </summary>
        [Test]
        [Order(3)]
        public void AddTask_InvalidDueDate()
        {
            res = JsonSerializer.Deserialize<Response>(t.AddTask("shay.klein11@gmail.com", "name", "task1", new DateTime(2025, 4, 10), "checking if task isn't created"));
            if (res.ErrorMessage == null)
            {
                Assert.Fail("TestAddTaskNegativeCase failed, Creating a new task with invalid data(past due date) should have failed.");
            }
            Assert.Pass("TestAddTaskNegativeCase passed.");
        }

        /// <summary>
        /// This test checks if a task can be edited successfully.
        /// Requirements: 20 (only assignee can change), 21 (all data except creation time)
        /// </summary>
        [Test]
        [Order(4)]
        public void EditTask_ValidEdit()
        {
            t.AddTask("shay.klein11@gmail.com", "name", "task2", new DateTime(2026, 4, 10), "task is created");
            res = JsonSerializer.Deserialize<Response>(t.EditTask("shay.klein11@gmail.com", "name", id, "task2", new DateTime(2027, 4, 10), "checking if task is edited"));
            id++;
            if (res.ErrorMessage == null)
            {
                Assert.Pass("TestEditTaskPositiveCase passed.");
            }
            Assert.Fail(res.ErrorMessage);
        }

        /// <summary>
        /// This test checks if editing a non-existent task fails.
        /// Requirements: 20 (only assignee can change), 21 (all data except creation time)
        /// </summary>
        [Test]
        [Order(5)]
        public void EditTask_NonExistentTask()
        {
            t.AddTask("shay.klein11@gmail.com", "name", "task3", new DateTime(2026, 4, 10), "task is created");
            id++;
            res = JsonSerializer.Deserialize<Response>(t.EditTask("shay.klein11@gmail.com", "name", id + 1, "task3", new DateTime(2026, 4, 10), "checking if task is edited"));
            if (res.ErrorMessage == null)
            {
                Assert.Fail("TestEditTaskNegativeCase Failed, editing a non-existent task should have failed.");
            }
            Assert.Pass("TestEditTaskNegativeCase passed.");
        }

        /// <summary>
        /// This test checks if a task can be moved between valid columns.
        /// Requirement: 19 (only allowed moves, only assignee can move)
        /// </summary>
        [Test]
        [Order(6)]
        public void MoveTask_ValidMove()
        {
            t.AddTask("shay.klein11@gmail.com", "name", "task4", new DateTime(2026, 4, 10), "task is created");
            t.EditTask("shay.klein11@gmail.com", "name", id, "task4", new DateTime(2026, 4, 10), "checking if task is edited");
            res = JsonSerializer.Deserialize<Response>(t.MoveTask("shay.klein11@gmail.com", "name", id, 1));
            id++;

            if (res.ErrorMessage == null)
            {
                Assert.Pass("TestMoveTaskPositiveCase passed.");
            }
            Assert.Fail(res.ErrorMessage);
        }

        /// <summary>
        /// This test checks if moving a task to an invalid column fails.
        /// Requirement: 19 (only allowed moves, only assignee can move)
        /// </summary>
        [Test]
        [Order(7)]
        public void MoveTask_InvalidColumn()
        {
            t.AddTask("shay.klein11@gmail.com", "name", "task5", new DateTime(2026, 4, 10), "task is created");
            res = JsonSerializer.Deserialize<Response>(t.MoveTask("shay.klein11@gmail.com", "name", id, 2));
            id++;

            if (res.ErrorMessage == null)
            {
                Assert.Fail("TestMoveTaskNegativeCase Failed, moving a task to an invalid column should have failed.");
            }
            Assert.Pass("TestMoveTaskNegativeCase passed.");
        }

        /// <summary>
        /// This test checks if a task can be retrieved successfully.
        /// Requirement: 5 (task attributes)
        /// </summary>
        [Test]
        [Order(8)]
        public void GetTask_ValidTask()
        {
            t.AddTask("shay.klein11@gmail.com", "name", "task6", new DateTime(2026, 4, 10), "task is created");
            res = JsonSerializer.Deserialize<Response>(t.GetTask("shay.klein11@gmail.com", "name", id));
            id++;

            if (res.ErrorMessage == null)
            {
                Assert.Pass("TestGetTaskPositiveCase passed.");
            }
            Assert.Fail(res.ErrorMessage);
        }

        /// <summary>
        /// This test checks if retrieving a non-existent task fails.
        /// Requirement: 5 (task attributes)
        /// </summary>
        [Test]
        [Order(9)]
        public void GetTask_NonExistentTask()
        {
            t.AddTask("shay.klein11@gmail.com", "name", "task7", new DateTime(2026, 4, 10), "task is created");
            id++;
            res = JsonSerializer.Deserialize<Response>(t.GetTask("shay.klein11@gmail.com", "name", id + 1));

            if (res.ErrorMessage == null)
            {
                Assert.Fail("TestGetTaskNegativeCase Failed, GetTask with a non-existent task should have failed.");
            }
            Assert.Pass("TestGetTaskNegativeCase passed.");
        }


        /// <summary>
        /// This test checks if a task can be assigned to another user by a board member.
        /// Requirement: 23 (assignment rules)
        /// </summary>
        [Test]
        [Order(10)]
        public void AssignTaskToUser_ValidAssignment()
        {
            us.Register("Shauli@gmail.com", "Password1234");
            b.CreateBoard("Shauli@gmail.com", "ABCD");
            t.AddTask("Shauli@gmail.com", "ABCD", "tasktome", new DateTime(2026, 4, 10), "task is created");
            cnt++;
            b.JoinBoard("shay.klein11@gmail.com", 2);
            Response res = JsonSerializer.Deserialize<Response>(t.AssignTaskToUser("Shauli@gmail.com", "ABCD", 0, "shay.klein11@gmail.com"));
            id++;

            if (res.ErrorMessage == null)
            {
                Assert.Pass("TestAssignTaskToUserPositiveCase passed.");
            }
            Assert.Fail(res.ErrorMessage);
        }

        /// <summary>
        /// This test checks if a task can be assigned to a different user by a board member.
        /// Requirement: 23 (assignment rules)
        /// </summary>
        [Test]
        [Order(11)]
        public void AssignTaskToUser_ValidAssignmentOtherUser()
        {
            t.AddTask("Shauli@gmail.com", "ABCD", "task for trump", new DateTime(2026, 4, 10), "task is for donald");
            us.Register("DonaldTrump@gmail.com", "TrumpHamelech1");
            b.JoinBoard("DonaldTrump@gmail.com", 2);
            Response res = JsonSerializer.Deserialize<Response>(t.AssignTaskToUser("Shauli@gmail.com", "ABCD", 1, "DonaldTrump@gmail.com"));
            id++;
            if (res.ErrorMessage == null)
            {
                Assert.Pass("TestAssignTaskToUserPositiveCase1 passed.");
            }
            Assert.Fail(res.ErrorMessage);
        }

        /// <summary>
        /// This test checks if assigning a non-existent task fails.
        /// Requirement: 23 (assignment rules)
        /// </summary>
        [Test]
        [Order(12)]
        public void AssignTaskToUser_NonExistentTask()
        {
            Response res = JsonSerializer.Deserialize<Response>(t.AssignTaskToUser("Shauli@gmail.com", "ABCD", 2, "shay.klein11@gmail.com"));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestAssignTaskToUserNegativeCase passed.");
            }
            Assert.Fail("TestAssignTaskToUserNegativeCase Failed, assigning a non-existent task should have failed.");
        }

        /// <summary>
        /// This test checks if assigning a task with an invalid user fails.
        /// Requirement: 23 (assignment rules)
        /// </summary>
        [Test]
        [Order(13)]
        public void AssignTaskToUser_InvalidUser()
        {
            Response res = JsonSerializer.Deserialize<Response>(t.AssignTaskToUser("Shauli@gmail.co", "USA", 0, "DonaldTrump@gmail.com"));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestAssignTaskToUserNegativeCase1 passed.");
            }
            Assert.Fail("TestAssignTaskToUserNegativeCase1 Failed, assigning a task with an invalid user fails");
        }
    }
}