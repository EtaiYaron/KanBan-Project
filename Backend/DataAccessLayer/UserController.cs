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
        private const string TableName = "Users";

        public UserController()
        {
            this._connectionString = $"Data Source={Path.Combine(Directory.GetCurrentDirectory(), "kanban.db")}";
        }

        public bool Insert(UserDAL userDal)
        {
            log.Info($"Attempting to insert to the DB user with email: {userDal.Email}.");
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    string insert = $"INSERT INTO {TableName} (email, passwordHash, salt) VALUES (@Email, @PasswordHash, @Salt)";
                    command.CommandText = insert;
                    command.Parameters.AddWithValue("@Email", userDal.Email);
                    command.Parameters.AddWithValue("@PasswordHash", userDal.PasswordHash);
                    command.Parameters.AddWithValue("@Salt", userDal.Salt);
                    res = command.ExecuteNonQuery();
                    log.Info($"User with email: {userDal.Email} inserted successfully into the DB.");
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to insert user with email: {userDal.Email}. Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return res > 0;
        }

        public bool Delete(UserDAL userDal)
        {
            log.Info($"Attempting to delete user with email: {userDal.Email}.");
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                try
                {
                    var command = connection.CreateCommand();
                    string delete = $"DELETE FROM {TableName} WHERE email = @Email";
                    command.CommandText = delete;
                    command.Parameters.AddWithValue("@Email", userDal.Email);
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

        public List<UserDAL> SelectAll()
        {
            log.Info("Attempting to select all users from the DB.");
            List<UserDAL> result = new List<UserDAL>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT email, passwordHash, salt FROM {TableName}";
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
                    dataReader?.Close();
                }
            }
            return result;
        }

        public void DeleteAllUsers()
        {
            log.Info("Attempting to delete all users from the DB.");
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = connection.CreateCommand();
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

        public bool UpdatePassword(string email, string passwordHash, string salt)
        {
            log.Info($"Attempting to update password for user with email: {email}.");
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    string update = $"UPDATE {TableName} SET passwordHash = @PasswordHash, salt = @Salt WHERE email = @Email";
                    command.CommandText = update;
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    command.Parameters.AddWithValue("@Salt", salt);
                    command.Parameters.AddWithValue("@Email", email);
                    res = command.ExecuteNonQuery();
                    if (res > 0)
                        log.Info($"Password updated successfully for user with email: {email}.");
                    else
                        log.Warn($"No user found with email: {email} to update password.");
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to update password for user with email: {email}. Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return res > 0;
        }

        private UserDAL ConvertReaderToUserDAL(SqliteDataReader dataReader)
        {
            string email = dataReader.GetString(0);
            string passwordHash = dataReader.GetString(1);
            string salt = dataReader.GetString(2);
            return new UserDAL(email, passwordHash, salt);
        }
    }
}
