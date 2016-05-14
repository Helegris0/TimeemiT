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
            TestSubmitTime();
        }

        static void TestLogin() {
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.WriteLine(new ServiceConsumer().Login(username, password));

            Console.ReadKey();
        }

        static void TestSubmitTime() {
            ServiceConsumer consumer = new ServiceConsumer();
            User user = new User();
            user.Username = "vmoss";
            user.Role = Role.WORKER;

            consumer.StartWork(user, DateTime.Now.AddHours(-4));
            consumer.EndWork(user, DateTime.Now);
        }
    }
}
