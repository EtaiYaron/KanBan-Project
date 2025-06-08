using IntroSE.Kanban.Backend.ServiceLayer;
using System.Text.Json;
namespace UnitTesting
{
    public class Tests
    {
        private UserService us;
        private BoardService b;
        private TaskService t;
        private int cnt;

        [OneTimeSetUp]
        public void Setup(UserService us, BoardService b, TaskService t)
        {      
            this.us = us;
            this.b = b;
            this.t = t;
            cnt = 0;
            us.Register("yaronet@post.bgu.ac.il", "Admin1");
            us.Register("shauli@gmail.com", "Haparlament1");
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}