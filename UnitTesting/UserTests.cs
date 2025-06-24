using IntroSE.Kanban.Backend.ServiceLayer;
using System.Text.Json;
namespace UnitTesting
{
    public class UserTests
    {
        private UserService us;

        [OneTimeSetUp]
        [Order(1)]
        public void GlobalSetup()
        {
            ServiceFactory s = new ServiceFactory();
            this.us = s.UserService;
            us.DeleteAllUsers();
        }

        /// <summary>
        /// Checks if a user can successfully register with a valid email and password.
        /// Requirement: 6 (Registration of new users)
        /// </summary>
        [Test]
        [Order(2)]
        public void Register_ValidInput()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(us.Register("etaiyaron@gmail.com", "Password1"));
            if (res.ErrorMessage != null)
            {
                Assert.Fail(res.ErrorMessage);
            }
            Assert.Pass("TestUserRegisterPositiveCase passed.");
        }

        /// <summary>
        /// Checks if a user can successfully register with another valid email and password.
        /// Requirement: 6 (Registration of new users)
        /// </summary>
        [Test]
        [Order(3)]
        public void Register_ValidInput_OtherUser()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(us.Register("Amztia@post.co.il", "Amztia1"));
            if (res.ErrorMessage != null)
            {
                Assert.Fail(res.ErrorMessage);
            }
            Assert.Pass("TestUserRegisterPositiveCase1 passed.");
        }

        /// <summary>
        /// Checks if registration fails when the password does not meet the validity criteria.
        /// Requirement: 2 (Valid password rules)
        /// </summary>
        [Test]
        [Order(4)]
        public void Register_InvalidPassword()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(us.Register("Amztia@post.co.il", "amztia1"));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestUserRegisterNegativeCase passed.");
            }
            Assert.Fail("TestUserRegisterNegativeCase Failed, registration when the password does not meet the validity criteria should have failed.");
        }

        /// <summary>
        /// Checks if registration fails when the email is invalid.
        /// Requirement: 3 (Valid email address)
        /// </summary>
        [Test]
        [Order(5)]
        public void Register_InvalidEmail()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(us.Register("Amztiapost.co.il", "Amztia1"));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestUserRegisterNegativeCase1 passed");
            }
            Assert.Fail("TestUserRegisterNegativeCase1 Failed, registration when the email is invalid should have failed.");
        }

        /// <summary>
        /// Checks if a user can successfully login with valid credentials.
        /// Requirement: 7 (Login functionality)
        /// </summary>+
        [Test]
        [Order(6)]
        public void Login_ValidInput()
        {
            us.Logout("etaiyaron@gmail.com");
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(us.Login("etaiyaron@gmail.com", "Password1"));
            if (res.ErrorMessage != null)
            {
                Assert.Fail(res.ErrorMessage);
            }
            Assert.Pass("TestUserLoginPositiveCase passed.");
        }

        /// <summary>
        /// Checks if another user can successfully login with valid credentials.
        /// Requirement: 7 (Login functionality)
        /// </summary>
        [Test]
        [Order(7)]
        public void Login_ValidInput_OtherUser()
        {
            us.Logout("Amztia@post.co.il");
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(us.Login("Amztia@post.co.il", "Amztia1"));
            if (res.ErrorMessage != null)
            {
                Assert.Fail(res.ErrorMessage);
            }
            Assert.Pass("TestUserLoginPositiveCase1");
        }

        /// <summary>
        /// Checks if login fails when the password is incorrect.
        /// Requirement: 7 (Login functionality)
        /// </summary>
        [Test]
        [Order(8)]
        public void Login_InvalidPassword()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(us.Login("etaiyaron@gmail.com", "password1"));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestUserLoginNegativeCase passed");
            }
            Assert.Fail("TestUserLoginNegativeCase Failed, login when the password is incorrect should have failed.");
        }

        /// <summary>
        /// Checks if login fails when the email is invalid.
        /// Requirement: 7 (Login functionality)
        /// </summary>
        [Test]
        [Order(9)]
        public void Login_InvalidEmail()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(us.Login("@gmail.com", "Password1"));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestUserLoginNegativeCase1 passed");
            }
            Assert.Fail("TestUserLoginNegativeCase1 Failed, login when the email is invalid should have failed.");
        }

        /// <summary>
        /// Checks if a user can successfully logout.
        /// Requirement: 7 (Logout functionality)
        /// </summary>
        [Test]
        [Order(10)]
        public void Logout_ValidUser()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(us.Logout("etaiyaron@gmail.com"));
            if (res.ErrorMessage != null)
            {
                Assert.Fail(res.ErrorMessage);
            }
            Assert.Pass("TestUserLogoutPositiveCase passed");
        }

        /// <summary>
        /// Checks if another user can successfully logout after registering.
        /// Requirements: 6 (Registration logs in the user), 7 (Logout functionality)
        /// </summary>
        [Test]
        [Order(11)]
        public void Logout_ValidUser_AfterRegister()
        {
            us.Register("Psagot@post.co.il", "Psagot2025");
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(us.Logout("Psagot@post.co.il"));
            if (res.ErrorMessage != null)
            {
                Assert.Fail(res.ErrorMessage);
            }
            Assert.Pass("TestUserLogoutPositiveCase1 passed");
        }

        /// <summary>
        /// Checks if logout fails when the email is invalid.
        /// Requirement: 7 (Logout functionality)
        /// </summary>
        [Test]
        [Order(12)]
        public void Logout_InvalidEmail()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(us.Logout("EtaiYaron"));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestUserLogoutNegativeCase passed");
            }
            Assert.Fail("TestUserLogoutNegativeCase Failed, logout when the email is invalid should have failed.");
        }

        /// <summary>
        /// Checks if logout fails when the email format is incorrect.
        /// Requirement: 7 (Logout functionality)
        /// </summary>
        [Test]
        [Order(13)]
        public void Logout_InvalidEmailFormat()
        {
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(us.Logout("Amztia@pol"));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestUserLogoutNegativeCase1 passed");
            }
            Assert.Fail("TestUserLogoutNegativeCase1 Failed, logout when the email format is incorrect should have failed.");
        }
    }
}
