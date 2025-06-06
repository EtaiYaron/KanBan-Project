using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.Model
{
    internal class UserModel
    {
        public string Email { get; }

        internal UserModel(string userEmail)
        {
            Email = userEmail;
        }
    }
}
