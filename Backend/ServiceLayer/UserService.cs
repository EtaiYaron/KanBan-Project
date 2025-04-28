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
        public UserService()
        {
            this.userFacade = new UserFacade(new AuthenticationFacade());
        }

        internal UserService(UserFacade userFacade)
        {
            this.userFacade = userFacade;
        }

        public string Login(string email, string password)
        {
            try
            {
                UserBL ubl = userFacade.Login(email, password);
                Response response = new Response(null, ubl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public string Register(string email, string password)
        {
            try
            {
                UserBL ubl = userFacade.Register(email, password);
                Response response = new Response(null, ubl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public string Logout(string email)
        {
            try
            {
                UserBL ubl = userFacade.Logout(email);
                Response response = new Response(null, ubl);
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
