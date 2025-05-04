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


        public Response CreateBoard(string email, string name)
        {
            try
            {
                BoardBL bbl = boardFacade.CreateBoard(email, name);
                Response response = new Response(null, new BoardSL(bbl));
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response DeleteBoard(string email, string name)
        {
            try
            {
                BoardBL bbl = boardFacade.DeleteBoard(email, name);
                Response response = new Response(null, new BoardSL(bbl));
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response GetBoard(string email, string name)
        {
            try
            {
                BoardBL bbl = boardFacade.GetBoard(email, name);
                Response response = new Response(null, new BoardSL(bbl));
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response getAllUserBoards(string email)
        {
            try
            {
                Dictionary<string, BoardBL> bbl = boardFacade.GetAllUserBoards(email);

                Dictionary<string, BoardSL> serviceBbl = new Dictionary<string, BoardSL>();
                foreach (string key in bbl.Keys) {
                    serviceBbl.Add(key, new BoardSL(bbl[key]));
                }

                Response response = new Response(null, serviceBbl);
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response LimitTasks(string email, string name, int column, int newLimit)
        {
            try
            {
                BoardBL bbl = boardFacade.LimitTasks(email, name, column, newLimit);
                Response response = new Response(null, new BoardSL(bbl));
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response GetTasksOfColumn(string email, string boardname, int column)
        {
            try
            {
                List<TaskBL> tbl = boardFacade.GetTasksOfColumn(email, boardname, column);
                List<TaskSL> tsl = new List<TaskSL>();
                foreach (TaskBL t in tbl)
                {
                    tsl.Add(new TaskSL(t));
                }

                Response response = new Response(null, tsl);
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response GetInProgressTasks(string email)
        {
            try
            {
                List<TaskBL> tbl = boardFacade.GetInProgressTasks(email);
                List<TaskSL> tsl = new List<TaskSL>();
                foreach (TaskBL t in tbl)
                {
                    tsl.Add(new TaskSL(t));
                }

                Response response = new Response(null, tsl);
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            try
            {
                int limit = boardFacade.GetColumnLimit(email, boardName, columnOrdinal);
                Response response = new Response(null, limit);
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
