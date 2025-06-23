using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Frontend.Model;
using System.Text.Json;

namespace IntroSE.Kanban.Frontend.Controllers
{
    internal class TaskController
    {
        private BoardService boardService;

        public TaskController(BoardService boardService)
        {
            this.boardService = boardService;
        }

        /// <summary>
        /// Retrieves all tasks in a specific column of a board for a user.
        /// </summary>
        /// <param name="email">The email of the user requesting the tasks.</param>
        /// <param name="boardName">The name of the board.</param>
        /// <param name="column">The column ordinal (e.g., 0 for backlog, 1 for in progress, 2 for done).</param>
        /// <returns>A list of <see cref="TaskModel"/> objects in the specified column.</returns>
        /// <exception cref="Exception">Thrown if the backend returns an error message.</exception>
        public List<TaskModel> GetTasksOfColumn(string email, string boardName, int column)
        {
            string response = boardService.GetTasksOfColumn(email, boardName, column);
            Response<TaskSL[]> res = JsonSerializer.Deserialize<Response<TaskSL[]>>(response);
            if (res.ErrorMessage != null)
            {
                throw new Exception(res.ErrorMessage);
            }

            List<TaskModel> tasks = new List<TaskModel>();
            for (int i = 0; i < res.ReturnValue.Length; i++)
            {
                tasks.Add(new TaskModel(res.ReturnValue[i]));
            }
            return tasks;
        }
    }
}
