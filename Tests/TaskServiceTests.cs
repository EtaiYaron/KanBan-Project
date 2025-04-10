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
        private TaskService ts = new TaskService(new BoardFacade(new AuthenticationFacade()));
        private readonly BoardService b = new BoardService(new BoardFacade(new AuthenticationFacade()));
        public void TaskServiceRunTests(){
            Console.WriteLine("Running Tests...");

            TaskServiceTests testsInstance = new TaskServiceTests();
            bool tests = testsInstance.TestAddTaskPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestAddTaskPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestAddTaskPositiveCase: Failed");
            }
            tests = testsInstance.TestAddTaskNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestAddTaskNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestAddTaskNegativeCase: Failed");
            }
            tests = testsInstance.TestEditTaskPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestEditTaskPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestEditTaskPositiveCase: Failed");
            }
            tests = testsInstance.TestEditTaskNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestEditTaskNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestEditTaskNegativeCase: Failed");
            }
            tests = testsInstance.TestMoveTaskPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestMoveTaskPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestMoveTaskPositiveCase: Failed");
            }
            tests = testsInstance.TestMoveTaskNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestMoveTaskNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestMoveTaskNegativeCase: Failed");
            }
            tests = testsInstance.TestGetTaskPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestGetTaskPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetTaskPositiveCase: Failed");
            }
            tests = testsInstance.TestGetTaskNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestGetTaskNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetTaskNegativeCase: Failed");
            }
            tests = testsInstance.TestGetAllTasksPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestGetAllTasksPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestGetAllTasksPositiveCase: Failed");
            }
            tests = testsInstance.TestGetTaskNegativeCase();
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
            Response res = ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "checking if task is created");
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }
        public bool TestAddTaskNegativeCase()
        {
            b.CreateBoard("name");
            Response res = ts.AddTask(b.GetId() + 1, 100, new DateTime(2025, 4, 10), "task1", "checking if task isn't created");
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }
        public bool TestEditTaskPositiveCase()
        {
            b.CreateBoard("name");
            ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = ts.EditTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task2", "checking if task is edited");
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }
        public bool TestEditTaskNegativeCase()
        {
            b.CreateBoard("name");
            ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = ts.EditTask(b.GetId() + 1, 100, new DateTime(2025, 4, 10), "task2", "checking if task isn't edited");
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }
        public bool TestMoveTaskPositiveCase()
        {
            b.CreateBoard("name");
            ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = ts.MoveTask(b.GetId(), "name", 100, 2);
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }
        public bool TestMoveTaskNegativeCase()
        {
            b.CreateBoard("name");
            ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = ts.MoveTask(b.GetId() , "name", 100, 7);
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }
        public bool TestGetTaskPositiveCase()
        {
            b.CreateBoard("name");
            ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = ts.GetTask(b.GetId(),  100);
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }
        public bool TestGetTaskNegativeCase()
        {
            b.CreateBoard("name");
            ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = ts.GetTask(b.GetId(), 101);
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }
        public bool TestGetAllTasksPositiveCase()
        {
            b.CreateBoard("name");
            ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = ts.GetTask(b.GetId());
            if (res.ErrorMessage == null)
            {
                return true;
            }
            return false;
        }
        public bool TestGetAllTasksNegativeCase()
        {
            b.CreateBoard("name");
            ts.AddTask(b.GetId(), 100, new DateTime(2025, 4, 10), "task1", "task is created");
            Response res = ts.GetTask(b.GetId() + 1);
            if (res.ErrorMessage == null)
            {
                return false;
            }
            return true;
        }
`       
    }
}
