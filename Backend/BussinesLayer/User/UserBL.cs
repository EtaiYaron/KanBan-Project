using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.BussinesLayer.User
{
    internal class UserBL
    {
        private string email;
        private string password;
        public UserDAL userDAL;

        public UserBL(string email, string password)
        {
            this.userDAL = new UserDAL(email,password);
            this.email = email;
            this.password = password;
            userDAL.persist();
        }

        /// <summary>
        /// This method is used to log in the user using the provided password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns>true if the password is correct and the user is successfully logged in. otherwise, false.</returns>
        public bool Login(string password)
        {
            return this.password == password;
        }

        public string Email {  get { return this.email; } }
    }
}
