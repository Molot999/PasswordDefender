using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace PasswordDefender.Model
{
    public static class AccessController // Класс, позволяющий установить или получить существующий мастер пароль
    {
        public static string MasterPassword { get; private set; } = "larik";
        public static string MasterPasswordFilePath { get; } = $@"{Environment.CurrentDirectory}\dt1";

        public static bool CheckMasterPassword(string masterPassword) // Проверить мастер-пароль. Возвращает true, если мастер-пароли совпадают
        {
            byte[] hashToCheck = EncryptMasterPassword(masterPassword);

            bool isMasterPasswordRight = GetMasterPassword().SequenceEqual(hashToCheck);

            if (isMasterPasswordRight == true)
            {
                MasterPassword = masterPassword;
            }

            return isMasterPasswordRight;
        }

        public static void SetMasterPassword(string masterPassword) // Сохранить мастер-пароль в файл
        {
            MasterPassword = masterPassword;

            byte[] hashOfMasterPassword = EncryptMasterPassword(masterPassword);
            new FileStream(MasterPasswordFilePath, FileMode.OpenOrCreate, FileAccess.Write).Write(hashOfMasterPassword, 0, hashOfMasterPassword.Length); // Записываем массив байтов в файл
        }

        static byte[] EncryptMasterPassword(string masterPassword) // Зашифровать мастер-пароль
        {
            return new SHA512Managed().ComputeHash(Encoding.UTF8.GetBytes(masterPassword)); // Получаем хэш-код в виде массива байтов из передаваемого в метод мастер-пароля
        }

        static byte[] GetMasterPassword() // Получить хэш-код мастер-пароля из файла
        {
            return File.ReadAllBytes(MasterPasswordFilePath);
        }

    }
}
