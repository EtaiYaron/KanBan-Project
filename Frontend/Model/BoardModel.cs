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

        private List<TaskModel> BacklogTasks { get; set; }

        private List<TaskModel> InProgressTasks { get; set; }

        private List<TaskModel> DoneTasks { get; set; }

        internal BoardModel(BoardSL board)
        {
            Name = board.Name;
            Owner = board.Owner;
            GetTasks();
        }

        internal BoardModel(string boardName, string owner)
        {
            Name = boardName;
            Owner = owner;
            GetTasks();
        }

        private void GetTasks()
        {
            BacklogTasks = ControllerFactory.Instance.TaskController.GetTasksOfColumn(Owner, Name, 0);
            InProgressTasks = ControllerFactory.Instance.TaskController.GetTasksOfColumn(Owner, Name, 1);
            DoneTasks = ControllerFactory.Instance.TaskController.GetTasksOfColumn(Owner, Name, 2);
        }
    }
}
