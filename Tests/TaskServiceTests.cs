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
        Response res;
        int id;

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

            // Test retrieving all tasks successfully (Requirement 17)
            tests = TestGetAllTasksPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestGetAllTasksPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetAllTasksPositiveCase: Failed");
            }

            // Test retrieving all tasks with invalid data (Requirement 17)
            tests = TestGetAllTasksNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestGetAllTasksNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetAllTasksNegativeCase: Failed");
            }
        }

        public void Before()
        {
            us.Register("yaronet@post.bgu.ac.il", "Admin1");
            b.CreateBoard("yaronet@post.bgu.ac.il", "name");
            res = JsonSerializer.Deserialize<Response>(t.AddTask("yaronet@post.bgu.ac.il", "name", "task0", new DateTime(2026, 4, 10), "checking if task is created"));
            id = 0;
        }

        public bool TestAddTaskPositiveCase()
        {
            // This test checks if a task can be added successfully to the backlog column (Requirement 13, 5)
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        public bool TestAddTaskNegativeCase()
        {
            // This test checks if adding a task with invalid data (e.g., past due date) fails (Requirement 5)
            res = JsonSerializer.Deserialize<Response>(t.AddTask("yaronet@post.bgu.ac.il", "name", "task1", new DateTime(2025, 4, 10), "checking if task isn't created"));
            id++;
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        public bool TestEditTaskPositiveCase()
        {
            // This test checks if a task can be edited successfully (Requirement 15, 16)
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task2", new DateTime(2026, 4, 10), "task is created");
            id++;
            res = JsonSerializer.Deserialize<Response>(t.EditTask("yaronet@post.bgu.ac.il", "name", id, "task2", new DateTime(2027, 4, 10), "checking if task is edited"));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        public bool TestEditTaskNegativeCase()
        {
            // This test checks if editing a non-existent task fails (Requirement 15, 16)
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task3", new DateTime(2026, 4, 10), "task is created");
            id++;
            res = JsonSerializer.Deserialize<Response>(t.EditTask("yaronet@post.bgu.ac.il", "name", id + 1, "task3", new DateTime(2026, 4, 10), "checking if task is edited"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        public bool TestMoveTaskPositiveCase()
        {
            // This test checks if a task can be moved between valid columns (Requirement 14)
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task4", new DateTime(2026, 4, 10), "task is created");
            id++;
            t.EditTask("yaronet@post.bgu.ac.il", "name", id, "task4", new DateTime(2026, 4, 10), "checking if task is edited");
            res = JsonSerializer.Deserialize<Response>(t.MoveTask("yaronet@post.bgu.ac.il", "name", id, 1));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        public bool TestMoveTaskNegativeCase()
        {
            // This test checks if moving a task to an invalid column fails (Requirement 14)
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task5", new DateTime(2026, 4, 10), "task is created");
            id++;
            res = JsonSerializer.Deserialize<Response>(t.MoveTask("yaronet@post.bgu.ac.il", "name", id, 2));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        public bool TestGetTaskPositiveCase()
        {
            // This test checks if a task can be retrieved successfully (Requirement 5)
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task6", new DateTime(2026, 4, 10), "task is created");
            id++;
            res = JsonSerializer.Deserialize<Response>(t.GetTask("yaronet@post.bgu.ac.il", "name", id));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        public bool TestGetTaskNegativeCase()
        {
            // This test checks if retrieving a non-existent task fails (Requirement 5)
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task7", new DateTime(2026, 4, 10), "task is created");
            id++;
            res = JsonSerializer.Deserialize<Response>(t.GetTask("yaronet@post.bgu.ac.il", "name", id + 1));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }

        public bool TestGetAllTasksPositiveCase()
        {
            // This test checks if all tasks can be retrieved successfully from a board (Requirement 17)
            res = JsonSerializer.Deserialize<Response>(t.GetAllTasks("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }

        public bool TestGetAllTasksNegativeCase()
        {
            // This test checks if retrieving tasks from a non-existent board fails (Requirement 17)
            res = JsonSerializer.Deserialize<Response>(t.GetAllTasks("yaronet@post.bgu.ac.il", "name2"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }
    }
}
