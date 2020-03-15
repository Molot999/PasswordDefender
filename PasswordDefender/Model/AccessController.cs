using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace PasswordDefender.Model
{
    static class AccessController // Класс, позволяющий установить или получить существующий мастер пароль
    {
        static readonly string _masterPasswordFilePath = $@"{Environment.CurrentDirectory}\dt1";
        public static async Task<bool> CheckMasterPassword(string masterPassword) // Проверить мастер-пароль. Возвращает true, если мастер-пароли совпадают
        {
            byte[] hashToCheck = new byte[Convert.ToByte(new FileInfo(_masterPasswordFilePath).Length)];
            await new FileStream(_masterPasswordFilePath, FileMode.Open, FileAccess.Read).ReadAsync(hashToCheck, 0, hashToCheck.Length);

                return GetMasterPassword().SequenceEqual(hashToCheck);
        }

        public static async void SetMasterPassword(string masterPassword) // Сохранить мастер-пароль в файл
        {
            byte[] hashOfMasterPassword = EncryptMasterPassword(masterPassword);
            await new FileStream(_masterPasswordFilePath, FileMode.OpenOrCreate, FileAccess.Write).WriteAsync(hashOfMasterPassword, 0, hashOfMasterPassword.Length); // Записываем массив байтов в файл
        }

        static byte[] EncryptMasterPassword(string masterPassword) // Зашифровать мастер-пароль
        {
            return new SHA512Managed().ComputeHash(Encoding.UTF8.GetBytes(masterPassword)); // Получаем хэш-код в виде массива байтов из передаваемого в метод мастер-пароля
        }

        static byte[] GetMasterPassword() // Получить хэш-код мастер-пароля из файла
        {
            return File.ReadAllBytes(_masterPasswordFilePath);
        }

    }
}
