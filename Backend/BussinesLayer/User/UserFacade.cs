using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;
using IntroSE.Kanban.Backend.DataAccessLayer;
using log4net;

namespace IntroSE.Kanban.Backend.BussinesLayer.User
{
    internal class UserFacade
    {
        private Dictionary<string, UserBL> users;
        private AuthenticationFacade authFacade;
        private static readonly ILog log = LogManager.GetLogger(typeof(UserFacade));
        private const int minlength = 6;
        private const int maxlength = 20;



        public UserFacade(AuthenticationFacade authFacade)
        {
            this.users = new Dictionary<string, UserBL>();
            this.authFacade = authFacade;
        }

        /// <summary>
        /// This method is used to login a user to the system.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>UserBL object if the login was successful, otherwise null.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public UserBL Login(string email, string password)
        {
            log.Info($"Attempting login for user with email: {email}.");
            if (email == null)
            {
                log.Error("Login failed, email can't be null.");
                throw new ArgumentNullException("email");
            }
            email = email.ToLower();
            if (password == null)
            {
                log.Error("Login failed, password can't be null.");
                throw new ArgumentNullException("password");
            }
            if (!users.ContainsKey(email))
            {
                log.Error($"Login failed, user with email {email} doesn't exist.");
                throw new Exception("User " + email + " doesn't exist");
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
            log.Error($"Login failed for user {email}, incorrect password.");
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
            email = email.ToLower();
            if (password == null)
            {
                log.Error("Registration failed, password can't be null.");
                throw new ArgumentNullException("password");
            }
            if (!IsValidEmail(email))
            {
                log.Error($"Registration failed, Invalid email: {email}.");
                throw new Exception("Illegal email");
            }

            if (!isValidPassword(password))
            {
                log.Error($"Registration failed, Invalid password: {password}.");
                throw new Exception("Illegal password");
            }
            if (users.ContainsKey(email))
            {
                log.Error($"Registration failed, user with email {email} alreay exists.");
                throw new Exception("User " + email + "already exist");
            }

            UserBL user = new UserBL(email, password);
            users[email] = user;
            authFacade.Login(email);
            user.UserDAL.persist();
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
            if (password.Length < minlength || password.Length > maxlength) return false;
            return Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[0-9]");
        }

        private bool IsValidEmail(string email)
        {
            String EmailRegex = "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:(?!\\d+\\.\\d+\\.\\d+\\.\\d+$)(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z]{2,}|(\\d{1,3}\\.){3}\\d{1,3}|\\[(\\d{1,3}\\.){3}\\d{1,3}\\])$";
            return Regex.IsMatch(email, EmailRegex, RegexOptions.IgnoreCase);
        }
    }
}
