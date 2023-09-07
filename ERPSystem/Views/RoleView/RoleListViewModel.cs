using ERPSystem.Models;
using ERPSystem.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Views.RoleView
{
    public class RoleListViewModel : BindableBase
    {
        IRoleService _roleService;

        public RoleListViewModel(IRoleService roleService)
        {
            _roleService = roleService;

            RoleInfoCommand = new RelayCommand<Role>(OnRoleInfo);
        }

        private ObservableCollection<Role> _roles;

        public ObservableCollection<Role> Roles
        {
            get { return _roles; }
            set { SetProperty(ref _roles, value); }
        }

        public void LoadRoles()
        {
            Roles = new ObservableCollection<Role>(_roleService.GetAllRoles());
        }

        public RelayCommand<Role> RoleInfoCommand { get; set; }
        
        public event Action<Role> RoleInfoRequested = delegate { };
        
        private void OnRoleInfo(Role role)
        {
            RoleInfoRequested(role);
        }
    }
}
