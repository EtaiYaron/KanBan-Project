using log4net;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class PasswordResetRequestController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PasswordResetRequestController));
        private readonly string _connectionString;
        private const string TableName = "PasswordResetRequests";

        public PasswordResetRequestController()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "kanban.db"));
            _connectionString = $"Data Source={path}";
        }

        public bool Insert(PasswordResetRequestDAL reqDal)
        {
            log.Info($"Inserting password reset request for email: {reqDal.Email}");
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    string insert = $"INSERT INTO {TableName} (email, token, expiresAt) VALUES (@Email, @Token, @ExpiresAt)";
                    command.CommandText = insert;
                    command.Parameters.AddWithValue("@Email", reqDal.Email);
                    command.Parameters.AddWithValue("@Token", reqDal.Token);
                    command.Parameters.AddWithValue("@ExpiresAt", reqDal.ExpiresAt.ToString("o"));
                    res = command.ExecuteNonQuery();
                    log.Info($"Inserted password reset request for {reqDal.Email}");
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to insert password reset request for {reqDal.Email}: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return res > 0;
        }

        public PasswordResetRequestDAL SelectByToken(string token)
        {
            log.Info($"Selecting password reset request by token.");
            PasswordResetRequestDAL result = null;
            using (var connection = new SqliteConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT email, token, expiresAt FROM {TableName} WHERE token = @Token";
                command.Parameters.AddWithValue("@Token", token);
                SqliteDataReader reader = null;
                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        result = ConvertReaderToDAL(reader);
                        log.Info($"Found password reset request for token.");
                    }
                }
                catch (Exception ex)
                {
                    log.Error($"Error selecting password reset request: {ex.Message}");
                }
                finally
                {
                    reader?.Close();
                }
            }
            return result;
        }

        public bool DeleteByToken(string token)
        {
            log.Info($"Deleting password reset request with token.");
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                try
                {
                    var command = connection.CreateCommand();
                    command.CommandText = $"DELETE FROM {TableName} WHERE token = @Token";
                    command.Parameters.AddWithValue("@Token", token);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    log.Info($"Deleted password reset request with token.");
                }
                catch (Exception ex)
                {
                    log.Error($"Failed to delete password reset request with token. Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }
            return res > 0;
        }

        private PasswordResetRequestDAL ConvertReaderToDAL(SqliteDataReader dataReader)
        {
            string email = dataReader.GetString(0);
            string token = dataReader.GetString(1);
            DateTime expiresAt = DateTime.Parse(dataReader.GetString(2));
            return new PasswordResetRequestDAL(email, token, expiresAt);
        }
    }
}
