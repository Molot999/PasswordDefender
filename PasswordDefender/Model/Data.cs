using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PasswordDefender.Model
{
    public class Data : INotifyPropertyChanged // Модель данных, подлежащих шифрованию и дешифрованию
    {
        public Data(string site, string login, string password, string masterPassword)
        {
            Site = site;
            Login = login;
            Password = password;
            this.masterPassword = masterPassword;
            Id = GetHashCode().ToString();
        }

        private string id;
        private string site;
        private string login;
        private string password;
        private string masterPassword;

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Site
        {
            get { return site; }
            set
            {
                site = value;
                OnPropertyChanged("Site");
            }
        }
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        public string MasterPassword
        {
            get { return masterPassword; }
            set
            {
                masterPassword = value;
                OnPropertyChanged("MasterPassword");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
