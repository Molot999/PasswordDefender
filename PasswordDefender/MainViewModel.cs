using PasswordDefender.Commands;
using PasswordDefender.Model;
using System;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PasswordDefender
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private Data selectedData;
        public Data SelectedData
        {
            get { return selectedData; }

            set
            {
                selectedData = value;
                OnPropertyChanged("SelectedData");
            }
        }

        public ObservableCollection<Data> DataCollection { get; set; }


        private string site;
        private string login;
        private string password;

        private string masterPassword;

        private bool masterPasswordIsTrue;
        private bool masterPasswordIsChecked;

        public string Site { get { return site; } set { site = value; } }
        public string Login { get { return login; } set { login = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string MasterPassword { get { return masterPassword; } set { masterPassword = value; } }

        private RelayCommand saveNewDataCommand;
        public RelayCommand SaveNewDataCommand
        {
            get
            {
                return saveNewDataCommand ??
                  (saveNewDataCommand = new RelayCommand(obj =>
                  {
                      if (!string.IsNullOrEmpty(Site) && !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password) && masterPasswordIsTrue == true)
                      {
                          Data data = new Data(site, login, password, AccessController.MasterPassword);
                          DataCollection.Insert(0, data);

                          RijndaelCryptographer.EncryptData(data);
                          DataFileManager.SaveDataToFile(data);

                          RijndaelCryptographer.DecryptData(data);
                          SelectedData = data;
                      }
                      else
                      { throw new Exception(); }
                  },

                  (obj) => masterPasswordIsTrue == true));
            }
        }


        private RelayCommand removeDataCommand;
        public RelayCommand RemoveDataCommand
        {
            get
            {
                return removeDataCommand ??
                    (removeDataCommand = new RelayCommand(obj =>
                    {
                        if (SelectedData != null)
                        {
                            DataFileManager.DeleteData(SelectedData);
                            DataCollection.Remove(SelectedData);
                        }
                    },

                    (obj) => DataCollection.Count > 0));
            }
        }

        private RelayCommand checkMasterPasswordCommand;
        public RelayCommand CheckMasterPasswordCommand
        {
            get
            {
                return checkMasterPasswordCommand ??
                  (checkMasterPasswordCommand = new RelayCommand(obj =>
                  {

                      if (string.IsNullOrEmpty(MasterPassword) == false)
                      {
                          masterPasswordIsTrue = AccessController.CheckMasterPassword(MasterPassword);

                          if (masterPasswordIsTrue == true)
                          {
                              masterPasswordIsChecked = true;

                              foreach (Data data in DataFileManager.GetAllData())
                              {
                                  DataCollection.Add(data);
                              }
                          }
                          else { throw new Exception(); }
                      }
                      else { throw new Exception(); }
                  },
                  (obj) => AccessController.IsMasterPasswordEstablished == true && masterPasswordIsChecked == false));
            }
        }

        private RelayCommand setMasterPasswordCommand;
        public RelayCommand SetMasterPasswordCommand
        {
            get
            {
                return setMasterPasswordCommand ??
                  (setMasterPasswordCommand = new RelayCommand(obj =>
                  {

                      if (string.IsNullOrEmpty(MasterPassword) == false)
                      {
                          AccessController.SetMasterPassword(MasterPassword);
                          Directory.CreateDirectory(DataFileManager.DataFilesDirectory);

                          CheckMasterPasswordCommand.Execute(obj);
                      }

                  },
                  (obj) => AccessController.IsMasterPasswordEstablished == false));
            }
        }

        public MainViewModel()
        {
            DataCollection = new ObservableCollection<Data>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
