using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UserService;

namespace TimeServices {

    public class CheckTimeService : ICheckTimeService {

        DBConnection dbConn = new DBConnection();

        public Dictionary<DateTime, TimeSpan> GetTimes(User user) {
            return dbConn.GetTimes(user);
        }

        public Dictionary<DateTime, TimeSpan> GetTimesInInterval(User user, DateTime sinceDate, DateTime untilDate) {
            return dbConn.GetTimesInInterval(user, sinceDate, untilDate);
        }
    }
}
