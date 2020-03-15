using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace PasswordDefender.Model
{
    class RijndaelCryptographer : Сryptographer
    {
        readonly static string _keyFilePath = $@"{Environment.CurrentDirectory}\dtK";
        readonly static string _IVFilePath = $@"{Environment.CurrentDirectory}\dtIV";

        static RijndaelManaged _rijndael = new RijndaelManaged();
        
        byte[] _IV = File.ReadAllBytes(_IVFilePath) ?? null;
        byte[] _Key = File.ReadAllBytes(_keyFilePath) ?? null;

        public Data EncryptData(Data data)
        {
           using (_rijndael)
            {
                if (_Key == null)
                {
                    _rijndael.GenerateKey();

                    using (FileStream writeNewKeyStream = File.Create(_keyFilePath))
                        writeNewKeyStream.Write(_rijndael.Key, 0, _rijndael.Key.Length);
                }

                if (_IV == null)
                {
                    _rijndael.GenerateIV();

                    using (FileStream writeNewIVStream = File.Create(_IVFilePath))
                        writeNewIVStream.Write(_rijndael.IV, 0, _rijndael.IV.Length);
                }

                ICryptoTransform rijndaelEncryptor = _rijndael.CreateEncryptor(_Key, _IV);

                using (MemoryStream memoryStreamOfEncryptor = new MemoryStream())
                {
                    memoryStreamOfEncryptor.
                    using (CryptoStream csEncrypt = new CryptoStream(memoryStreamOfEncryptor, rijndaelEncryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            swEncrypt.Write();
                        }
                        encrypted = msEncrypt.ToArray();
                        csEncrypt.
                    }
                }

            }

            return data;
        }

        public Data DecryptData(Data data)
        {
            using (_rijndael)
            {

            }

            return data;
        }

        void SetIV()
        {
            _rijndael.
            
        }

        void SetKey()
        {

        }

    }
}
