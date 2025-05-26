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
        private bool isPersistent;


        /// <summary>
        /// This is the constructor for the UserDAL class.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
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

        /// <summary>
        /// This method is used to check if the user is persistent in the database.
        /// </summary>
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
