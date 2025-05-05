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

        public Response AddTask(string email, string boardName, string title, DateTime dueTime, string description)
        {
            try
            {
                TaskBL tbl = boardFacade.AddTask(email, boardName, title, dueTime, description);
                Response response = new Response(null, new TaskSL(tbl));
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response EditTask(string email, string boardName, int taskId, string title, DateTime? dueTime, string description)
        {
            try
            {
                BoardBL bbl = boardFacade.EditTask(email, boardName, taskId, title, dueTime, description);
                Response response = new Response(null, new BoardSL(bbl));
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response MoveTask(string email, string boardName, int taskId, int dest)
        {
            try
            {
                BoardBL bbl = boardFacade.MoveTask(email, boardName, taskId, dest);
                Response response = new Response(null, new BoardSL(bbl));
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response GetTask(string email, string boardName, int taskId)
        {
            try
            {
                TaskBL tbl = boardFacade.GetTask(email, boardName, taskId);
                Response response = new Response(null, new TaskSL(tbl));
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return response;
            }
        }

        public Response GetAllTasks(string email, string boardName)
        {
            try
            {
                Dictionary<int, TaskBL> tbl = boardFacade.GetAllTasks(email, boardName);

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
