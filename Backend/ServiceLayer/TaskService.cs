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

        /// <summary>
        /// This method is used to add a task to a board.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="title"></param>
        /// <param name="dueTime"></param>
        /// <param name="description"></param>
        /// <returns>An empty response, unless an error occurs</returns>
        public string AddTask(string email, string boardName, string title, DateTime dueTime, string description)
        {
            try
            {
                boardFacade.AddTask(email, boardName, title, dueTime, description);
                Response<object> response = new Response<object>();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response<object> response = new Response<object>(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// This method is used to edit a task in a board.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="taskId"></param>
        /// <param name="title"></param>
        /// <param name="dueTime"></param>
        /// <param name="description"></param>
        /// <returns>An empty response, unless an error occurs</returns>
        public string EditTask(string email, string boardName, int taskId, string title, DateTime? dueTime, string description)
        {
            try
            {
                BoardBL bbl = boardFacade.EditTask(email, boardName, taskId, title, dueTime, description);
                Response<object> response = new Response<object>();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response<object> response = new Response<object>(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// This method is used to move a task in a board.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="taskId"></param>
        /// <param name="dest"></param>
        /// <returns>An empty response, unless an error occurs</returns>
        public string MoveTask(string email, string boardName, int taskId, int dest)
        {
            try
            {
                BoardBL bbl = boardFacade.MoveTask(email, boardName, taskId, dest);
                Response<object> response = new Response<object>();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response<object> response = new Response<object>(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// This method is used to get a task from a board.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="taskId"></param>
        /// <returns>A string with the task's details, unless an error occurs</returns>
        public string GetTask(string email, string boardName, int taskId)
        {
            try
            {
                TaskBL tbl = boardFacade.GetTask(email, boardName, taskId);
                Response<TaskSL> response = new Response<TaskSL>(null, new TaskSL(tbl));
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response<TaskSL> response = new Response<TaskSL>(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

        /// <summary>
        /// Assigns a task to a specified user on a board.
        /// </summary>
        /// <param name="email">The email of the user performing the assignment.</param>
        /// <param name="boardname">The name of the board containing the task.</param>
        /// <param name="taskId">The ID of the task to assign.</param>
        /// <param name="emailTo">The email of the user to assign the task to.</param>
        /// <returns>A JSON serialized response indicating success or error details.</returns>
        public string AssignTaskToUser(string email, string boardname, int taskId, string emailTo)
        {
            try
            {
                boardFacade.AssignTaskToUser(email, boardname, taskId, emailTo);
                Response<object> response = new Response<object>();
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                Response<object> response = new Response<object>(ex.Message);
                return JsonSerializer.Serialize(response);
            }
        }

    }
}
