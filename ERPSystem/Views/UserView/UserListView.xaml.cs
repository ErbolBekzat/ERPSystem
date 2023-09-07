using ERPSystem.Services;
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

namespace ERPSystem.Views.UserView
{
    public partial class UserListView : UserControl
    {
        private UserListViewModel viewModel;

        public UserListViewModel ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public UserListView(IUserService userService, IRoleService roleService, ICurrentUser currentUser)
        {
            InitializeComponent();

            ViewModel = new UserListViewModel(userService, roleService, currentUser);

            DataContext = ViewModel;
        }
    }
}
