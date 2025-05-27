using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Data.SQLite;
using System.Security.AccessControl;
using IntroSE.Kanban.Backend.BussinesLayer.User;
using log4net;


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
            using (var connection = new SqliteConnection(_connectionString))
            {
                int res = -1;
                SqliteCommand command = new SqliteCommand(null, connection);
                try
                {
                    connection.Open();

                    string insert = $"INSERT INTO {TableName} (taskId,boardId,title,creationTime,dueDate,description,state,assigneeEmail) VALUES (@taskId,@boardId,@title,@creationTime,@dueDate,@description,@state,@assigneeEmail)";
                    SqliteParameter taskIdParameter = new SqliteParameter(@"taskId", taskDAL.TaskId);
                    SqliteParameter boardIdParameter = new SqliteParameter(@"boardId", taskDAL.BoardId);
                    SqliteParameter titleParameter = new SqliteParameter(@"title", taskDAL.Title);
                    SqliteParameter creationTimeParameter = new SqliteParameter(@"creationTime", taskDAL.CreationTime);
                    SqliteParameter dueDateParameter = new SqliteParameter(@"dueDate", taskDAL.DueDate);
                    SqliteParameter descriptionParameter = new SqliteParameter(@"description", taskDAL.Description);
                    SqliteParameter stateParameter = new SqliteParameter(@"state", taskDAL.State);
                    SqliteParameter assigneeEmailParameter = new SqliteParameter(@"assigneeEmail", taskDAL.AssigneeEmail);
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
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {TableName} where TaskId={taskDAL.TaskId}"
                };
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
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
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                SqliteCommand command = new SqliteCommand(null, connection);
                string update = $"UPDATE {TableName} SET {attributeName} = @attributeValue WHERE TaskId = @taskId";

                try
                {
                    command.Parameters.Add(new SqliteParameter(@"attributeValue", attributeValue));
                    connection.Open();
                    res = command.ExecuteNonQuery();
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


        /// <summary>
        /// This method is used to select all tasks from the database.
        /// </summary>
        /// <returns>returns a list of tasks</returns>
        public List<TaskDAL> SelectAll()
        {
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
            return new TaskDAL(taskId, boardId, title, dueDate, creationTime, description)
            {
                AssigneeEmail = assigneeEmail
            };
        }
    }
}
