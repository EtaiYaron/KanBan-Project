using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BussinesLayer.Board
{
    internal class ColumnBL
    {
        private Dictionary<int, TaskBL> tasks;
        private readonly int columnId;
        private readonly string name;
        private int maxTasks;
        public ColumnBL(int columnId, string name, int maxTasks=-1)
        {
            tasks = new Dictionary<int, TaskBL>();
            this.columnId = columnId;
            this.name = name;
            this.maxTasks = maxTasks;
        }
        /// <summary>
        /// This method is used to add a task to the board.
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="title"></param>
        /// <param name="dueDate"></param>
        /// <param name="description"></param>
        public void AddTask(TaskBL task)
        {
            tasks.Add(task.TaskId, task);
        }

        /// <summary>
        /// Removes a task with the specified task ID from the column.
        /// </summary>
        /// <param name="taskid">The ID of the task to remove.</param>
        public void RemoveTask(int taskid)
        {
            tasks.Remove(taskid);
        }
        public int ColumnId => columnId;
        public string Name => name;
        public int MaxTasks
        {
            get => maxTasks;
            set
            {
                maxTasks = value;
            }
        }
        public int GetNumTasks()
        {
            return tasks.Count;
        }

        public Dictionary<int, TaskBL> Tasks
        {
            get => tasks;
        }
    }
}
