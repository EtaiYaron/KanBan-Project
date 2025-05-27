using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;
using IntroSE.Kanban.Backend.BussinesLayer.User;
using IntroSE.Kanban.Backend.ServiceLayer;



namespace Tests
{
    class UserTests
    {
        private UserService us;

        public UserTests(UserService us)
        {
            this.us = us;
        }

        public void UserRunTests()
        {
            Console.WriteLine("Running Tests...");

            // Test registering a user successfully (Requirement 6)
            bool tests = TestUserRegisterPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestUserRegisterPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestUserRegisterPositiveCase: Failed");
            }
            /*
            // Test registering another user successfully (Requirement 6)
            tests = TestUserRegisterPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestUserRegisterPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestUserRegisterPositiveCase1: Failed");
            }

            // Test registering a user with invalid password (Requirement 2)
            tests = TestUserRegisterNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestUserRegisterNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestUserRegisterNegativeCase: Failed");
            }

            // Test registering a user with invalid email (Requirement 3)
            tests = TestUserRegisterNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestUserRegisterNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestUserRegisterNegativeCase1: Failed");
            }

            // Test logging in a user successfully (Requirement 7)
            tests = TestUserLoginPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestUserLoginPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLoginPositiveCase: Failed");
            }

            // Test logging in another user successfully (Requirement 7)
            tests = TestUserLoginPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestUserLoginPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLoginPositiveCase1: Failed");
            }

            // Test logging in with incorrect password (Requirement 7)
            tests = TestUserLoginNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestUserLoginNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLoginNegativeCase: Failed");
            }

            // Test logging in with invalid email (Requirement 7)
            tests = TestUserLoginNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestUserLoginNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLoginNegativeCase1: Failed");
            }

            // Test logging out a user successfully (Requirement 7)
            tests = TestUserLogoutPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestUserLogoutPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLogoutPositiveCase: Failed");
            }

            // Test logging out another user successfully (Requirement 6, 7)
            tests = TestUserLogoutPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestUserLogoutPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLogoutPositiveCase1: Failed");
            }

            // Test logging out with invalid email (Requirement 7)
            tests = TestUserLogoutNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestUserLogoutNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLogoutNegativeCase: Failed");
            }

            // Test logging out with incorrect email format (Requirement 7)
            tests = TestUserLogoutNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestUserLogoutNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLogoutNegativeCase1: Failed");
            }
            */
        }

        /// <summary>
        /// Checks if a user can successfully register with a valid email and password.
        /// Requirement: 6 (Registration of new users)
        /// </summary>
        public bool TestUserRegisterPositiveCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(us.Register("shay.klein11@gmail.com", "Password1"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if a user can successfully register with another valid email and password.
        /// Requirement: 6 (Registration of new users)
        /// </summary>
        public bool TestUserRegisterPositiveCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(us.Register("Amztia@post.co.il", "Amztia1"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if registration fails when the password does not meet the validity criteria.
        /// Requirement: 2 (Valid password rules)
        /// </summary>
        public bool TestUserRegisterNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(us.Register("Amztia@post.co.il", "amztia1"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if registration fails when the email is invalid.
        /// Requirement: 3 (Valid email address)
        /// </summary>
        public bool TestUserRegisterNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(us.Register("Amztiapost.co.il", "Amztia1"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a user can successfully login with valid credentials.
        /// Requirement: 7 (Login functionality)
        /// </summary>
        public bool TestUserLoginPositiveCase()
        {
            us.Logout("etaiyaron@gmail.com");
            Response res = JsonSerializer.Deserialize<Response>(us.Login("etaiyaron@gmail.com", "Password1"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if another user can successfully login with valid credentials.
        /// Requirement: 7 (Login functionality)
        /// </summary>
        public bool TestUserLoginPositiveCase1()
        {
            us.Logout("Amztia@post.co.il");
            Response res = JsonSerializer.Deserialize<Response>(us.Login("Amztia@post.co.il", "Amztia1"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if login fails when the password is incorrect.
        /// Requirement: 7 (Login functionality)
        /// </summary>
        public bool TestUserLoginNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(us.Login("etaiyaron@gmail.com", "password1"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if login fails when the email is invalid.
        /// Requirement: 7 (Login functionality)
        /// </summary>
        public bool TestUserLoginNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(us.Login("@gmail.com", "Password1"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a user can successfully logout.
        /// Requirement: 7 (Logout functionality)
        /// </summary>
        public bool TestUserLogoutPositiveCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(us.Logout("etaiyaron@gmail.com"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if another user can successfully logout after registering.
        /// Requirements: 6 (Registration logs in the user), 7 (Logout functionality)
        /// </summary>
        public bool TestUserLogoutPositiveCase1()
        {
            us.Register("Psagot@post.co.il", "Psagot2025");
            Response res = JsonSerializer.Deserialize<Response>(us.Logout("Psagot@post.co.il"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if logout fails when the email is invalid.
        /// Requirement: 7 (Logout functionality)
        /// </summary>
        public bool TestUserLogoutNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(us.Logout("EtaiYaron"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if logout fails when the email format is incorrect.
        /// Requirement: 7 (Logout functionality)
        /// </summary>
        public bool TestUserLogoutNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(us.Logout("Amztia@pol"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }
    }
}

