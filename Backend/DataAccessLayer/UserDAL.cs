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
        private string passwordHash;
        private string salt;
        private UserController userController;
        public string UserEmailColumnName = "Email";
        public string UserPasswordHashColumnName = "PasswordHash";
        public string UserSaltColumnName = "Salt";
        private bool isPersistent;

        /// <summary>
        /// Constructor for new user (registration) with hash and salt.
        /// </summary>
        public UserDAL(string email, string passwordHash, string salt)
        {
            this.email = email;
            this.passwordHash = passwordHash;
            this.salt = salt;
            userController = new UserController();
            isPersistent = false;
        }

        /// <summary>
        /// Constructor for loading user from DB (with hash and salt).
        /// </summary>
        public UserDAL(string email, string passwordHash, string salt, bool isPersistent = true)
        {
            this.email = email;
            this.passwordHash = passwordHash;
            this.salt = salt;
            userController = new UserController();
            this.isPersistent = isPersistent;
        }

        public string Email
        {
            get { return email; }
        }

        public string PasswordHash
        {
            get { return passwordHash; }
        }

        public string Salt
        {
            get { return salt; }
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
