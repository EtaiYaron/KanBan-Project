using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using IntroSE.Kanban.Backend.BussinesLayer.Cross_Cutting;
using IntroSE.Kanban.Backend.BussinesLayer.User;
using IntroSE.Kanban.Backend.ServiceLayer;
using log4net;

namespace IntroSE.Kanban.Backend.BussinesLayer.Board
{
    internal class BoardFacade
    {
        private readonly Dictionary<string, Dictionary<string, BoardBL>> boards;
        private AuthenticationFacade authenticationFacade;
        private static readonly ILog log = LogManager.GetLogger(typeof(UserFacade));
        private int nextBoardId;

        public BoardFacade(AuthenticationFacade authenticationFacade)
        {
            this.boards = new Dictionary<string, Dictionary<string, BoardBL>>();
            this.authenticationFacade = authenticationFacade;
            this.nextBoardId = 0;
        }

        /// <summary>
        /// This method is used to create a new board for a user.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <returns>returns the boardBL object we created</returns>
        /// <exception cref="ArgumentException"></exception>
        public BoardBL CreateBoard(string email, string boardname)
        {
            log.Info($"Attempting to create board {boardname} for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            if (string.IsNullOrEmpty(boardname) || string.IsNullOrEmpty(boardname.Trim()))
            {
                log.Error($"CreateBoard failed, boardName can't be null.");
                throw new ArgumentException("boardname isn't valid");
            }
            if (boards.ContainsKey(email) && CustomContainsKey(boards[email], boardname))
            {
                log.Error($"CreateBoard failed, boardName {boardname} already exist for user with email {email}.");
                throw new ArgumentException("boardname already exist under this user");
            }
            if (!boards.ContainsKey(email))
            {
                boards.Add(email, new Dictionary<string, BoardBL>());
            }
            BoardBL curr = new BoardBL(nextBoardId, boardname, email);
            boards[email].Add(boardname, curr);
            nextBoardId++;
            curr.JoinUser(email);
            log.Info($"Successfully created new board {boardname} for user with email {email}.");
            return curr;
        }

        /// <summary>
        /// This method is used to check if a board with the given name exists in the dictionary.
        /// </summary>
        /// <param name="boards"></param>
        /// <param name="boardName"></param>
        /// <returns>>returns true if a board with the given name already exists.</returns></returns>
        private bool CustomContainsKey(Dictionary<string, BoardBL> boards, string boardName)
        {
            foreach (string key in boards.Keys)
            {
                if (key.ToLower() == boardName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// This method is used to delete a board for a user.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <returns>returns the boardBL object we deleted</returns>
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


        /// <summary>
        /// This method is used to move a task from its current column to the next column.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <param name="taskId"></param>
        /// <param name="destcolumn"></param>
        /// <returns>returns the boardBL object we moved the task in</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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

        /// <summary>
        /// This method is used to add a task to a board.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <param name="title"></param>
        /// <param name="dueTime"></param>
        /// <param name="description"></param>
        /// <returns>returns the taskBL object we added</returns>
        /// <exception cref="ArgumentException"></exception>
        public TaskBL AddTask(string email, string boardname, string title, DateTime dueTime, string description = "")
        {
            log.Info($"Attempting to add task in board {boardname} for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            ValidateBoardExists(email, boardname);
            BoardBL board = boards[email][boardname];
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
            TaskBL task = new TaskBL(board.NextTaskId, title, dueTime, description);
            board.AddTask(title, dueTime, description);
            board.NumTasks0++;
            log.Info($"Successfully added task {board.NextTaskId} to board {boardname} for user with email {email}.");
            return task;
        }

        /// <summary>
        /// This method is used to edit a task in a board.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <param name="taskId"></param>
        /// <param name="title"></param>
        /// <param name="dueTime"></param>
        /// <param name="description"></param>
        /// <returns>returns the boardBL object we edited the task in</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
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
            if (board.Tasks[taskId].State == 2)
            {
                log.Error($"EditTask failed, can't edit a task that is done");
                throw new Exception("Editing a done task is not allowed");
            }
            if (!string.IsNullOrEmpty(title) && (title.Length > 50 || title.Trim() == ""))
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

        /// <summary>
        /// This method is used to get the name of a column by its ordinal number.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="columnOrdinal"></param>
        /// <returns>returns the name of the column</returns>
        /// <exception cref="ArgumentException"></exception>
        public string GetNameOfColumn(string email, string boardName, int columnOrdinal)
        {
            log.Info($"Attempting to get the column name of column with ordinal {columnOrdinal}.");
            EnsureUserIsLoggedIn(email);
            ValidateBoardExists(email, boardName);
            if (columnOrdinal < 0 || columnOrdinal > 2)
            {
                log.Error($"Got invalid column ordinal, such ordinal doesn't exist");
                throw new ArgumentException("Invalid column ordinal");
            }          

            if (columnOrdinal == 0) 
                return "backlog";
            if (columnOrdinal == 1) 
                return "in progress";
            return "done";
        }

        /// <summary>
        /// This method is used to get a board by its name.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <returns>returns the boardBL object we got</returns>
        public BoardBL GetBoard(string email, string boardname)
        {
            log.Info($"Attempting to get board {boardname} for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            ValidateBoardExists(email, boardname);
            log.Info($"successfully got board {boardname} for user with email {email}.");
            return boards[email][boardname];
        }

        /// <summary>
        /// This method is used to get a task by its ID.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <param name="taskId"></param>
        /// <returns>returns the taskBL object we got</returns>
        /// <exception cref="ArgumentException"></exception>
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


        /// <summary>
        /// This method is used to get all tasks in a board.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <returns>returns a dictionary of all tasks in the board</returns>
        public Dictionary<int, TaskBL> GetAllTasks(string email, string boardname)
        {
            log.Info($"Attemting to get all tasks from board {boardname} for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            ValidateBoardExists(email, boardname);
            BoardBL board = boards[email][boardname];
            log.Info($"Successfully got all tasks from board {boardname} for user with email {email}.");
            return board.Tasks;
        }

        /// <summary>
        /// Retrieves the IDs of all boards that the specified user is a member of.
        /// The user must be logged in. If the user has no boards, an empty list is returned.
        /// </summary>
        /// <param name="email">The email of the user whose boards are to be retrieved.</param>
        /// <returns>A list of board IDs that the user is a member of.</returns>
        public List<int> GetUserBoards(string email)
        {
            log.Info($"Attempting to get all boards for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            List<int> boardIds = new List<int>();
            if (!boards.ContainsKey(email))
            {
                return boardIds;
            }
            foreach (var board in boards[email].Values)
            {
                boardIds.Add(board.BoardId);
            }
            log.Info($"Successfully got all boards for user with email {email}.");
            return boardIds;
        }

        /// <summary>
        /// This method is used to limit the number of tasks in a column.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <param name="column"></param>
        /// <param name="newLimit"></param>
        /// <returns>returns the boardBL object we limited the tasks in</returns>
        /// <exception cref="Exception"></exception>
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
            if (newLimit <= 0 && newLimit != -1)
            {
                log.Error($"LimitTasks failed, newLimit {newLimit} is not vaild. must be non negative for user with email {email}.");
                throw new Exception("limit isn't valid");

            }

            BoardBL board = boards[email][boardname];
            if (newLimit != -1 && ((column == 0 && newLimit < board.NumTasks0) || (column == 1 && newLimit < board.NumTasks1) || (column == 2 && newLimit < board.NumTasks2)))
            {
                log.Error($"LimitTasks failed, newLimit {newLimit} is lower than current numTasks for user with email {email}.");
                throw new Exception("there are already more tasks in the column than the new limit.");
            }
            board.LimitTasks(column, newLimit);
            log.Info($"Successfully limited tasks to {newLimit} in column {column} on board {boardname} for user with email {email}.");
            return board;
        }

        /// <summary>
        /// This method is used to get all tasks in a column.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <param name="column"></param>
        /// <returns>returns a list of all tasks in the column</returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// This method is used to get the limit of a column.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <param name="column"></param>
        /// <returns>returns the limit of the column</returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// This method is used to get all tasks that are in progress.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>returns a list of all tasks that are in progress</returns>
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

        /// <summary>
        /// This method is used to ensure that the user is logged in.
        /// </summary>
        /// <param name="email"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void JoinBoard(string email, int boardId)
        {
            log.Info($"Attempting to join board with ID {boardId} for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            BoardBL board = GetBoardIfExists(boardId);
            if (board.IsUserInBoard(email))
            {
                log.Error($"JoinBoard failed, user with email {email} is already joined to board with ID {boardId}.");
                throw new Exception("User is already joined this board");
            }
            board.JoinUser(email);
            if (!boards.ContainsKey(email))
            {
                boards.Add(email, new Dictionary<string, BoardBL>());
            }
            boards[email].Add(board.Name, board);
            log.Info($"Successfully joined board with ID {boardId} for user with email {email}.");
        }

        /// <summary>
        /// Allows a user to leave a board by its ID.
        /// The user must be logged in, must be a member of the board, and cannot be the owner.
        /// Removes the user from the board and from the user's board list.
        /// </summary>
        /// <param name="email">The email of the user leaving the board.</param>
        /// <param name="boardId">The ID of the board to leave.</param>
        /// <exception cref="InvalidOperationException">Thrown if the user is not logged in.</exception>
        /// <exception cref="Exception">Thrown if the user is not in the board or is the owner.</exception>
        public void LeaveBoard(string email, int boardId)
        {
            log.Info($"Attempting to leave board with ID {boardId} for user with email {email}.");
            EnsureUserIsLoggedIn(email);
            BoardBL board = GetBoardIfExists(boardId);
            if (!board.IsUserInBoard(email))
                throw new Exception("User is not in board");
            if (board.Owner == email)
                throw new Exception("Owner of board can't leave it");
            board.LeaveUser(email);
            boards[email].Remove(board.Name);
            log.Info($"Successfully left board with ID {boardId} for user with email {email}.");
        }

        /// <summary>
        /// Changes the owner of a board to a new user.
        /// The current owner must be logged in, must be the actual owner of the board, and both the current and new owner must be members of the board.
        /// If the requirements are not met, an exception is thrown.
        /// </summary>
        /// <param name="owneremail">The email of the current owner of the board.</param>
        /// <param name="newOwnerEmail">The email of the new owner to assign.</param>
        /// <param name="boardname">The name of the board whose ownership is to be changed.</param>
        /// <returns>The email of the new owner.</returns>
        /// <exception cref="Exception">Thrown if the current owner is not logged in, not the owner, or if either user is not a member of the board.</exception>
        public void ChangeOwner(string owneremail, string newOwnerEmail, string boardname)
        {
            log.Info($"Attempting to change owner of board {boardname} from {owneremail} to {newOwnerEmail}.");
            EnsureUserIsLoggedIn(owneremail);
            ValidateBoardExists(owneremail, boardname);
            BoardBL board = boards[owneremail][boardname];
            if (!board.IsUserInBoard(owneremail) || !board.IsUserInBoard(newOwnerEmail))
            {
                log.Error($"ChangeOwner failed, user {owneremail} or {newOwnerEmail} is not in board {boardname}.");
                throw new Exception("User is not in board");
            }
            if (board.Owner != owneremail)
            {
                log.Error($"ChangeOwner failed, user {owneremail} is not the owner of board {boardname}.");
                throw new Exception("User is not the owner of the board");
            }
            if (!board.IsUserInBoard(newOwnerEmail))
            {
                log.Error($"ChangeOwner failed, new owner {newOwnerEmail} is not in board {boardname}.");
                throw new Exception("New owner is not in board");
            }
            board.Owner = newOwnerEmail;
            log.Info($"Successfully changed owner of board {boardname} from {owneremail} to {newOwnerEmail}.");
        }

        /// <summary>
        /// Retrieves the name of a board by its unique board ID.
        /// Searches all users' boards for a matching board ID and returns the board's name.
        /// </summary>
        /// <param name="boardId">The unique identifier of the board.</param>
        /// <returns>The name of the board with the specified ID.</returns>
        /// <exception cref="Exception">Thrown if no board with the given ID exists.</exception>
        public string GetBoardNameById(int boardId)
        {
            log.Info($"Attempting to get board name by ID {boardId}.");
            BoardBL board = GetBoardIfExists(boardId);
            log.Info($"Successfully got board name {board.Name} for board ID {boardId}.");
            return board.Name;
        }

        /// <summary>
        /// Retrieves a board by its unique board ID if it exists.
        /// Searches all users' boards for a matching board ID.
        /// </summary>
        /// <param name="boardId">The ID of the board to find.</param>
        /// <returns>The BoardBL object with the specified ID.</returns>
        /// <exception cref="Exception">Thrown if no board with the given ID exists.</exception>
        private BoardBL GetBoardIfExists(int boardId)
        {
            foreach (string email in boards.Keys)
            {
                Dictionary<string, BoardBL> keyValuePairs = boards[email];
                foreach (string boardName in keyValuePairs.Keys)
                {
                    if (keyValuePairs[boardName].BoardId == boardId)
                        return keyValuePairs[boardName];
                }
            }
            log.Error($"GetBoardIfExists failed, no board with id {boardId} exists.");
            throw new Exception("Board with such id doesn't exist");
        }

        /// <summary>
        /// Ensures that the user with the given email is currently logged in.
        /// </summary>
        /// <param name="email">The email of the user to check.</param>
        /// <exception cref="InvalidOperationException">Thrown if the user is not logged in.</exception>
        private void EnsureUserIsLoggedIn(string email)
        {
            if (!authenticationFacade.isLoggedIn(email))
            {
                log.Error($"EnsureUserIsLoggedIn failed, user with email {email} is not logged in.");
                throw new InvalidOperationException("User is not logged in");
            }
        }

        /// <summary>
        /// This method is used to validate that a board exists for the user.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <exception cref="ArgumentException"></exception>
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
