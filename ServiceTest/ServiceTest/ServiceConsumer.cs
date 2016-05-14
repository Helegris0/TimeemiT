using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UserService;

namespace ServiceTest {

    class ServiceConsumer {

        private string loginWebServiceName = "http://localhost:55374/LoginService.svc";
        private string submitTimeWebServiceName = "http://localhost:55374/SubmitTimeService.svc";

        public bool Login(string username, string password) {
            var myBinding = new BasicHttpBinding();
            var myEndpoint = new EndpointAddress(loginWebServiceName);
            ChannelFactory<ILoginService> myChannelFactory = new ChannelFactory<ILoginService>(myBinding, myEndpoint);

            ILoginService client = myChannelFactory.CreateChannel();

            User user = null;

            try {
                user = client.Login(username, password);
            } catch (Exception) {
                if (client != null) {
                    ((ICommunicationObject)client).Abort();
                }
            }

            if (((ICommunicationObject)client).State == CommunicationState.Opened) {
                ((ICommunicationObject)client).Close();
            }

            return user != null;
        }

        public void StartWork(User user, DateTime startTime) {
            var myBinding = new BasicHttpBinding();
            var myEndpoint = new EndpointAddress(submitTimeWebServiceName);
            ChannelFactory<ISubmitTimeService> myChannelFactory = new ChannelFactory<ISubmitTimeService>(myBinding, myEndpoint);

            ISubmitTimeService client = myChannelFactory.CreateChannel();

            try {
                client.StartWork(user, startTime);
            } catch (Exception) {
                if (client != null) {
                    ((ICommunicationObject)client).Abort();
                }
            }

            if (((ICommunicationObject)client).State == CommunicationState.Opened) {
                ((ICommunicationObject)client).Close();
            }
        }

        public void EndWork(User user, DateTime endTime) {
            var myBinding = new BasicHttpBinding();
            var myEndpoint = new EndpointAddress(submitTimeWebServiceName);
            ChannelFactory<ISubmitTimeService> myChannelFactory = new ChannelFactory<ISubmitTimeService>(myBinding, myEndpoint);

            ISubmitTimeService client = myChannelFactory.CreateChannel();

            try {
                client.EndWork(user, endTime);
            } catch (Exception) {
                if (client != null) {
                    ((ICommunicationObject)client).Abort();
                }
            }

            if (((ICommunicationObject)client).State == CommunicationState.Opened) {
                ((ICommunicationObject)client).Close();
            }
        }
    }
}
