using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using IntroSE.Kanban.Frontend.Controllers;
using IntroSE.Kanban.Frontend.Model;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    internal class LoginVM : Notifiable
    {
        private string email;
        private string password;
        private string errorMessage;

        UserController userController = ControllerFactory.Instance.UserController;

        public LoginVM()
        {
            email = string.Empty;
            password = string.Empty;
            errorMessage = string.Empty;
        }

        /// <summary>
        /// Attempts to log in the user with the provided email and password.
        /// Updates the error message if login fails.
        /// </summary>
        /// <returns>The <see cref="UserModel"/> if login is successful; otherwise, null.</returns>
        internal UserModel? Login()
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ErrorMessage = "Email and password cannot be empty.";
                return null;
            }
            try
            {
                return userController.Login(email, password);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Attempts to register a new user with the provided email and password.
        /// Updates the error message if registration fails.
        /// </summary>
        /// <returns>The <see cref="UserModel"/> if registration is successful; otherwise, null.</returns>
        internal UserModel? Register()
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ErrorMessage = "Email and password cannot be empty.";
                return null;
            }
            try
            {
                return userController.Register(email, password);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
            }
        }

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }

    }
}
