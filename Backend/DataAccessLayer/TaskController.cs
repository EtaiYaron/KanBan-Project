using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Security.AccessControl;
using IntroSE.Kanban.Backend.BussinesLayer.User;
using log4net;
using Microsoft.VisualBasic;


namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class TaskController
    {
        private readonly string _connectionString;
        private const string TableName = "Tasks";
        private static readonly ILog log = LogManager.GetLogger(typeof(UserFacade));

        public TaskController()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "kanban.db"));
            this._connectionString = $"Data Source={Path.Combine(Directory.GetCurrentDirectory(), "kanban.db")}";
        }

        /// <summary>
        /// This method is used to insert a task into the database.
        /// </summary>
        /// <param name="taskDAL"></param>
        /// <returns>returns true if the task was inserted successfully</returns>
        public bool Insert(TaskDAL taskDAL)
        {
            log.Info($"Attempting to insert to the DB task with ID: {taskDAL.TaskId}, Board ID: {taskDAL.BoardId}, Title: {taskDAL.Title}.");
            using (var connection = new SqliteConnection(_connectionString))
            {
                int res = -1;
                SqliteCommand command = new SqliteCommand(null, connection);
                try
                {
                    connection.Open();

                    string insert = $"INSERT INTO {TableName} (taskId,boardId,title,creationTime,dueDate,description,state,assigneeEmail) VALUES (@taskIdVal,@boardIdVal,@titleVal,@creationTimeVal,@dueDateVal,@descriptionVal,@stateVal,@assigneeEmailVal)";
                    SqliteParameter taskIdParameter = new SqliteParameter(@"taskIdVal", taskDAL.TaskId);
                    SqliteParameter boardIdParameter = new SqliteParameter(@"boardIdVal", taskDAL.BoardId);
                    SqliteParameter titleParameter = new SqliteParameter(@"titleVal", taskDAL.Title);
                    SqliteParameter creationTimeParameter = new SqliteParameter(@"creationTimeVal", taskDAL.CreationTime);
                    SqliteParameter dueDateParameter = new SqliteParameter(@"dueDateVal", taskDAL.DueDate);
                    SqliteParameter descriptionParameter = new SqliteParameter(@"descriptionVal", taskDAL.Description);
                    SqliteParameter stateParameter = new SqliteParameter(@"stateVal", taskDAL.State);
                    string assigneeEmail = taskDAL.AssigneeEmail;
                    if (taskDAL.AssigneeEmail == null)
                    {
                        assigneeEmail = "";
                    }
                    SqliteParameter assigneeEmailParameter = new SqliteParameter(@"assigneeEmailVal", assigneeEmail);
                    
                    command.CommandText = insert;
                    command.Parameters.Add(taskIdParameter);
                    command.Parameters.Add(boardIdParameter);
                    command.Parameters.Add(titleParameter);
                    command.Parameters.Add(creationTimeParameter);
                    command.Parameters.Add(dueDateParameter);
                    command.Parameters.Add(descriptionParameter);
                    command.Parameters.Add(stateParameter);
                    command.Parameters.Add(assigneeEmailParameter);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                    log.Info($"Task with ID: {taskDAL.TaskId} inserted successfully.");
                }
                catch (Exception ex)
                {
                    log.Error($"Error inserting task: {ex.Message}");
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }

        /// <summary>
        /// This method is used to delete a task from the database.
        /// </summary>
        /// <param name="taskDAL"></param>
        /// <returns>returns true if the task was deleted successfully</returns>
        public bool Delete(TaskDAL taskDAL)
        {
            log.Info($"Attempting to delete from the DB task with ID: {taskDAL.TaskId}, in board: {taskDAL.BoardId}.");
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {TableName} where TaskId={taskDAL.TaskId} and boardId={taskDAL.BoardId}"
                };
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    log.Info($"Task with ID: {taskDAL.TaskId} deleted successfully.");
                }
                catch (Exception ex)
                {
                    log.Error($"Error deleting task with ID {taskDAL.TaskId}: {ex.Message}");
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return res > 0;
        }

        /// <summary>
        /// This method is used to update a task in the database.
        /// </summary>
        /// <param name="taskDAL"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        /// <returns>returns true if the task was updated successfully</returns>
        public bool Update(TaskDAL taskDAL, string attributeName, string attributeValue)
        {
            log.Info($"Attempting to update task with ID: {taskDAL.TaskId} - Attribute: {attributeName}, Value: {attributeValue}.");
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                SqliteCommand command = new SqliteCommand(null, connection);
                string update = $"UPDATE {TableName} SET {attributeName} = @attributeValue WHERE taskId = @taskId";
                command.CommandText = update;

                try
                {
                    SqliteParameter attribute = new SqliteParameter(@"attributeValue", attributeValue);
                    command.Parameters.Add(attribute);
                    SqliteParameter taskId = new SqliteParameter(@"taskId", taskDAL.TaskId);
                    command.Parameters.Add(taskId);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    log.Info($"Task with ID: {taskDAL.TaskId} updated successfully - Attribute: {attributeName}, Value: {attributeValue}.");
                }
                catch (Exception ex)
                {
                    log.Error($"Error updating task with ID {taskDAL.TaskId}: {ex.Message} - Attribute: {attributeName}, Value: {attributeValue}");
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return res > 0;
        }

        public bool UpdateDueDate(TaskDAL taskDAL, DateTime? newDueDate)
        {
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                SqliteCommand command = new SqliteCommand(null, connection);
                string update = $"UPDATE {TableName} SET dueDate = @attributeValue WHERE taskId = @taskId";
                command.CommandText = update;

                try
                {
                    SqliteParameter attribute = new SqliteParameter(@"attributeValue", newDueDate);
                    command.Parameters.Add(attribute);
                    SqliteParameter taskId = new SqliteParameter(@"taskId", taskDAL.TaskId);
                    command.Parameters.Add(taskId);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return res > 0;
        }


        /// <summary>
        /// This method is used to select all tasks from the database.
        /// </summary>
        /// <returns>returns a list of tasks</returns>
        public List<TaskDAL> SelectAll()
        {
            log.Info("Selecting all tasks from the database.");
            List<TaskDAL> results = new List<TaskDAL>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                SqliteCommand command = new SqliteCommand(null, connection);
                command.CommandText = $"select * from {TableName};";
                SqliteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToTaskDAL(dataReader));
                    }
                    log.Info($"Successfully selected all tasks from the database.");
                }
                catch (Exception ex)
                {
                    log.Error($"Error selecting all tasks: {ex.Message}");
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }
                    command.Dispose();
                    connection.Close();
                }
            }
            return results;
        }

        /// <summary>
        /// This method is used to select tasks by board id from the database.
        /// </summary>
        /// <param name="boardId"></param>
        /// <returns>returns a list of all tasks in board</returns>
        public List<TaskDAL> SelectByBoardId(long boardId)
        {
            log.Info($"Selecting tasks by board ID: {boardId} from the database.");
            List<TaskDAL> results = new List<TaskDAL>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                SqliteCommand command = new SqliteCommand(null, connection);
                command.CommandText = $"select * from {TableName} where BoardId={boardId};";
                SqliteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToTaskDAL(dataReader));
                    }
                    log.Info($"Successfully selected tasks for board ID: {boardId}.");
                }
                catch (Exception ex)
                {
                    log.Error($"Error selecting tasks by board ID {boardId}: {ex.Message}");
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }
                    command.Dispose();
                    connection.Close();
                }
            }
            return results;
        }

        public void DeleteAllTasks()
        {
            log.Info("Attempting to delete all tasks from the DB.");
            using (var connection = new SqliteConnection(_connectionString))
            {
                SqliteCommand command = new SqliteCommand(null, connection);
                command.CommandText = $"DELETE FROM {TableName}";
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    log.Info($"Successfully deleted all tasks from the DB. Rows affected: {rowsAffected}.");
                }
                catch (Exception ex)
                {
                    log.Error($"Error deleting all tasks: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method is used to convert a data reader to a task.
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns>taskDAL object</returns>
        public TaskDAL ConvertReaderToTaskDAL(SqliteDataReader dataReader)
        {
            int taskId = dataReader.GetInt32(0);
            long boardId = dataReader.GetInt64(1);
            string title = dataReader.GetString(2);
            DateTime creationTime = dataReader.GetDateTime(3);
            DateTime dueDate = dataReader.GetDateTime(4);
            string description = dataReader.GetString(5);
            string assigneeEmail = dataReader.GetString(6);
            TaskDAL t = new TaskDAL(taskId, boardId, title, dueDate, creationTime, description);
            t.AssingeeEmail(assigneeEmail);
            return t;
        }
    }
}
