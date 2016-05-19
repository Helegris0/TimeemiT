using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserService;

namespace WorkerClient.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private ServiceConsumer serviceConsumer;
        private User user;

        public string UsernameLabel { get; set; }
        public string PasswordLabel { get; set; }
        public string errorLabel;
        public string ErrorLabel
        {
            get { return errorLabel; }
            set
            {
                errorLabel = value;
                OnPropertyChanged("ErrorLabel");
            }
        }

        public string usernameText;
        public string UsernameText
        {
            get { return usernameText; }
            set
            {
                usernameText = value;
                OnPropertyChanged("UsernameText");
            }
        }

        public string passwordText;
        public string PasswordText
        {
            get { return passwordText; }
            set
            {
                passwordText = value;
                OnPropertyChanged("PasswordText");
            }
        }

        public string LoginButtonContent { get; set; }
        public ICommand LoginButtonCommand { get; set; }

        public MainViewModel()
        {
            UsernameLabel = "Felhasználónév:";
            PasswordLabel = "Jelszó:";
            ErrorLabel = "";

            UsernameText = "";
            PasswordText = "";

            LoginButtonContent = "Bejelentkezés";
            LoginButtonCommand = new RelayCommand(Login);

            serviceConsumer = new ServiceConsumer();
        }

        private void Login()
        {
            string username = UsernameText;
            string password = PasswordText;

            user = serviceConsumer.Login(username, password);

            if (user != null)
            {
                UsernameText = "";
                PasswordText = "";
                ErrorLabel = "";

                //FillListBox();

                //StartInputIsEnabled(true);

                //userTab.IsSelected = true;
                return;
            }

            ErrorLabel = "Sikertelen bejelentkezés!";
        }
    }
}
