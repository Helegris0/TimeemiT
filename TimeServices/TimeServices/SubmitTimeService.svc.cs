using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UserService;

namespace TimeServices {

    public class SubmitTimeService : ISubmitTimeService {

        DBConnection dbConn = new DBConnection();

        public void StartWork(User user, DateTime startTime) {
            dbConn.StartWork(user, startTime);
        }

        public void EndWork(User user, DateTime endTime) {
            dbConn.EndWork(user, endTime);
        }
    }
}
