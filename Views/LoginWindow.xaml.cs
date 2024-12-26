using disaster_management.Services;
using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Shapes;

namespace disaster_management.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        private readonly IUserService _userService;
        public LoginWindow()
        {
            InitializeComponent();
            // Resolve user service from DI
            _userService = App.ServiceProvider.GetRequiredService<IUserService>();
        }
        private void btnTogglePassword_Click(object sender, RoutedEventArgs e)
        {
            if (pwdPassword.Visibility == Visibility.Visible)
            {
                // Show password as plain text
                txtVisiblePassword.Text = pwdPassword.Password;
                txtVisiblePassword.Visibility = Visibility.Visible;
                pwdPassword.Visibility = Visibility.Collapsed;
                btnTogglePassword.Content = "🔒";
            }
            else
            {
                // Hide password
                pwdPassword.Password = txtVisiblePassword.Text;
                txtVisiblePassword.Visibility = Visibility.Collapsed;
                pwdPassword.Visibility = Visibility.Visible;
                btnTogglePassword.Content = "👁";
            }
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = pwdPassword.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var currentUser =  await _userService.ValidateUser(username, password);
            if (currentUser != null)
            {
                DialogResult = true; // Login successful
                Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

       
    }
    }
