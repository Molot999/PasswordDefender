using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDefender.Model
{
    interface Encryptor // Интерфейс для классов, предоставляющих методы шифрования данных
    {
        Data EncryptData(Data data); // Метод, дешифрующий свойства класса Data
    }
}
