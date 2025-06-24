using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Frontend.Model;

namespace IntroSE.Kanban.Frontend.Controllers
{
    internal class UserController
    {
        private UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Logs in a user with the specified email and password.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A <see cref="UserModel"/> representing the logged-in user.</returns>
        /// <exception cref="Exception">Thrown if the backend returns an error message.</exception>
        public UserModel Login(string email, string password)
        {
            string response = userService.Login(email, password);
            Response<string> res = JsonSerializer.Deserialize<Response<string>>(response);
            if (res.ErrorMessage != null)
            {
                throw new Exception(res.ErrorMessage);
            }
            return new UserModel(res.ReturnValue);
        }

        /// <summary>
        /// Registers a new user with the specified email and password.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A <see cref="UserModel"/> representing the registered user.</returns>
        /// <exception cref="Exception">Thrown if the backend returns an error message.</exception>
        public UserModel Register(string email, string password)
        {
            string response = userService.Register(email, password);
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(response);
            if (res.ErrorMessage != null)
            {
                throw new Exception(res.ErrorMessage);
            }
            return new UserModel(email.ToLower());
        }

        /// <summary>
        /// Logs out the user with the specified email.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <exception cref="Exception">Thrown if the backend returns an error message.</exception>
        public void Logout(string email)
        {
            string response = userService.Logout(email);
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(response);
            if (res.ErrorMessage != null)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

    }
}
