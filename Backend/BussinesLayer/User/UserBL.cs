using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace IntroSE.Kanban.Backend.BussinesLayer.User
{


    internal class UserBL
    {
        private string email;
        private string passwordHash;
        private string salt;
        private UserDAL userDAL;

        public UserBL(string email, string password)
        {
            this.email = email;
            (this.passwordHash, this.salt) = PasswordHasher.HashPassword(password);
            this.userDAL = new UserDAL(email, passwordHash, salt);
            userDAL.persist();
        }

        public UserBL(UserDAL userDAL)
        {
            this.email = userDAL.Email;
            this.passwordHash = userDAL.PasswordHash;
            this.salt = userDAL.Salt;
            this.userDAL = userDAL;
        }

        public bool Login(string password)
        {
            return PasswordHasher.Verify(password, salt, passwordHash);
        }

        public string Email { get { return this.email; } }
        public UserDAL UserDAL { get { return this.userDAL; } }
    }
}
