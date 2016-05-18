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
using Xceed.Wpf.Toolkit;

namespace WorkerClient
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

            user = serviceConsumer.Login(username, password);

            if (user != null)
            {
                usernameTextBox.Clear();
                passwordBox.Clear();

                FillListBox();

                StartInputIsEnabled(true);

                userTab.IsSelected = true;
            }
        }

        private void FillListBox()
        {
            listBox.Items.Clear();

            Dictionary<DateTime, TimeSpan> dic = serviceConsumer.GetTimes(user);
            foreach (var item in dic)
            {
                listBox.Items.Add(item.Key + " " + item.Value);
            }
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            loginTab.IsSelected = true;
        }

        private void registertStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (startDateTimePicker.Value != null)
            {
                serviceConsumer.StartWork(user, (DateTime)startDateTimePicker.Value);

                StartInputIsEnabled(false);
            }
        }

        private void registerEndButton_Click(object sender, RoutedEventArgs e)
        {
            if (endDateTimePicker.Value != null)
            {
                serviceConsumer.EndWork(user, (DateTime)endDateTimePicker.Value);

                StartInputIsEnabled(true);
            }

            startDateTimePicker.Text = "";
            endDateTimePicker.Text = "";

            FillListBox();
        }

        private void StartInputIsEnabled(bool value)
        {
            startDateTimePicker.IsEnabled = value;
            registertStartButton.IsEnabled = value;

            endDateTimePicker.IsEnabled = !value;
            registerEndButton.IsEnabled = !value;
        }
    }
}
