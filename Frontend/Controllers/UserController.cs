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
