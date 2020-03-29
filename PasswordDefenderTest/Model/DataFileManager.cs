using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace PasswordDefenderTest.Model
{
    static class DataFileManager // Класс, предоставляющий методы сохранения и получения зашифрованных данных
    {
        readonly static string _dataFilesDirectory = $@"{Environment.CurrentDirectory}\data\";
        public static void SaveDataToFile(Data dataToSave) 
        {
            string dataInJSONToSave = JsonConvert.SerializeObject(dataToSave);

            using (TextWriter writeEncryptedDataToFileStream = new StreamWriter($@"{_dataFilesDirectory}{ dataInJSONToSave.GetHashCode()}"))
                writeEncryptedDataToFileStream.WriteLine(dataInJSONToSave);
        }

        public static Data[] GetAllData()
        {

            string[] encryptedDataPaths = Directory.GetFiles(_dataFilesDirectory);

            Data[] allDownloadedData = new Data[encryptedDataPaths.Length];

            RijndaelCryptographer rijndaelCryptographer = new RijndaelCryptographer();

                for (int i = 0; i <= encryptedDataPaths.Length - 1; i++)
                {
                    using (TextReader readEncryptedDataFromDirectoryStream = new StreamReader(encryptedDataPaths[i]))
                    {
                        Data downloadedData = JsonConvert.DeserializeObject<Data>(readEncryptedDataFromDirectoryStream.ReadToEnd());


                        rijndaelCryptographer.DecryptData(downloadedData);


                        allDownloadedData[i] = downloadedData;
                    }
                }

            return allDownloadedData;
        }


    }
}
