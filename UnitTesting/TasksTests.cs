using IntroSE.Kanban.Backend.BussinesLayer.Board;
using IntroSE.Kanban.Backend.BussinesLayer.User;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Text.Json;
namespace UnitTesting
{
    public class TasksTests
    {
        private BoardFacade b;
        private UserFacade us;
        private int cnt;
        private int id;

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
            id = 0;
            us.Register("yaronet@post.bgu.ac.il", "Admin1");
            us.Register("shauli@gmail.com", "Haparlament1");
            us.Register("shay.klein11@gmail.com", "Admin1");
            b.CreateBoard("shay.klein11@gmail.com", "name");
        }

        /// <summary>
        /// This test checks if a task can be added successfully to the backlog column.
        /// Requirements: 13 (board member can add), 5 (task attributes)
        /// </summary>
        [Test]
        [Order(2)]
        public void AddTask_ValidTaskToBacklog()
        {
            try
            {
                b.AddTask("shay.klein11@gmail.com", "name", "task0", new DateTime(2026, 6, 10), "checking if task isn't created");
                id++;
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
            Assert.Pass("AddTask_ValidTaskToBacklog passed.");
        }

        /// <summary>
        /// This test checks if adding a task with invalid data (e.g., past due date) fails.
        /// Requirement: 5 (task attributes)
        /// </summary>
        [Test]
        [Order(3)]
        public void AddTask_InvalidDueDate()
        {
            try
            {
                b.AddTask("shay.klein11@gmail.com", "name", "task1", new DateTime(2025, 4, 10), "checking if task isn't created");
            }
            catch(Exception ex)
            {
                Assert.Pass("AddTask_InvalidDueDate passed.");
            }
            Assert.Fail("AddTask_InvalidDueDate failed, Creating a new task with invalid data(past due date) should have failed.");    
        }


        /// <summary>
        /// This test checks if a task can be edited successfully.
        /// Requirements: 20 (only assignee can change), 21 (all data except creation time)
        /// </summary>
        [Test]
        [Order(4)]
        public void EditTask_ValidEdit()
        {
            try
            {
                b.AddTask("shay.klein11@gmail.com", "name", "task2", new DateTime(2026, 4, 10), "task is created");
                b.EditTask("shay.klein11@gmail.com", "name", id, "task2", new DateTime(2027, 4, 10), "checking if task is edited");
                id++;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Assert.Pass("EditTask_ValidEdit passed.");
        }

        /// <summary>
        /// This test checks if editing a non-existent task fails.
        /// Requirements: 20 (only assignee can change), 21 (all data except creation time)
        /// </summary>
        [Test]
        [Order(5)]
        public void EditTask_NonExistentTask()
        {
            try
            {
                b.AddTask("shay.klein11@gmail.com", "name", "task3", new DateTime(2026, 4, 10), "task is created");
                id++;
                b.EditTask("shay.klein11@gmail.com", "name", id + 1, "task3", new DateTime(2026, 4, 10), "checking if task is edited");
            }
            catch(Exception ex)
            {
                Assert.Pass("EditTask_NonExistentTask passed");
            }
            Assert.Fail("EditTask_NonExistentTask Failed, editing a non-existent task should have failed.");
        }

        /// <summary>
        /// This test checks if moving a task to an invalid column fails.
        /// Requirement: 19 (only allowed moves, only assignee can move)
        /// </summary>
        [Test]
        [Order(6)]
        public void MoveTask_InvalidColumn()
        {
            try
            {
                b.AddTask("shay.klein11@gmail.com", "name", "task5", new DateTime(2026, 4, 10), "task is created");
                b.MoveTask("shay.klein11@gmail.com", "name", id, 2);
                id++;
            }
            catch(Exception ex)
            {
                Assert.Pass("MoveTask_InvalidColumn passed.");
            }
            Assert.Fail("TestMoveTaskNegativeCase Failed, moving a task to an invalid column should have failed.");
        }

        /// <summary>
        /// This test checks if a task can be retrieved successfully.
        /// Requirement: 5 (task attributes)
        /// </summary>
        [Test]
        [Order(7)]
        public void GetTask_ValidTask()
        {
            try
            {
                b.AddTask("shay.klein11@gmail.com", "name", "task6", new DateTime(2026, 4, 10), "task is created");
                b.GetTask("shay.klein11@gmail.com", "name", id);
                id++;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Assert.Pass("GetTask_ValidTask passed.");
        }

        /// <summary>
        /// This test checks if retrieving a non-existent task fails.
        /// Requirement: 5 (task attributes)
        /// </summary>
        [Test]
        [Order(8)]
        public void GetTask_NonExistentTask()
        {
            try
            {
                b.AddTask("shay.klein11@gmail.com", "name", "task7", new DateTime(2026, 4, 10), "task is created");
                id++;
                b.GetTask("shay.klein11@gmail.com", "name", id + 1);
            }
            catch(Exception ex)
            {
                Assert.Pass("GetTask_NonExistentTask passed.");
            }
            Assert.Fail("GetTask_NonExistentTask Failed, GetTask with a non-existent task should have failed.");
        }


        /// <summary>
        /// This test checks if a task can be assigned to another user by a board member.
        /// Requirement: 23 (assignment rules)
        /// </summary>
        [Test]
        [Order(9)]
        public void AssignTaskToUser_ValidAssignment()
        {
            try
            {
                //us.Register("Shauli@gmail.com", "Password1234");
                b.CreateBoard("Shauli@gmail.com", "ABCD");
                b.AddTask("Shauli@gmail.com", "ABCD", "tasktome", new DateTime(2026, 4, 10), "task is created");
                cnt++;
                b.JoinBoard("shay.klein11@gmail.com", 2);
                b.AssignTaskToUser("Shauli@gmail.com", "ABCD", 0, "shay.klein11@gmail.com");
                id++;
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Assert.Pass("AssignTaskToUser_ValidAssignment passed.");
        }

        /// <summary>
        /// This test checks if a task can be assigned to a different user by a board member.
        /// Requirement: 23 (assignment rules)
        /// </summary>
        [Test]
        [Order(10)]
        public void AssignTaskToUser_ValidAssignmentOtherUser()
        {
            try
            {
                b.AddTask("Shauli@gmail.com", "ABCD", "task for trump", new DateTime(2026, 4, 10), "task is for donald");
                us.Register("DonaldTrump@gmail.com", "TrumpHamelech1");
                b.JoinBoard("DonaldTrump@gmail.com", 2);
                b.AssignTaskToUser("Shauli@gmail.com", "ABCD", 1, "DonaldTrump@gmail.com");
                id++;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Assert.Pass("AssignTaskToUser_ValidAssignmentOtherUser passed.");
        }

        /// <summary>
        /// This test checks if assigning a non-existent task fails.
        /// Requirement: 23 (assignment rules)
        /// </summary>
        [Test]
        [Order(11)]
        public void AssignTaskToUser_NonExistentTask()
        {
            try
            {
                b.AssignTaskToUser("Shauli@gmail.com", "ABCD", 2, "shay.klein11@gmail.com");
            }
            catch(Exception ex)
            {
                Assert.Pass("AssignTaskToUser_NonExistentTask passed.");
            }
            Assert.Fail("AssignTaskToUser_NonExistentTask Failed, assigning a non-existent task should have failed.");
        }

        /// <summary>
        /// This test checks if assigning a task with an invalid user fails.
        /// Requirement: 23 (assignment rules)
        /// </summary>
        [Test]
        [Order(12)]
        public void AssignTaskToUser_InvalidUser()
        {
            try
            {
                b.AssignTaskToUser("Shauli@gmail.co", "USA", 0, "DonaldTrump@gmail.com");
            }
            catch(Exception ex)
            {
                Assert.Pass("AssignTaskToUser_InvalidUser passed.");
            }
            Assert.Fail("AssignTaskToUser_InvalidUser Failed, assigning a task with an invalid user fails");
        }
    }
}