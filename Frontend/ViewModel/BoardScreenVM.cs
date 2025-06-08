using IntroSE.Kanban.Frontend.Controllers;
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

        BoardController boardController = ControllerFactory.Instance.BoardController;
        UserController userController = ControllerFactory.Instance.UserController;

        public BoardScreenVM(string email)
        {
            this.email = email;
            this.email_msg = "Welcome, " + email + "!";
            RaisePropertyChanged(nameof(EmailMsg));
            this.errorMessage = string.Empty;
        }

        public string EmailMsg
        {
            get => email_msg;
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

        public void CreateBoard(string boardName)
        {
            // Logic to add a board
            // This could involve calling a method in the BoardController to create a new board
            // and then updating the UI to reflect the new board.
        }

        public void DeleteBoard(string boardName)
        {
            // Logic to delete a board
            // This could involve calling a method in the BoardController to delete the board
            // and then updating the UI to reflect the deletion.
        }

        public void ViewBoard(string boardName)
        {
            // Logic to view a board
            // This could involve navigating to a new window that displays the board details.
            // You might want to call a method in the BoardController to get the board details
            // and then update the UI accordingly.
        }

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
