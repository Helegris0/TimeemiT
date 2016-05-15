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
        private string checkTimeWebServiceName = "http://localhost:55374/CheckTimeService.svc";
        private string getUsersWebServiceName = "http://localhost:55374/GetUsersService.svc";

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

        public Dictionary<DateTime, TimeSpan> GetTimes(User user) {
            var myBinding = new BasicHttpBinding();
            var myEndpoint = new EndpointAddress(checkTimeWebServiceName);
            ChannelFactory<ICheckTimeService> myChannelFactory = new ChannelFactory<ICheckTimeService>(myBinding, myEndpoint);

            ICheckTimeService client = myChannelFactory.CreateChannel();

            Dictionary<DateTime, TimeSpan> timesForDays = null;

            try {
                timesForDays = client.GetTimes(user);
            } catch (Exception) {
                if (client != null) {
                    ((ICommunicationObject)client).Abort();
                }
            }

            if (((ICommunicationObject)client).State == CommunicationState.Opened) {
                ((ICommunicationObject)client).Close();
            }

            return timesForDays;
        }

        public Dictionary<DateTime, TimeSpan> GetTimesInInterval(User user, DateTime sinceDate, DateTime untilDate) {
            var myBinding = new BasicHttpBinding();
            var myEndpoint = new EndpointAddress(checkTimeWebServiceName);
            ChannelFactory<ICheckTimeService> myChannelFactory = new ChannelFactory<ICheckTimeService>(myBinding, myEndpoint);

            ICheckTimeService client = myChannelFactory.CreateChannel();

            Dictionary<DateTime, TimeSpan> timesForDays = null;

            try {
                timesForDays = client.GetTimesInInterval(user, sinceDate, untilDate);
            } catch (Exception) {
                if (client != null) {
                    ((ICommunicationObject)client).Abort();
                }
            }

            if (((ICommunicationObject)client).State == CommunicationState.Opened) {
                ((ICommunicationObject)client).Close();
            }

            return timesForDays;
        }

        public List<User> GetAllWorkers() {
            var myBinding = new BasicHttpBinding();
            var myEndpoint = new EndpointAddress(getUsersWebServiceName);
            ChannelFactory<IGetUsersService> myChannelFactory = new ChannelFactory<IGetUsersService>(myBinding, myEndpoint);

            IGetUsersService client = myChannelFactory.CreateChannel();

            List<User> workers = null;

            try {
                workers = client.GetAllWorkers();
            } catch (Exception) {
                if (client != null) {
                    ((ICommunicationObject)client).Abort();
                }
            }

            if (((ICommunicationObject)client).State == CommunicationState.Opened) {
                ((ICommunicationObject)client).Close();
            }

            return workers;
        }
    }
}
