using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;
using log4net;

namespace IntroSE.Kanban.Backend.BussinesLayer.User
{
    internal class UserFacade
    {
        private Dictionary<string, UserBL> users;
        private AuthenticationFacade authFacade;
        private static readonly ILog log = LogManager.GetLogger(typeof(UserFacade));


        public UserFacade(AuthenticationFacade authFacade)
        {
            this.users = new Dictionary<string, UserBL>();
            this.authFacade = authFacade;
        }

        public UserBL Login(string email, string password)
        {
            log.Info($"Attempting login for user with email: {email}.");
            if (email == null)
            {
                log.Error("Login failed, email can't be null.");
                throw new ArgumentNullException("email");
            }
            if (password == null)
            {
                log.Error("Login failed, password can't be null.");
                throw new ArgumentNullException("password");
            }
            if (!users.ContainsKey(email))
            {
                log.Error($"Login failed, user with email {email} doesn't exist.");
                throw new Exception("User " + email + "doesn't exist");
            }
            if (authFacade.isLoggedIn(email))
            {
                log.Error($"Login failed, user with email {email} already logged in.");
                throw new Exception("User already logged in");

            }

            UserBL user = users[email];
            if (user.Login(password))
            {
                authFacade.Login(email);
                log.Info($"User with email {email}, logged in successfully.");
                return user;
            }
            log.Warn($"Login failed for user {email}, incorrect password.");
            return null;
        }

        public UserBL Register(string email, string password)
        {
            log.Info($"Attempting to Register user with email: {email} and password: {password}.");
            if (email == null)
            {
                log.Error("Registration failed, email can't be null.");
                throw new ArgumentNullException("email");
            }
            if (password == null)
            {
                log.Error("Registration failed, password can't be null.");
                throw new ArgumentNullException("password");
            }
            if (!IsValidEmail(email))
            {
                log.Error($"Registration failed, Invalid email: {email}.");
                throw new Exception("Invalid email");
            }

            if (!isValidPassword(password))
            {
                log.Error($"Registration failed, Invalid password: {password}.");
                throw new Exception("Invalid password");
            }
            if (users.ContainsKey(email))
            {
                log.Error($"Registration failed, user with email {email} alreay exists.");
                throw new Exception("User " + email + "already exist");
            }

            UserBL user = new UserBL(email, password);
            users[email] = user;
            authFacade.Login(email);
            log.Info($"User with email {email} registered successfully.");
            return user;
        }

        public UserBL Logout(string email)
        {
            log.Info($"Attempting Logout for user with email: {email}.");
            if (email == null)
            {
                log.Error($"Logout failed, email can't be null.");
                throw new ArgumentNullException("email");
            }
            if (!authFacade.isLoggedIn(email))
            {
                log.Error($"Logout failed, user with email {email} is not logged in.");
                throw new Exception("User not logged in");
            }
            authFacade.Logout(email);
            log.Info($"User with email {email} logged out successfully.");
            return users[email];
        }

        private bool isValidPassword(string password)
        {
            if (password.Length < 6 || password.Length > 20) return false;
            return password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit);
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[\w._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
