using System;
using System.Collections.Generic;
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
        private readonly string currentUserEmail;
        private static readonly ILog log = LogManager.GetLogger(typeof(UserFacade));
        private int id;

        public BoardFacade(AuthenticationFacade authenticationFacade, string currUserEmail)
        {
            this.boards = new Dictionary<string, Dictionary<string, BoardBL>>();
            this.authenticationFacade = authenticationFacade;
            this.currentUserEmail = currUserEmail;
            this.id = 0;
        }

        public BoardBL CreateBoard(string boardname)
        {
            log.Info($"Attempting to create board {boardname} for user with email {currentUserEmail}.");
            EnsureUserIsLoggedIn();
            if (string.IsNullOrEmpty(boardname) || string.IsNullOrEmpty(boardname.Trim()))
            {
                log.Error($"boardName can't be null.");
                throw new ArgumentException("boardname isn't valid");
            }
            if (boards.ContainsKey(currentUserEmail) && boards[currentUserEmail].ContainsKey(boardname))
            {
                log.Error($"boardName {boardname} already exist for user with email {currentUserEmail}.");
                throw new ArgumentException("boardname already exist under this user");
            }
            if (!boards.ContainsKey(currentUserEmail))
            {
                boards.Add(currentUserEmail, new Dictionary<string, BoardBL>());
            }
            BoardBL curr = new BoardBL(boardname);
            boards[currentUserEmail].Add(boardname, curr);
            log.Info($"Successfully created new board {boardname} for user with email {currentUserEmail}.");
            return curr;
        }

        public BoardBL DeleteBoard(string boardname)
        {
            log.Info($"Attempting to delete board {boardname} for user with email {currentUserEmail}.");
            EnsureUserIsLoggedIn();
            ValidateBoardExists(boardname);
            BoardBL curr = boards[currentUserEmail][boardname];
            boards[currentUserEmail].Remove(boardname);
            log.Info($"Successfully deleted board {boardname} for user with email {currentUserEmail}.");
            return curr;
        }

        public BoardBL MoveTask(string boardname, int taskId, int destcolumn)
        {
            log.Info($"Attempting to move task {taskId} to column {destcolumn} in board {boardname} for user with email {currentUserEmail}.");
            EnsureUserIsLoggedIn();
            ValidateBoardExists(boardname);
            BoardBL board = boards[currentUserEmail][boardname];
            if (!(board.Tasks).ContainsKey(taskId))
            {
                log.Error($"task {taskId} doesn't exist in board {boardname}.");
                throw new ArgumentException("taskId doesn't exist under this board");
            }
            if (destcolumn < 0 || destcolumn > 2)
            {
                log.Error($"dest nust be between 0 and 2, and dest is {destcolumn}.");
                throw new ArgumentOutOfRangeException("dest must be between 0 and 2");
            }
            int fromcolumn = board.GetTask(taskId).State;
            if (destcolumn - fromcolumn != 1)
            {
                log.Error($"can't move task {taskId} from column {fromcolumn} to column {destcolumn}.");
                throw new ArgumentException("cannot move the task to this destination");
            }
            board.MoveTask(taskId, destcolumn);
            log.Info($"Successfully moved task {taskId} to column {destcolumn} in board {boardname} for user with email {currentUserEmail}.");
            return board;
        }

        public TaskBL AddTask(string boardname, string title, DateTime dueTime, string description = "")
        {
            log.Info($"Attempting to add task {id} in board {boardname} for user with email {currentUserEmail}.");
            EnsureUserIsLoggedIn();
            ValidateBoardExists(boardname);
            BoardBL board = boards[currentUserEmail][boardname];
            if ((board.Tasks).ContainsKey(id))
            {
                log.Error($"AddTask failed, task {id} already exist in board {boardname}.");
                throw new ArgumentException("taskId exist task in this board");
            }
            if (string.IsNullOrEmpty(title) || title.Length > 50)
            {
                log.Error($"AddTask failed, title {title} is null / empty / over 50 characters.");
                throw new ArgumentException("title isn't valid");
            }
            if( description.Length > 300)
            {
                log.Error($"AddTask failed, description {description} over 300 character.");
                throw new ArgumentException("description isn't valid");
            }
            if (DateTime.Now.CompareTo(dueTime) >= 0 )
            {
                log.Error($"AddTask failed, dueTime {dueTime} is before current time.");
                throw new ArgumentException("duedate isn't valid");
            }
            TaskBL task = new TaskBL(id, title, dueTime, description);
            board.AddTask(id, title, dueTime, description);
            this.id++;
            log.Info($"Successfully added task {id} to board {boardname} for user with email {currentUserEmail}.");
            return task;
        }

        public BoardBL EditTask(string boardname, int taskId, string title, DateTime dueTime, string description = "")
        {
            log.Info($"Attempting to Edit task {taskId} in board {boardname} for user with email {currentUserEmail}.");
            EnsureUserIsLoggedIn();
            ValidateBoardExists(boardname);
            BoardBL board = boards[currentUserEmail][boardname];
            if (!(board.Tasks).ContainsKey(taskId))
            {
                log.Error($"EditTask failed, task {taskId} isn't exist in board {boardname}.");
                throw new ArgumentException("taskId isn't exist task in this board");
            }
            if (string.IsNullOrEmpty(title) || title.Length > 50)
            {
                log.Error($"AddTask failed, new title {title} is null / empty / over 50 characters.");
                throw new ArgumentException("title isn't valid");
            }
            if (description.Length > 300)
            {
                log.Error($"AddTask failed, new description {description} over 300 character.");
                throw new ArgumentException("title isn't valid");
            }
            if (DateTime.Now.CompareTo(dueTime) >= 0)
            {
                log.Error($"AddTask failed, new dueTime {dueTime} is before current time.");
                throw new ArgumentException("duedate isn't valid");
            }
            board.EditTask(taskId, title, dueTime, description);
            log.Info($"successfully edited task {taskId} in board {boardname} for user with email {currentUserEmail}.");
            return board;
        }

        public BoardBL GetBoard(string boardname)
        {
            log.Info($"Attempting to get board {boardname} for user with email {currentUserEmail}.");
            EnsureUserIsLoggedIn();
            ValidateBoardExists(boardname);
            log.Info($"successfully got board {boardname} for user with email {currentUserEmail}.");
            return boards[currentUserEmail][boardname];
        }

        public TaskBL GetTask(string boardname, int taskId)
        {
            log.Info($"Attempting to get task {taskId} from board {boardname} for user with email {currentUserEmail}.");
            EnsureUserIsLoggedIn();
            ValidateBoardExists(boardname);
            BoardBL board = boards[currentUserEmail][boardname];
            if (!(board.Tasks).ContainsKey(taskId))
            {
                log.Error($"GetTask failed, task {taskId} doesn't exist in board {boardname}");
                throw new ArgumentException("taskId doesn't exist under this board");
            }
            log.Info($"Successfully got task {taskId} from board {boardname} for user with email {currentUserEmail}.");
            return board.GetTask(taskId);
        }

        public Dictionary<int, TaskBL> GetAllTasks(string boardname)
        {
            log.Info($"Attemting to get all tasks from board {boardname} for user with email {currentUserEmail}.");
            EnsureUserIsLoggedIn();
            ValidateBoardExists(boardname);
            BoardBL board = boards[currentUserEmail][boardname];
            log.Info($"Successfully got all tasks from board {boardname} for user with email {currentUserEmail}.");
            return board.Tasks;
        }

        public Dictionary<string, BoardBL> GetAllUserBoards()
        {
            EnsureUserIsLoggedIn();
            return boards[currentUserEmail];
        }

        public BoardBL LimitTasks(string boardname, int column, int newLimit)
        {
            log.Info($"Attempting to limit tasks to {newLimit} in column {column} on board {boardname} for user with email {currentUserEmail}.");
            EnsureUserIsLoggedIn();
            ValidateBoardExists(boardname);
            if (column > 2 || column < 0)
            {
                log.Error($"LimitTasks failed, column {column} is not vaild. must be between 0 and 2.");
                throw new Exception("column isn't valid");
            }
            if (newLimit < 0)
            {
                log.Error($"LimitTasks failed, newLimit {newLimit} is not vaild. must be non negative for user with email {currentUserEmail}.");
                throw new Exception("limit isn't valid");

            }
            BoardBL board = boards[currentUserEmail][boardname];
            board.LimitTasks(column, newLimit);
            log.Info($"Successfully limited tasks to {newLimit} in column {column} on board {boardname} for user with email {currentUserEmail}.");
            return board;
        }

        private void EnsureUserIsLoggedIn()
        {
            if (!authenticationFacade.isLoggedIn(currentUserEmail))
            {
                log.Error($"EnsureUserIsLoggedIn failed, user with email {currentUserEmail} is not logged in.");
                throw new InvalidOperationException("User is not logged in");
            }
        }

        private void ValidateBoardExists(string boardname)
        {
            if (string.IsNullOrEmpty(boardname))
            {
                log.Error($"ValidateBoardExists failed, boardname {boardname} can't be null or empty.");
                throw new ArgumentException(nameof(boardname), "Board name is not valid");
            }
            if (!boards.ContainsKey(currentUserEmail) || !boards[currentUserEmail].ContainsKey(boardname))
            {
                log.Error($"ValidateBoardExists failed,  Board {boardname} does not exist for user with email {currentUserEmail}.");
                throw new ArgumentException("Board does not exist under this user");
            }
        }

    }


}
