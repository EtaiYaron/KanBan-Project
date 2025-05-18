using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class UserDAL
    {
        private string email;
        private string password;
        public UserController userController { get; set; }
        public string UserEmailColumnName = "Email";
        public string UserPasswordColumnName = "Password";
        private bool isPersistent;

        public UserDAL(string email, string password)
        {
            this.email = email;
            this.password = password;
            userController = new UserController();
            isPersistent = false;
        }

        public string Email
        {
            get { return email; }
        }

        public string Password
        {
            get { return password; }
        }

        public void persist()
        {
            if (!isPersistent)
            {
                userController.Insert(this);
                isPersistent = true;
            }
        }
    }
}
