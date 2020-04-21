using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PasswordDefender.Model
{
    public static class AccessController // Класс, позволяющий установить или получить существующий мастер пароль
    {
        private static readonly string v = $@"{Environment.CurrentDirectory}\dt1";

        public static string MasterPassword { get; private set; }
        public static string MasterPasswordFilePath { get; } = v;
        public static bool IsMasterPasswordEstablished => File.Exists(MasterPasswordFilePath);

        public static bool CheckMasterPassword(string masterPassword) // Проверить мастер-пароль. Возвращает true, если мастер-пароли совпадают
        {
            byte[] hashToCheck = EncryptMasterPassword(masterPassword);

            bool isMasterPasswordRight = GetMasterPassword().SequenceEqual(hashToCheck);

            if (isMasterPasswordRight == true)
            {
                MasterPassword = masterPassword;
            }
            else
            {
                throw new Exception();
            }

            return isMasterPasswordRight;
        }

        public static void SetMasterPassword(string masterPassword) // Сохранить мастер-пароль в файл
        {
            MasterPassword = masterPassword;

            byte[] hashOfMasterPassword = EncryptMasterPassword(masterPassword);

            using (FileStream fs = new FileStream(MasterPasswordFilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {

                fs.Write(hashOfMasterPassword, 0, hashOfMasterPassword.Length);

            }
        }

        private static byte[] EncryptMasterPassword(string masterPassword) // Зашифровать мастер-пароль
        {
            return new SHA512Managed().ComputeHash(Encoding.UTF8.GetBytes(masterPassword)); // Получаем хэш-код в виде массива байтов из передаваемого в метод мастер-пароля
        }

        private static byte[] GetMasterPassword() // Получить хэш-код мастер-пароля из файла
        {
            return File.ReadAllBytes(MasterPasswordFilePath);
        }
    }
}
