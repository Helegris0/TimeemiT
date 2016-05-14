using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UserService;

namespace ServiceTest {

    class UserAccess {

        private string accessPoint;

        public UserAccess(string accessPoint) {
            this.accessPoint = accessPoint;
        }

        public User GetUser(string username, string password) {
            var myBinding = new BasicHttpBinding();
            var myEndpoint = new EndpointAddress(accessPoint);
            ChannelFactory<ILoginService> myChannelFactory = new ChannelFactory<ILoginService>(myBinding, myEndpoint);

            ILoginService client = myChannelFactory.CreateChannel();

            User user = null;

            try {
                user = client.Login(username, password);
            } catch (Exception ex) {
                if (client != null) {
                    ((ICommunicationObject)client).Abort();
                }
            }

            if (((ICommunicationObject)client).State == CommunicationState.Opened) {
                ((ICommunicationObject)client).Close();
            }

            return user;
        }
    }
}
