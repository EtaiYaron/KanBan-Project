using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BussinesLayer.User
{
    class UserBL
    {
        private string username;
        private string password;
        private string email;

        public UserBL(string username, string password, string email)
        {
            this.username = username;
            this.password = password;
            this.email = email;
        }

        public bool Login(string password)
        {
            return this.password == password;
        }
    }
}
