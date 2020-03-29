using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PasswordDefender.Model;
using System.IO;


namespace PasswordDefender
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            if (File.Exists(AccessController.MasterPasswordFilePath) == true)
                SetMasterPasswordButton.IsEnabled = false;
            else
                CheckMasterPasswordButton.IsEnabled = false;

            Directory.CreateDirectory(DataFileManager.DataFilesDirectory);

        }

        private void SetMasterPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string _masterPasswordInPasswordBox = MasterPasswordBox.Password;

            if (string.IsNullOrEmpty(_masterPasswordInPasswordBox) == false)
            {
                AccessController.SetMasterPassword(_masterPasswordInPasswordBox);
                SetMasterPasswordButton.IsEnabled = false;
                MasterPasswordBox.IsEnabled = false;
                MessageBox.Show("Мастер-пароль установлен");
                UpdateDataGrid_Click(null, null);
            }
            else
                MessageBox.Show("Строка пустая!");
        }

        private void CheckMasterPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string _masterPasswordInPasswordBox = MasterPasswordBox.Password;

            if (string.IsNullOrEmpty(_masterPasswordInPasswordBox) == false)
            {

                bool b = AccessController.CheckMasterPassword(_masterPasswordInPasswordBox);

                if (b == true)
                {
                    MessageBox.Show("Мастер-пароль введен верно");
                    CheckMasterPasswordButton.IsEnabled = false;
                    MasterPasswordBox.IsEnabled = false;
                    UpdateDataGrid_Click(null, null);
                }
                else
                    MessageBox.Show("Мастер-пароль НЕ верен!");
            }

            else
                MessageBox.Show("Строка пустая!");

        }

        private void SaveNewDataButton_Click(object sender, RoutedEventArgs e)
        {
            string siteForNewData = SiteOfDataBox.Text;
            string loginForNewData = LoginOfDataBox.Text;
            string passwordForNewData = PasswordOfDataBox.Text;

            if (!(!string.IsNullOrEmpty(siteForNewData) || !string.IsNullOrEmpty(loginForNewData) || !string.IsNullOrEmpty(passwordForNewData)))
                MessageBox.Show("Укажите значения для каждого поля!");

            else
            {
                if (AccessController.MasterPassword != null)
                {
                    Data newData = new Data(siteForNewData, loginForNewData, passwordForNewData, AccessController.MasterPassword);

                    Cryptographer cryptographer = new RijndaelCryptographer();
                    cryptographer.EncryptData(newData);

                    DataFileManager.SaveDataToFile(newData);

                    SiteOfDataBox.Text = "";
                    LoginOfDataBox.Text = "";
                    PasswordOfDataBox.Text = "";
                    MessageBox.Show("Данные зашифрованы и сохранены");
                    UpdateDataGrid_Click(null, null);
                }

                else
                    MessageBox.Show("Подтвердите мастер-пароль в разделе \"НАСТРОЙКИ\"!");


            }

        }

        private void UpdateDataGrid_Click(object sender, RoutedEventArgs e)
        {
            if (AccessController.MasterPassword != null)
            {
                Data[] receivedData = DataFileManager.GetAllData();

                if (receivedData.Length != 0)
                    AllDataGrid.ItemsSource = receivedData;

                else
                    MessageBox.Show("Данные отсутствуют");
             }

            else
                MessageBox.Show("Подтвердите мастер-пароль в разделе \"НАСТРОЙКИ\"!");
        }
    }
}
