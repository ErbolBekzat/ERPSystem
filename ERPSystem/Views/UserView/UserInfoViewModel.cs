using ERPSystem.Models;
using ERPSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Views.UserView
{
    public class UserInfoViewModel : BindableBase
    {
        IUserService _userService;

        public UserInfoViewModel(IUserService userService)
        {
            _userService = userService;

            EditUserCommand = new RelayCommand(OnEditUser);
            DeleteUserCommand = new RelayCommand(OnDeleteUser);
        }

        private User _currentUser;

        public User CurrentUser
        {
            get { return _currentUser; }
            set { SetProperty(ref _currentUser, value); }
        }

        public void SetUser(User user)
        {
            CurrentUser = user;
        }

        public RelayCommand EditUserCommand { get; set; }
        public RelayCommand DeleteUserCommand { get; private set; }

        public event Action<User> EditUserRequested = delegate { };
        public event Action DeleteUserRequested = delegate { };

        private void OnEditUser()
        {
            EditUserRequested(CurrentUser);
        }

        private void OnDeleteUser()
        {
            _userService.DeleteUser(CurrentUser.Id);
        }
    }
}
