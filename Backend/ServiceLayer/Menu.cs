using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    class Menu
    {
        private static ServiceFactory serviceFactory = new ServiceFactory();
        private string email;
        private string password;
        private bool isLoggedIn = false;

        public Menu()
        {
           if(!isLoggedIn)
                ShowMenu();
            else
                BoardActionsMenu();


        }
        public void ShowMenu()
        {
            int entered1, entered2;
            do
            {
                Console.WriteLine("Hello! would you like to:");
                Console.WriteLine("1. Register or 2. log in");
                Console.WriteLine("write the number of your desired action");
                entered1 = int.Parse(Console.ReadLine());
            } while (entered1 != 1 && entered1 != 2);
            if (entered1 == 1)
            {
                entered1 = 0;
                do
                {
                    do
                    {
                        Console.WriteLine("if you wish to go back to the Menu enter 1 if you wish to proceed enter 2");
                        entered2 = int.Parse(Console.ReadLine());
                    } while (entered2 != 1 && entered2 != 2);
                    if (entered2 == 1)
                    {
                        ShowMenu();
                        return;
                    }
                    entered2 = 0;
                    Console.WriteLine("please enter your email");
                    email = Console.ReadLine();
                    Console.WriteLine("please enter your password");
                    password = Console.ReadLine();
                    Response response = serviceFactory.UserService.Register(email, password);
                } while (response.ErrorMessage != null);
                Console.WriteLine("Registration successful");
                Console.WriteLine("Welcome " + email);

                do
                {
                    Console.WriteLine("if you wish to go Log In enter 2 if you to Exit enter 3");
                    entered1 = int.Parse(Console.ReadLine());
                } while (entered1 != 1 && entered1 != 2);
                if (entered1 == 3)
                    return;
            }
            if (entered1 == 2)
            {
                entered1 = 0;
                do
                {
                    do
                    {
                        Console.WriteLine("if you wish to go back to the Menu enter 1 if you wish to proceed enter 2");
                        entered2 = int.Parse(Console.ReadLine());
                    } while (entered2 != 1 && entered2 != 2);
                    if (entered2 == 1)
                    {
                        ShowMenu();
                        return;
                    }
                    entered2 = 0;
                    Console.WriteLine("please enter your email");
                    email = Console.ReadLine();
                    Console.WriteLine("please enter your password");
                    password = Console.ReadLine();
                    Response response = serviceFactory.UserService.Login(email, password);
                } while (response.ErrorMessage != null);
                Console.WriteLine("Login successful");
                Console.WriteLine("Welcome " + email);
                isLoggedIn = true;
                do
                {
                    Console.WriteLine("if you wish to go proceed enter 1 if you to Log Out enter 2");
                    entered1 = int.Parse(Console.ReadLine());
                } while (entered1 != 1 && entered1 != 2);
                if (entered1 == 2)
                {
                    do
                    {
                        Console.WriteLine("if you wish to go proceed enter 1 if you sure you want to Log Out enter 2");
                        entered2 = int.Parse(Console.ReadLine());
                    } while (entered2 != 1 && entered2 != 2);
                    if (entered2 == 1)
                    {
                        BoardActionsMenu();
                        return;
                    }
                    Response response = serviceFactory.UserService.Logout(email);
                    if (response.ErrorMessage != null)
                    {
                        Console.WriteLine(response.ErrorMessage);
                        return;
                    }
                    Console.WriteLine("Logout successful");
                    return;
                }
                else
                {
                    BoardActionsMenu();
                }
            }
        }
        public void BoardActionsMenu() { 

            int entered1, entered2;
            Console.WriteLine("Welcome to the Kanban board");
            do
            {
                Console.WriteLine("if you wish to create a board enter 1");
                Console.WriteLine("if you wish to delete a board enter 2");
                Console.WriteLine("if you wish to get a board enter 3");
                Console.WriteLine("if you wish to get all boards enter 4");
                Console.WriteLine("if you wish to get all tasks enter 5");
                Console.WriteLine("if you wish to get a task enter 6");
                Console.WriteLine("if you wish to edit a task enter 7");
                Console.WriteLine("if you wish to move a task enter 8");
                Console.WriteLine("if you wish to limit tasks enter 9");
                Console.WriteLine("if you wish to add task enter 10");
                Console.WriteLine("if you wish to log out enter 11");
                entered1 = int.Parse(Console.ReadLine());


            } while (entered1 < 1 || entered1 > 10);
            if (entered1 == 1)
            {
                do
                {
                    do
                    {
                        Console.WriteLine("if you wish to go back enter 1 if you wish to proceed enter 2");
                        entered2 = int.Parse(Console.ReadLine());
                    } while (entered2 != 1 && entered2 != 2);
                    if (entered2 == 1)
                    {
                        BoardActionsMenu();
                        return;
                    }
                    Console.WriteLine("please enter the name of the board");
                    string boardName = Console.ReadLine();
                    Response response = serviceFactory.BoardService.CreateBoard(boardName);
                } while (response.ErrorMessage != null);
                Console.WriteLine("Board created successfully");
                BoardActionsMenu();
            }
            if (entered1 == 2)
            {
                do
                {
                    do
                    {
                        Console.WriteLine("if you wish to go back enter 1 if you wish to proceed enter 2");
                        entered2 = int.Parse(Console.ReadLine());
                    } while (entered2 != 1 && entered2 != 2);
                    if (entered2 == 1)
                    {
                        BoardActionsMenu();
                        return;
                    }
                    Console.WriteLine("please enter the name of the board");
                    string boardName = Console.ReadLine();
                    Response response = serviceFactory.BoardService.DeleteBoard(boardName);
                } while (response.ErrorMessage != null);
                Console.WriteLine("Board deleted successfully");
                BoardActionsMenu();

            }
            if (entered1 == 3)
            {               
                do
                {
                    do
                    {
                        Console.WriteLine("if you wish to go back enter 1 if you wish to proceed enter 2");
                        entered2 = int.Parse(Console.ReadLine());
                    } while (entered2 != 1 && entered2 != 2);
                    if (entered2 == 1)
                    {
                        BoardActionsMenu();
                        return;
                    }
                    Console.WriteLine("please enter the name of the board");
                    string boardName = Console.ReadLine();
                    Response response = serviceFactory.BoardService.GetId(boardName);
                } while (response.ErrorMessage != null);
                Console.WriteLine("Board retrieved successfully");
                BoardActionsMenu();

            }
            if (entered1 == 4)
            {
                do
                {
                    Console.WriteLine("if you wish to go back enter 1 if you wish to proceed enter 2");
                    entered2 = int.Parse(Console.ReadLine());
                } while (entered2 != 1 && entered2 != 2);
                if (entered2 == 1)
                {
                    BoardActionsMenu();
                    return;
                }
                Response response = serviceFactory.BoardService.GetAllBoards();
                if (response.ErrorMessage != null)
                {
                    Console.WriteLine(response.ErrorMessage);
                    return;
                }
                Console.WriteLine("All boards retrieved successfully");
                BoardActionsMenu();

            }
            if (entered1 == 5)
            {
                do
                {
                    Console.WriteLine("if you wish to go back enter 1 if you wish to proceed enter 2");
                    entered2 = int.Parse(Console.ReadLine());
                } while (entered2 != 1 && entered2 != 2);
                if (entered2 == 1)
                {
                    BoardActionsMenu();
                    return;
                }
                Console.WriteLine("please enter the name of the board");
                string boardName = Console.ReadLine();
                Response response = serviceFactory.BoardService.GetAllTasks(boardName);
                if (response.ErrorMessage != null)
                {
                    Console.WriteLine(response.ErrorMessage);
                    return;
                }
                Console.WriteLine("All tasks retrieved successfully");
                BoardActionsMenu();

            }
            if (entered1 == 6)
            {                
                do
                {
                    do
                    {
                        Console.WriteLine("if you wish to go back enter 1 if you wish to proceed enter 2");
                        entered2 = int.Parse(Console.ReadLine());
                    } while (entered2 != 1 && entered2 != 2);
                    if (entered2 == 1)
                    {
                        BoardActionsMenu();
                        return;
                    }
                    Console.WriteLine("please enter the name of the board");
                    string boardName = Console.ReadLine();
                    Console.WriteLine("please enter the id of the task");
                    int taskId = int.Parse(Console.ReadLine());
                    Response response = serviceFactory.BoardService.GetTask(boardName, taskId);
                } while (response.ErrorMessage != null);
                Console.WriteLine("Task retrieved successfully");
                BoardActionsMenu();

            }
            if (entered1 == 7)
            {
                do
                {
                    do
                    {
                        Console.WriteLine("if you wish to go back enter 1 if you wish to proceed enter 2");
                        entered2 = int.Parse(Console.ReadLine());
                    } while (entered2 != 1 && entered2 != 2);
                    if (entered2 == 1)
                    {
                        BoardActionsMenu();
                        return;
                    }
                    Console.WriteLine("please enter the name of the board");
                    string boardName = Console.ReadLine();
                    Console.WriteLine("please enter the id of the task");
                    int taskId = int.Parse(Console.ReadLine());
                    Console.WriteLine("please enter the new title of the task");
                    string title = Console.ReadLine();
                    Console.WriteLine("please enter the new due date of the task");
                    DateTime dueDate = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("please enter the new description of the task");
                    string description = Console.ReadLine();
                    Response response = serviceFactory.BoardService.EditTask(boardName, taskId, title, dueDate, description);
                } while (response.ErrorMessage != null);
                Console.WriteLine("Task edited successfully");
                BoardActionsMenu();

            }
            if (entered1 == 8)
            {
                do
                {
                    do
                    {
                        Console.WriteLine("if you wish to go back enter 1 if you wish to proceed enter 2");
                        entered2 = int.Parse(Console.ReadLine());
                    } while (entered2 != 1 && entered2 != 2);
                    if (entered2 == 1)
                    {
                        BoardActionsMenu();
                        return;
                    }
                    Console.WriteLine("please enter the name of the board");
                    string boardName = Console.ReadLine();
                    Console.WriteLine("please enter the id of the task");
                    int taskId = int.Parse(Console.ReadLine());
                    Console.WriteLine("please enter the destination column of the task");
                    int dest = int.Parse(Console.ReadLine());
                    Response response = serviceFactory.BoardService.MoveTask(boardName, taskId, dest);
                } while (response.ErrorMessage != null);
                Console.WriteLine("Task moved successfully");
                BoardActionsMenu();

            }
            if (entered1 == 9)
            {
                do
                {
                    do
                    {
                        Console.WriteLine("if you wish to go back enter 1 if you wish to proceed enter 2");
                        entered2 = int.Parse(Console.ReadLine());
                    } while (entered2 != 1 && entered2 != 2);
                    if (entered2 == 1)
                    {
                        BoardActionsMenu();
                        return;
                    }
                    Console.WriteLine("please enter the name of the board");
                    string boardName = Console.ReadLine();
                    Console.WriteLine("please enter the column of the task");
                    int col = int.Parse(Console.ReadLine());
                    Console.WriteLine("please enter the new limit of the task");
                    int newLimit = int.Parse(Console.ReadLine());
                    Response response = serviceFactory.BoardService.LimitTasks(boardName, col, newLimit);
                } while (response.ErrorMessage != null);
                Console.WriteLine("Task limit set successfully");
                BoardActionsMenu();
            }
            if (entered1 == 10)
            {
                do
                {
                    do
                    {
                        Console.WriteLine("if you wish to go back enter 1 if you wish to proceed enter 2");
                        entered2 = int.Parse(Console.ReadLine());
                    } while (entered2 != 1 && entered2 != 2);
                    if (entered2 == 1)
                    {
                        BoardActionsMenu();
                        return;
                    }
                    Console.WriteLine("please enter the name of the board");
                    string boardName = Console.ReadLine();
                    Console.WriteLine("please enter the id of the task");
                    int taskId = int.Parse(Console.ReadLine());
                    Console.WriteLine("please enter the title of the task");
                    string title = Console.ReadLine();
                    Console.WriteLine("please enter the due date of the task (yyyy-MM-dd)");
                    DateTime dueDate = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("please enter the description of the task");
                    string description = Console.ReadLine();
                    Response response = serviceFactory.TaskService.AddTask(boardName, taskId, title, dueDate, description);
                } while (response.ErrorMessage != null);
                Console.WriteLine("Task added successfully");
                BoardActionsMenu();
            }
            else
            {
                do
                {
                    Console.WriteLine("if you wish to go back enter 1 if you wish to proceed enter 2");
                    entered2 = int.Parse(Console.ReadLine());
                } while (entered2 != 1 && entered2 != 2);
                if (entered2 == 1)
                {
                    BoardActionsMenu();
                    return;
                }
                Response response = serviceFactory.UserService.Logout(email);
                if (response.ErrorMessage != null)
                {
                    Console.WriteLine(response.ErrorMessage);
                    return;
                }
                Console.WriteLine("Logout successful");
                return;
            }
        }
    }
}
