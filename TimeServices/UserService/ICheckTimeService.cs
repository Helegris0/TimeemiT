using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace UserService {

    [ServiceContract]
    public interface ICheckTimeService {

        [OperationContract]
        Dictionary<DateTime, TimeSpan> GetTimes(User user);

        [OperationContract]
        Dictionary<DateTime, TimeSpan> GetTimesInInterval(User user, DateTime sinceDate, DateTime untilDate);
    }
}
