using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BussinesLayer.User
{
    internal class UserBL
    {
        private string email;
        private string password;
        private UserDAL userDAL;

        public UserBL(string email, string password)
        {
            this.email = email;
            this.password = password;
            this.userDAL = new UserDAL(email, password);
            userDAL.persist();
        }

        public bool Login(string password)
        {
            return this.password == password;
        }

        public string Email {  get { return this.email; } }

        public UserDAL UserDAL
        {
            get { return this.userDAL; }
        }
    }
}
