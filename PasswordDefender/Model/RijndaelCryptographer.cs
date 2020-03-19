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

        byte[] _IV;
        byte[] _Key;

        public Data GetEncryptedData(Data dataToEncrypt)
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
                            swEncrypt.WriteLine(dataToEncrypt.site);
                            dataToEncrypt.site = Encoding.UTF8.GetString(memoryStreamOfEncryptor.ToArray());

                            swEncrypt.Write(dataToEncrypt.login);
                            dataToEncrypt.login = Encoding.UTF8.GetString(memoryStreamOfEncryptor.ToArray());

                            swEncrypt.Write(dataToEncrypt.password);
                            dataToEncrypt.password = Encoding.UTF8.GetString(memoryStreamOfEncryptor.ToArray());
                        }
                        
                    }
                }

            }

            return dataToEncrypt;

        }

        public Data GetDecryptedData(Data data)
        {
            using (_rijndael)
            {

            }

            return data;
        }

        byte[] GetKey()
        {
            if (File.Exists(_keyFilePath) == false)
            {
                SetKey();
            }

                return File.ReadAllBytes(_keyFilePath);
        }

        byte[] GetIV()
        {
            if (File.Exists(_IVFilePath) == false)
            {
                SetIV();
            }

                return File.ReadAllBytes(_IVFilePath);
        }

        void SetIV()
        {
            _rijndael.GenerateIV();

            using (FileStream writeNewIVStream = File.Create(_IVFilePath))
                writeNewIVStream.Write(_rijndael.IV, 0, _rijndael.IV.Length);

        }

        void SetKey()
        {

            _rijndael.GenerateKey();

            using (FileStream writeNewKeyStream = File.Create(_keyFilePath))
                writeNewKeyStream.Write(_rijndael.Key, 0, _rijndael.Key.Length);

        }

    }
}
