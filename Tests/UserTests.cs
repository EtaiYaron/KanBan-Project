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
            bool tests = TestUserRegisterPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestUserRegisterPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestUserRegisterPositiveCase: Failed");
            }
            tests = TestUserRegisterPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestUserRegisterPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestUserRegisterPositiveCase1: Failed");
            }
            tests = TestUserRegisterNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestUserRegisterNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestUserRegisterNegativeCase: Failed");
            }
            tests = TestUserRegisterNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestUserRegisterNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestUserRegisterNegativeCase1: Failed");
            }
            tests = TestUserLoginPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestUserLoginPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLoginPositiveCase: Failed");
            }
            tests = TestUserLoginPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestUserLoginPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLoginPositiveCase1: Failed");
            }
            tests = TestUserLoginNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestUserLoginNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLoginNegativeCase: Failed");
            }
            tests = TestUserLoginNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestUserLoginNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLoginNegativeCase1: Failed");
            }
            tests = TestUserLogoutPositiveCase();
            if (tests)
            {
                Console.WriteLine("TestUserLogoutPositiveCase: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLogoutPositiveCase: Failed");
            }
            tests = TestUserLogoutPositiveCase1();
            if (tests)
            {
                Console.WriteLine("TestUserLogoutPositiveCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLogoutPositiveCase1: Failed");
            }
            tests = TestUserLogoutNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestUserLogoutNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLogoutNegativeCase: Failed");
            }
            tests = TestUserLogoutNegativeCase1();
            if (tests)
            {
                Console.WriteLine("TestUserLogoutNegativeCase1: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLogoutNegativeCase1: Failed");
            }
        }

        public bool TestUserRegisterPositiveCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(us.Register("etaiyaron@gmail.com", "Password1"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }
        public bool TestUserRegisterPositiveCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(us.Register("Amztia@post.co.il", "Amztia1"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }
        public bool TestUserRegisterNegativeCase()
        {
            Response res = JsonSerializer.Deserialize < Response > (us.Register("Amztia@post.co.il",  "amztia1"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }
        public bool TestUserRegisterNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(us.Register("Amztiapost.co.il", "Amztia1"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }


        public bool TestUserLoginPositiveCase()
        {
            us.Logout("etaiyaron@gmail.com");
            Response res = JsonSerializer.Deserialize < Response > (us.Login("etaiyaron@gmail.com", "Password1"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }
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

        public bool TestUserLoginNegativeCase()
        {
            Response res = JsonSerializer.Deserialize < Response > (us.Login("etaiyaron@gmail.com", "password1"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }
        public bool TestUserLoginNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(us.Login("@gmail.com", "Password1"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }
        public bool TestUserLogoutPositiveCase()
        {
            Response res = JsonSerializer.Deserialize < Response > (us.Logout("etaiyaron@gmail.com"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }
        public bool TestUserLogoutPositiveCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(us.Logout("Amztia@post.co.il"));
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }
        public bool TestUserLogoutNegativeCase()
        {
            Response res = JsonSerializer.Deserialize < Response > (us.Logout("EtaiYaron"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }
        public bool TestUserLogoutNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(us.Logout("Amztia@post.co.il"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }
    }
}
