using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Frontend.Controllers;
using IntroSE.Kanban.Frontend.Model;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    internal class CreateBoardVM : Notifiable
    {
        private string name;
        private string owner;
        private string errorMessage;

        BoardController boardController = ControllerFactory.Instance.BoardController;

        public CreateBoardVM(string email)
        {
            name = string.Empty;
            owner = email;
            errorMessage = string.Empty;
        }

        internal BoardModel? CreateBoard()
        {
            if (string.IsNullOrEmpty(name))
            {
                ErrorMessage = "Name cannot be empty.";
                return null;
            }
            try
            {
                return boardController.CreateBoard(name, owner);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }

        public string Owner
        {
            get { return owner; }
            set
            {
                owner = value;
            }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }


    }
}
