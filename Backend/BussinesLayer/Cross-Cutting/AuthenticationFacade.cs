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

        public void Login(string email)
        {
            throw new NotImplementedException();
        }

        public void Logout(string email) 
        { 
            throw new NotImplementedException(); 
        }

        public bool isLoggedIn(string email)
        {
            throw new NotImplementedException();
        }
    }
}
