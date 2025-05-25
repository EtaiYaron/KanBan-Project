using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

        internal UserService(UserFacade userFacade)
        {
            this.userFacade = userFacade;
        }

        /// <summary>
        /// This method is used to login a user to the system.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>An empty response, unless an error occurs</returns>
        public string Login(string email, string password)
        {
            try
            {
                UserBL ubl = userFacade.Login(email, password);
                Response response = new Response(null, email.ToLower());
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// This method is used to register a user to the system.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>An empty response, unless an error occurs</returns>
        public string Register(string email, string password)
        {
            try
            {
                UserBL ubl = userFacade.Register(email, password);
                Response response = new Response();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// This method is used to logout a user from the system.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>An empty response, unless an error occurs</returns>
        public string Logout(string email)
        {
            try
            {
                UserBL ubl = userFacade.Logout(email);
                Response response = new Response();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }
    }  
}
