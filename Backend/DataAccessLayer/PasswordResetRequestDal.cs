using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class PasswordResetRequestDAL
    {
        private PasswordResetRequestController controller;

        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }

        public PasswordResetRequestDAL(string email, string token, DateTime expiresAt)
        {
            Email = email;
            Token = token;
            ExpiresAt = expiresAt;
            controller = new PasswordResetRequestController();
        }

        public bool Insert()
        {
            return controller.Insert(this);
        }

        public static PasswordResetRequestDAL SelectByToken(string token)
        {
            PasswordResetRequestController controller = new PasswordResetRequestController();
            return controller.SelectByToken(token);
        }

        public bool Delete()
        {
            return controller.DeleteByToken(this.Token);
        }
    }
}
