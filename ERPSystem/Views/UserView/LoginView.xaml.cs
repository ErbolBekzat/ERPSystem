using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using System.Diagnostics;
using ERPSystem.Services;

namespace ERPSystem.Views.UserView
{
    public partial class LoginView : UserControl
    {
        private LoginViewModel viewModel;

        public LoginViewModel ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public LoginView(IUserService userService, ICurrentUser currentUser)
        {
            InitializeComponent();

            ViewModel = new LoginViewModel(userService, currentUser, Email, Password, EmailErrorTextBlock, PasswordErrorTextBlock);

            DataContext = ViewModel;
        }
    }
}
