using Microsoft.Practices.Unity;
using System.Windows.Controls;

namespace ERPSystem.Views.UserView
{
    public partial class UserInfoView : UserControl
    {
        private UserInfoViewModel _viewModel;

        public UserInfoViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        public UserInfoView()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Container.Resolve<UserInfoViewModel>();

            DataContext = ViewModel;
        }
    }
}
