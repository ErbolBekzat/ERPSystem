using ERPSystem.Models;
using ERPSystem.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using ERPSystem.Views.MaterialView;
using Microsoft.EntityFrameworkCore;
using ERPSystem.Services.BlockServices;

namespace ERPSystem.Views.TaskView
{
    public class AddEditTaskViewModel : BindableBase
    {

        readonly Brush lineColor;
        readonly Brush textColor;

        private IUserService _userRepo;
        private ITaskService _taskRepo;
        private IMaterialService _materialRepo;
        private IProjectService _projectRepo;
        private IBlockService _blockService;

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }

        public AddEditTaskViewModel(IUserService userRepo, ITaskService taskRepo, IMaterialService materialService, IProjectService projectService,
            IBlockService blockService)
        {
            textColor = new SolidColorBrush((Color)Application.Current.Resources["TextColor"]);
            lineColor = new SolidColorBrush((Color)Application.Current.Resources["LineColor"]);

            _userRepo = userRepo;
            _taskRepo = taskRepo;
            _materialRepo = materialService;
            _projectRepo = projectService;
            _blockService = blockService;


            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave);

            LoadUsers();
        }

        public void LoadUsers()
        {
            Users = new ObservableCollection<User>(
                _userRepo.GetAllUsers());
        }

        private bool _EditMode;

        public bool EditMode
        {
            get { return _EditMode; }
            set { SetProperty(ref _EditMode, value); }
        }

        private bool inputError;

        public bool InputError
        {
            get { return inputError; }
            set { SetProperty(ref inputError, value); }
        }


        private SimpleEditableTask _Task;

        public SimpleEditableTask Task
        {
            get { return _Task; }
            set { SetProperty(ref _Task, value); }
        }

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        public event Action<Block> Done = delegate { };

        private Models.Task _editingTask = null;

        public void SetTask(Models.Task task)
        {
            _editingTask = task;

            Task = new SimpleEditableTask();
            CopyTask(task, Task);
            InputError = false;
        }

        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void CopyTask(Models.Task source, SimpleEditableTask target)
        {
            target.Id = source.Id;
            target.ProjectId = source.ProjectId;
            target.StartDate = source.StartDate;
            target.EndDate = source.EndDate;
            target.CompletedDate = source.CompletedDate;

            if (EditMode)
            {
                target.Title = source.Title;
                target.Description = source.Description;
                target.Status = source.Status;
                target.AssignedUserId = source.AssignedUserId;
                target.AssignedUser = source.AssignedUser;
            }

        }


        private async void OnCancel()
        {
            Block block = _blockService.GetBlockById(_editingTask.BlockId);
            Done(block);
        }

        private void OnSave()
        {
            if (ValidateInputs())
            {
                UpdateTask(Task, _editingTask);

                if (EditMode)
                {
                    _taskRepo.UpdateTask(_editingTask);
                }

                else
                {
                    _taskRepo.AddTask(_editingTask);
                }

                OnCancel();
            }
            else
            {
                InputError = true;
            }
        }

        private void UpdateTask(SimpleEditableTask source, Models.Task target)
        {
            target.Title = source.Title;
            target.Description = source.Description;
            target.StartDate = source.StartDate; // Convert to UTC
            target.EndDate = source.EndDate;     // Convert to UTC
            target.CompletedDate = source.CompletedDate; // Convert to UTC, handle nullable case
            target.Status = source.Status;
            target.AssignedUserId = source.AssignedUserId;
            target.AssignedUser = source.AssignedUser;
        }


        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(Task.Title) || string.IsNullOrEmpty(Task.Description) || Task.AssignedUserId == null || !CheckIfValidUserId(Task.AssignedUserId)) return false;
            else return true;
        }

        private bool CheckIfValidUserId(int userId)
        {
            return _userRepo.GetUserById(userId) != null;
        }
    }
}
