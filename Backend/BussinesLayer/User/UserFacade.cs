using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;

namespace IntroSE.Kanban.Backend.BussinesLayer.User
{
    internal class UserFacade
    {
        private Dictionary<string, UserBL> users;
        private AuthenticationFacade authFacade;

        public UserFacade(AuthenticationFacade authFacade)
        {
            this.users = new Dictionary<string, UserBL>();
            this.authFacade = authFacade;
        }

        public UserBL Login(string email, string password)
        {
            if (email == null) throw new ArgumentNullException("email");
            if (password == null) throw new ArgumentNullException("password");

            if (!users.ContainsKey(email))
                throw new Exception("User " + email + "doesn't exist");

            UserBL user = users[email];
            if (user.Login(password))
            {
                authFacade.Login(email);
                return user;
            }
            return null;
        }

        public UserBL Register(string email, string password)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (email == null) throw new ArgumentNullException("email");

            if (!isValidPassword(password)) throw new Exception("Invalid password");
            return null;
        }

        public UserBL Logout(string email)
        {
            throw new NotImplementedException();
        }

        private bool isValidPassword(string password)
        {
            if (password.Length < 6 || password.Length > 20) return false;
            return password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit);
        }
    }
}
