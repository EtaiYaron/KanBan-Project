using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BussinesLayer.Board
{
    internal class BoardBL
    {
        private string name;
        private Dictionary<int, TaskBL> tasks;
        private int maxTasks;

        public BoardBL(string name)
        {
            this.name = name;
            this.tasks = new Dictionary<int, TaskBL>();
            this.maxTasks = -1;
        }

        public void AddTask(int taskId, string title, DateTime dueDate, string description)
        {
            throw new NotImplementedException();
        }

        public void EditTask(int taskId, string title, DateTime dueDate, string description)
        {
            throw new NotImplementedException();
        }

        public void MoveTask(int taskId, int dest)
        {
            throw new NotImplementedException();
        }

        public TaskBL GetTask(int taskId)
        {
            throw new NotImplementedException();
        }

        public List<TaskBL> GetAllTasks()
        {
            throw new NotImplementedException();
        }

        public void LimitTasks(int newLimit)
        {
            throw new NotImplementedException();
        }
    }
}
