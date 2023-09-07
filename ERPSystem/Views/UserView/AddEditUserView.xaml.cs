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
    public partial class AddEditUserView : UserControl
    {
        private AddEditUserViewModel _viewMode;

        public AddEditUserViewModel ViewModel
        {
            get { return _viewMode; }
            set { _viewMode = value; }
        }


        public AddEditUserView(IUserService userService, IRoleService roleService, ICurrentUser currentUser)
        {
            InitializeComponent();

            ViewModel = new AddEditUserViewModel(userService, roleService, currentUser);

            DataContext = ViewModel;
        }
    }
}
