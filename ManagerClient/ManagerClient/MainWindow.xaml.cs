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
using UserService;

namespace ManagerClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServiceConsumer serviceConsumer;
        private User user;


        public MainWindow()
        {
            InitializeComponent();
            serviceConsumer = new ServiceConsumer();

            foreach (TabItem item in tabControl.Items)
            {
                item.Visibility = Visibility.Collapsed;
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;

            loginErrorLabel.Content = "";

            user = serviceConsumer.Login(username, password);

            if (user != null)
            {
                usernameTextBox.Clear();
                passwordBox.Clear();

                FillListBox();

                userTab.IsSelected = true;
            }
            else
            {
                loginErrorLabel.Content = "Sikertelen bejelentkezés!";
            }
        }

        private void FillListBox()
        {
            listBox.Items.Clear();

            List<User> list = serviceConsumer.GetAllWorkers();
            foreach (var item in list)
            {
                listBox.Items.Add(item);
            }
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            loginTab.IsSelected = true;
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listBox.SelectedItem == null) return;

            User user = (User)listBox.SelectedItem;

            usernameLabel.Content = user.Username;

            worktimeListBox.Items.Clear();
            Dictionary<DateTime, TimeSpan> dic = serviceConsumer.GetTimes(user);

            foreach (var item in dic)
            {
                worktimeListBox.Items.Add(item.Key.ToString().Substring(0, 11) + " munkaóra: " + item.Value);
            }
        }
    }
}
