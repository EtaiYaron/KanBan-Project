using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using IntroSE.Kanban.Backend.BussinesLayer.Board;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;
using IntroSE.Kanban.Backend.BussinesLayer.User;
using log4net;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardService
    {
        private BoardFacade boardFacade;
        /// <summary>
        /// Empty Constructor for the BoardService class just for now.
        /// </summary>
        internal BoardService(BoardFacade boardFacade)
        {
            this.boardFacade = boardFacade;
        }

        /// <summary>
        /// This method is used to create a new board for a user.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <returns>An empty response, unless an error occurs</returns>
        public string CreateBoard(string email, string name)
        {
            try
            {
                email = email.ToLower();
                BoardBL bbl = boardFacade.CreateBoard(email, name);
                Response response = new Response();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// This method is used to delete a board for a user.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <returns>An empty response, unless an error occurs</returns>
        public string DeleteBoard(string email, string name)
        {
            try
            {
                email = email.ToLower();
                BoardBL bbl = boardFacade.DeleteBoard(email, name);
                Response response = new Response();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// This method is used to get the name of a column in a board.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="columnOrdinal"></param>
        /// <returns>A strinrg with the column's name, unless an error occurs</returns>
        public string GetNameOfColumn(string email, string boardName, int columnOrdinal)
        {
            try
            {
                email = email.ToLower();
                string name = boardFacade.GetNameOfColumn(email, boardName, columnOrdinal);
                Response response = new Response(null, name);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// This method is used to get a board for a user.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <returns>A string with the board's name, unless an error occurs</returns>
        public string GetBoard(string email, string name)
        {
            try
            {
                email = email.ToLower();
                BoardBL bbl = boardFacade.GetBoard(email, name);
                Response response = new Response(null, new BoardSL(bbl));
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// This method is used to get all boards for a user.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A string with all the user's boards, unless an error occurs</returns>
        public string GetUserBoards(string email)
        {
            try
            {
                email = email.ToLower();
                List<int> list = boardFacade.GetUserBoards(email);
                Response response = new Response(null, list.ToArray());
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// This method is used to limit the number of tasks in a column.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="column"></param>
        /// <param name="newLimit"></param>
        /// <returns>An empty response, unless an error occurs</returns>
        public string LimitTasks(string email, string name, int column, int newLimit)
        {
            try
            {
                email = email.ToLower();
                BoardBL bbl = boardFacade.LimitTasks(email, name, column, newLimit);
                Response response = new Response();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// This method is used to get all tasks in a column.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <param name="column"></param>
        /// <returns>A string with all the tasks in the column, unless an error occurs</returns>
        public string GetTasksOfColumn(string email, string boardname, int column)
        {
            try
            {
                email = email.ToLower();
                List<TaskBL> tbl = boardFacade.GetTasksOfColumn(email, boardname, column);
                List<TaskSL> tsl = new List<TaskSL>();
                foreach (TaskBL t in tbl)
                {
                    tsl.Add(new TaskSL(t));
                }

                Response response = new Response(null, tsl.ToArray());
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// This method is used to get all tasks in progress for a user.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A string with a list of the in-progress tasks of the user, unless an error occurs</returns>
        public string GetInProgressTasks(string email)
        {
            try
            {
                email = email.ToLower();
                List<TaskBL> tbl = boardFacade.GetInProgressTasks(email);
                List<TaskSL> tsl = new List<TaskSL>();
                foreach (TaskBL t in tbl)
                {
                    tsl.Add(new TaskSL(t));
                }

                Response response = new Response(null, tsl.ToArray());
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// This method is used to get the limit of tasks in a column.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="columnOrdinal"></param>
        /// <returns>A string with the limit of tasks in the column, unless an error occurs</returns>
        public string GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            try
            {
                email = email.ToLower();
                int limit = boardFacade.GetColumnLimit(email, boardName, columnOrdinal);
                Response response = new Response(null, limit);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// Allows a user to join a board by its ID.
        /// The user must be logged in and the board must exist.
        /// </summary>
        /// <param name="email">The email of the user joining the board.</param>
        /// <param name="boardId">The ID of the board to join.</param>
        /// <returns>An empty response if successful, or an error message if an exception occurs.</returns>
        public string JoinBoard(string email, int boardId)
        {
            try
            {
                email = email.ToLower();
                boardFacade.JoinBoard(email, boardId);
                Response response = new Response();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// Allows a user to leave a board by its ID.
        /// The user must be logged in, must be a member of the board, and cannot be the owner.
        /// </summary>
        /// <param name="email">The email of the user leaving the board.</param>
        /// <param name="boardId">The ID of the board to leave.</param>
        /// <returns>An empty response if successful, or an error message if an exception occurs.</returns>
        public string LeaveBoard(string email, int boardId)
        {
            try
            {
                email = email.ToLower();
                boardFacade.LeaveBoard(email, boardId);
                Response response = new Response();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// Retrieves the name of a board by its unique board ID.
        /// </summary>
        /// <param name="email">The email of the user requesting the board name.</param>
        /// <param name="boardId">The unique identifier of the board.</param>
        /// <returns>A response containing the board's name if successful, or an error message if an exception occurs.</returns>
        public string GetBoardNameById(string email, int boardId)
        {
            try
            {
                email = email.ToLower();
                string name = boardFacade.GetBoardNameById(boardId);
                Response response = new Response(null, name);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public string ChangeOwner(string email, string newOwnerEmail, string boardname)
        {
            try
            {
                email = email.ToLower();
                newOwnerEmail = newOwnerEmail.ToLower();
                boardFacade.ChangeOwner(email, newOwnerEmail, boardname);
                Response response = new Response();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }
    }
}
