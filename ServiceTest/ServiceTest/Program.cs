using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTest {

    class Program {

        static void Main(string[] args) {
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.WriteLine(new ServiceConsumer().Login(username, password));

            Console.ReadKey();
        }
    }
}
