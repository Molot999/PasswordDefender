using System.Windows;


namespace PasswordDefender
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            MainViewModel viewModel = new MainViewModel(); 
            DataContext = viewModel;

        }

    }
}
