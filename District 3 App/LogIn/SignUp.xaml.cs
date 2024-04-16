﻿using District_3_App.ExtraInfo;
using Log_In;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace District_3_App.LogIn
{
    public partial class SignUp : UserControl
    {
        public UserInfo UserInfo { get; set; }
        public SignUp()
        {
            InitializeComponent();
            UserInfo = new UserInfo("", "", "", "");
            DataContext = this;
        }

        private void SignInLink_Click(object sender, RoutedEventArgs e)
        {
            var newContent = new SignInPage();
            Grid.SetColumn(newContent, 2);
            Grid.SetRow(newContent, 0);
            Grid.SetRowSpan(newContent, 6);
            SignUpGrid.Children.Clear();
            SignUpGrid.Children.Add(newContent);

        }

        private void TogglePasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordVisibilityIcon.Source.ToString().Contains("hidePasswordIcon") && txtVisiblePassword.Visibility == Visibility.Collapsed)
            {
                txtPassword.Visibility = Visibility.Collapsed;
                txtVisiblePassword.Visibility = Visibility.Visible;
                PasswordVisibilityIcon.Source = new BitmapImage(new Uri("pack://application:,,,/images/showPasswordIcon.png"));
                txtVisiblePassword.Text = txtPassword.Password;
            }
            else
            {
                txtPassword.Visibility = Visibility.Visible;
                txtVisiblePassword.Visibility = Visibility.Collapsed;
                PasswordVisibilityIcon.Source = new BitmapImage(new Uri("pack://application:,,,/images/hidePasswordIcon.png"));
                txtPassword.Password = txtVisiblePassword.Text;
            }
        }

        private void ToggleConfirmPasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (ConfirmPasswordVisibilityIcon.Source.ToString().Contains("hidePasswordIcon") && txtVisibleConfirmPassword.Visibility == Visibility.Collapsed)
            {
                txtConfirmPassword.Visibility = Visibility.Collapsed;
                txtVisibleConfirmPassword.Visibility = Visibility.Visible;
                ConfirmPasswordVisibilityIcon.Source = new BitmapImage(new Uri("pack://application:,,,/images/showPasswordIcon.png"));
                txtVisibleConfirmPassword.Text = txtConfirmPassword.Password;
            }
            else
            {
                txtConfirmPassword.Visibility = Visibility.Visible;
                txtVisibleConfirmPassword.Visibility = Visibility.Collapsed;
                ConfirmPasswordVisibilityIcon.Source = new BitmapImage(new Uri("pack://application:,,,/images/hidePasswordIcon.png"));
                txtConfirmPassword.Password = txtVisibleConfirmPassword.Text;
            }
        }

       private void SignUpButton_Click(object sender, RoutedEventArgs e) 
       {
            bool isValid = true;

            if(!ValidateUsername(txtUsername.Text))
            {
                isValid = false;
            }

            if(!ValidateEmail(txtEmail.Text)) 
            {
                MessageBox.Show("Invalid email.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            if (!ValidatePassword(txtPassword.Password))
            {
                MessageBox.Show("Invalid password. The password's length must be between 5 and 10 characters and must contain at least one uppercase letter, one lowercase letter, one number, and one special character(/_-.)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            if (!ValidateConfirmPassword(txtConfirmPassword.Password))
            {
                MessageBox.Show("Invalid confirmation password. The password's length must be between 5 and 10 characters and must contain at least one uppercase letter, one lowercase letter, one number, and one special character(/_-.)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            if(!(txtPassword.Password.Equals(txtConfirmPassword.Password)))
            {
                MessageBox.Show("Please choose the same password!.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            if (isValid)
            {
                UserInfo.Username = txtUsername.Text;
                UserInfo.Email = txtEmail.Text;
                UserInfo.Password = txtPassword.Password;
                UserInfo.ConfirmationPassword = txtConfirmPassword.Password;
                var newContent = new MainWindow();
                newContent.Show();
                Window.GetWindow(this).Close();
                
            }
       }

        private bool ValidateUsername(string username)
        {
            var hasMiniMaxChars = new Regex(@".{5,11}");
            var hasNumericChar = new Regex(@"[0-9]+");
            var hasAlphabeticChar = new Regex(@"[A-Za-z]");
            var hasSpecialChar = new Regex(@"[_.-]");

            if (hasMiniMaxChars.IsMatch(username) && hasNumericChar.IsMatch(username) && hasAlphabeticChar.IsMatch(username) && hasSpecialChar.IsMatch(username))
                return true;
            else if(!(hasMiniMaxChars.IsMatch(username)))
            {
                MessageBox.Show("Invalid username. The username's length must be between 5 and 11 characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!(hasNumericChar.IsMatch(username)))
            {
                MessageBox.Show("Invalid username. The username must contain at least one number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!(hasAlphabeticChar.IsMatch(username)))
            {
                MessageBox.Show("Invalid username. The username must contain at least one alphabetic character..", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!(hasSpecialChar.IsMatch(username)))
            {
                MessageBox.Show("Invalid username. The username must contain at least one special character(_ . -).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateEmail(string email)
        {
            if(Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }

        private bool ValidatePassword(string password) 
        {
            var hasNumericChar = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSpecialChar = new Regex(@"[/-_.]+");
            var hasMiniMaxChars = new Regex(@".{5,10}");

            if(hasNumericChar.IsMatch(password) && hasUpperChar.IsMatch(password) && hasLowerChar.IsMatch(password) && hasSpecialChar.IsMatch(password) && hasMiniMaxChars.IsMatch(password))
            {
                return true;
            }
            return false;
        }

        private bool ValidateConfirmPassword(string confirmPassword) 
        {
            var hasNumericChar = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSpecialChar = new Regex(@"[/\-_.]+");
            var hasMiniMaxChars = new Regex(@".{5,10}");

            if(hasNumericChar.IsMatch(confirmPassword) && hasUpperChar.IsMatch(confirmPassword) && hasLowerChar.IsMatch(confirmPassword) && hasSpecialChar.IsMatch(confirmPassword) && hasMiniMaxChars.IsMatch(confirmPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
         
        }
    }
}
