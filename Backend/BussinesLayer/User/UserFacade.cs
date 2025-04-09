using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;

namespace IntroSE.Kanban.Backend.BussinesLayer.User
{
    class UserFacade
    {
        private Dictionary<string, UserBL> users;
        private AuthenticationFacade authFacade;

        public UserFacade(AuthenticationFacade authFacade)
        {
            this.users = new Dictionary<string, UserBL>();
            this.authFacade = authFacade;
        }

        public UserBL Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public UserBL Register(string username, string password, string email)
        {
            throw new NotImplementedException();
        }

        public UserBL Logout(string username)
        {
            throw new NotImplementedException();
        }
    }
}
