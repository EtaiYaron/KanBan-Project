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

            bool tests = TestAddTaskPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestAddTaskPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestAddTaskPositiveCase: Failed");
            }
            tests = TestAddTaskNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestAddTaskNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestAddTaskNegativeCase: Failed");
            }
            tests = TestEditTaskPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestEditTaskPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestEditTaskPositiveCase: Failed");
            }
            tests = TestEditTaskNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestEditTaskNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestEditTaskNegativeCase: Failed");
            }
            tests = TestMoveTaskPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestMoveTaskPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestMoveTaskPositiveCase: Failed");
            }
            tests = TestMoveTaskNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestMoveTaskNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestMoveTaskNegativeCase: Failed");
            }
            tests = TestGetTaskPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestGetTaskPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetTaskPositiveCase: Failed");
            }
            tests = TestGetTaskNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestGetTaskNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetTaskNegativeCase: Failed");
            }
            tests = TestGetAllTasksPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestGetAllTasksPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetAllTasksPositiveCase: Failed");
            }
            tests = TestGetTaskNegativeCase();
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
            res = JsonSerializer.Deserialize < Response > (t.AddTask("yaronet@post.bgu.ac.il", "name", "task0", new DateTime(2026, 4, 10), "checking if task is created"));
        }

        public bool TestAddTaskPositiveCase()
        {
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }
        public bool TestAddTaskNegativeCase()
        {
            res = JsonSerializer.Deserialize < Response > (t.AddTask("yaronet@post.bgu.ac.il", "name", "task1", new DateTime(2025, 4, 10), "checking if task isn't created"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }
        public bool TestEditTaskPositiveCase()
        {
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task2", new DateTime(2026, 4, 10), "task is created");
            TaskSL task = ((TaskSL) res.ReturnValue);
            res = JsonSerializer.Deserialize<Response>(t.EditTask("yaronet@post.bgu.ac.il", "name", task.Id, "task2", new DateTime(2027, 4, 10), "checking if task is edited"));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }
        public bool TestEditTaskNegativeCase()
        {
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task3", new DateTime(2026, 4, 10), "task is created");
            TaskSL task = ((TaskSL)res.ReturnValue);
            res = JsonSerializer.Deserialize<Response>(t.EditTask("yaronet@post.bgu.ac.il", "name", task.Id+1, "task3", new DateTime(2026, 4, 10), "checking if task is edited"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }
        public bool TestMoveTaskPositiveCase()
        {
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task4", new DateTime(2026, 4, 10), "task is created");
            TaskSL task = ((TaskSL)res.ReturnValue);
            t.EditTask("yaronet@post.bgu.ac.il", "name", task.Id, "task4", new DateTime(2026, 4, 10), "checking if task is edited");
            res = JsonSerializer.Deserialize<Response>(t.MoveTask("yaronet@post.bgu.ac.il", "name", task.Id, 1));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }
        public bool TestMoveTaskNegativeCase()
        {
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task5", new DateTime(2026, 4, 10), "task is created");
            TaskSL task = ((TaskSL)res.ReturnValue);
            t.EditTask("yaronet@post.bgu.ac.il", "name", task.Id, "task5", new DateTime(2026, 4, 10), "checking if task is edited");
            res = JsonSerializer.Deserialize<Response>(t.MoveTask("yaronet@post.bgu.ac.il", "name", task.Id, 2));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }
        public bool TestGetTaskPositiveCase()
        {
            t.AddTask("yaronet@post.bgu.ac.il" ,"name", "task6", new DateTime(2026, 4, 10), "task is created");
            res = JsonSerializer.Deserialize < Response > (t.GetTask("yaronet@post.bgu.ac.il" ,"name", ((TaskSL)res.ReturnValue).Id));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }
        public bool TestGetTaskNegativeCase()
        {
            t.AddTask("yaronet@post.bgu.ac.il", "name", "task7", new DateTime(2026, 4, 10), "task is created");
            res = JsonSerializer.Deserialize < Response > (t.GetTask("yaronet@post.bgu.ac.il", "name", ((TaskSL)res.ReturnValue).Id +1));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }
        public bool TestGetAllTasksPositiveCase()
        {
            res = JsonSerializer.Deserialize < Response > (t.GetAllTasks("yaronet@post.bgu.ac.il", "name"));
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }
        public bool TestGetAllTasksNegativeCase()
        {
            res = JsonSerializer.Deserialize < Response > (t.GetAllTasks("yaronet@post.bgu.ac.il", "name2"));
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }      
    }
}
