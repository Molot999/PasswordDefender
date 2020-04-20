using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace PasswordDefender.Model
{
    public class RijndaelCryptographer : Cryptographer
    {
        readonly static string _keyFilePath = $@"{Environment.CurrentDirectory}\dtK";
        readonly static string _IVFilePath = $@"{Environment.CurrentDirectory}\dtIV";

        readonly static RijndaelManaged _rijndael = new RijndaelManaged();
        static ICryptoTransform _rijndaelEncryptor;
        static ICryptoTransform _rijndaelDecryptor;

        static byte[] _IV;
        static byte[] _Key;

        public static byte[] IV 
        {

            get 
            {
                if (_IV == null)
                    return GetIV();

                else
                    return _IV;
            }
            
        }

        public static byte[] Key
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
                
                dataToEncrypt.Site = EncryptProperty(dataToEncrypt.Site);
                dataToEncrypt.Login = EncryptProperty(dataToEncrypt.Login);
                dataToEncrypt.Password = EncryptProperty(dataToEncrypt.Password);
                dataToEncrypt.MasterPassword = EncryptProperty(dataToEncrypt.MasterPassword);

            }

        }

        public void DecryptData(Data dataToDecrypt)
        {

            using (_rijndael)
            {

                string masterPasswordInData = DecryptProperty(dataToDecrypt.MasterPassword);

                if (masterPasswordInData == AccessController.MasterPassword)
                {
                    dataToDecrypt.MasterPassword = masterPasswordInData;
                    dataToDecrypt.Site = DecryptProperty(dataToDecrypt.Site);
                    dataToDecrypt.Login = DecryptProperty(dataToDecrypt.Login);
                    dataToDecrypt.Password = DecryptProperty(dataToDecrypt.Password);
                }
                else
                {
                    dataToDecrypt.MasterPassword = "Ошибка";
                    dataToDecrypt.Site = "Ошибка";
                    dataToDecrypt.Login = "Ошибка";
                    dataToDecrypt.Password = "Ошибка";
                }
            }

        }

        static byte[] GetKey()
        {
            if (File.Exists(_keyFilePath) == false)
            {
                SetKey();
            }

            _Key = File.ReadAllBytes(_keyFilePath);

                return _Key;
        }

        static byte[] GetIV()
        {
            if (File.Exists(_IVFilePath) == false)
            {
                SetIV();
            }

            _IV = File.ReadAllBytes(_IVFilePath);

            return _IV;
        }

        static void SetIV()
        {
            _rijndael.GenerateIV();

            using (FileStream writeNewIVStream = File.Create(_IVFilePath))
                writeNewIVStream.Write(_rijndael.IV, 0, _rijndael.IV.Length);

            _IV = _rijndael.IV;

        }

        static void SetKey()
        {

            _rijndael.GenerateKey();

            using (FileStream writeNewKeyStream = File.Create(_keyFilePath))
                writeNewKeyStream.Write(_rijndael.Key, 0, _rijndael.Key.Length);

            _Key = _rijndael.Key;

        }

        static string EncryptProperty(string propertyOfData)
        {

            if (_rijndaelEncryptor == null)
                 _rijndaelEncryptor = _rijndael.CreateEncryptor(Key, IV);

            using (MemoryStream memoryStreamOfEncryptor = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(memoryStreamOfEncryptor, _rijndaelEncryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.WriteLine(propertyOfData);
                    }

                    return  Convert.ToBase64String(memoryStreamOfEncryptor.ToArray());

                }
            }
        }

        static string DecryptProperty(string propertyOfData)
        {

            if (_rijndaelDecryptor == null)
                _rijndaelDecryptor = _rijndael.CreateDecryptor(Key, IV);

            string decryptedProperty = null;

            using (MemoryStream memoryStreamOfDecryptor = new MemoryStream(Convert.FromBase64String(propertyOfData)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(memoryStreamOfDecryptor, _rijndaelDecryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        decryptedProperty = srDecrypt.ReadLine();
                    }
                                        
                }
            }

            return decryptedProperty;

        }

    }
}
