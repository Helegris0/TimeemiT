using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UserService;

namespace TimeServices {

    public class LoginService : ILoginService {

        DBConnection dbConn = new DBConnection();

        public User Login(string username, string password) {
            return dbConn.Login(username, password);
        }
    }
}
