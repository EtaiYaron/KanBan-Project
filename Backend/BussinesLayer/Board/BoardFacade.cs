using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;
using IntroSE.Kanban.Backend.BussinesLayer.User;
using log4net;

namespace IntroSE.Kanban.Backend.BussinesLayer.Board
{
    internal class BoardFacade
    {
        private readonly Dictionary<string, Dictionary<string, BoardBL>> boards;
        private AuthenticationFacade authenticationFacade;
        private static readonly ILog log = LogManager.GetLogger(typeof(UserFacade));
        private int id;

        public BoardFacade(AuthenticationFacade authenticationFacade)
        {
            this.boards = new Dictionary<string, Dictionary<string, BoardBL>>();
            this.authenticationFacade = authenticationFacade;
            this.id = 0;
        }

        public BoardBL CreateBoard(string email, string boardname)
        {
            log.Info($"Attempting to create board {boardname} for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            if (string.IsNullOrEmpty(boardname) || string.IsNullOrEmpty(boardname.Trim()))
            {
                log.Error($"CreateBoard failed, boardName can't be null.");
                throw new ArgumentException("boardname isn't valid");
            }
            if (boards.ContainsKey(email) && boards[email].ContainsKey(boardname))
            {
                log.Error($"CreateBoard failed, boardName {boardname} already exist for user with email {email}.");
                throw new ArgumentException("boardname already exist under this user");
            }
            if (!boards.ContainsKey(email))
            {
                boards.Add(email, new Dictionary<string, BoardBL>());
            }
            BoardBL curr = new BoardBL(boardname);
            boards[email].Add(boardname, curr);
            log.Info($"Successfully created new board {boardname} for user with email {email}.");
            return curr;
        }

        public BoardBL DeleteBoard(string email, string boardname)
        {
            log.Info($"Attempting to delete board {boardname} for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            ValidateBoardExists(email, boardname);
            BoardBL curr = boards[email][boardname];
            boards[email].Remove(boardname);
            log.Info($"Successfully deleted board {boardname} for user with email {email}.");
            return curr;
        }

        public BoardBL MoveTask(string email, string boardname, int taskId, int destcolumn)
        {
            log.Info($"Attempting to move task {taskId} to column {destcolumn} in board {boardname} for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            ValidateBoardExists(email, boardname);
            BoardBL board = boards[email][boardname];
            if (!(board.Tasks).ContainsKey(taskId))
            {
                log.Error($"MoveTask failed, task {taskId} doesn't exist in board {boardname}.");
                throw new ArgumentException("taskId doesn't exist under this board");
            }
            if (destcolumn <= 0 || destcolumn > 2)
            {
                log.Error($"MoveTask failed, dest nust be between 1 and 2, and dest is {destcolumn}.");
                throw new ArgumentOutOfRangeException("dest must be between 1 and 2");
            }
            int fromcolumn = board.GetTask(taskId).State;
            if (destcolumn - fromcolumn != 1)
            {
                log.Error($"MoveTask failed, can't move task {taskId} from column {fromcolumn} to column {destcolumn}.");
                throw new ArgumentException("cannot move the task to this destination");
            }
            if (destcolumn == 1)
            {
                if (board.MaxTasks1 != -1 && board.NumTasks1 >= board.MaxTasks1)
                {
                    log.Error($"MoveTask failed, column 1 is full.");
                    throw new ArgumentException("column 1 is full");
                }
            }
            if (board.MaxTasks2 != -1 && board.NumTasks2 >= board.MaxTasks2)
            {
                log.Error($"MoveTask failed, column 2 is full.");
                throw new ArgumentException("column 2 is full");
            }
            board.MoveTask(taskId, destcolumn);
            if (destcolumn == 1)
            {
                board.NumTasks1++;
                board.NumTasks0--;
            }
            else if (destcolumn == 2)
            {
                board.NumTasks2++;
                board.NumTasks1--;
            }
            log.Info($"Successfully moved task {taskId} to column {destcolumn} in board {boardname} for user with email {email}.");
            return board;
        }

        public TaskBL AddTask(string email, string boardname, string title, DateTime dueTime, string description = "")
        {
            log.Info($"Attempting to add task {id} in board {boardname} for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            ValidateBoardExists(email, boardname);
            BoardBL board = boards[email][boardname];
            if ((board.Tasks).ContainsKey(id))
            {
                log.Error($"AddTask failed, task {id} already exist in board {boardname}.");
                throw new ArgumentException("taskId exist task in this board");
            }
            if (string.IsNullOrEmpty(title.Trim()) || title.Length > 50)
            {
                log.Error($"AddTask failed, title {title} is null / empty / over 50 characters.");
                throw new ArgumentException("title isn't valid");
            }
            if (description.Length > 300)
            {
                log.Error($"AddTask failed, description {description} over 300 character.");
                throw new ArgumentException("description isn't valid");
            }
            if (dueTime <= DateTime.Now)
            {
                log.Error($"AddTask failed, dueTime {dueTime} is before current time.");
                throw new ArgumentException("duedate isn't valid");
            }
            if (board.MaxTasks0 != -1 && board.NumTasks0 >= board.MaxTasks0)
            {
                log.Error($"AddTask failed, column 0 is full.");
                throw new ArgumentException("column 0 is full");
            }
            TaskBL task = new TaskBL(id, title, dueTime, description);
            board.AddTask(id, title, dueTime, description);
            board.NumTasks0++;
            this.id++;
            log.Info($"Successfully added task {id} to board {boardname} for user with email {email}.");
            return task;
        }

        public BoardBL EditTask(string email, string boardname, int taskId, string title, DateTime? dueTime, string description = "")
        {
            log.Info($"Attempting to Edit task {taskId} in board {boardname} for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            ValidateBoardExists(email, boardname);
            BoardBL board = boards[email][boardname];
            if (!(board.Tasks).ContainsKey(taskId))
            {
                log.Error($"EditTask failed, task {taskId} isn't exist in board {boardname}.");
                throw new ArgumentException("taskId isn't exist task in this board");
            }
            if (title == null && dueTime == null && description == null)
            {
                log.Error($"EditTask failed, no new arguments to update");
                throw new ArgumentException("all task arguments are null");
            }
            if (title != null && title.Length > 50)
            {
                log.Error($"EditTask failed, new title {title} is null / empty / over 50 characters.");
                throw new ArgumentException("title isn't valid");
            }
            if (description != null && description.Length > 300)
            {
                log.Error($"EditTask failed, new description {description} over 300 character.");
                throw new ArgumentException("title isn't valid");
            }
            if (dueTime != null && dueTime <= DateTime.Now)
            {
                log.Error($"EditTask failed, new dueTime {dueTime} is before current time.");
                throw new ArgumentException("duedate isn't valid");
            }
            board.EditTask(taskId, title, dueTime, description);
            log.Info($"successfully edited task {taskId} in board {boardname} for user with email {email}.");
            return board;
        }

        public BoardBL GetBoard(string email, string boardname)
        {
            log.Info($"Attempting to get board {boardname} for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            ValidateBoardExists(email, boardname);
            log.Info($"successfully got board {boardname} for user with email {email}.");
            return boards[email][boardname];
        }

        public TaskBL GetTask(string email, string boardname, int taskId)
        {
            log.Info($"Attempting to get task {taskId} from board {boardname} for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            ValidateBoardExists(email, boardname);
            BoardBL board = boards[email][boardname];
            if (!(board.Tasks).ContainsKey(taskId))
            {
                log.Error($"GetTask failed, task {taskId} doesn't exist in board {boardname}");
                throw new ArgumentException("taskId doesn't exist under this board");
            }
            log.Info($"Successfully got task {taskId} from board {boardname} for user with email {email}.");
            return board.GetTask(taskId);
        }

        public Dictionary<int, TaskBL> GetAllTasks(string email, string boardname)
        {
            log.Info($"Attemting to get all tasks from board {boardname} for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            ValidateBoardExists(email, boardname);
            BoardBL board = boards[email][boardname];
            log.Info($"Successfully got all tasks from board {boardname} for user with email {email}.");
            return board.Tasks;
        }

        public Dictionary<string, BoardBL> GetAllUserBoards(string email)
        {
            EnsureUserIsLoggedIn(email);
            return boards[email];
        }

        public BoardBL LimitTasks(string email, string boardname, int column, int newLimit)
        {
            log.Info($"Attempting to limit tasks to {newLimit} in column {column} on board {boardname} for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            ValidateBoardExists(email, boardname);
            if (column > 2 || column < 0)
            {
                log.Error($"LimitTasks failed, column {column} is not vaild. must be between 0 and 2.");
                throw new Exception("column isn't valid");
            }
            if (newLimit < 0)
            {
                log.Error($"LimitTasks failed, newLimit {newLimit} is not vaild. must be non negative for user with email {email}.");
                throw new Exception("limit isn't valid");

            }

            BoardBL board = boards[email][boardname];
            if ((column == 0 && newLimit < board.NumTasks0) || (column == 1 && newLimit < board.NumTasks1) || (column == 2 && newLimit < board.NumTasks2))
            {
                log.Error($"LimitTasks failed, newLimit {newLimit} is lower than current numTasks for user with email {email}.");
                throw new Exception("there are already more tasks in the column than the new limit.");
            }
            board.LimitTasks(column, newLimit);
            log.Info($"Successfully limited tasks to {newLimit} in column {column} on board {boardname} for user with email {email}.");
            return board;
        }

        public List<TaskBL> GetTasksOfColumn(string email, string boardname, int column)
        {
            log.Info($"Attempting to get tasks of column {column} on board {boardname} for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            ValidateBoardExists(email, boardname);
            if (column < 0 || column > 2)
            {
                log.Error($"GetTasksOfColumn failed, column {column} inValid.");
                throw new Exception("Illegal column identifier");
            }
            BoardBL board = boards[email][boardname];
            log.Info($"successfully got tasks of column {column} on board {boardname} for user with email {email}.");
            return board.GetTasksOfColumn(column);
        }

        public int GetColumnLimit(string email, string boardname, int column)
        {
            log.Info($"Attempting to get column limit of column {column} on board {boardname} for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            ValidateBoardExists(email, boardname);
            if (column < 0 || column > 2)
            {
                log.Error($"GetColumnLimit failed, column {column} inValid.");
                throw new Exception("Illegal column identifier");
            }
            BoardBL board = boards[email][boardname];
            log.Info($"successfully got column limit of column {column} on board {boardname} for user with email {email}.");
            return board.GetColumnLimit(column);
        }

        public List<TaskBL> GetInProgressTasks(string email)
        {
            log.Info($"Attempting to get in progress tasks for user with email {email}.");
            EnsureUserIsLoggedIn(email);

            List<TaskBL> tasks = new List<TaskBL>();
            if (!boards.ContainsKey(email))
            {
                log.Error($"GetInProgressTasks failed, user with email {email} has no boards.");
                return tasks;
            }

            Dictionary<string, BoardBL> boardsOfUser = boards[email];
            foreach (string boardName in boardsOfUser.Keys)
            {
                foreach (int taskId in boardsOfUser[boardName].Tasks.Keys)
                {
                    if (boardsOfUser[boardName].Tasks[taskId].State == 1)
                        tasks.Add(boardsOfUser[boardName].Tasks[taskId]);
                }
            }
            log.Info($"Successfully got in progress tasks for user with email {email}.");
            return tasks;
        }

        private void EnsureUserIsLoggedIn(string email)
        {
            if (!authenticationFacade.isLoggedIn(email))
            {
                log.Error($"EnsureUserIsLoggedIn failed, user with email {email} is not logged in.");
                throw new InvalidOperationException("User is not logged in");
            }
        }

        private void ValidateBoardExists(string email, string boardname)
        {
            if (string.IsNullOrEmpty(boardname))
            {
                log.Error($"ValidateBoardExists failed, boardname {boardname} can't be null or empty.");
                throw new ArgumentException(nameof(boardname), "Board name is not valid");
            }
            if (!boards.ContainsKey(email) || !boards[email].ContainsKey(boardname))
            {
                log.Error($"ValidateBoardExists failed,  Board {boardname} does not exist for user with email {email}.");
                throw new ArgumentException("Board does not exist under this user");
            }
        }

    }


}
