using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BussinesLayer.User;
using log4net;
using Microsoft.Data.Sqlite;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class BoardController
    {
        private readonly string _connectionString;
        private const string TableName = "Boards";
        private static readonly ILog log = LogManager.GetLogger(typeof(UserFacade));


        public BoardController()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "kanban.db"));
            this._connectionString = $"Data Source={Path.Combine(Directory.GetCurrentDirectory(), "kanban.db")}";
        }

        /// <summary>
        /// This method is used to insert a board into the database.
        /// </summary>
        /// <param name="boardDal"></param>
        /// <returns>true if the board was inserted successfully, otherwise false.</returns>
        public bool Insert(BoardDAL boardDal)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                SqliteCommand command = new SqliteCommand(null, connection);
                int res = -1;

                try
                {
                    string insert = $"INSERT INTO {TableName} (boardId,boardName,ownerEmail,maxTasks0,maxTasks1,maxTasks2,nextTaskId) Values (@boardIdVal,@boardName,@ownerEmailVal,@maxTasks0,@maxTasks1,@maxTasks2,@nextTaskId)";
                    SqliteParameter boardIdParameter = new SqliteParameter(@"boardIdVal", boardDal.BoardId);
                    SqliteParameter boardNameParameter = new SqliteParameter(@"boardName", boardDal.BoardName);
                    SqliteParameter ownerEmailParameter = new SqliteParameter(@"ownerEmailVal", boardDal.OwnerEmail);
                    SqliteParameter maxTasks0Parameter = new SqliteParameter(@"maxTasks0", boardDal.MaxTasks0);
                    SqliteParameter maxTasks1Parameter = new SqliteParameter(@"maxTasks1", boardDal.MaxTasks1);
                    SqliteParameter maxTasks2Parameter = new SqliteParameter(@"maxTasks2", boardDal.MaxTasks2);
                    command.CommandText = insert;
                    command.Parameters.Add(boardIdParameter);
                    command.Parameters.Add(boardNameParameter);
                    command.Parameters.Add(ownerEmailParameter);
                    command.Parameters.Add(maxTasks0Parameter);
                    command.Parameters.Add(maxTasks1Parameter);
                    command.Parameters.Add(maxTasks2Parameter);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to insert board with id: {boardDal.BoardId}. Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
                return res > 0;
            }

        }

        /// <summary>
        /// This method is used to update a specific attribute of a board in the database.
        /// </summary>
        /// <param name="boardDAL"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        /// <returns>true if the update was successful, otherwise false.</returns>
        public bool Update(BoardDAL boardDAL, string attributeName, string attributeValue)
        {
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                SqliteCommand command = new SqliteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {TableName} SET {attributeName}=@attributeValue WHERE boardId={boardDAL.BoardId}"
                };

                try
                {
                    command.Parameters.Add(new SqliteParameter(@"attributeValue", attributeValue));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to update board with id: {boardDAL.BoardId}. Error: {ex.Message}");
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
        /// This method is used to delete a board from the database by its BoardDAL object.
        /// </summary>
        /// <param name="boardDAL"></param>
        /// <returns>true if the board was deleted successfully, otherwise false.</returns>
        public bool Delete(BoardDAL boardDAL)
        {
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand
                {
                    Connection = connection,
                    CommandText = $"DELETE FROM {TableName} WHERE boardId={boardDAL.BoardId}"
                };


                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to delete board with id: {boardDAL.BoardId}. Error: {ex.Message}");
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
        /// This method is used to retrieve all boards from the database.
        /// </summary>
        /// <returns>A list of BoardDAL objects representing all boards in the database.</returns>
        public List<BoardDAL> SelectAllBoards()
        {
            List<BoardDAL> results = new List<BoardDAL>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                SqliteCommand command = new SqliteCommand(null, connection);
                command.CommandText = $"SELECT * FROM {TableName};";
                SqliteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToTask(dataReader));
                    }
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to select all boards. Error: {ex.Message}");
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
        /// This method is used to convert a SqliteDataReader object to a BoardDAL object.
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns>A BoardDAL object containing the data from the SqliteDataReader.</returns>
        public BoardDAL ConvertReaderToTask(SqliteDataReader dataReader)
        {
            int boardId = dataReader.GetInt32(0);
            string boardName = dataReader.GetString(1);
            string ownerEmail = dataReader.GetString(2);
            int maxTasks0 = dataReader.GetInt32(3);
            int maxTasks1 = dataReader.GetInt32(4);
            int maxTasks2 = dataReader.GetInt32(5);
            int nextTaskId = dataReader.GetInt32(6);
            return new BoardDAL(boardId, boardName, ownerEmail, maxTasks0, maxTasks1, maxTasks2, nextTaskId);
        }
    }
}
