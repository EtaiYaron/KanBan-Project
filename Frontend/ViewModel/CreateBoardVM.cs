using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    internal class CreateBoardVM: Notifiable
    {
        private string name;
        private string owner;
        private string errorMessage;

        BoardController boardController = ControllerFactory.Instance.BoardController;

        public CreateBoardVM()
        {
            name = string.Empty;
            owner = string.Empty;
            errorMessage = string.Empty;
        }


    }
}
