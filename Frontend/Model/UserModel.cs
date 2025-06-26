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
            Boards = new List<BoardModel>();
            GetBoards();
        }

        /// <summary>
        /// Retrieves all boards that the user is a member of and populates the <see cref="Boards"/> list.
        /// For each board ID associated with the user, this method fetches the board's name and details,
        /// then adds the corresponding <see cref="BoardModel"/> to the user's boards collection.
        /// </summary>
        /// <remarks>
        /// This method is called during user model initialization. It may result in multiple backend calls,
        /// one for each board the user is a member of.
        /// </remarks>
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
