using ERPSystem.Services;
using System.Windows;
using System.Windows.Controls;

namespace ERPSystem.Views.MaterialView
{
    public partial class AddEditMaterialView : Window
    {
        AddEditMaterialViewModel viewModel;

        public AddEditMaterialViewModel ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public AddEditMaterialView(ITaskService taskService, IMaterialService materialService)
        {
            InitializeComponent();

            ViewModel = new AddEditMaterialViewModel(materialService, taskService);

            DataContext = ViewModel;
        }
    }
}
