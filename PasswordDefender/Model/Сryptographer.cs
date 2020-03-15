using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDefender.Model
{
    interface Сryptographer
    {
        Data EncryptData(Data data); // Метод, дешифрующий свойства класса Data

        Data DecryptData(Data data); // Метод, шифрующий свойства класса Data
    }
}
