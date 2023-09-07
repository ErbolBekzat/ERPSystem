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
    public class RoleInfoViewModel : BindableBase
    {
        IRoleService _roleService;
        IRolePermissionMappingService _rolePermissionMappingService;

        private ObservableCollection<string> _objects;
        public ObservableCollection<string> Objects
        {
            get { return _objects; }
            set { SetProperty(ref _objects, value); }

        }

        private Role _observingRole;

        public Role ObservingRole
        {
            get { return _observingRole; }
            set { SetProperty(ref _observingRole, value); }
        }

        private ObservableCollection<RolePermissionMapping> _rolePermissionMappings;

        public ObservableCollection<RolePermissionMapping> RolePermissionMappings
        {
            get { return _rolePermissionMappings; }
            set { SetProperty(ref _rolePermissionMappings, value); }
        }



        public RoleInfoViewModel(IRoleService roleService, IRolePermissionMappingService rolePermissionMappingService)
        {
            _roleService = roleService;
            _rolePermissionMappingService = rolePermissionMappingService;

            Objects = new ObservableCollection<string>
            {
                "Пользователь", "Роль", "Проект", "Блок", "Задача", "Подзадача", "Проблема", "Материал", "Движение Товаров"
            };

            EditRoleCommand = new RelayCommand(OnEditRole);
            DeleteRoleCommand = new RelayCommand(OnDelete);
            CancelCommand = new RelayCommand(OnCancel);
        }

        public void SetRole(Role role)
        {
            ObservingRole = role;
            RolePermissionMappings = 
                new ObservableCollection<RolePermissionMapping>(_rolePermissionMappingService.GetRolePermissionMappingsByRoleId(ObservingRole.RoleId));
        }

        public RelayCommand EditRoleCommand { get; set; }
        public RelayCommand DeleteRoleCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public event Action<Role> EditRoleRequested = delegate { };
        public event Action Done = delegate { };

        private void OnEditRole()
        {
            EditRoleRequested(ObservingRole);
        }

        private void OnDelete()
        {
            _roleService.DeleteRole(ObservingRole);
            Done();
        }

        private void OnCancel()
        {
            Done();
        }
    }
}
