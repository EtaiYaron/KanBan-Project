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
        private static readonly ILog log = LogManager.GetLogger(typeof(TaskService));


        /// <summary>
        /// Empty Constructor for the TaskService class just for now.
        /// </summary>
        public TaskService()
        {
            this.boardFacade = new BoardFacade(new AuthenticationFacade());
        }

        internal TaskService(BoardFacade boardFacade)
        {
            this.boardFacade = boardFacade;
        }

        public string AddTask(string boardName, int taskId, string title, DateTime dueTime, string description)
        {
            try
            {
                BoardBL bbl = boardFacade.AddTask(boardName, taskId, title, dueTime, description);
                Response response = new Response(null, bbl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public String EditTask(string boardName, int taskId, string title, DateTime dueTime, string description)
        {
            try
            {
                BoardBL bbl = boardFacade.EditTask(boardName, taskId, title, dueTime, description);
                Response response = new Response(null, bbl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public String MoveTask(string boardName, int taskId, int dest)
        {
            try
            {
                BoardBL bbl = boardFacade.MoveTask(boardName, taskId, dest);
                Response response = new Response(null, bbl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public String GetTask(string boardName, int taskId)
        {
            try
            {
                TaskBL tbl = boardFacade.GetTask(boardName, taskId);
                Response response = new Response(null, tbl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public String GetAllTasks(string boardName)
        {
            try
            {
                Dictionary<int, TaskBL> dtbl = boardFacade.GetAllTasks(boardName);
                Response response = new Response(null, dtbl);
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
