using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDefender.Model
{
    public interface Cryptographer
    {
        Data GetEncryptedData(Data data); // Метод, дешифрующий свойства класса Data

        Data GetDecryptedData(Data data); // Метод, шифрующий свойства класса Data
    }
}
