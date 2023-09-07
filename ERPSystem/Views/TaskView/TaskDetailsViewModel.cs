using ERPSystem.Data;
using ERPSystem.Models;
using ERPSystem.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using ERPSystem.Services.BlockServices;

namespace ERPSystem.Views.TaskView
{
    public class TaskDetailsViewModel : BindableBase
    {
        private ITaskService _taskService;
        private ISubtaskService _subtaskService;
        private IMaterialService _materialService;
        private IProjectService _projectService;
        private IBlockService _blockService;
        private IRoleService _roleService;

        private ICurrentUser _currentUser;

        //private DataBaseContext _dataBaseContext;

        readonly Brush textColor;
        readonly Brush lineColor;

        ObservableCollection<Task> Tasks { get; set; }

        private SimpleEditableTask _task;

		public SimpleEditableTask Task
		{
			get { return _task; }
			set { SetProperty(ref _task, value); }
		}

        private ObservableCollection<Subtask> subtasks;

        public ObservableCollection<Subtask> Subtasks
        {
            get { return subtasks; }
            set { SetProperty(ref subtasks, value); }
        }


        private Task _readingTask = null;

        private Grid tasksOrProblemsGrid;
        private Grid materialsGrid;

        private bool _canUpdateTask;

        public bool CanUpdateTask
        {
            get { return _canUpdateTask; }
            set { SetProperty(ref _canUpdateTask, value); }
        }

        private bool _canDeleteTask;

        public bool CanDeleteTask
        {
            get { return _canDeleteTask; }
            set { SetProperty(ref _canDeleteTask, value); }
        }



        public TaskDetailsViewModel(ITaskService taskRepo, ISubtaskService subtaskService, IMaterialService materialService, 
            IProjectService projectService, IRoleService roleService, ICurrentUser currentUser,
            IBlockService blockService, Grid tasksOrProblemsGrid, Grid materials)
        {
            _taskService = taskRepo;
            _subtaskService = subtaskService;
            _materialService = materialService;
            _projectService = projectService;
            _blockService = blockService;
            _roleService = roleService;

            _currentUser = currentUser;

            this.tasksOrProblemsGrid = tasksOrProblemsGrid;
            materialsGrid = materials;

            textColor = new SolidColorBrush((Color)Application.Current.Resources["TextColor"]);
            lineColor = new SolidColorBrush((Color)Application.Current.Resources["LineColor"]);


            BackCommand = new RelayCommand(OnBack);
            EditTaskCommand = new RelayCommand(OnEditTask);
            DeleteTaskCommand = new RelayCommand(OnDeleteTask);

            Subtasks = new ObservableCollection<Subtask>();
        }

		public void SetTask(Task task)
		{
            _readingTask = task;

            //GetChildTasks(task);
            //GetMaterials(task);

            Task = new SimpleEditableTask();
			CopyTask(task);

            if (_roleService.GetRoleById(_currentUser.UserRole).RoleName != "SuperUser")
            {
                CanUpdateTask = _roleService.HasPermissions(_currentUser.UserRole, "Задача Редактировать");
                CanDeleteTask = _roleService.HasPermissions(_currentUser.UserRole, "Задача Удалять");
            }
            else
            {
                CanUpdateTask = true;
                CanDeleteTask = true;
            }
        }

        private void GetChildTasks(Task task)
        {
            tasksOrProblemsGrid.Children.Clear();
            tasksOrProblemsGrid.RowDefinitions.Clear();

            Subtasks = new ObservableCollection<Subtask>();

            if (Subtasks.Count == 0) return;

            int i = 0;

            foreach (Subtask childTask in Subtasks)
            {
                tasksOrProblemsGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(0, GridUnitType.Auto)
                });

                StackPanel buttons = new StackPanel();
                buttons.Orientation = Orientation.Horizontal;
                Grid.SetColumn(buttons, 0);
                Grid.SetRow(buttons, i);
                tasksOrProblemsGrid.Children.Add(buttons);

                Button infoButton = new Button();
                infoButton.Content = "Info";
                //infoButton.Command = TaskDetailsCommand;
                infoButton.CommandParameter = task.Id;
                infoButton.Background = new SolidColorBrush(Colors.Transparent);
                infoButton.Foreground = textColor;
                infoButton.Width = 30;
                infoButton.HorizontalAlignment = HorizontalAlignment.Left;
                infoButton.SetValue(Grid.ColumnProperty, 1);
                buttons.Children.Add(infoButton);

                Button editButton = new Button();
                editButton.Content = "Edit";
                //infoButton.Command = TaskDetailsCommand;
                editButton.CommandParameter = task.Id;
                editButton.Background = new SolidColorBrush(Colors.Transparent);
                editButton.Foreground = textColor;
                editButton.Width = 30;
                editButton.HorizontalAlignment = HorizontalAlignment.Left;
                editButton.SetValue(Grid.ColumnProperty, 1);
                buttons.Children.Add(editButton);

                Button deleteButton = new Button();
                deleteButton.Content = "Delete";
                //deleteButton.Command = TaskDetailsCommand;
                deleteButton.CommandParameter = task.Id;
                deleteButton.Background = new SolidColorBrush(Colors.Transparent);
                deleteButton.Foreground = textColor;
                deleteButton.Width = 50;
                deleteButton.HorizontalAlignment = HorizontalAlignment.Left;
                deleteButton.SetValue(Grid.ColumnProperty, 1);
                buttons.Children.Add(deleteButton);


                Border titleBorder = new Border();
                titleBorder.VerticalAlignment = VerticalAlignment.Top;
                titleBorder.BorderBrush = lineColor;
                titleBorder.BorderThickness = new Thickness(.5);
                Grid.SetColumn(titleBorder, 1);
                Grid.SetRow(titleBorder, i);

                TextBlock title = new TextBlock();
                title.Text = childTask.Title;
                title.Height = 29;
                title.HorizontalAlignment = HorizontalAlignment.Left;
                title.Foreground = textColor;

                titleBorder.Child = title;

                tasksOrProblemsGrid.Children.Add(titleBorder);

                Border descriptionBorder = new Border();
                descriptionBorder.VerticalAlignment = VerticalAlignment.Top;
                descriptionBorder.BorderBrush = lineColor;
                descriptionBorder.BorderThickness = new Thickness(.5);
                Grid.SetColumn(descriptionBorder, 2);
                Grid.SetRow(descriptionBorder, i);

                TextBlock description = new TextBlock();
                description.Text = childTask.Description;
                description.Height = 29;
                description.HorizontalAlignment = HorizontalAlignment.Left;
                description.Foreground = textColor;

                descriptionBorder.Child = description;

                tasksOrProblemsGrid.Children.Add(descriptionBorder);

                i++;
            }
        }

        private void GetMaterials(Task task)
        {
            materialsGrid.Children.Clear();
            materialsGrid.RowDefinitions.Clear();
            materialsGrid.ColumnDefinitions.Clear();
       
            ObservableCollection<Material> materials;

            materials = GetMaterialsByParentTaskId(task.Id);

            PropertyInfo[] properties = typeof(Material).GetProperties()
                    .Where(p => p.Name != "Id" && p.Name != "TaskId" && p.Name != "Task").ToArray();
            
            int i = 0;

            materialsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            foreach (PropertyInfo property in properties)
            {
                materialsGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    SharedSizeGroup = property.Name,
                });

                Border border = new Border();
                border.VerticalAlignment = VerticalAlignment.Top;
                border.BorderBrush = lineColor;
                border.BorderThickness = new Thickness(.5);
                Grid.SetColumn(border, i);
                Grid.SetRow(border, 0);

                TextBlock textBlock = new TextBlock();
                textBlock.Text = property.Name;
                textBlock.Height = 29;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.Foreground = textColor;

                border.Child = textBlock;

                materialsGrid.Children.Add(border);

                i++;
            }

            int rowIndex = 1;

            foreach (Material material in materials)
            {
                i = 0;

                materialsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                foreach (PropertyInfo property in properties)
                {
                    Border border = new Border();
                    border.VerticalAlignment = VerticalAlignment.Top;
                    border.BorderBrush = lineColor;
                    border.BorderThickness = new Thickness(.5);
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, rowIndex);

                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = property.GetValue(material).ToString();
                    textBlock.Height = 29;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock.Foreground = textColor;

                    border.Child = textBlock;

                    materialsGrid.Children.Add(border);

                    i++;
                }

                rowIndex++;
            }
        }

        public ObservableCollection<Material> GetMaterialsByParentTaskId(int parentTaskId)
        {
            //var materials = from material in _dataBaseContext.Materials
            //                join task in _dataBaseContext.Tasks equals task.Id
            //                select material;

            //List<Material> mergedMaterials = materials
            //               .GroupBy(m => m.Title)
            //               .Select(g => new Material
            //               {
            //                   Title = g.Key,
            //                   Description = g.First().Description,
            //                   Count = g.Sum(m => m.Count),
            //                   Unit = g.First().Unit,
            //                   Price = g.First().Price,
            //                   TaskId = g.First().TaskId,
            //                   Task = g.First().Task
            //               })
            //               .ToList();

            return new ObservableCollection<Material>();
        }

        private void CopyTask(Task source)
        {
            Task.Id = source.Id;
            Task.ProjectId = source.ProjectId;
            Task.Title = source.Title;
            Task.Description = source.Description;
            Task.StartDate = source.StartDate;
            Task.EndDate = source.EndDate;
            Task.CompletedDate = source.CompletedDate;
            Task.Status = source.Status;
            Task.AssignedUserId = source.AssignedUserId;
            Task.AssignedUser = source.AssignedUser;
        }

        public RelayCommand BackCommand { get; private set; }
        public RelayCommand EditTaskCommand { get; private set; }
        public RelayCommand DeleteTaskCommand { get; private set; }

        public event Action<Block> BackRequest = delegate { };
        public event Action<Task> EditTaskRequest = delegate { };
        public event Action DeleteTaskRequest = delegate { };

        private void OnBack()
        {
            Block block =_blockService.GetBlockById(_readingTask.BlockId);
            BackRequest(block);
        }

        private void OnEditTask()
        {
            EditTaskRequest(_readingTask);
        }

        private void OnDeleteTask()
        {
            List<Subtask> subtasks = new List<Subtask>(_subtaskService.GetAllSubtasksWithTaskId(_readingTask.Id));

            bool deleted = _taskService.DeleteTask(_readingTask.Id);

            if (!deleted)
            {
                MessageBox.Show("Вы не можете удалить задачу с подзадачами!\nУдалите их первыми", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            OnBack();
        }

    }
}
