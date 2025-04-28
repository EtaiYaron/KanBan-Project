using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
        public BoardService()
        {
            this.boardFacade = new BoardFacade(new AuthenticationFacade());
        }
        internal BoardService(BoardFacade boardFacade)
        {
            this.boardFacade = boardFacade;
        }


        public string CreateBoard(string name)
        {
            try
            {
                BoardBL bbl = boardFacade.CreateBoard(name);
                Response response = new Response(null, bbl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public string DeleteBoard(string name)
        {
            try
            {
                BoardBL bbl = boardFacade.DeleteBoard(name);
                Response response = new Response(null, bbl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public string GetBoard(string name)
        {
            try
            {
                BoardBL bbl = boardFacade.GetBoard(name);
                Response response = new Response(null, bbl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public string LimitTasks(string name, int column, int newLimit)
        {
            try
            {
                BoardBL bbl = boardFacade.LimitTasks(name, column, newLimit);
                Response response = new Response(null, bbl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public Response GetId(string name)
        {
            throw new NotImplementedException();
        }
    }
}
