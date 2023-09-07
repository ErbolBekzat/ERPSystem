using ERPSystem.Services;
using ERPSystem.Services.StockServices;
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

namespace ERPSystem.Views.ProjectView
{
    public partial class AddEditProjectView : UserControl
    {
        public AddEditProjectViewModel ViewModel;

        public AddEditProjectView(IProjectService projectService, IUserService userService, IStockService stockService, ICurrentUser currentUser)
        {
            InitializeComponent();

            ViewModel = new AddEditProjectViewModel(projectService, userService, stockService, currentUser);

            DataContext = ViewModel;
        }
    }
}
