using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class TaskServiceTests
    {
        private TaskService ts = new TaskService();
        private Board b = new Board();
        public void TaskServiceRunTests(){
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

        public static bool TestAddTaskPositiveCase()
        {
            b.CreateBoard("name");
            Response res = ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "checking if task is created");
            if (res.Errormsg == null)
            {
                return true;
            }
            return false;
        }
        public static bool TestAddTaskNegativeCase()
        {
            b.CreateBoard("name");
            Response res = ts.AddTask(b.GetId() + 1, 100, new DateTime(2025, 4, 10), "task1", "checking if task isn't created");
            if (res.Errormsg == null)
            {
                return false;
            }
            return true;
        }
        public static bool TestEditTaskPositiveCase()
        {
            b.CreateBoard("name");
            ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = ts.EditTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task2", "checking if task is edited");
            if (res.Errormsg == null)
            {
                return true;
            }
            return false;
        }
        public static bool TestEditTaskNegativeCase()
        {
            b.CreateBoard("name");
            ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = ts.EditTask(b.GetId() + 1, 100, new DateTime(2025, 4, 10), "task2", "checking if task isn't edited");
            if (res.Errormsg == null)
            {
                return false;
            }
            return true;
        }
        public static bool TestMoveTaskPositiveCase()
        {
            b.CreateBoard("name");
            ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = ts.MoveTask(b.GetId(), "name", 100, 2);
            if (res.Errormsg == null)
            {
                return true;
            }
            return false;
        }
        public static bool TestMoveTaskNegativeCase()
        {
            b.CreateBoard("name");
            ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = ts.MoveTask(b.GetId() , "name", 100, 7);
            if (res.Errormsg == null)
            {
                return false;
            }
            return true;
        }
        public static bool TestGetTaskPositiveCase()
        {
            b.CreateBoard("name");
            ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = ts.GetTask(b.GetId(),  100);
            if (res.Errormsg == null)
            {
                return true;
            }
            return false;
        }
        public static bool TestGetTaskNegativeCase()
        {
            b.CreateBoard("name");
            ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = ts.GetTask(b.GetId(), 101);
            if (res.Errormsg == null)
            {
                return false;
            }
            return true;
        }
        public static bool TestGetAllTasksPositiveCase()
        {
            b.CreateBoard("name");
            ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = ts.GetTask(b.GetId());
            if (res.Errormsg == null)
            {
                return true;
            }
            return false;
        }
        public static bool TestGetAllTasksNegativeCase()
        {
            b.CreateBoard("name");
            ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = ts.GetTask(b.GetId() + 1);
            if (res.Errormsg == null)
            {
                return false;
            }
            return true;
        }
`       
    }
}
