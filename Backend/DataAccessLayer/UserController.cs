using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Security.AccessControl;
using IntroSE.Kanban.Backend.BussinesLayer.User;
using log4net;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class UserController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UserFacade));
        private readonly string _connectionString;
        private readonly string _tableName;
        private const string TableName = "Users";

        /// <summary>
        /// This is the constructor for the UserController class.
        /// </summary>
        public UserController()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "kanban.db"));
            this._connectionString = $"Data Source={Path.Combine(Directory.GetCurrentDirectory(), "kanban.db")}";
            this._tableName = TableName;
        }

        /// <summary>
        /// This method is used to insert a user into the database.
        /// </summary>
        /// <param name="userDal"></param>
        /// <returns>true if the user inserted successfully into the database</returns>
        public bool Insert(UserDAL userDal)
        {
            log.Info($"Attempting to insert to the DB user with email: {userDal.Email} and password: {userDal.Password}.");
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand(null, connection);
                    string insert = $"INSERT INTO {TableName} (email,password) Values (@emailVal, @passwordVal)";

                    SqliteParameter emailParameter = new SqliteParameter(@"emailVal", userDal.Email);
                    SqliteParameter passwordParameter = new SqliteParameter(@"passwordVal", userDal.Password);
                    command.CommandText = insert;
                    command.Parameters.Add(emailParameter);
                    command.Parameters.Add(passwordParameter);

                    res = command.ExecuteNonQuery();
                    log.Info($"User with email: {userDal.Email} inserted successfully into the DB.");
                }
                catch (Exception ex) {
                    log.Error($"Failed to insert user with email: {userDal.Email}. Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }

            return res > 0;
        }

        /// <summary>
        /// This method is used to delete a user from the database.
        /// </summary>
        /// <param name="userDal"></param>
        /// <<returns>true if the user deleted successfully from the database</returns>
        public bool Delete(UserDAL userDal)
        {
            log.Info($"Attempting to delete user with email: {userDal.Email}.");
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                try
                {
                    SqliteCommand command = new SqliteCommand(null, connection);
                    string delete = $"DELETE FROM {TableName} WHERE email = @emailVal";
                    SqliteParameter emailParameter = new SqliteParameter(@"emailVal", userDal.Email);
                    command.CommandText = delete;
                    command.Parameters.Add(emailParameter);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    log.Info($"User with email: {userDal.Email} deleted successfully from the DB.");
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to delete user with email: {userDal.Email}. Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return res > 0;
        }


        /// <summary>
        /// This method is used to select all users from the database.
        /// </summary>
        /// <<returns>List of all users in the database</returns>
        public List<UserDAL> SelectAll()
        {
            log.Info("Attempting to select all users from the DB.");
            List<UserDAL> result = new List<UserDAL>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                SqliteCommand command = new SqliteCommand(null, connection);
                command.CommandText = $"SELECT * FROM {TableName}";
                SqliteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        result.Add(ConvertReaderToUserDAL(dataReader));
                    }
                    log.Info("Successfully selected all users from the DB.");
                }
                catch (Exception ex)
                {
                    log.Error($"Error selecting all users: {ex.Message}");
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }
                }
            }
            return result;
        }

        public void DeleteAllUsers()
        {
            log.Info("Attempting to delete all users from the DB.");
            using (var connection = new SqliteConnection(_connectionString))
            {
                SqliteCommand command = new SqliteCommand(null, connection);
                command.CommandText = $"DELETE FROM {TableName}";
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    log.Info($"Successfully deleted all users from the DB. Rows affected: {rowsAffected}.");
                }
                catch (Exception ex)
                {
                    log.Error($"Error deleting all users: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// This method is used to convert a SqliteDataReader to a UserDAL object.
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns>UserDAL object</returns>
        private UserDAL ConvertReaderToUserDAL(SqliteDataReader dataReader)
        {
            return new UserDAL(dataReader.GetString(0), dataReader.GetString(1));
        }
    }
}
