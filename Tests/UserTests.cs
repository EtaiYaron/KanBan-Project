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
            tests = TestUserLoginNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestUserLoginNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLoginNegativeCase: Failed");
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
            tests = TestUserLoginNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestUserLoginNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLoginNegativeCase: Failed");
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
            tests = TestUserLogoutNegativeCase();
            if (tests)
            {
                Console.WriteLine("TestUserLogoutNegativeCase: Passed");
            }
            else
            {
                Console.WriteLine("TestUserLogoutNegativeCase: Failed");
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
        public bool TestUserRegisterNegativeCase()
        {
            Response res = JsonSerializer.Deserialize < Response > (us.Register("Amztia@gmail.com",  "amztia1"));
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
        public bool TestUserLoginNegativeCase()
        {
            Response res = JsonSerializer.Deserialize < Response > (us.Login("etaiyaron@gmail.com", "password1"));
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
        public bool TestUserLogoutNegativeCase()
        {
            us.Login("etaiyaron@gmail.com", "Password1");
            Response res = JsonSerializer.Deserialize < Response > (us.Logout("EtaiYaron"));
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }
    }
}
