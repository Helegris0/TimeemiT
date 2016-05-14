using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService;

namespace ServiceTest {

    class ServiceConsumer {

        private string loginWebServiceName = "http://localhost:55374/LoginService.svc";

        public ServiceConsumer() {
        }

        public bool Login(string username, string password) {
            UserAccess userAccess = new UserAccess(loginWebServiceName);
            User user = userAccess.GetUser(username, password);
            return user != null;
        }
    }
}
