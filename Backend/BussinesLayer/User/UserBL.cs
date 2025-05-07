using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BussinesLayer.User
{
    internal class UserBL
    {
        private string email;
        private string password;

        public UserBL(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public bool Login(string password)
        {
            return this.password == password;
        }

        public string Email {  get { return this.email; } }
    }
}
