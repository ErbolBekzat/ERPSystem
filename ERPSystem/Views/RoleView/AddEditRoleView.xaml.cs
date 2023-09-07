using ERPSystem.Models;
using ERPSystem.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Configuration;
using System;
using ERPSystem.Views.UserView;
using Microsoft.Practices.Unity;

namespace ERPSystem.Views.RoleView
{
    public partial class AddEditRoleView : UserControl
    {
        private AddEditRoleViewModel viewModel;
        public AddEditRoleViewModel ViewModel
        {   get { return viewModel; }
            set { viewModel = value; } 
        }


        private IRoleService _roleService;
        private IPermissionService _permissionService;
        private IRolePermissionMappingService _rolePermissionMappingService;

        public AddEditRoleView()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Container.Resolve<AddEditRoleViewModel>();

            DataContext = ViewModel;
        }
    }


}
