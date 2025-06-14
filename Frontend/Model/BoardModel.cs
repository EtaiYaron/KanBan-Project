using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Frontend.Controllers;

namespace IntroSE.Kanban.Frontend.Model
{
    internal class BoardModel
    {
        public string Name { get; }
        public string Owner { get; }
        private string Email { get; set; }

        internal List<string> JoinedUsers { get; private set; }

        internal List<TaskModel> BacklogTasks { get; private set; }

        internal List<TaskModel> InProgressTasks { get; private set; }

        internal List<TaskModel> DoneTasks { get; private set; }

        internal BoardModel(string email, BoardSL board)
        {
           
            Name = board.Name;
            Owner = board.Owner;
            Email = email;
            JoinedUsers = board.JoinedUsers;
            LoadTasks();
        }

        internal BoardModel(string email, string boardName, string owner)
        {
            Name = boardName;
            Owner = owner;
            Email = email;
            JoinedUsers = new List<string> { owner };
            LoadTasks();
        }

        private void LoadTasks()
        {
            BacklogTasks = ControllerFactory.Instance.TaskController.GetTasksOfColumn(Email, Name, 0);
            InProgressTasks = ControllerFactory.Instance.TaskController.GetTasksOfColumn(Email, Name, 1);
            DoneTasks = ControllerFactory.Instance.TaskController.GetTasksOfColumn(Email, Name, 2);
        }
    }
}
