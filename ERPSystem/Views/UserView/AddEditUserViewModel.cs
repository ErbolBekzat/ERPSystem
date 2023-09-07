using ERPSystem.Models;
using ERPSystem.Services;
using ERPSystem.Views.TaskView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Net.Mail;
using System.Security.Cryptography;

namespace ERPSystem.Views.UserView
{
    public class AddEditUserViewModel : BindableBase
    {
        ICurrentUser _currentUser;

        readonly Brush lineColor;
        readonly Brush textColor;

        private IUserService _userService;
        private IRoleService _roleService;

        private ObservableCollection<Role> _roles;
        public ObservableCollection<Role> Roles
        {
            get { return _roles; }
            set { SetProperty(ref _roles, value); }
        }

        public AddEditUserViewModel(IUserService userRepo, IRoleService roleService, ICurrentUser currentUser)
        {
            _currentUser = currentUser;

            textColor = new SolidColorBrush((Color)Application.Current.Resources["TextColor"]);
            lineColor = new SolidColorBrush((Color)Application.Current.Resources["LineColor"]);

            _userService = userRepo;
            _roleService = roleService;


            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }

        public void LoadRoles()
        {
            var allRoles = _roleService.GetAllRoles();
            Role currentUserRole = _roleService.GetRoleById(_currentUser.UserRole);
            
            if (currentUserRole.RoleName == "SuperUser")
            {
                Roles = new ObservableCollection<Role>(allRoles
                .Where(role => role.RoleName != "SuperUser"));
            }
            else
            {
                Roles = new ObservableCollection<Role>(allRoles
                .Where(role => role.RoleName != "SuperUser" && role.RoleName != "Administrator"));
            }
            
        }

        private bool _EditMode;

        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }


        private CurrentUser _User;

        public CurrentUser User
        {
            get { return _User; }
            set { SetProperty(ref _User, value); }
        }

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        public event Action Done = delegate { };

        private User _editingUser = null;

        public void SetUser(User user)
        {
            _editingUser = user;

            User = new CurrentUser();

            CopyUser(user, User);
        }

        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void CopyUser(Models.User source, CurrentUser target)
        {
            target.Id = source.Id;

            if (EditMode)
            {
                target.UserRole = source.UserRole;
                target.FirstName = source.FirstName;
                target.LastName = source.LastName;
                target.Email = source.Email;
                target.Phone = source.Phone;
                target.LastOnline = source.LastOnline;
            }
        }

        private void UpdateUser(CurrentUser source, User target)
        {
            target.UserRole = source.UserRole;
            target.FirstName = source.FirstName;
            target.LastName = source.LastName;
            target.Email = source.Email;
            target.Phone = source.Phone;
            target.LastOnline = source.LastOnline;
        }


        private void OnCancel()
        {
            Done();
        }

        private void OnSave()
        {
            UpdateUser(User, _editingUser);

            if (EditMode)
            {
                _userService.UpdateUser(_editingUser);
                if (_editingUser.Id == _currentUser.Id)
                {
                    _currentUser.Id = _editingUser.Id;
                    _currentUser.Password = _editingUser.Password;
                    _currentUser.Salt = _editingUser.Salt;
                    _currentUser.UserRole = _editingUser.UserRole;
                    _currentUser.FirstName = _editingUser.FirstName;
                    _currentUser.LastName = _editingUser.LastName;
                    _currentUser.Email = _editingUser.Email;
                    _currentUser.Phone = _editingUser.Phone;
                    _currentUser.LastOnline = _editingUser.LastOnline;
                }
            }

            else
            {
                GenerateHashForPassword(_editingUser);
                _userService.AddUser(_editingUser);
            }

            Done();
        }

        private bool CanSave()
        {
            return true;
        }

        

        private string GeneratePassword(int length)
        {
            const string lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
            const string uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            const string specialCharacters = "!@#$%^&*";

            string validCharacters = lowercaseLetters + uppercaseLetters + numbers + specialCharacters;

            Random random = new Random();
            StringBuilder password = new StringBuilder();

            int randomIndex = random.Next(0, uppercaseLetters.Length);
            password.Append(uppercaseLetters[randomIndex]);

            for (int i = 0; i < length - 1; i++)
            {
                randomIndex = random.Next(0, validCharacters.Length);
                password.Append(validCharacters[randomIndex]);
            }

            return password.ToString();
        }

        public void GenerateHashForPassword(User user)
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Password to be hashed
            string password = GeneratePassword(10);

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

            user.Salt = salt;
            user.Password = hashedPassword;

            SendEmail(user.Email, user, password);
        }

        public void SendEmail(string to, User user, string password)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("dj.erbol.b@gmail.com", "cazznheywqeyokka");
            SmtpServer.EnableSsl = true;
            SmtpServer.EnableSsl = true;

            MailMessage mail = new MailMessage();
            //default from email
            mail.From = new MailAddress("dj.erbol.b@gmail.com");

            //email recipient
            string addresses = to;
            foreach (var address in addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (address.Contains("@") == false || address.Contains(".com") == false)
                {
                    MessageBox.Show("Please enter actual email address");
                    return;
                }
                mail.To.Add(address);
            }

            //email subject
            mail.Subject = "User creadetials";


            //email body
            mail.Body = "Credentials of " + user.FirstName + " " + user.LastName + "\nEmail: " + user.Email + "\nPassword: " + password;

            SmtpServer.Send(mail);
            MessageBox.Show("We have sent credentials of a user to his/her email");
        }
    }
}
