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
using NUnit.Framework; 


namespace Tests
{
    [TestFixture]
    class UserTests
    {
        private UserService us;


        [OneTimeSetUp]
        public void SetUp()
        {
            ServiceFactory sf = new ServiceFactory();
            us = sf.UserService;
        }

        [Test]
        public void TestUserRegisterPositiveCase()
        {
            var res = JsonSerializer.Deserialize<Response>(us.Register("etaiyaron@gmail.com", "Password1"));
            Assert.Equals(res.ErrorMessage == null , true);
        }

        [Test]
        public void TestUserRegisterPositiveCase1()
        {
            var res = JsonSerializer.Deserialize<Response>(us.Register("Amztia@post.co.il", "Amztia1"));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestUserRegisterNegativeCase()
        {
            var res = JsonSerializer.Deserialize<Response>(us.Register("Amztia@post.co.il", "amztia1"));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestUserRegisterNegativeCase1()
        {
            var res = JsonSerializer.Deserialize<Response>(us.Register("Amztiapost.co.il", "Amztia1"));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestUserLoginPositiveCase()
        {
            us.Logout("etaiyaron@gmail.com");
            var res = JsonSerializer.Deserialize<Response>(us.Login("etaiyaron@gmail.com", "Password1"));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestUserLoginPositiveCase1()
        {
            us.Logout("Amztia@post.co.il");
            var res = JsonSerializer.Deserialize<Response>(us.Login("Amztia@post.co.il", "Amztia1"));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestUserLoginNegativeCase()
        {
            var res = JsonSerializer.Deserialize<Response>(us.Login("etaiyaron@gmail.com", "password1"));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestUserLoginNegativeCase1()
        {
            var res = JsonSerializer.Deserialize<Response>(us.Login("@gmail.com", "Password1"));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestUserLogoutPositiveCase()
        {
            var res = JsonSerializer.Deserialize<Response>(us.Logout("etaiyaron@gmail.com"));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestUserLogoutPositiveCase1()
        {
            us.Register("Psagot@post.co.il", "Psagot2025");
            var res = JsonSerializer.Deserialize<Response>(us.Logout("Psagot@post.co.il"));
            Assert.Equals(res.ErrorMessage == null, true);
        }

        [Test]
        public void TestUserLogoutNegativeCase()
        {
            var res = JsonSerializer.Deserialize<Response>(us.Logout("EtaiYaron"));
            Assert.Equals(res.ErrorMessage == null, false);
        }

        [Test]
        public void TestUserLogoutNegativeCase1()
        {
            var res = JsonSerializer.Deserialize<Response>(us.Logout("Amztia@pol"));
            Assert.Equals(res.ErrorMessage == null, false);
        }
    }
}
