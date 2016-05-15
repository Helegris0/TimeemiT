using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService;

namespace ServiceTest {

    class Program {

        static void Main(string[] args) {
            //TestLogin();
            //TestSubmitTime("srowe");
            TestCheckTime();
            //TestGetAllWorkers();
        }

        static void TestLogin() {
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.WriteLine(new ServiceConsumer().Login(username, password));

            Console.ReadKey();
        }

        static User TestSubmitTime(string username) {
            ServiceConsumer consumer = new ServiceConsumer();
            User user = new User();
            user.Username = username;
            user.Role = Role.WORKER;

            //consumer.StartWork(user, DateTime.Now.AddHours(-4).AddDays(-1));
            //consumer.EndWork(user, DateTime.Now.AddDays(-1));


            //consumer.StartWork(user, DateTime.Now.AddHours(-4).AddDays(-2));
            //consumer.EndWork(user, DateTime.Now.AddDays(-2));

            return user;
        }

        static void TestCheckTime() {
            ServiceConsumer consumer = new ServiceConsumer();
            User user = TestSubmitTime("rscott");
            Dictionary<DateTime, TimeSpan> dictionary = consumer.GetTimes(user);
            foreach (var item in dictionary) {
                Console.WriteLine(item.Key + " " + item.Value);
            }

            dictionary = consumer.GetTimesInInterval(user, DateTime.Now.AddDays(-1).Date, DateTime.Now.Date);
            Console.WriteLine("");
            foreach (var item in dictionary) {
                Console.WriteLine(item.Key + " " + item.Value);
            }

            Console.ReadKey();
        }

        static void TestGetAllWorkers() {
            List<User> workers = new ServiceConsumer().GetAllWorkers();
            workers.ForEach(worker => Console.WriteLine(worker.GetName()));

            Console.ReadKey();
        }
    }
}
