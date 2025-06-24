using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Frontend.Model;

namespace IntroSE.Kanban.Frontend.Controllers
{
    internal class BoardController
    {
        private BoardService boardService;

        public BoardController(BoardService boardService)
        {
            this.boardService = boardService;
        }

        /// <summary>
        /// Deletes a board for a specific user.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteBoard(string email, string boardName)
        {
            string response = boardService.DeleteBoard(email, boardName);
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(response);
            if (res.ErrorMessage != null)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        /// <summary>
        /// Gets the IDs of all boards the user is a member of.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int[]? GetUserBoards(string email)
        {
            string response = boardService.GetUserBoards(email);
            Response<int[]> res = JsonSerializer.Deserialize<Response<int[]>>(response);
            if (res.ErrorMessage != null)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res.ReturnValue;
        }

        /// <summary>
        /// Gets the name of a board by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetBoardName(int id)
        {
            string response = boardService.GetBoardNameById(id);
            Response<string> res = JsonSerializer.Deserialize<Response<string>>(response);
            if (res.ErrorMessage != null)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res.ReturnValue;
        }

        /// <summary>
        /// Gets a board model for a specific user and board name.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="boardName">The name of the board.</param>
        /// <returns>A <see cref="BoardModel"/> representing the board.</returns>
        /// <exception cref="Exception">Thrown if the backend returns an error.</exception>
        public BoardModel GetBoard(string email, string boardName)
        {
            string response = boardService.GetBoard(email, boardName);
            Response<BoardSL> res = JsonSerializer.Deserialize<Response<BoardSL>>(response);
            if (res.ErrorMessage != null)
            {
                throw new Exception(res.ErrorMessage);
            }
            return new BoardModel(email, res.ReturnValue);
        }

        /// <summary>
        /// Creates a new board for a user.
        /// </summary>
        /// <param name="name">The name of the new board.</param>
        /// <param name="owner">The email of the board owner.</param>
        /// <returns>A <see cref="BoardModel"/> representing the created board.</returns>
        /// <exception cref="Exception">Thrown if the backend returns an error.</exception>
        public BoardModel CreateBoard(string name, string owner)
        {
            string response = boardService.CreateBoard(owner, name);
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(response);
            if (res.ErrorMessage != null)
            {
                throw new Exception(res.ErrorMessage);
            }
            return new BoardModel(owner.ToLower(), name, owner.ToLower());
        }
    }
}
