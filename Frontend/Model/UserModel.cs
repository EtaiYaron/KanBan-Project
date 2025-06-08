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
        public List<BoardModel> Boards { get; }

        internal UserModel(string userEmail)
        {
            Email = userEmail;
            GetBoards();
        }

        private void GetBoards()
        {
            int[]? boardIds = ControllerFactory.Instance.BoardController.GetUserBoards(Email);
            if (boardIds != null)
            {
                for (int i = 0; i < boardIds.Length; i++)
                {
                    string name = ControllerFactory.Instance.BoardController.GetBoardName(boardIds[i]);
                    BoardModel board = ControllerFactory.Instance.BoardController.GetBoard(Email, name);
                    if (board != null)
                    {
                        Boards.Add(board);
                    }
                }
            }
        }
    }
}
