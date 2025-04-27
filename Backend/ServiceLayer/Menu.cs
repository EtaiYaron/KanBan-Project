using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Menu
    {
        private ServiceFactory serviceFactory;
        private string email;
        private bool isLoggedIn = false;

        public Menu()
        {
            serviceFactory = new ServiceFactory();
            if(!isLoggedIn)
                ShowMenu();
            else
                BoardActionsMenu();
        }
        public void ShowMenu()
        {
            int entered1, entered2;
            Response response;
            do
            {
                Console.WriteLine("Hello! would you like to:");
                Console.WriteLine("1. Register or 2. log in");
                Console.WriteLine("write the number of your desired action");
                entered1 = int.Parse(Console.ReadLine());
            } while (entered1 != 1 && entered1 != 2);
            Console.Clear();

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
                    Console.Clear();

                    if (entered2 == 1)
                    {
                        ShowMenu();
                        return;
                    }
                    entered2 = 0;
                    Console.WriteLine("please enter your email");
                    email = Console.ReadLine();
                    Console.WriteLine("please enter your password");
                    string password = Console.ReadLine();
                    response = serviceFactory.UserService.Register(email, password);
                } while (response.ErrorMessage != null);
                Console.Clear();
                Console.WriteLine("Registration successful");
                Console.WriteLine("Welcome " + email);
                isLoggedIn = true;
                serviceFactory.initializeBoardFacade(email);
                do
                {
                    Console.WriteLine("if you wish to go Log In enter 2 if you to Exit enter 3");
                    entered1 = int.Parse(Console.ReadLine());
                } while (entered1 != 1 && entered1 != 2);
                Console.Clear();
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
                    Console.Clear();
                    if (entered2 == 1)
                    {
                        ShowMenu();
                        return;
                    }
                    entered2 = 0;
                    Console.WriteLine("please enter your email");
                    email = Console.ReadLine();
                    Console.WriteLine("please enter your password");
                    string password = Console.ReadLine();
                    response = serviceFactory.UserService.Login(email, password);
                } while (response.ErrorMessage != null);
                Console.Clear();
                Console.WriteLine("Login successful");
                Console.WriteLine("Welcome " + email);
                isLoggedIn = true;
                serviceFactory.initializeBoardFacade(email);
                do
                {
                    Console.WriteLine("if you wish to go proceed enter 1 if you to Log Out enter 2");
                    entered1 = int.Parse(Console.ReadLine());
                } while (entered1 != 1 && entered1 != 2);
                Console.Clear();
                if (entered1 == 2)
                {
                    do
                    {
                        Console.WriteLine("if you wish to go proceed enter 1 if you sure you want to Log Out enter 2");
                        entered2 = int.Parse(Console.ReadLine());
                    } while (entered2 != 1 && entered2 != 2);
                    Console.Clear();
                    if (entered2 == 1)
                    {
                        BoardActionsMenu();
                        return;
                    }
                    response = serviceFactory.UserService.Logout(email);
                    if (response.ErrorMessage != null)
                    {
                        Console.WriteLine(response.ErrorMessage);
                        return;
                    }
                    Console.WriteLine("Logout successful");
                    isLoggedIn = false;
                    ShowMenu();
                }
                else
                {
                    BoardActionsMenu();
                }
            }
        }
        public void BoardActionsMenu() { 

            int entered1, entered2;
            Response response;
            Console.WriteLine("Welcome to the Kanban board");
            do
            {
                Console.WriteLine("click the number that match to the operation you want to do:");
                Console.WriteLine("1.create a board");
                Console.WriteLine("2. delete a board");
                Console.WriteLine("3. get a board");
                Console.WriteLine("4. get all boards");
                Console.WriteLine("5. get all tasks");
                Console.WriteLine("6. get a task");
                Console.WriteLine("7. edit a task");
                Console.WriteLine("8. move a task");
                Console.WriteLine("9. limit tasks");
                Console.WriteLine("10. add task");
                Console.WriteLine("11. log out");
                entered1 = int.Parse(Console.ReadLine());


            } while (entered1 < 1 || entered1 > 10);
            Console.Clear();
            if (entered1 == 1)
            {
                do
                {
                    do
                    {
                        Console.WriteLine("if you wish to go back enter 1 if you wish to proceed enter 2");
                        entered2 = int.Parse(Console.ReadLine());
                    } while (entered2 != 1 && entered2 != 2);
                    Console.Clear();
                    if (entered2 == 1)
                    {
                        BoardActionsMenu();
                        return;
                    }
                    Console.WriteLine("please enter the name of the board");
                    string boardName = Console.ReadLine();
                    response = serviceFactory.BoardService.CreateBoard(boardName);
                } while (response.ErrorMessage != null);
                Console.Clear();
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
                    Console.Clear();
                    if (entered2 == 1)
                    {
                        BoardActionsMenu();
                        return;
                    }
                    Console.WriteLine("please enter the name of the board");
                    string boardName = Console.ReadLine();
                    response = serviceFactory.BoardService.DeleteBoard(boardName);
                } while (response.ErrorMessage != null);
                Console.Clear();
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
                    Console.Clear();
                    if (entered2 == 1)
                    {
                        BoardActionsMenu();
                        return;
                    }
                    Console.WriteLine("please enter the name of the board");
                    string boardName = Console.ReadLine();
                    response = serviceFactory.BoardService.GetBoard(boardName);
                } while (response.ErrorMessage != null);
                Console.Clear();
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
                Console.Clear();
                if (entered2 == 1)
                {
                    BoardActionsMenu();
                    return;
                }
                response = serviceFactory.BoardService.getAllUserBoards();
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
                Console.Clear();
                if (entered2 == 1)
                {
                    BoardActionsMenu();
                    return;
                }
                Console.WriteLine("please enter the name of the board");
                string boardName = Console.ReadLine();
                response = serviceFactory.TaskService.GetAllTasks(boardName);
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
                    Console.Clear();
                    if (entered2 == 1)
                    {
                        BoardActionsMenu();
                        return;
                    }
                    Console.WriteLine("please enter the name of the board");
                    string boardName = Console.ReadLine();
                    Console.WriteLine("please enter the id of the task");
                    int taskId = int.Parse(Console.ReadLine());
                    response = serviceFactory.TaskService.GetTask(boardName, taskId);
                } while (response.ErrorMessage != null);
                Console.Clear();
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
                    Console.Clear();
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
                    response = serviceFactory.TaskService.EditTask(boardName, taskId, title, dueDate, description);
                } while (response.ErrorMessage != null);
                Console.Clear();
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
                    Console.Clear();
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
                    response = serviceFactory.TaskService.MoveTask(boardName, taskId, dest);
                } while (response.ErrorMessage != null);
                Console.Clear();
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
                    Console.Clear();
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
                    response = serviceFactory.BoardService.LimitTasks(boardName, col, newLimit);
                } while (response.ErrorMessage != null);
                Console.Clear();
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
                    Console.Clear();
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
                    response = serviceFactory.TaskService.AddTask(boardName, taskId, title, dueDate, description);
                } while (response.ErrorMessage != null);
                Console.Clear();
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
                Console.Clear();
                if (entered2 == 1)
                {
                    BoardActionsMenu();
                    return;
                }
                response = serviceFactory.UserService.Logout(email);
                if (response.ErrorMessage != null)
                {
                    Console.WriteLine(response.ErrorMessage);
                    return;
                }
                Console.WriteLine("Logout successful");
                isLoggedIn = false;
                ShowMenu();
            }
        }
    }
}
