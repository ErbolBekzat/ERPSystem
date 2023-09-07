using ERPSystem.Models;
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

namespace ERPSystem.Views.ProjectView
{
    public partial class ProjectBriefInfo : UserControl
    {
        public ProjectBriefInfoViewModel ViewModel;

        public ProjectBriefInfo(Project project, ICommand navToStockCommand, ICommand navToTaskCommand, ITaskService taskService)
        {
            InitializeComponent();

            ViewModel = new ProjectBriefInfoViewModel(project, taskService);

            DataContext = ViewModel;

            StockBtn.Command = navToStockCommand;
            StockBtn.CommandParameter = project;

            BlocksBtn.Command = navToTaskCommand;
            BlocksBtn.CommandParameter = project;
        }
    }
}
