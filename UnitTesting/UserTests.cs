using IntroSE.Kanban.Backend.ServiceLayer;
using System.Text.Json;
namespace UnitTesting
{
    public class UserTests
    {
        private UserService _us;

        [OneTimeSetUp]
        public void Setup(UserService userService)
        {
            _us = userService;
        }

        /// <summary>
        /// Checks if a user can successfully register with a valid email and password.
        /// Requirement: 6 (Registration of new users)
        /// </summary>
        [Test]
        public void TestUserRegisterPositiveCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(_us.Register("etaiyaron@gmail.com", "Password1"));
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
        public void TestUserRegisterPositiveCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(_us.Register("Amztia@post.co.il", "Amztia1"));
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
        public void TestUserRegisterNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(_us.Register("Amztia@post.co.il", "amztia1"));
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
        public void TestUserRegisterNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(_us.Register("Amztiapost.co.il", "Amztia1"));
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
        public void TestUserLoginPositiveCase()
        {
            _us.Logout("etaiyaron@gmail.com");
            Response res = JsonSerializer.Deserialize<Response>(_us.Login("etaiyaron@gmail.com", "Password1"));
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
        public void TestUserLoginPositiveCase1()
        {
            _us.Logout("Amztia@post.co.il");
            Response res = JsonSerializer.Deserialize<Response>(_us.Login("Amztia@post.co.il", "Amztia1"));
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
        public void TestUserLoginNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(_us.Login("etaiyaron@gmail.com", "password1"));
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
        public void TestUserLoginNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(_us.Login("@gmail.com", "Password1"));
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
        public void TestUserLogoutPositiveCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(_us.Logout("etaiyaron@gmail.com"));
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
        public void TestUserLogoutPositiveCase1()
        {
            _us.Register("Psagot@post.co.il", "Psagot2025");
            Response res = JsonSerializer.Deserialize<Response>(_us.Logout("Psagot@post.co.il"));
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
        public void TestUserLogoutNegativeCase()
        {
            Response res = JsonSerializer.Deserialize<Response>(_us.Logout("EtaiYaron"));
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
        public void TestUserLogoutNegativeCase1()
        {
            Response res = JsonSerializer.Deserialize<Response>(_us.Logout("Amztia@pol"));
            if (res.ErrorMessage != null)
            {
                Assert.Pass("TestUserLogoutNegativeCase1 passed");
            }
            Assert.Fail("TestUserLogoutNegativeCase1 Failed, logout when the email format is incorrect should have failed.");
        }
    }
}
