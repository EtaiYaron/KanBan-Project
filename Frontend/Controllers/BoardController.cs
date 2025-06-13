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

        public void DeleteBoard(string email, string boardName)
        {
            string response = boardService.DeleteBoard(email, boardName);
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(response);
            if (res.ErrorMessage != null)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

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

        public BoardModel GetBoard(string email, string boardName)
        {
            string response = boardService.GetBoard(email, boardName);
            Response<BoardSL> res = JsonSerializer.Deserialize<Response<BoardSL>>(response);
            if (res.ErrorMessage != null)
            {
                throw new Exception(res.ErrorMessage);
            }
            return new BoardModel(res.ReturnValue);
        }

        public BoardModel CreateBoard(string name, string owner)
        {
            string response = boardService.CreateBoard(owner, name);
            Response<object> res = JsonSerializer.Deserialize<Response<object>>(response);
            if (res.ErrorMessage != null)
            {
                throw new Exception(res.ErrorMessage);
            }
            return new BoardModel(name, owner.ToLower());
        }
    }
}
