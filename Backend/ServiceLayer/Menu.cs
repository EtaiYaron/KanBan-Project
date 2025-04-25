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

        public static void ShowMenu()
        {
            Console.WriteLine("Hello! would you like to:");
            Console.WriteLine("1. Register or 2. log in");
            Console.WriteLine("write the number of your desired action");
            String s = Console.ReadLine();
            while (s != "1" && s != "2")
            {
                Console.WriteLine("Invalid input, please enter 1 or 2 as your desired action");
                s = Console.ReadLine();
            }
            if (s == "1")
            {
                Console.WriteLine("please enter your email");
                String email = Console.ReadLine();
                Console.WriteLine("please enter your password");
                String password = Console.ReadLine();
                Response response = serviceFactory.UserService.Register(email, password);
                while (response.ErrorMessage != null)
                {
                    Console.WriteLine(response.ErrorMessage);
                    Console.WriteLine("if you wish to go back to the menu enter 1 if you wish to try again enter 2 if you wish to exit enter 3");
                    String s1 = Console.ReadLine();
                    while (s1 != "1" && s1 != "2" && s1 != "3")
                    {
                        Console.WriteLine("Invalid input, please enter 1 or 2 or 3 as your desired action");
                        s1 = Console.ReadLine();
                    }
                    if (s1 == "1")
                    {
                        ShowMenu();
                        return;
                    }
                    else if (s1 == "3")
                    {
                        Console.WriteLine("Goodbye");
                        return;
                    }
                    Console.WriteLine("please enter your email again");
                    email = Console.ReadLine();
                    Console.WriteLine("please enter your password again");
                    password = Console.ReadLine();
                }
                Console.WriteLine("Registration successful");
                Console.WriteLine("Welcome " + email);
            }
            else
            {
                Console.WriteLine("please enter your email");
                String email = Console.ReadLine();
                Console.WriteLine("please enter your password");
                String password = Console.ReadLine();
                Response response = serviceFactory.UserService.Login(email, password);
                while (response.ErrorMessage != null)
                {
                    Console.WriteLine(response.ErrorMessage);
                    Console.WriteLine("if you wish to go back to the menu enter 1 if you wish to try again enter 2 if you wish to exit enter 3");
                    String s1 = Console.ReadLine();
                    while (s1 != "1" && s1 != "2" && s1 != "3")
                    {
                        Console.WriteLine("Invalid input, please enter 1 or 2 or 3 as your desired action");
                        s1 = Console.ReadLine();
                    }
                    if (s1 == "1")
                    {
                        ShowMenu();
                        return;
                    }
                    else if (s1 == "3")
                    {
                        Console.WriteLine("Goodbye");
                        return;
                    }
                    Console.WriteLine("please enter your email again");
                    email = Console.ReadLine();
                    Console.WriteLine("please enter your password again");
                    password = Console.ReadLine();
                }
                Console.WriteLine("Login successful");
                Console.WriteLine("Welcome " + email);
            }
        }
    }
}
