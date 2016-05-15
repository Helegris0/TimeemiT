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

        public Dictionary<DateTime, TimeSpan> GetTimes(User user) {
            Dictionary<DateTime, TimeSpan> timesForDays = new Dictionary<DateTime, TimeSpan>();

            sqliteConnection.Open();

            using (SQLiteCommand command = sqliteConnection.CreateCommand()) {
                command.CommandText = string.Format("SELECT * FROM Times WHERE username='{0}' AND start IS NOT NULL AND end IS NOT NULL", user.Username);

                using (SQLiteDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        DateTime startDateTime = UnixTimeToDateTime((long)reader["start"]);
                        DateTime endDateTime = UnixTimeToDateTime((long)reader["end"]);
                        DateTime day = startDateTime.Date;
                        if (day.Equals(endDateTime.Date)) {
                            TimeSpan timeSpan = endDateTime.Subtract(startDateTime);

                            if (timesForDays.ContainsKey(day)) {
                                timesForDays[day] = timesForDays[day].Add(timeSpan);
                            } else {
                                timesForDays.Add(day, timeSpan);
                            }
                        }
                    }
                }
            }

            sqliteConnection.Close();

            return timesForDays;
        }

        public Dictionary<DateTime, TimeSpan> GetTimesInInterval(User user, DateTime sinceDate, DateTime untilDate) {
            Dictionary<DateTime, TimeSpan> timesForDays = new Dictionary<DateTime, TimeSpan>();
            long sinceUnix = DateTimeToUnixTime(sinceDate.Date);
            long untilUnix = DateTimeToUnixTime(untilDate.AddDays(1).Date);

            sqliteConnection.Open();

            using (SQLiteCommand command = sqliteConnection.CreateCommand()) {
                command.CommandText = string.Format("SELECT * FROM Times WHERE username='{0}' AND {1} < start AND end < {2}", user.Username, sinceUnix, untilUnix);

                using (SQLiteDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        DateTime startDateTime = UnixTimeToDateTime((long)reader["start"]);
                        DateTime endDateTime = UnixTimeToDateTime((long)reader["end"]);
                        DateTime day = startDateTime.Date;
                        if (day.Equals(endDateTime.Date)) {
                            TimeSpan timeSpan = endDateTime.Subtract(startDateTime);

                            if (timesForDays.ContainsKey(day)) {
                                timesForDays[day] = timesForDays[day].Add(timeSpan);
                            } else {
                                timesForDays.Add(day, timeSpan);
                            }
                        }
                    }
                }
            }

            sqliteConnection.Close();

            return timesForDays;
        }

        public List<User> GetAllWorkers() {
            Role role = Role.WORKER;
            List<User> workers = new List<User>();

            sqliteConnection.Open();

            using (SQLiteCommand command = sqliteConnection.CreateCommand()) {
                command.CommandText = string.Format("SELECT * FROM Users WHERE role_id={0}", (long)role);

                using (SQLiteDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        string username = (string)reader["username"];
                        string firstName = (string)reader["first_name"];
                        string lastName = (string)reader["last_name"];
                        string password = (string)reader["password"];
                        workers.Add(new User(username, firstName, lastName, password, role));
                    }
                }
            }

            return workers;
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
