﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDefenderTest.Model
{
    public interface Cryptographer
    {
        void EncryptData(Data dataToEncrypt); // Метод, дешифрующий свойства класса Data

        void DecryptData(Data dataToDecrypt); // Метод, шифрующий свойства класса Data
    }
}