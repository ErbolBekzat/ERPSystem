using System;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Linq;
using ERPSystem.Services;
using System.Windows.Controls;
using ERPSystem.Models;

namespace ERPSystem.Views.UserView
{
    public class LoginViewModel
    {
        IUserService _userService;
        ICurrentUser _currentUser;

        TextBox emailBox;
        TextBox passwordBox;

        TextBlock emailErrorTextBlock;
        TextBlock passwordErrorTextBlock;

        public LoginViewModel(IUserService userService, ICurrentUser currentUser, TextBox emailTextBox, TextBox passwordTextBox, TextBlock emailErrorTextBlock, TextBlock passwordErrorTextBlock)
        {
            _userService = userService;
            _currentUser = currentUser;

            emailBox = emailTextBox;
            passwordBox = passwordTextBox;

            LoginCommand = new RelayCommand(CheckUserByEmail);
            this.emailErrorTextBlock = emailErrorTextBlock;
            this.passwordErrorTextBlock = passwordErrorTextBlock;
        }

        public RelayCommand LoginCommand { get; set; }

        public event Action Done = delegate { };

        private void CheckUserByEmail()
        {
            if (_userService.GetUserByEmail(emailBox.Text) == null)
            {
                emailErrorTextBlock.Visibility = System.Windows.Visibility.Visible;
                return;
            }
            else
            {
                emailErrorTextBlock.Visibility = System.Windows.Visibility.Collapsed;
                User user = _userService.GetUserByEmail(emailBox.Text);
                if (CheckPassword(user.Salt, passwordBox.Text, user) == false)
                {
                    passwordErrorTextBlock.Visibility = System.Windows.Visibility.Visible;
                    return;
                }
                else
                {
                    emailErrorTextBlock.Visibility = System.Windows.Visibility.Collapsed;
                    passwordErrorTextBlock.Visibility = System.Windows.Visibility.Collapsed;

                    _currentUser.Id = user.Id;
                    _currentUser.Password = user.Password;
                    _currentUser.Salt = user.Salt;
                    _currentUser.UserRole = user.UserRole;
                    _currentUser.FirstName = user.FirstName;
                    _currentUser.LastName = user.LastName;
                    _currentUser.Email = user.Email;
                    _currentUser.Phone = user.Phone;
                    _currentUser.LastOnline = user.LastOnline;

                    Done();
                }
            }
        }

        private bool CheckPassword(byte[] userSalt, string userPassword, User user)
        {
            byte[] salt = userSalt;

            string password = userPassword;

            // Concatenate the salt and password
            byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray();

            string hashedPassword;

            // Create an instance of the hashing algorithm
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value of the salted password bytes
                byte[] hashedBytes = sha256.ComputeHash(saltedPasswordBytes);

                // Store or transmit the hashed password and salt for later verification
                hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", string.Empty);
            }

            return hashedPassword == user.Password;
        }
    }
}
