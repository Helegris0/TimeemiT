using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UserService;

namespace TimeServices {

    public class GetUsersService : IGetUsersService {

        DBConnection dbConn = new DBConnection();

        public List<User> GetAllWorkers() {
            return dbConn.GetAllWorkers();
        }
    }
}
