using ERPSystem.Models;
using ERPSystem.Services;
using System;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ERPSystem.Views.UserView
{
    public partial class AddSuperUserView : UserControl
    {
        IUserService _userService;
        IRoleService _roleService;

        public AddSuperUserView(IUserService userService, IRoleService roleService)
        {
            InitializeComponent();

            _userService = userService;
            _roleService = roleService;

            DataContext = this;

            AddSuperUserCommand = new RelayCommand(AddSuperUser);
        }

        public bool CheckIfUserExist()
        {
            return _userService.GetAllUsers().ToList().Count() > 0;
        }

        public RelayCommand AddSuperUserCommand { get; set; }

        public event Action Done = delegate { };

        private void AddSuperUser()
        {
            int userCount = _userService.GetAllUsers().ToList().Count;

            if (userCount == 0)
            {
                Role superUserRole = _roleService.GetRoleByName("SuperUser");

                if (superUserRole == null)
                {
                    _roleService.CreateRole(new Role { RoleName = "SuperUser" });
                    superUserRole = _roleService.GetRoleByName("SuperUser");
                }


                User superUser = new User();
                superUser.UserRole = superUserRole.RoleId;
                superUser.FirstName = FirstName.Text;
                superUser.LastName = LastName.Text;
                superUser.Email = Email.Text;
                superUser.Phone = Phone.Text;
                GenerateHashForPassword(superUser);

                _userService.AddUser(superUser);
            }

            Done();
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
            mail.Body = "Credentials of a Super User " + user.FirstName + " " + user.LastName + "\nEmail: " + Email.Text + "\nPassword: " + password;

            SmtpServer.Send(mail);
            MessageBox.Show("We sent credentials of a Super User to his email");
        }
    }
}
