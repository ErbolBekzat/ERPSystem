using ERPSystem.Models;
using ERPSystem.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using System.Windows;
using ERPSystem.Services.TaskMaterialServices;
using ERPSystem.Services.BlockServices;

namespace ERPSystem.Views.SubtaskView
{
    public class AddEditSubtaskViewModel : BindableBase
    {
        readonly Brush lineColor;
        readonly Brush textColor;

        ICurrentUser user;

        private IUserService _userRepo;
        private ITaskService _taskRepo;
        private ISubtaskService _subtaskRepo;
        private IMaterialService _materialRepo;
        private IProjectService _projectRepo;
        private IBlockService _blockService;
        private ITaskMaterialService _taskMaterialRepo;

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }

        private ObservableCollection<Material> materials;
        public ObservableCollection<Material> Materials
        {
            get { return materials; }
            set { SetProperty(ref materials, value); }
        }

        public AddEditSubtaskViewModel(IUserService userRepo, ITaskService taskRepo, ISubtaskService subtaskService, IMaterialService materialService, 
            IProjectService projectService, ICurrentUser currentUser, IBlockService blockService,
            ITaskMaterialService taskMaterialService)
        {
            textColor = new SolidColorBrush((Color)Application.Current.Resources["TextColor"]);
            lineColor = new SolidColorBrush((Color)Application.Current.Resources["LineColor"]);

            this.user = user;

            _userRepo = userRepo;
            _taskRepo = taskRepo;
            _subtaskRepo = subtaskService;
            _materialRepo = materialService;
            _projectRepo = projectService;
            _blockService = blockService;
            _taskMaterialRepo = taskMaterialService;


            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave);

            AddNewMaterialRowCommand = new RelayCommand(AddNewMaterialRow);

            LoadUsers();
        }

        public void LoadUsers()
        {
            Users = new ObservableCollection<User>(
                _userRepo.GetAllUsers());

            Materials = new ObservableCollection<Material>(_materialRepo.GetAllMaterials());
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

        private SimpleEditableSubtask _Subtask;

        public SimpleEditableSubtask Subtask
        {
            get { return _Subtask; }
            set 
            { 
                SetProperty(ref _Subtask, value);
            }
        }

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        public event Action<Block> Done = delegate { };

        private Models.Subtask _editingSubtask = null;

        private ObservableCollection<TaskMaterials> _existingTaskMaterials;
        public ObservableCollection<TaskMaterials> ExistingTaskMaterials
        {
            get { return _existingTaskMaterials; }
            set { SetProperty(ref _existingTaskMaterials, value); }
        }

        private ObservableCollection<TaskMaterials> _newTaskMaterials;
        public ObservableCollection<TaskMaterials> NewTaskMaterials
        {
            get { return _newTaskMaterials; }
            set { SetProperty(ref _newTaskMaterials, value); }
        }

        public void SetSubtask(Models.Subtask task)
        {
            LoadUsers();

            _editingSubtask = task;

            ExistingTaskMaterials = new ObservableCollection<TaskMaterials>(_taskMaterialRepo.GetTaskMaterialsBySubtaskId(_editingSubtask.Id));
            NewTaskMaterials = new ObservableCollection<TaskMaterials>();

            Subtask = new SimpleEditableSubtask();

            CopyTask(task, Subtask);
            InputError = false;
        }

        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void CopyTask(Models.Subtask source, SimpleEditableSubtask target)
        {
            target.Id = source.Id;
            target.TaskId = source.TaskId;
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


        private void OnCancel()
        {
            Models.Task task = _taskRepo.GetTaskById(Subtask.TaskId);
            Block block = _blockService.GetBlockById(task.BlockId);
            Done(block);
        }

        private void OnSave()
        {
            if (ValidateInputs())
            {
                UpdateSubtask(Subtask, _editingSubtask);

                if (EditMode)
                {
                    _subtaskRepo.UpdateSubtask(_editingSubtask);

                    foreach (TaskMaterials taskMaterial in NewTaskMaterials)
                    {
                        taskMaterial.TaskId = _editingSubtask.Id;
                    }

                    UpdateMaterials();

                    AddMaterials();
                }

                else
                {
                    _subtaskRepo.AddSubtask(_editingSubtask);

                    foreach (TaskMaterials taskMaterial in NewTaskMaterials)
                    {
                        taskMaterial.TaskId = _editingSubtask.Id;
                    }

                    UpdateMaterials();

                    AddMaterials();
                }

                OnCancel();
            }
            else
            {
                InputError = true;
            }
        }


        private void UpdateSubtask(SimpleEditableSubtask source, Models.Subtask target)
        {
            target.Title = source.Title;
            target.Description = source.Description;
            target.StartDate = source.StartDate;
            target.EndDate = source.EndDate;
            target.CompletedDate = source.CompletedDate;
            target.Status = source.Status;
            target.AssignedUserId = source.AssignedUserId;
            target.AssignedUser = source.AssignedUser;
            target.TaskId = source.TaskId;
        }

        public RelayCommand AddNewMaterialRowCommand { get; set; }

        private void AddNewMaterialRow()
        {
            TaskMaterials taskMaterial = new TaskMaterials
            {
                QuantityRequired = 0,
                Cost = 0
            };

            NewTaskMaterials.Add(taskMaterial);
        }

        private void AddMaterials()
        {
            _taskMaterialRepo.AddTaskMaterialsRange(NewTaskMaterials);
        }

        private void UpdateMaterials()
        {
            var duplicateMaterials = NewTaskMaterials.Where(newTaskMaterial => ExistingTaskMaterials.Any(existingTaskMaterial => existingTaskMaterial.MaterialId == newTaskMaterial.MaterialId)).ToList();


            foreach (var duplicateMaterial in duplicateMaterials)
            {
                var matchingExistingTaskMaterial = ExistingTaskMaterials.FirstOrDefault(tm => tm.MaterialId == duplicateMaterial.MaterialId);

                if (matchingExistingTaskMaterial != null)
                {
                    matchingExistingTaskMaterial.QuantityRequired += duplicateMaterial.QuantityRequired;
                }

                NewTaskMaterials.Remove(duplicateMaterial);
            }

            _taskMaterialRepo.UpdateTaskMaterialsRange(ExistingTaskMaterials);
        }


        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(Subtask.Title) || string.IsNullOrEmpty(Subtask.Description) || Subtask.AssignedUserId == null || !CheckIfValidUserId(Subtask.AssignedUserId)) return false;
            else return true;
        }

        private bool CheckIfValidUserId(int userId)
        {
            return _userRepo.GetUserById(userId) != null;
        }

    }
}
