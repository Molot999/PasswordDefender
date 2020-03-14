using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDefender.Model
{
    interface Decyptor // Интерфейс для классов, предоставляющих методы дешифрования данных
    {
        Data DecryptData(Data data); // Метод, шифрующий свойства класса Data
    }
}
