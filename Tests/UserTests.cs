using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Response res = us.Register("etaiyaron@gmail.com", "Password1");
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }
        public bool TestUserRegisterNegativeCase()
        {
            Response res = us.Register("Amztia@gmail.com",  "amztia1");
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }

        public bool TestUserLoginPositiveCase()
        {
            Response res = us.Login("EtaiYaron", "Password1");
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }
        public bool TestUserLoginNegativeCase()
        {
            Response res = us.Login("EtaiYaron", "password1");
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }
        public bool TestUserLogoutPositiveCase()
        {
            us.Login("EtaiYaron", "Password1");
            Response res = us.Logout("EtaiYaron");
            if (res.ErrorMessage != null)
            {
                return false;
            }
            return true;
        }
        public bool TestUserLogoutNegativeCase()
        {
            us.Login("EtaiYaron", "Password1");
            us.Logout("EtaiYaron");
            Response res = us.Logout("EtaiYaron");
            if (res.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }
    }
}
