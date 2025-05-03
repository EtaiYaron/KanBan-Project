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
using log4net;


namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class TaskService
    {
        private BoardFacade boardFacade;

        /// <summary>
        /// Empty Constructor for the TaskService class just for now.
        /// </summary>

        internal TaskService(BoardFacade boardFacade)
        {
            this.boardFacade = boardFacade;
        }

        public Response AddTask(string boardName, string title, DateTime dueTime, string description)
        {
            try
            {
                TaskBL tbl = boardFacade.AddTask(boardName, title, dueTime, description);
                Response response = new Response(null, new TaskSL(tbl));
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response EditTask(string boardName, int taskId, string title, DateTime dueTime, string description)
        {
            try
            {
                BoardBL bbl = boardFacade.EditTask(boardName, taskId, title, dueTime, description);
                Response response = new Response(null, new BoardSL(bbl));
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response MoveTask(string boardName, int taskId, int dest)
        {
            try
            {
                BoardBL bbl = boardFacade.MoveTask(boardName, taskId, dest);
                Response response = new Response(null, new BoardSL(bbl));
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response GetTask(string boardName, int taskId)
        {
            try
            {
                TaskBL tbl = boardFacade.GetTask(boardName, taskId);
                Response response = new Response(null, new TaskSL(tbl));
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response GetAllTasks(string boardName)
        {
            try
            {
                Dictionary<int, TaskBL> tbl = boardFacade.GetAllTasks(boardName);

                Dictionary<int, TaskSL> serviceTbl = new Dictionary<int, TaskSL>();
                foreach (int key in tbl.Keys)
                {
                    serviceTbl.Add(key, new TaskSL(tbl[key]));
                }

                Response response = new Response(null, serviceTbl);
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
