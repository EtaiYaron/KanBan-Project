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
                log.Info($"Attemting to add new task to board {boardName}.");
                BoardBL bbl = boardFacade.AddTask(boardName, taskId, title, dueTime, description);
                log.Info($"Task added successfully to board {boardName}.");
                Response response = new Response(null, bbl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                log.Error($"Failed to add task to board {boardName}: {ex.Message}.",ex);
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public String EditTask(string boardName, int taskId, string title, DateTime dueTime, string description)
        {
            try
            {
                log.Info($"Attemting to edit task {taskId} on board {boardName}.");
                BoardBL bbl = boardFacade.EditTask(boardName, taskId, title, dueTime, description);
                log.Info($"Task {taskId} edited successfully on board {boardName}.");
                Response response = new Response(null, bbl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                log.Error($"Failed to edit task {taskId} on board {boardName}: {ex.Message}.",ex);
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public String MoveTask(string boardName, int taskId, int dest)
        {
            try
            {
                log.Info($"Attemting to move task {taskId} to {dest} on board {boardName}.");
                BoardBL bbl = boardFacade.MoveTask(boardName, taskId, dest);
                log.Info($"Task {taskId} moved successfully to {dest} on board {boardName}.");
                Response response = new Response(null, bbl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                log.Error($"Failed to move task {taskId} to {dest} on board {boardName}: {ex.Message}.", ex);
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public String GetTask(string boardName, int taskId)
        {
            try
            {
                log.Info($"Attemting to get task {taskId} from board {boardName}.");
                TaskBL tbl = boardFacade.GetTask(boardName, taskId);
                log.Info($"Successfully got task {taskId} from board {boardName}.");
                Response response = new Response(null, tbl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                log.Error($"Failed to get task {taskId} from board: {ex.Message}.", ex);
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        public String GetAllTasks(string boardName)
        {
            try
            {
                log.Info($"Attemting to get all tasks from board {boardName} .");
                Dictionary<int, TaskBL> dtbl = boardFacade.GetAllTasks(boardName);
                log.Info($"Successfully got all tasks from board {boardName} .");
                Response response = new Response(null, dtbl);
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                log.Error($"Failed to get all tasks from board: {ex.Message}.", ex);
                Response response = new Response(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }
    }
}
