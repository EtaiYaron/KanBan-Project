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
    internal class BoardsUsersController
    {
        private readonly string _connectionString;
        private const string TableName = "BoardsUsers";
        private static readonly ILog log = LogManager.GetLogger(typeof(UserFacade));

        public BoardsUsersController()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "kanban.db"));
            this._connectionString = $"Data Source={Path.Combine(Directory.GetCurrentDirectory(), "kanban.db")}";
        }

        /// <summary>
        /// This method is used to insert a board user into the database.
        /// </summary>
        /// <param name="boardUserDal"></param>
        /// <returns>true if the board user was inserted successfully, otherwise false.</returns>
        public bool Insert(BoardsUsersDAL boardUserDal)
        {
            log.Info($"Attempting to insert BoardUser to the DB with boardId: {boardUserDal.BoardId} and email: {boardUserDal.UserEmail}.");
            using (var connection = new SqliteConnection(_connectionString))
            {
                SqliteCommand command = new SqliteCommand(null, connection);
                int res = -1;
                try
                {
                    string insert = $"INSERT INTO {TableName} (boardId,userEmail,isOwner) VALUES (@boardId,@email,@isOwner)";
                    SqliteParameter boardIdParameter = new SqliteParameter(@"boardId", boardUserDal.BoardId);
                    SqliteParameter emailParameter = new SqliteParameter(@"email", boardUserDal.UserEmail);
                    SqliteParameter ownerEmailParameter = new SqliteParameter(@"isOwner", boardUserDal.IsOwner ? 1 : 0);

                    command.CommandText = insert;
                    command.Parameters.Add(boardIdParameter);
                    command.Parameters.Add(emailParameter);
                    command.Parameters.Add(ownerEmailParameter);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    log.Info($"Board user with boardId: {boardUserDal.BoardId} and email: {boardUserDal.UserEmail} inserted successfully.");
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to insert BoardUser to the DB, with boardId: {boardUserDal.BoardId} and email: {boardUserDal.UserEmail}. Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
                return res > 0;

            }
        }



        /// <summary>
        /// This method is used to delete a board user from the database.
        /// </summary>
        /// <param name="boardUserDal"></param>
        /// <returns>true if the board user was deleted successfully, otherwise false.</returns>
        public bool Delete(BoardsUsersDAL boardUserDal)
        {
            log.Info($"Attempting to delete BoardUser from the DB, with boardId: {boardUserDal.BoardId}, email: {boardUserDal.UserEmail}, ownerEmail: {boardUserDal.IsOwner}.");
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand
                {
                    Connection = connection,
                    CommandText = $"DELETE FROM {TableName} WHERE boardId=@boardId AND userEmail=@userEmail"
                };

                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to delete board user with boardId: {boardUserDal.BoardId}, email: {boardUserDal.UserEmail}, ownerEmail: {boardUserDal.IsOwner}. Error: {ex.Message}");
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
        /// This method is used to update isOwner field - if the user is the Owner of the board.
        /// </summary>
        /// <param name="boardUserDal"></param>
        /// <param name="newIsOwner"></param>
        /// <returns>true if the isOwner field was updated successfully, otherwise false.</returns>
        public bool UpdateIsOwner(BoardsUsersDAL boardUserDal, bool newIsOwner)
        {
            log.Info($"Attempting to update isOwner for boardId: {boardUserDal.BoardId}, email: {boardUserDal.UserEmail} to {newIsOwner}.");
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = new SqliteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {TableName} SET isOwner=@newIsOwner WHERE boardId=@boardId AND userEmail=@userEmail"
                };
                try
                {
                    command.Parameters.Add(new SqliteParameter(@"newIsOwner", newIsOwner));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    log.Info($"Successfully updated isOwner for boardId: {boardUserDal.BoardId}, email: {boardUserDal.UserEmail} to {newIsOwner}.");
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to update owner for boardId: {boardUserDal.BoardId}, email: {boardUserDal.UserEmail}. Error: {ex.Message}");
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
        /// This method is used to select all BoardUsersDALs from the database.
        /// </summary>
        /// <returns></returns>
        public List<BoardsUsersDAL> SelectAll()
        {
            log.Info("Attempting to select all BoardUsersDALs from the DB.");
            List<BoardsUsersDAL> results = new List<BoardsUsersDAL>();
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
                        results.Add(ConvertReaderToBoardsUsersDAL(dataReader));
                    }
                    log.Info("Successfully selected all BoardUsersDALs from the DB.");
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
        /// This method is used to select all BoardUsersDALs by boardId from the database.
        /// </summary>
        /// <param name="boardId"></param>
        /// <returns>returns a list of BoardUsersDALs that belong to the specified boardId.</returns>
        public List<BoardsUsersDAL> SelectByBoardId(int boardId)
        {
            log.Info($"Attempting to select all BoardUsersDALs from the DB by boardId: {boardId}.");
            List<BoardsUsersDAL> results = new List<BoardsUsersDAL>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                SqliteCommand command = new SqliteCommand(null, connection);
                command.CommandText = $"select * from {TableName} where boardId=@boardId;";
                command.Parameters.Add(new SqliteParameter(@"boardId", boardId));
                SqliteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToBoardsUsersDAL(dataReader));
                    }
                    log.Info($"Successfully selected all BoardUsersDALs from the DB by boardId: {boardId}.");
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
        /// This method is used to convert a SqliteDataReader to a BoardsUsersDAL object.
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns>returns a BoardsUsersDAL object that represents the data in the SqliteDataReader.</returns>
        public BoardsUsersDAL ConvertReaderToBoardsUsersDAL(SqliteDataReader dataReader)
        {
            int boardId = dataReader.GetInt32(0);
            string userEmail = dataReader.GetString(1);
            bool isOwner = dataReader.GetBoolean(2);
            return new BoardsUsersDAL(boardId, userEmail, isOwner);


        }
    }
}
