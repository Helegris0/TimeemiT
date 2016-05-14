using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService {

    public class User {

        private string firstName;
        public string FirstName {
            get { return firstName; }
            set {
                firstName = value;
                //if (LastName != null) {
                //    CreateUsername();
                //}
            }
        }

        private string lastName;
        public string LastName {
            get { return lastName; }
            set {
                lastName = value;
                //if (FirstName != null) {
                //    CreateUsername();
                //}
            }
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public Role Role { get; set; }

        public User() {
        }

        public User(string firstName, string lastName, string password, Role role) {
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Role = role;
            //CreateUsername();
        }

        public User(string username, string firstName, string lastName, string password, Role role)
            : this(firstName, lastName, password, role) {
            Username = username;
        }

        private void CreateUsername() {
            Username = FirstName.ToLower().Substring(0, 1) + LastName.ToLower();
        }

        public string GetName() {
            return String.Format("{0} {1}", FirstName, LastName);
        }
    }
}
