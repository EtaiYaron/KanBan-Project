using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Frontend.Controllers;
using IntroSE.Kanban.Frontend.Model;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    internal class TasksVM : Notifiable
    {
        private string taskMsg;

        private string owner;
        private List<string> joinedUsers;
        private List<TaskModel> backlog;
        private List<TaskModel> inProgress;
        private List<TaskModel> done;
        
        public TasksVM(BoardModel bm)
        {
            this.backlog = bm.BacklogTasks;
            this.inProgress = bm.InProgressTasks;
            this.done = bm.DoneTasks;
            this.joinedUsers = bm.JoinedUsers;
            this.owner = "Owner of this board: " + bm.Owner;
            this.taskMsg = "Tasks for board: " + bm.Name;
        }

        public string Owner
        {
            get => owner;
        }

        public List<string> JoinedUsers
        {
            get => joinedUsers;
        }

        public List<TaskModel> Backlog
        {
            get => backlog;
        }

        public List<TaskModel> InProgress
        {
            get => inProgress;
        }

        public List<TaskModel> Done
        {
            get => done;
        }

        public string TaskMsg
        {
            get => taskMsg;
        }
    }
}
