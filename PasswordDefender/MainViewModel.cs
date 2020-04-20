using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using PasswordDefender.Model;
using PasswordDefender.Commands;
using System.IO;

namespace PasswordDefender
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public bool MasterPasswordEstablished { get; private set; } = File.Exists(AccessController.MasterPasswordFilePath);

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

        public ObservableCollection<Data> DataCollection { get; private set; }


        private string site;
        private string login;
        private string password;

        private string masterPassword;

        private bool masterPasswordIsTrue;

        public string Site { get { return site; } set { site = value; } }
        public string Login { get { return login ; } set { login = value; } }
        public string Password { get { return password; } set { password = value; } }


        public string MasterPassword { get { return masterPassword; } set { masterPassword = value; } }

        RijndaelCryptographer cryptographer = new RijndaelCryptographer();

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

                          cryptographer.EncryptData(data);
                          DataFileManager.SaveDataToFile(data);
                          SelectedData = data;
                      }
                      else
                      { throw new Exception(); }
                  }));
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
                        Data data = obj as Data;
                        if (data != null)
                        {
                            DataCollection.Remove(data);
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
                              //DataCollection = new ObservableCollection<Data>(DataFileManager.GetAllData());

                              if (DataCollection == null)
                                  throw new Exception();
                          }

                          else { throw new Exception(); }
                      }
                      else { throw new Exception();}
                   }));
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

                      }

                  }));
            }
        }

        public MainViewModel()
        {
            DataCollection = new ObservableCollection<Data>(DataFileManager.GetAllData());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        
    }
}
