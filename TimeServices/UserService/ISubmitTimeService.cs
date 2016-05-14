using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace UserService {

    [ServiceContract]
    public interface ISubmitTimeService {

        [OperationContract]
        void StartWork(User user, DateTime startTime);

        [OperationContract]
        void EndWork(User user, DateTime endTime);
    }
}
