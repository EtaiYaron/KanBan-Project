using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class UserTests
    {
        public static void RunTests()
        {
            // Add your test cases here
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
            
            

        }

        public static bool TestUserRegisterPositiveCase()
        {
            Response res = us.Register("etaiyaron@gmail.com", "EtaiYaron", "Password1");
            if (res.Errormsg != null)
            {
                return false;
            }
            return true;

        }
        public static bool TestUserRegisterNegativeCase()
        {
            Response res = us.Register("Amztia@gmail.com", "Amtzia", "amztia1");
            if (res.Errormsg != null)
            {
                return true;
            }
            return false;
        }

        public static bool TestUserLoginShouldPass()
        {
            Response res = us.Login("EtaiYaron", "Password1");
            if (res.Errormsg != null)
            {
                return false;
            }
            return true;
        }
        public static bool TestUserLoginShouldFail()
        {
            Response res = us.Login("EtaiYaron", "password1");
            if (res.Errormsg != null)
            {
                return true;
            }
            return false;
        }
        public static bool TestUserLogoutShouldPass()
        {
            us.Login("EtaiYaron", "Password1");
            Response res = us.Logout();
            if (res.Errormsg != null)
            {
                return false;
            }
            return true;
        }
        public static bool TestUserLogoutShouldFail()
        {
            us.Login("EtaiYaron", "Password1");
            Response res = us.Logout();
            if (res.Errormsg != null)
            {
                return true;
            }
            return false;
        }
    }
}
