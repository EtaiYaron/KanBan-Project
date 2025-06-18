using IntroSE.Kanban.Frontend.Controllers;
using IntroSE.Kanban.Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    internal class BoardScreenVM : Notifiable
    {
        private string email;
        private string email_msg;
        private string errorMessage;
        private List<BoardModel> boards;
        private UserModel user;

        UserController userController = ControllerFactory.Instance.UserController;
        BoardController boardController = ControllerFactory.Instance.BoardController;

        public BoardScreenVM(UserModel um)
        {
            this.user = um;
            this.email = um.Email;
            this.email_msg = "Welcome, " + email;
            RaisePropertyChanged(nameof(EmailMsg));
            this.errorMessage = string.Empty;
            this.boards = um.Boards;
        }

        public string EmailMsg
        {
            get => email_msg;
        }

        public List<BoardModel> Boards
        {
            get => boards;
        }

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }

        /// <summary>
        /// Deletes the selected board for the user.
        /// Updates the boards list and error message as needed.
        /// </summary>
        /// <param name="board">The board object to delete (should be a <see cref="BoardModel"/>).</param>
        public void DeleteBoard(object board)
        {
            if (board == null)
            {
                ErrorMessage = "No Board was selected";
                return;
            }
            try
            {
                BoardModel selectedBoard = (BoardModel)board;
                boardController.DeleteBoard(email, selectedBoard.Name);
                this.user = new UserModel(this.email);
                this.boards = user.Boards;
                RaisePropertyChanged(nameof(Boards));
                ErrorMessage = "";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        /// <summary>
        /// Logs out the current user.
        /// Updates the error message if logout fails.
        /// </summary>
        /// <returns>True if logout was successful; otherwise, false.</returns>
        public bool Logout()
        {
            try
            {
                userController.Logout(email);
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
            
        }


    }
}
