using ERPSystem.Models;
using ERPSystem.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using ERPSystem.Utilities;
using System.Windows.Media;
using System.Reflection;
using System.Diagnostics;

namespace ERPSystem.Views.RoleView
{
    public class AddEditRoleViewModel : BindableBase
    {
        IRoleService _roleService;
		IPermissionService _permissionService;
        IRolePermissionMappingService _rolePermissionMappingService;

		private ObservableCollection<Permission> _permissions;

		public ObservableCollection<Permission> Permissions
		{
			get { return _permissions; }
			set { SetProperty(ref _permissions, value); }
		}

        private ObservableCollection<string> _objects;
        public ObservableCollection<string> Objects
        {
            get { return _objects; }
            set { SetProperty(ref _objects, value); }
        }

        readonly Brush lineColor;
        readonly Brush textColor;

        public AddEditRoleViewModel(IRoleService roleService, IPermissionService permissionService, IRolePermissionMappingService rolePermissionMappingService) 
		{
            textColor = new SolidColorBrush((Color)Application.Current.Resources["TextColor"]);
            lineColor = new SolidColorBrush((Color)Application.Current.Resources["LineColor"]);

            _roleService = roleService;
			_permissionService = permissionService;
		    _rolePermissionMappingService = rolePermissionMappingService;

            AddRoleCommand = new RelayCommand<TextBox>(OnAddRole);
            CancelCommand = new RelayCommand(OnCancel);
            AddPermissionCommand = new RelayCommand<CheckBox>(OnAddPermission);

            Objects = new ObservableCollection<string>
            {
                "Пользователь", "Роль", "Проект", "Блок", "Задача", "Подзадача", "Проблема", "Материал", "Движение Товаров"
            };
        }

        private bool _editMode;

        public bool EditMode
        {
            get { return _editMode; }
            set { SetProperty(ref _editMode, value); }
        }

        private Role _editingRole;

        public Role EditingRole
        {
            get { return _editingRole; }
            set { SetProperty(ref _editingRole, value); }
        }

        private StackPanel _mainStackPanel;

        public StackPanel MainStackPanel
        {
            get { return _mainStackPanel; }
            set { SetProperty(ref _mainStackPanel, value); }
        }


        private ObservableCollection<RolePermissionMapping> rolePermissionMappingsToAdd = new ObservableCollection<RolePermissionMapping>();
        public ObservableCollection<RolePermissionMapping> ExistingRolePermissionMappings;
        private ObservableCollection<RolePermissionMapping> rolePermissionMappingsToRemove = new ObservableCollection<RolePermissionMapping>();

        ObservableCollection<CheckBox> CheckBoxes = new ObservableCollection<CheckBox>();

        public void SetRole(Role role)
        {
            EditingRole = role;

            Permissions = new ObservableCollection<Permission>(_permissionService.GetAllPermissions());

            ExistingRolePermissionMappings =
                    new ObservableCollection<RolePermissionMapping>(_rolePermissionMappingService.GetRolePermissionMappingsByRoleId(role.RoleId));

            DisplayPermissions();
        }

        private void DisplayPermissions()
        {
            MainStackPanel = new StackPanel();
            MainStackPanel.Orientation = Orientation.Vertical;
            MainStackPanel.Margin = new Thickness(10);


            // Iterate through the Objects collection and create Border elements dynamically
            foreach (var obj in Objects)
            {
                Border stackPanelBorder = new Border();
                stackPanelBorder.BorderBrush = lineColor;
                stackPanelBorder.BorderThickness = new Thickness(1);

                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;

                stackPanelBorder.Child = stackPanel;

                if (obj is string permissionName)
                {
                    var border = new Border();
                    border.BorderBrush = lineColor;
                    border.BorderThickness = new Thickness(0, 0, 1, 0);
                    border.Width = 150;

                    var textBlock = new TextBlock();
                    textBlock.Text = permissionName;
                    textBlock.Margin = new Thickness(10);
                    textBlock.Foreground = textColor;

                    // Add the TextBlock to the Border
                    border.Child = textBlock;

                    var permissionsStackPanel = new StackPanel();
                    permissionsStackPanel.Orientation = Orientation.Horizontal;
                    permissionsStackPanel.Margin = new Thickness(10);

                    // Create the CheckBoxes dynamically for each action
                    var actions = new[] { "Создавать", "Просматривать", "Редактировать", "Удалять" };
                    foreach (var action in actions)
                    {
                        var checkBox = new CheckBox();
                        checkBox.Content = action;
                        checkBox.Foreground = textColor;
                        checkBox.Margin = new Thickness(5, 0, 10, 0);
                        checkBox.Tag = obj + " " + action;

                        checkBox.IsChecked = 
                            ExistingRolePermissionMappings.Any(mapping => string.Equals(mapping.PermissionName, obj + " " + action, StringComparison.OrdinalIgnoreCase));
                        checkBox.Command = AddPermissionCommand;

                        if (checkBox.IsChecked == true)
                        {
                            CheckBoxes.Add(checkBox);
                        }
                        checkBox.CommandParameter = checkBox;

                        // Add the CheckBox to the StackPanel
                        permissionsStackPanel.Children.Add(checkBox);
                    }

                    stackPanel.Children.Add(border);
                    stackPanel.Children.Add(permissionsStackPanel);

                    MainStackPanel.Children.Add(stackPanelBorder);
                }
            }
        }


        public RelayCommand<TextBox> AddRoleCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        private RelayCommand EditRoleCommand { get; set; }

        public RelayCommand<CheckBox> AddPermissionCommand { get; private set; }

        public event Action Done = delegate { };

        private void OnAddRole(TextBox roleNameTextBox)
        {
            if (!string.IsNullOrEmpty(roleNameTextBox.Text))
            {
                if (EditMode == false)
                {
                    EditingRole.RoleName = roleNameTextBox.Text;

                    _roleService.CreateRole(EditingRole);
                }
                else
                {
                    EditingRole.RoleName = roleNameTextBox.Text;
                    _roleService.UpdateRole(EditingRole);
                }

                foreach (RolePermissionMapping rolePermissionMapping in rolePermissionMappingsToAdd)
                {
                    rolePermissionMapping.RoleId = EditingRole.RoleId;
                    rolePermissionMapping.RoleName = EditingRole.RoleName;

                    _rolePermissionMappingService.CreateRolePermissionMapping(rolePermissionMapping);
                }

                foreach (RolePermissionMapping rolePermissionMapping in rolePermissionMappingsToRemove)
                {
                    _rolePermissionMappingService.DeleteRolePermissionMapping(rolePermissionMapping);
                }

                foreach (CheckBox checkBox in CheckBoxes)
                {
                    checkBox.IsChecked = false;
                }

                CheckBoxes.Clear();

                roleNameTextBox.Text = string.Empty;
            }
        }

        private void OnCancel()
        {
            Done();
        }

        private async void OnAddPermission(CheckBox checkBox)
        {
            if (checkBox != null && checkBox.IsChecked == true && checkBox.Tag is string permissionName)
            {
                Permission permission = Permissions.FirstOrDefault(p => p.PermissionName == permissionName);

                if (permission != null)
                {
                    RolePermissionMapping rolePermissionMapping = new RolePermissionMapping();
                    rolePermissionMapping.PermissionId = permission.PermissionId;
                    rolePermissionMapping.PermissionName = permission.PermissionName;

                    if (EditMode == false)
                    {
                        rolePermissionMappingsToAdd.Add(rolePermissionMapping);
                        CheckBoxes.Add(checkBox);
                    }
                    else
                    {
                        if (rolePermissionMappingsToRemove.Contains(rolePermissionMapping))
                        {
                            ExistingRolePermissionMappings.Add(rolePermissionMapping);
                            rolePermissionMappingsToRemove.Remove(rolePermissionMapping);
                            CheckBoxes.Add(checkBox);
                        }
                        else
                        {
                            rolePermissionMappingsToAdd.Add(rolePermissionMapping);
                            CheckBoxes.Add(checkBox);
                        }
                    }
                    
                }
                else
                {
                    //int lastSpaceIndex = permissionName.LastIndexOf(' ');

                    //string firstPart = permissionName.Substring(0, lastSpaceIndex).ToLower();
                    //string secondPart = permissionName.Substring(lastSpaceIndex + 1).ToLower();

                    //Permission newPermission = new Permission();
                    //newPermission.PermissionName = permissionName;
                    //newPermission.PermissionDescription = $"Может {secondPart} экземпляр объекта \"{firstPart}\"";
                    //_permissionService.CreatePermission(newPermission);
                }
            }
            else if (checkBox != null && checkBox.IsChecked == false && checkBox.Tag is string uncheckedPermissionName)
            {
                Permission newPermission = Permissions.FirstOrDefault(p => p.PermissionName == uncheckedPermissionName);
                
                if (newPermission != null)
                {
                    RolePermissionMapping newMappingToRemove = rolePermissionMappingsToAdd.FirstOrDefault(m => m.PermissionId == newPermission.PermissionId);
                    RolePermissionMapping existingMappingToRemove = ExistingRolePermissionMappings.FirstOrDefault(m => m.PermissionId == newPermission.PermissionId);

                    if (newMappingToRemove != null)
                    {
                        rolePermissionMappingsToAdd.Remove(newMappingToRemove);
                        CheckBoxes.Remove(checkBox);
                    }

                    if (existingMappingToRemove != null)
                    {
                        rolePermissionMappingsToRemove.Add(existingMappingToRemove);
                        ExistingRolePermissionMappings.Remove(existingMappingToRemove);
                        CheckBoxes.Remove(checkBox);
                    }
                }
            }
        }

    }
}
