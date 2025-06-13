using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Frontend.Controllers;
using IntroSE.Kanban.Frontend.Model;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    internal class TasksVM : Notifiable
    {
        private string errorMessage;
        private UserModel user;

        private List<TaskModel> backlog;
        private List<TaskModel> inProgress;
        private List<TaskModel> done;

        TaskController taskController = ControllerFactory.Instance.TaskController;
    }
}
