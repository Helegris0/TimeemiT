using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace UserService {

    [ServiceContract]
    public interface ILoginService {

        [OperationContract]
        User Login(string username, string password);
    }
}
