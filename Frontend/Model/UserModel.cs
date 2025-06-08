using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Frontend.Controllers;

namespace IntroSE.Kanban.Frontend.Model
{
    internal class UserModel
    {
        public string Email { get; }
        public int[] Boards { get; }

        internal UserModel(string userEmail)
        {
            Email = userEmail;
            Boards = ControllerFactory.Instance.BoardController.GetUserBoards(userEmail);
        }
    }
}
