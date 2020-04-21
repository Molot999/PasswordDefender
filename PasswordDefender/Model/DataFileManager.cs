using Newtonsoft.Json;
using System;
using System.IO;

namespace PasswordDefender.Model
{
    static class DataFileManager // Класс, предоставляющий методы сохранения и получения зашифрованных данных
    {
        public static readonly string DataFilesDirectory = $@"{Environment.CurrentDirectory}\data\";
        //private static string[] occupiedFileNames = File.
        public static void SaveDataToFile(Data dataToSave)
        {
            string dataInJSONToSave = JsonConvert.SerializeObject(dataToSave);

            using (TextWriter writeEncryptedDataToFileStream = new StreamWriter($@"{DataFilesDirectory}{dataToSave.Id}"))
                writeEncryptedDataToFileStream.WriteLine(dataInJSONToSave);

        }

        public static void DeleteData(Data dataToDelete)
        {
            File.Delete(DataFilesDirectory + dataToDelete.Id);
        }

        public static Data[] GetAllData()
        {

            string[] encryptedDataPaths = Directory.GetFiles(DataFilesDirectory);

            Data[] allDownloadedData = new Data[encryptedDataPaths.Length];

            for (int i = 0; i <= encryptedDataPaths.Length - 1; i++)
            {
                using (TextReader readEncryptedDataFromDirectoryStream = new StreamReader(encryptedDataPaths[i]))
                {
                    Data downloadedData = JsonConvert.DeserializeObject<Data>(readEncryptedDataFromDirectoryStream.ReadToEnd());


                    RijndaelCryptographer.DecryptData(downloadedData);


                    allDownloadedData[i] = downloadedData;
                }
            }

            return allDownloadedData;
        }

    }
}
