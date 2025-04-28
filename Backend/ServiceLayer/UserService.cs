using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;
using IntroSE.Kanban.Backend.BussinesLayer.User;
using log4net;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class UserService
    {
        private UserFacade userFacade;


        /// <summary>
        /// Empty Constructor for the UserService class just for now.
        /// </summary>

        internal UserService(UserFacade userFacade)
        {
            this.userFacade = userFacade;
        }

        public Response Login(string email, string password)
        {
            try
            {
                UserBL ubl = userFacade.Login(email, password);
                Response response = new Response(null, ubl);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response Register(string email, string password)
        {
            try
            {
                UserBL ubl = userFacade.Register(email, password);
                Response response = new Response(null, ubl);
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response Logout(string email)
        {
            try
            {
                UserBL ubl = userFacade.Logout(email);
                Response response = new Response(null, ubl);
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }
    }  
}
