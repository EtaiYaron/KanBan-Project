using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting
{
    internal class AuthenticationFacade
    {
        private HashSet<string> loggedUsers;

        public AuthenticationFacade() 
        {
            this.loggedUsers = new HashSet<string>();
        }

        /// <summary>
        /// This method is used to login a user to the system.
        /// </summary>
        /// <param name="email"></param>
        public void Login(string email)
        {
            loggedUsers.Add(email);
            return;
        }

        /// <summary>
        /// This method is used to logout a user from the system.
        /// </summary>
        /// <param name="email"></param>
        public void Logout(string email) 
        { 
            loggedUsers.Remove(email);
            return;
        }

        /// <summary>
        /// This method is used to check if a user is logged in to the system.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool isLoggedIn(string email)
        {
            return loggedUsers.Contains(email);
        }
    }
}
