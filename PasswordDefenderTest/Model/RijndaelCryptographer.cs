using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace PasswordDefenderTest.Model
{
    public class RijndaelCryptographer : Cryptographer
    {
        readonly static string _keyFilePath = $@"{Environment.CurrentDirectory}\dtK";
        readonly static string _IVFilePath = $@"{Environment.CurrentDirectory}\dtIV";

        readonly static RijndaelManaged _rijndael = new RijndaelManaged();

        byte[] _IV;
        byte[] _Key;


        public byte[] IV 
        {

            get 
            {
                if (_IV == null)
                    return GetIV();

                else
                    return _IV;
            }
            
        }

        public byte[] Key
        {

            get
            {
                if (_Key == null)
                    return GetKey();

                else
                    return _Key;
            }

        }


        public void EncryptData(Data dataToEncrypt)
        {
            using (_rijndael)
            {              
                ICryptoTransform rijndaelEncryptor = _rijndael.CreateEncryptor(Key, IV);

                using (MemoryStream memoryStreamOfEncryptor = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(memoryStreamOfEncryptor, rijndaelEncryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(dataToEncrypt.site);

                            /*
                            Console.WriteLine(memoryStreamOfEncryptor.ToArray().Length);

                            swEncrypt.Write(dataToEncrypt.login);
                            dataToEncrypt.login = Encoding.UTF8.GetString(memoryStreamOfEncryptor.ToArray());

                            swEncrypt.Write(dataToEncrypt.password);
                            dataToEncrypt.password = Encoding.UTF8.GetString(memoryStreamOfEncryptor.ToArray());
                            */

                        }

                        var site = memoryStreamOfEncryptor.ToArray();


                    }
                }

            }

        }

        public void DecryptData(Data dataToDecrypt)
        {

            using (_rijndael)
            {
                ICryptoTransform rijndaelEncryptor = _rijndael.CreateEncryptor(GetKey(), GetIV());

                using (MemoryStream memoryStreamOfEncryptor = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(memoryStreamOfEncryptor, rijndaelEncryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.WriteLine(dataToDecrypt.site);
                            dataToDecrypt.site = Encoding.UTF8.GetString(memoryStreamOfEncryptor.ToArray());

                            swEncrypt.Write(dataToDecrypt.login);
                            dataToDecrypt.login = Encoding.UTF8.GetString(memoryStreamOfEncryptor.ToArray());

                            swEncrypt.Write(dataToDecrypt.password);
                            dataToDecrypt.password = Encoding.UTF8.GetString(memoryStreamOfEncryptor.ToArray());
                        }

                    }
                }
            }

        }

        byte[] GetKey()
        {
            if (File.Exists(_keyFilePath) == false)
            {
                SetKey();
            }

            _Key = File.ReadAllBytes(_keyFilePath);

                return _Key;
        }

        byte[] GetIV()
        {
            if (File.Exists(_IVFilePath) == false)
            {
                SetIV();
            }

            _IV = File.ReadAllBytes(_IVFilePath);

            return _IV;
        }

        void SetIV()
        {
            _rijndael.GenerateIV();

            using (FileStream writeNewIVStream = File.Create(_IVFilePath))
                writeNewIVStream.Write(_rijndael.IV, 0, _rijndael.IV.Length);

            _IV = _rijndael.IV;

        }

        void SetKey()
        {

            _rijndael.GenerateKey();

            using (FileStream writeNewKeyStream = File.Create(_keyFilePath))
                writeNewKeyStream.Write(_rijndael.Key, 0, _rijndael.Key.Length);

            _Key = _rijndael.Key;

        }

    }
}
