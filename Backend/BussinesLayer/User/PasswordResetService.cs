using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BussinesLayer.User
{
    public class PasswordResetService
    {
        private readonly PasswordResetRequestController _resetController;
        private readonly UserController _userController;

        public PasswordResetService()
        {
            _resetController = new PasswordResetRequestController();
            _userController = new UserController();
        }

        public string RequestPasswordReset(string email)
        {
            string token = Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(32));
            DateTime expires = DateTime.UtcNow.AddHours(1);
            var requestDal = new PasswordResetRequestDAL(email, token, expires);
            requestDal.Insert();
            return token;
        }

        public bool ResetPassword(string token, string newPassword)
        {
            var request = _resetController.SelectByToken(token);
            if (request == null || request.ExpiresAt < DateTime.UtcNow)
                return false;

            var (hash, salt) = PasswordHasher.HashPassword(newPassword);
            _userController.UpdatePassword(request.Email, hash, salt);
            _resetController.DeleteByToken(token);
            return true;
        }
    }
}
