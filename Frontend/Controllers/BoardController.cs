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

        public int[] GetUserBoards(string email)
        {
            string response = boardService.GetUserBoards(email);
            Response res = JsonSerializer.Deserialize<Response>(response);
            if (res.ErrorMessage != null)
            {
                throw new Exception(res.ErrorMessage);
            }
            return (int[])res.ReturnValue;
        }

        public BoardModel CreateBoard(string name, string owner)
        {
            string response = boardService.CreateBoard(owner, name);
            Response res = JsonSerializer.Deserialize<Response>(response);
            if (res.ErrorMessage != null)
            {
                throw new Exception(res.ErrorMessage);
            }
            return new BoardModel(name, owner.ToLower());
        }
    }
}
