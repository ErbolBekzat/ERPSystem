using ERPSystem.Models;
using ERPSystem.Services;
using ERPSystem.Views.TaskView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ERPSystem.Views.MaterialView
{
    public class AddEditMaterialViewModel : BindableBase
    {
        private IMaterialService _materialService;
        private ITaskService _taskService;

        Window _window;

        public AddEditMaterialViewModel(IMaterialService materialRepo, ITaskService taskService)
        {
            _materialService = materialRepo;
            _taskService = taskService;


            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave);
        }

        private bool _EditMode;

        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }

        private Material _Material;

        public Material Material
        {
            get { return _Material; }
            set { SetProperty(ref _Material, value); }
        }

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        public event Action<Project> SaveRequested = delegate { };

        private bool inputError;

        public bool InputError
        {
            get { return inputError; }
            set { SetProperty(ref inputError, value); }
        }

        Project project;

        public void SetMaterial(Material material, Window window, Project project)
        {
            _window = window;
            this.project = project;
            Material = material;

            InputError = false;
        }

        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }


        private void OnCancel()
        {
            _window.Close();
        }

        private void OnSave() 
        {
            if (ValidateInputs())
            {
                if (EditMode)
                {
                    _materialService.UpdateMaterial(Material);
                }
                else
                {
                    Material.QuantityInStock = 0;
                    _materialService.AddMaterial(Material);
                }

                SaveRequested(project);
                OnCancel();
            }
            else
            {
                InputError = true;
            }
        }

        private bool ValidateInputs()
        {
            if (Material.Name == null || Material.Description == null || Material.UnitOfMeasure == null || Material.UnitPrice == null) return false;
            return true;
        }
    }
}
