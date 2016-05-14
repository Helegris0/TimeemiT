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
    }
}
