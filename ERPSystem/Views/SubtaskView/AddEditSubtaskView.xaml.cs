using ERPSystem.Services;
using ERPSystem.Services.BlockServices;
using ERPSystem.Services.TaskMaterialServices;
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

namespace ERPSystem.Views.SubtaskView
{
    public partial class AddEditSubtaskView : UserControl
    {
        public AddEditSubtaskViewModel ViewModel { get; set; }

        public AddEditSubtaskView(IUserService userService, ITaskService taskService, ISubtaskService subtaskService, IMaterialService materialService, IProjectService projectService, 
            ICurrentUser currentUser, ITaskMaterialService taskMaterialService, IBlockService blockService)
        {
            InitializeComponent();

            ViewModel = new AddEditSubtaskViewModel(userService, taskService, subtaskService, materialService, projectService, currentUser,
                blockService, taskMaterialService);

            DataContext = ViewModel;
        }

        private void NumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Allow only numeric input
            e.Handled = !IsNumericInput(e.Text);
        }

        private bool IsNumericInput(string text)
        {
            return double.TryParse(text, out _);
        }
    }
}
