using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace PasswordDefenderTest
{
    class Program
    {
        static readonly string _masterPasswordFile = Environment.CurrentDirectory + @"\dt1";
        static void Main(string[] args)
        {
            Data data = new Data("larik", "larik123");
            Test(data);
            
            Console.ReadLine();

        }

        static async void Test(Data data)
        {

            byte[] hash = new SHA512Managed().ComputeHash(Encoding.UTF8.GetBytes(data.password));
            var s = new FileStream(_masterPasswordFile, FileMode.OpenOrCreate, FileAccess.Write).WriteAsync(hash, 0, hash.Length);

            int i = 0;
            while (s.IsCompleted == false)
            {
                Console.WriteLine($"Ждем {i++}");
            }
            

        }
    }

    class Data
    {
        public string login { get; set; }
        public string password { get; set; }

        public Data(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
    }
}
