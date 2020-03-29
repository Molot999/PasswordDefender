using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using PasswordDefenderTest.Model;

namespace PasswordDefenderTest
{
    class Program
    {
        static void Main ()
        {
            AccessController.CheckMasterPassword("larik");
            Data[] data = DataFileManager.GetAllData();
            Console.WriteLine("(" + data[0].masterPassword + ")");

            Console.Read();

        }


    }
       
 }





