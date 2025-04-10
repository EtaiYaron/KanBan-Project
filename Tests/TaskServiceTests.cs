using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Board;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace Tests
{
    class TaskServiceTests
    {
        private BoardService b;
        private TaskService t;

        public TaskServiceTests(BoardService b, TaskService t)
        {
            this.b = b;
            this.t = t;
        }

        public void TaskServiceRunTests()
        {
            Console.WriteLine("Running Tests...");

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

        public bool TestAddTaskPositiveCase()
        {
            b.CreateBoard("name");
            Response res = t.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "checking if task is created");
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }
        public bool TestAddTaskNegativeCase()
        {
            b.CreateBoard("name");
            Response res = t.AddTask(b.GetId() + 1, 100, new DateTime(2025, 4, 10), "task1", "checking if task isn't created");
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }
        public bool TestEditTaskPositiveCase()
        {
            b.CreateBoard("name");
            t.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = t.EditTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task2", "checking if task is edited");
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }
        public bool TestEditTaskNegativeCase()
        {
            b.CreateBoard("name");
            t.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = t.EditTask(b.GetId() + 1, 100, new DateTime(2025, 4, 10), "task2", "checking if task isn't edited");
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }
        public bool TestMoveTaskPositiveCase()
        {
            b.CreateBoard("name");
            t.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = t.MoveTask(b.GetId(), "name", 100, 2);
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }
        public bool TestMoveTaskNegativeCase()
        {
            b.CreateBoard("name");
            t.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = t.MoveTask(b.GetId() , "name", 100, 7);
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }
        public bool TestGetTaskPositiveCase()
        {
            b.CreateBoard("name");
            t.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = t.GetTask(b.GetId(),  100);
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }
        public bool TestGetTaskNegativeCase()
        {
            b.CreateBoard("name");
            t.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = t.GetTask(b.GetId(), 101);
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }
        public bool TestGetAllTasksPositiveCase()
        {
            b.CreateBoard("name");
            t.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = t.GetTask(b.GetId());
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }
        public bool TestGetAllTasksNegativeCase()
        {
            b.CreateBoard("name");
            t.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = t.GetTask(b.GetId() + 1);
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }      
    }
}
