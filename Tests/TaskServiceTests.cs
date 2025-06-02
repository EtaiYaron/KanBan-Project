using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Board;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace Tests
{
    class TaskServiceTests
    {
        
        private UserService us;
        private BoardService b;
        private TaskService t;
        private Response res;
        private int id;
        private int cnt;

        public TaskServiceTests(UserService us, BoardService b, TaskService t)
        {
            this.us = us;
            this.b = b;
            this.t = t;
        }

        public void TaskServiceRunTests()
        {
            Console.WriteLine("Running Tests...");

            Before();

            // Test adding a task successfully (Requirement 13, 5)
            bool tests = TestAddTaskPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestAddTaskPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestAddTaskPositiveCase: Failed");
            }

            // Test adding a task with invalid data (Requirement 5)
            tests = TestAddTaskNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestAddTaskNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestAddTaskNegativeCase: Failed");
            }

            // Test editing a task successfully (Requirement 15, 16)
            tests = TestEditTaskPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestEditTaskPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestEditTaskPositiveCase: Failed");
            }

            // Test editing a task with invalid data (Requirement 15, 16)
            tests = TestEditTaskNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestEditTaskNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestEditTaskNegativeCase: Failed");
            }

            // Test moving a task successfully (Requirement 14)
            tests = TestMoveTaskPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestMoveTaskPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestMoveTaskPositiveCase: Failed");
            }

            // Test moving a task with invalid data (Requirement 14)
            tests = TestMoveTaskNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestMoveTaskNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestMoveTaskNegativeCase: Failed");
            }

            // Test retrieving a task successfully (Requirement 5)
            tests = TestGetTaskPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestGetTaskPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetTaskPositiveCase: Failed");
            }

            // Test retrieving a task with invalid data (Requirement 5)
            tests = TestGetTaskNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestGetTaskNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetTaskNegativeCase: Failed");
            }

            // Test: Assigning a task to another user (Requirement 23)
            tests = TestAssignTaskToUserPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestAssignTaskToUserPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestAssignTaskToUserPositiveCase: Failed");
            }

            // Test: Assigning a task to a different user (Requirement 23)
            tests = TestAssignTaskToUserPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestAssignTaskToUserPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestAssignTaskToUserPositiveCase1: Failed");
            }

            // Test: Assigning a non-existent task fails (Requirement 23)
            tests = TestAssignTaskToUserNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestAssignTaskToUserNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestAssignTaskToUserNegativeCase: Failed");
            }

            // Test: Assigning a task with an invalid user fails (Requirement 23)
            tests = TestAssignTaskToUserNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestAssignTaskToUserNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestAssignTaskToUserNegativeCase1: Failed");
            }
        }


        public void Before()
        {
            us.Register("shay.klein11@gmail.com", "Admin1");
            b.CreateBoard("shay.klein11@gmail.com", "name");
            res = JsonSerializer.Deserialize<Response>(t.AddTask("shay.klein11@gmail.com", "name", "task0", new DateTime(2026, 4, 10), "checking if task is created"));
            id = 0;
            cnt = 0;
        }

        /// <summary>
        /// This test checks if a task can be added successfully to the backlog column.
        /// Requirements: 13 (board member can add), 5 (task attributes)
        /// </summary>
        public bool TestAddTaskPositiveCase()
        {
            res = JsonSerializer.Deserialize<Response>(t.AddTask("shay.klein11@gmail.com", "name", "task0", new DateTime(2026, 6, 10), "checking if task isn't created"));
            id++;
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This test checks if adding a task with invalid data (e.g., past due date) fails.
        /// Requirement: 5 (task attributes)
        /// </summary>
        public bool TestAddTaskNegativeCase()
        {
            res = JsonSerializer.Deserialize<Response>(t.AddTask("shay.klein11@gmail.com", "name", "task1", new DateTime(2025, 4, 10), "checking if task isn't created"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This test checks if a task can be edited successfully.
        /// Requirements: 20 (only assignee can change), 21 (all data except creation time)
        /// </summary>
        public bool TestEditTaskPositiveCase()
        {
            t.AddTask("shay.klein11@gmail.com", "name", "task2", new DateTime(2026, 4, 10), "task is created");
            res = JsonSerializer.Deserialize<Response>(t.EditTask("shay.klein11@gmail.com", "name", id, "task2", new DateTime(2027, 4, 10), "checking if task is edited"));
            id++;
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This test checks if editing a non-existent task fails.
        /// Requirements: 20 (only assignee can change), 21 (all data except creation time)
        /// </summary>
        public bool TestEditTaskNegativeCase()
        {
            t.AddTask("shay.klein11@gmail.com", "name", "task3", new DateTime(2026, 4, 10), "task is created");
            id++;
            res = JsonSerializer.Deserialize<Response>(t.EditTask("shay.klein11@gmail.com", "name", id + 1, "task3", new DateTime(2026, 4, 10), "checking if task is edited"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This test checks if a task can be moved between valid columns.
        /// Requirement: 19 (only allowed moves, only assignee can move)
        /// </summary>
        public bool TestMoveTaskPositiveCase()
        {
            t.AddTask("shay.klein11@gmail.com", "name", "task4", new DateTime(2026, 4, 10), "task is created");
            t.EditTask("shay.klein11@gmail.com", "name", id, "task4", new DateTime(2026, 4, 10), "checking if task is edited");
            res = JsonSerializer.Deserialize<Response>(t.MoveTask("shay.klein11@gmail.com", "name", id, 1));
            id++;

            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This test checks if moving a task to an invalid column fails.
        /// Requirement: 19 (only allowed moves, only assignee can move)
        /// </summary>
        public bool TestMoveTaskNegativeCase()
        {
            t.AddTask("shay.klein11@gmail.com", "name", "task5", new DateTime(2026, 4, 10), "task is created");
            res = JsonSerializer.Deserialize<Response>(t.MoveTask("shay.klein11@gmail.com", "name", id, 2));
            id++;

            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// This test checks if a task can be retrieved successfully.
        /// Requirement: 5 (task attributes)
        /// </summary>
        public bool TestGetTaskPositiveCase()
        {
            t.AddTask("shay.klein11@gmail.com", "name", "task6", new DateTime(2026, 4, 10), "task is created");
            res = JsonSerializer.Deserialize<Response>(t.GetTask("shay.klein11@gmail.com", "name", id));
            id++;

            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This test checks if retrieving a non-existent task fails.
        /// Requirement: 5 (task attributes)
        /// </summary>
        public bool TestGetTaskNegativeCase()
        {
            t.AddTask("shay.klein11@gmail.com", "name", "task7", new DateTime(2026, 4, 10), "task is created");
            id++;
            res = JsonSerializer.Deserialize<Response>(t.GetTask("shay.klein11@gmail.com", "name", id + 1));

            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// This test checks if a task can be assigned to another user by a board member.
        /// Requirement: 23 (assignment rules)
        /// </summary>
        public bool TestAssignTaskToUserPositiveCase()
        {
            b.CreateBoard("Shauli@gmail.com", "ABCD");
            t.AddTask("Shauli@gmail.com", "ABCD", "tasktome", new DateTime(2026, 4, 10), "task is created");
            cnt=11;
            b.JoinBoard("shay.klein11@gmail.com", cnt);
            Response res = JsonSerializer.Deserialize<Response>(t.AssignTaskToUser("Shauli@gmail.com", "ABCD", 0, "shay.klein11@gmail.com"));
            id++;

            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This test checks if a task can be assigned to a different user by a board member.
        /// Requirement: 23 (assignment rules)
        /// </summary>
        public bool TestAssignTaskToUserPositiveCase1()
        {
            t.AddTask("Shauli@gmail.com", "ABCD", "task for trump", new DateTime(2026, 4, 10), "task is for donald");
            b.JoinBoard("DonaldTrump@gmail.com", cnt);
            Response res = JsonSerializer.Deserialize<Response>(t.AssignTaskToUser("Shauli@gmail.com", "ABCD", 1, "DonaldTrump@gmail.com"));
            id++;
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This test checks if assigning a non-existent task fails.
        /// Requirement: 23 (assignment rules)
        /// </summary>
        public bool TestAssignTaskToUserNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(t.AssignTaskToUser("Shauli@gmail.com", "ABCD", 2, "shay.klein11@gmail.com"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This test checks if assigning a task with an invalid user fails.
        /// Requirement: 23 (assignment rules)
        /// </summary>
        public bool TestAssignTaskToUserNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(t.AssignTaskToUser("Shauli@gmail.co", "USA", 0, "DonaldTrump@gmail.com"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        
    }
}