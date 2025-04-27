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
        private static readonly ILog log = LogManager.GetLogger(typeof(UserService));


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
                log.Info($"attempting Login for user with email: {email}.");
                UserBL ubl = userFacade.Login(email, password);
                log.Info($"User with email {email}, logged in successfully.");
                Response response = new Response(null, ubl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                log.Error($"Login failed for user with email {email}: {ex.Message}", ex);
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public string Register(string email, string password)
        {
            try
            {
                log.Info($"Attempting to Register for user with email: {email}.");
                UserBL ubl = userFacade.Register(email, password);
                log.Info($"User with email {email}, Registered successfully.");
                Response response = new Response(null, ubl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                log.Error($"Registration failed for user with email {email}: {ex.Message}", ex);
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public string Logout(string email)
        {
            try
            {
                log.Info($"Attempting Logout for user with email: {email}.");
                UserBL ubl = userFacade.Logout(email);
                Response response = new Response(null, ubl);
                log.Info($"User with email {email}, Logged out successfully.");
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                log.Error($"Log out failed for user with email {email}: {ex.Message}", ex);
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }
    }  
}
