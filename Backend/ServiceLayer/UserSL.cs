using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.User;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class UserSL
    {
        private readonly string email;

        public UserSL(UserBL ubl)
        {
            this.email = ubl.Email;
        }
    }
}
