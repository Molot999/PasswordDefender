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

namespace PasswordDefender
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Data selectedData;

        public ObservableCollection<Data> DataCollection { get; set; }

        private RelayCommand addDataCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addDataCommand ??
                  (addDataCommand = new RelayCommand(obj =>
                  {
                      Data data = new Data("", "", "", "");
                      DataCollection.Insert(0, data);
                      SelectedData = data;
                  }));
            }
        }


        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
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

        private RelayCommand checkMasterPassword;
        public RelayCommand CheckMasterPassword
        {
            get
            {
                return addDataCommand ??
                  (addDataCommand = new RelayCommand(obj =>
                  {
                      string masterPasswordInPasswordBox = obj as string;

                      if (string.IsNullOrEmpty(masterPasswordInPasswordBox) == false)
                      {

                          bool b = AccessController.CheckMasterPassword(masterPasswordInPasswordBox);

                          if (b == true)
                          {
                              //MessageBox.Show("Мастер-пароль введен верно");
                              //CheckMasterPasswordButton.IsEnabled = false;
                              //MasterPasswordBox.IsEnabled = false;
                          }
                          //else
                              //MessageBox.Show("Мастер-пароль НЕ верен!");
                      }

                      //else
                          //MessageBox.Show("Строка пустая!"); 

                  }));
            }
        }

        public Data SelectedData
        { 
            get { return selectedData; }
            
            set
            {
                selectedData = value;
                OnPropertyChanged("SelectedData");
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
