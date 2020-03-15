﻿using System;
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

        static byte[] _IV;
        static byte[] _Key;

        readonly static string _keyFilePath = $@"{Environment.CurrentDirectory}\dtK";
        readonly static string _IVFilePath = $@"{Environment.CurrentDirectory}\dtIV";
        static void Main(string[] args)
        {
            RijndaelManaged _rijndael = new RijndaelManaged();

            using (_rijndael)
            {
                //if (File.Exists == false)
                {
                    _rijndael.GenerateKey();

                    using (FileStream writeNewKeyStream = File.Create(_keyFilePath))
                        writeNewKeyStream.Write(_rijndael.Key, 0, _rijndael.Key.Length);
                }

                if (File.ReadAllBytes(_IVFilePath) == null)
                {
                    _rijndael.GenerateIV();

                    using (FileStream writeNewIVStream = File.Create(_IVFilePath))
                        writeNewIVStream.Write(_rijndael.IV, 0, _rijndael.IV.Length);
                }

                ICryptoTransform rijndaelEncryptor = _rijndael.CreateEncryptor(_Key, _IV);

                using (MemoryStream memoryStreamOfEncryptor = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(memoryStreamOfEncryptor, rijndaelEncryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            swEncrypt.Write(new Data("larik", "larik123").login);
                        }
                        byte[] encrypted = memoryStreamOfEncryptor.ToArray();
                        foreach (byte b in encrypted)
                            Console.WriteLine(b);
                    }
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
}
