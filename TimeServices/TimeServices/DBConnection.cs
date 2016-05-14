using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using UserService;

namespace TimeServices {

    public class DBConnection {

        SQLiteConnection sqliteConnection;
        string dbPath;

        public DBConnection() : this(@"D:\\database.sqlite") {
        }

        public DBConnection(string path) {
            dbPath = path;
            sqliteConnection = new SQLiteConnection(string.Format("Data Source = {0}; Version = 3;", dbPath));
        }

        public User Login(string username, string password) {
            User user = null;

            sqliteConnection.Open();

            using (SQLiteCommand command = sqliteConnection.CreateCommand()) {
                command.CommandText = string.Format("SELECT * FROM Users WHERE username='{0}' LIMIT 1", username);

                using (SQLiteDataReader reader = command.ExecuteReader()) {
                    if (reader.Read()) {
                        string realPassword = (string)reader["password"];

                        if (password.Equals(realPassword)) {
                            user = new User(username,
                                (string)reader["first_name"],
                                (string)reader["last_name"],
                                password,
                                (Role)((long)reader["role_id"]));
                        }
                    }
                }
            }

            sqliteConnection.Close();

            return user;
        }

        public void StartWork(User user, DateTime startTime) {
            sqliteConnection.Open();

            using (SQLiteCommand command = sqliteConnection.CreateCommand()) {
                command.CommandText = string.Format("INSERT INTO Times (username, start) VALUES('{0}', {1})", user.Username, DateTimeToUnixTime(startTime));
                command.ExecuteNonQuery();
            }

            sqliteConnection.Close();
        }

        public void EndWork(User user, DateTime endTime) {
            sqliteConnection.Open();

            using (SQLiteCommand commandSelect = sqliteConnection.CreateCommand()) {
                commandSelect.CommandText = string.Format("SELECT id FROM Times WHERE username='{0}' AND end IS NULL ORDER BY start DESC LIMIT 1", user.Username);

                using (SQLiteDataReader reader = commandSelect.ExecuteReader()) {
                    if (reader.Read()) {
                        long id = (long)reader["id"];
                        using (SQLiteCommand commandUpdate = sqliteConnection.CreateCommand()) {
                            commandUpdate.CommandText = string.Format("UPDATE Times SET end={0} WHERE id={1}", DateTimeToUnixTime(endTime), id);
                            commandUpdate.ExecuteNonQuery();
                        }
                    }
                }
            }

            sqliteConnection.Close();
        }

        private long DateTimeToUnixTime(DateTime datetime) {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)(datetime - sTime).TotalSeconds;
        }

        private DateTime UnixTimeToDateTime(long unixtime) {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return sTime.AddSeconds(unixtime);
        }
    }
}
