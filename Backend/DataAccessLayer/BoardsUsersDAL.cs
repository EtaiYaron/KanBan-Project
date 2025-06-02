using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class BoardsUsersDAL
    {
        private int boardId;
        private string userEmail;
        private BoardsUsersController boardsUsersController;
        private bool isOwner;
        private bool isPersistent;

        public BoardsUsersDAL(int boardId, string userEmail, bool isOwner)
        {
            this.boardId = boardId;
            this.userEmail = userEmail;
            this.isOwner = isOwner;
            this.boardsUsersController = new BoardsUsersController();
            this.isPersistent = false;
        }


        public BoardsUsersController BoardsUsersController
        {
            get { return boardsUsersController; }
        }
        public int BoardId
        {
            get { return boardId; }
        }

        public string UserEmail
        {
            get { return userEmail; }
        }

        public bool IsOwner
        {
            get { return isOwner; }
            set
            {
                boardsUsersController.UpdateIsOwner(this, value);
                isOwner = value;
            }
        }

        /// <summary>
        /// This method is used to persist the BoardsUsersDAL to the database.
        /// </summary>
        public void persist()
        {
            if (!isPersistent)
            {
                boardsUsersController.Insert(this);
                isPersistent = true;
            }
        }


    }
}
