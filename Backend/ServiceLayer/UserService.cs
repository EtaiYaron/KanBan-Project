using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;
using IntroSE.Kanban.Backend.BussinesLayer.User;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class UserService
    {
        private UserFacade userFacade;

        /// <summary>
        /// Empty Constructor for the UserService class just for now.
        /// </summary>
        public UserService()
        {
            this.userFacade = new UserFacade(new AuthenticationFacade());
        }

        internal UserService(UserFacade userFacade)
        {
            this.userFacade = userFacade;
        }

        public Response Login(string email, string password)
        {
            try
            {
                UserBL
            }
        }

        public Response Register(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Response Logout(string email)
        {
            throw new NotImplementedException();
        }
    }  
}
