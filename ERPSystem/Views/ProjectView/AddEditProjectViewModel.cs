using ERPSystem.Models;
using ERPSystem.Services;
using ERPSystem.Services.StockServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Views.ProjectView
{
    public class AddEditProjectViewModel : BindableBase
    {
        IProjectService _projectService;
        IUserService _userService;
        IStockService _stockService;

        ICurrentUser currentUser;

        public AddEditProjectViewModel(IProjectService projectService, IUserService userService, IStockService stockService, ICurrentUser currentUser)
        {
            this._projectService = projectService;
            this._userService = userService;
            this._stockService = stockService;
            this.currentUser = currentUser;

            AddProjectCommand = new RelayCommand(OnAddProject);
            CancelCommand = new RelayCommand(OnCancel);
        }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }

        private int _stockManagerId;
        public int StockManagerId
        {
            get { return _stockManagerId; }
            set { SetProperty(ref _stockManagerId, value); }
        }

        private bool editMode;

        public bool EditMode
        {
            get { return editMode; }
            set { SetProperty(ref editMode, value); }
        }


        private Project project;

        public Project Project
        {
            get { return project; }
            set { SetProperty(ref project, value); }
        }

        private bool inputError;

        public bool InputError
        {
            get { return inputError; }
            set { SetProperty(ref inputError, value); }
        }


        public void SetProject(Project project)
        {
            Project = project;

            InputError = false;

            Users = new ObservableCollection<User>(_userService.GetAllUsers());
        }

        public RelayCommand AddProjectCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public event Action Done;

        private async void OnAddProject()
        {
            if (ValidateInputs())
            {
                Project.Version = 1;
                Project.Status = Models.TaskStatus.NotStarted;
                Project.PublishedStatus = ProjectPublishedStatus.NotPublished;

                await _projectService.CreateProjectAsync(Project);

                AddStockForProject();

                Done();
            }
            else
            {
                InputError = true;
            }
        }

        private void AddStockForProject()
        {
            Stock newStock = new Stock();
            newStock.Name = Project.Name + "Stock";
            newStock.Description = Project.Description = "Description";
            newStock.ProjectId = Project.Id;
            newStock.StockManagerId = StockManagerId;

            _stockService.CreateStock(newStock);
        }

        private void OnCancel()
        {
            Done();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(Project.Name) || string.IsNullOrEmpty(Project.Description)) return false;
            else return true;
        }


    }
}
