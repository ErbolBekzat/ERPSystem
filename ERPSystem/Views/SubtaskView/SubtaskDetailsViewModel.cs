using ERPSystem.Data;
using ERPSystem.Models;
using ERPSystem.Services;
using ERPSystem.Views.TaskView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using ERPSystem.Services.SubtaskRelationshipServices;
using ERPSystem.Services.TaskMaterialServices;
using ERPSystem.Services.StockMovementServices;
using ERPSystem.Services.ProblemServices;
using System.Windows.Media.Imaging;
using ERPSystem.Utilities.GoogleStorage;
using System.Security.AccessControl;
using System.IO;
using System.Diagnostics;
using ERPSystem.Services.BlockServices;
using ERPSystem.Services.StockServices;

namespace ERPSystem.Views.SubtaskView
{
    public class SubtaskDetailsViewModel : BindableBase
    {
        private IBlockService _blockService;

        private ITaskService _taskService;
        private ISubtaskService _subtaskService;

        private IProblemService _problemService;

        private ISubtaskRelationshipService _subtaskRelationshipService;

        private IMaterialService _materialService;
        private IProjectService _projectService;

        private ITaskMaterialService _taskMaterialService;
        private IStockMovementService _stockMovementService;
        private IStockService _stockService;

        private DataBaseContext _dataBaseContext;

        readonly Brush textColor;
        readonly Brush lineColor;

        private SimpleEditableSubtask _subtask;

        public SimpleEditableSubtask Subtask
        {
            get { return _subtask; }
            set { SetProperty(ref _subtask, value); }
        }

        private ObservableCollection<Subtask> subtasks;

        public ObservableCollection<Subtask> Subtasks
        {
            get { return subtasks; }
            set { SetProperty(ref subtasks, value); }
        }

        private ObservableCollection<TaskMaterials> taskMaterials;

        public ObservableCollection<TaskMaterials> TaskMaterials
        {
            get { return taskMaterials; }
            set { SetProperty(ref taskMaterials, value); }
        }

        private ObservableCollection<Problem> _problems;

        public ObservableCollection<Problem> Problems
        {
            get { return _problems; }
            set { SetProperty(ref _problems, value); }
        }

        private BitmapImage _bitmap;

        public BitmapImage Bitmap
        {
            get { return _bitmap; }
            set { SetProperty(ref _bitmap, value); }
        }



        private Subtask _readingSubtask = null;

        private Grid problemsGrid;
        private Grid materialsGrid;

        public SubtaskDetailsViewModel(IBlockService blockService, ITaskService taskService, ISubtaskService subtaskService, ISubtaskRelationshipService subtaskRelationshipService, 
            IMaterialService materialService, IProjectService projectService, ITaskMaterialService taskMaterialService, IStockMovementService stockMovementService, 
            IProblemService problemService, IStockService stockService, DataBaseContext context, Grid problemsGrid, Grid materials)
        {
            _blockService = blockService;
            _taskService = taskService;
            _subtaskService = subtaskService;
            _problemService = problemService;

            _subtaskRelationshipService = subtaskRelationshipService;

            _materialService = materialService;
            _projectService = projectService;

            _taskMaterialService = taskMaterialService;
            _stockMovementService = stockMovementService;
            _stockService = stockService;

            _dataBaseContext = context;

            this.problemsGrid = problemsGrid;
            materialsGrid = materials;

            textColor = new SolidColorBrush((Color)Application.Current.Resources["TextColor"]);
            lineColor = new SolidColorBrush((Color)Application.Current.Resources["LineColor"]);


            BackCommand = new RelayCommand(OnBack);
            EditSubtaskCommand = new RelayCommand(OnEditSubtask);
            CompleteSubtaskCommand = new RelayCommand(OnCompleteSubtask, CanCompleteSubtask);
            DeleteSubtaskCommand = new RelayCommand(OnDeleteSubtask);

            AddNewProblemCommand = new RelayCommand(OnAddNewProblem);
            CompleteProblemCommand = new RelayCommand<int>(OnCompleteProblem);
            EditProblemCommand = new RelayCommand<int> (OnEditProblem);
            DeleteProblemCommand = new RelayCommand<int>(OnDeleteProblem);
            OpenImageCommand = new RelayCommand<string> (OnOpenImage);

            Subtasks = new ObservableCollection<Subtask>();
        }

        public void SetSubtask(Subtask subtask)
        {
            _readingSubtask = subtask;

            TaskMaterials = new ObservableCollection<TaskMaterials>(_taskMaterialService.GetTaskMaterialsBySubtaskId(subtask.Id));
            Problems = new ObservableCollection<Problem>(_problemService.GetAllProblemsBySubtaskId(subtask.Id));

            Subtask = new SimpleEditableSubtask();
            CopySubtask(_readingSubtask);

            CompleteSubtaskCommand.RaiseCanExecuteChanged();
        }

        private void CopySubtask(Subtask source)
        {
            Subtask.Id = source.Id;
            Subtask.TaskId = source.TaskId;
            Subtask.Title = source.Title;
            Subtask.Description = source.Description;
            Subtask.StartDate = source.StartDate;
            Subtask.EndDate = source.EndDate;
            Subtask.CompletedDate = source.CompletedDate;
            Subtask.Status = source.Status;
            Subtask.AssignedUserId = source.AssignedUserId;
            Subtask.AssignedUser = source.AssignedUser;
        }


        public RelayCommand BackCommand { get; private set; }
        public RelayCommand EditSubtaskCommand { get; private set; }
        public RelayCommand CompleteSubtaskCommand { get; private set; }
        public RelayCommand DeleteSubtaskCommand { get; private set; }

        public event Action<Block> BackRequested = delegate { };
        public event Action<Subtask> EditSubtaskRequested = delegate { };
        public event Action DeleteSubtaskRequested = delegate { };

        private void OnBack()
        {
            Block block = _blockService.GetBlockById(_readingSubtask.Task.BlockId);
            BackRequested(block);
        }

        private void OnEditSubtask()
        {
            EditSubtaskRequested(_readingSubtask);
        }

        private void OnCompleteSubtask()
        {
            _readingSubtask.Status = Models.TaskStatus.Completed;
            _readingSubtask.CompletedDate = DateTime.Now.Date;
            _readingSubtask.CompletedDate = _readingSubtask.CompletedDate?.Date;
            _subtaskService.UpdateSubtask(_readingSubtask);

            CompleteSubtaskCommand.RaiseCanExecuteChanged();

            CopySubtask(_readingSubtask);

            if (TaskMaterials.Count > 0)
            {
                foreach (TaskMaterials taskMaterial in TaskMaterials)
                {
                    Stock stock = _stockService.GetStockByProjectId(_readingSubtask.Task.ProjectId);
                    Material material = _materialService.GetMaterialById(taskMaterial.MaterialId);
                    StockMovement stockMovement = new StockMovement();
                    stockMovement.MaterialId = taskMaterial.MaterialId;
                    stockMovement.SubtaskId = _readingSubtask.Id;
                    stockMovement.Description = "Использовалось для задачи: " + _readingSubtask.Title;
                    stockMovement.CreatedDate = DateTime.Now.Date;
                    stockMovement.Quantity = taskMaterial.QuantityRequired;
                    stockMovement.Cost = material.UnitPrice * taskMaterial.QuantityRequired;
                    stockMovement.MovementType = StockMovementType.Subtraction;
                    stockMovement.StockId = stock.Id;
                    material.QuantityInStock -= taskMaterial.QuantityRequired;

                    _materialService.UpdateMaterial(material);
                    _stockMovementService.AddStockMovement(stockMovement);
                }
            }

            if (AreAllSubtasksCompleted(_readingSubtask.Task))
            {
                Task task = _taskService.GetTaskById(_readingSubtask.Task.Id);
                task.Status = TaskStatus.Completed;
                _taskService.UpdateTask(task);
            }
        }

        private bool CanCompleteSubtask()
        {
            if (_readingSubtask != null && _readingSubtask.Status != Models.TaskStatus.Completed)
            {
                return true;
            }

            return false;
        }

        private void OnDeleteSubtask()
        {
            bool seleteSubtask = _subtaskService.DeleteSubtask(_readingSubtask.Id);
            
            if (!seleteSubtask)
            {
                MessageBox.Show("Вы не можете удалить подзадачу с материалами, проблемами или связью!" +
                    "\nУдалите их первыми", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            OnBack();
        }

        public RelayCommand AddNewProblemCommand { get; set; }

        public event Action<Subtask> AddNewProblemRequested = delegate { };

        private void OnAddNewProblem()
        {
            AddNewProblemRequested(_readingSubtask);
        }

        public RelayCommand<int> CompleteProblemCommand { get; set; }
        private void OnCompleteProblem(int problemId)
        {
            Problem problem = _problemService.GetProblemById(problemId);
            problem.ProblemStatus = TaskStatus.Completed;

            _problemService.UpdateProblem(problem);
        }

        public RelayCommand<int> EditProblemCommand { get; set; }
        public event Action<Problem> EditProblemRequested = delegate { };
        private void OnEditProblem(int problemId)
        {
            Problem problem = _problemService.GetProblemById(problemId);
            problem.ProblemStatus = TaskStatus.Completed;

            EditProblemRequested(problem);
        }

        public RelayCommand<int> DeleteProblemCommand { get; private set; }
        private void OnDeleteProblem(int problemId)
        {
            Problem problem = _problemService.GetProblemById(problemId);
            Problems.Remove(problem);

            var cloudStorageService = new CloudStorageService();
            cloudStorageService.DeleteFile(problem.Image);

            _problemService.DeleteProblem(problemId);
        }

        public RelayCommand<string> OpenImageCommand { get; private set; }
        private void OnOpenImage(string image)
        {
            string imagePathFolder = "C:\\Users\\derbo\\OneDrive\\Изображения\\40Tribes\\";
            string imagePath = Path.Combine(imagePathFolder, image);

            Process.Start(new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = imagePath,
            });
        }


        public bool AreAllSubtasksCompleted(Task task)
        {
            if (task.Subtasks == null || task.Subtasks.Count == 0)
            {
                // If there are no subtasks, consider them all completed
                return true;
            }

            foreach (var subtask in task.Subtasks)
            {
                if (subtask.Status != TaskStatus.Completed)
                {
                    // If any subtask is not completed, return false
                    return false;
                }
            }

            // All subtasks are completed
            return true;
        }


    }
}
