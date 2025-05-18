using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;
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

        public UserController()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "database.db"));
            this._connectionString = $"Data Source={Path.Combine(Directory.GetCurrentDirectory(), "database.db")}";
            this._tableName = TableName;
        }

        public bool Insert(UserDAL userDal)
        {
            //log.Info($"Attempting to insert user with email: {userDal.Email} and password: {userDal.Password}.");
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                try
                {
                    SqliteCommand command = new SqliteCommand(null, connection);
                    string insert = $"INSERT INTO {TableName} ({userDal.UserEmailColumnName},{userDal.UserPasswordColumnName}) Values {@"emailVal"},{@"passwordVal"}";

                    SqliteParameter emailParameter = new SqliteParameter(@"emailVal", userDal.Email);
                    SqliteParameter passwordParameter = new SqliteParameter(@"passwordVal", userDal.Password);
                    command.CommandText = insert;
                    command.Parameters.Add(emailParameter);
                    command.Parameters.Add(passwordParameter);

                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex) {
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
            int res = -1;
            using (var connection = new SqliteConnection(_connectionString))
            {
                try
                {
                    SqliteCommand command = new SqliteCommand(null, connection);
                    string delete = $"DELETE FROM {TableName} WHERE {userDal.UserEmailColumnName} = @emailVal";
                    SqliteParameter emailParameter = new SqliteParameter(@"emailVal", userDal.Email);
                    command.CommandText = delete;
                    command.Parameters.Add(emailParameter);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    connection.Close();
                }
            }
            return res > 0;
        }

        public List<UserDAL> SelectAllUsers()
        {
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
                        result.Add(ConvertReaderToObject(dataReader));
                    }
                }
                catch (Exception ex)
                {
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

        private UserDAL ConvertReaderToObject(SqliteDataReader dataReader)
        {
            return new UserDAL(dataReader.GetString(0), dataReader.GetString(1));
        }
    }
}
