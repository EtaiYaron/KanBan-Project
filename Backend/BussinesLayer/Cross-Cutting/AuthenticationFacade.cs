using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting
{
    internal class AuthenticationFacade
    {
        private Dictionary<string, string> loggedUsers;

        public AuthenticationFacade() 
        {
            this.loggedUsers = new Dictionary<string, string>();
        }

        public void Login(string username)
        {
            throw new NotImplementedException();
        }

        public void Logout(string username) 
        { 
            throw new NotImplementedException(); 
        }

        public bool isLoggedIn(string username)
        {
            throw new NotImplementedException();
        }
    }
}
