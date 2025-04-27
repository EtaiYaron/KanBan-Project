using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using IntroSE.Kanban.Backend.BussinesLayer.Board;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;
using IntroSE.Kanban.Backend.BussinesLayer.User;

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


        public Response CreateBoard(string name)
        {
            try
            {
                BoardBL bbl = boardFacade.CreateBoard(name);
                Response response = new Response(null, bbl);
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response DeleteBoard(string name)
        {
            try
            {
                BoardBL bbl = boardFacade.DeleteBoard(name);
                Response response = new Response(null, bbl);
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response GetBoard(string name)
        {
            try
            {
                BoardBL bbl = boardFacade.GetBoard(name);
                Response response = new Response(null, bbl);
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response getAllUserBoards()
        {
            try
            {
                Dictionary<string, BoardBL> bbl = boardFacade.GetAllUserBoards();
                Response response = new Response(null, bbl);
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response LimitTasks(string name, int column, int newLimit)
        {
            try
            {
                BoardBL bbl = boardFacade.LimitTasks(name, column, newLimit);
                Response response = new Response(null, bbl);
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }
    }
}
