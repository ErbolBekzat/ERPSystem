using ERPSystem.Models;
using ERPSystem.Services;
using ERPSystem.Services.SubtaskRelationshipServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace ERPSystem.Views.TaskView
{
    public partial class TaskGanttView : UserControl
    {
        ObservableCollection<Models.Task> Tasks { get; set; }
        ObservableCollection<Subtask> Subtasks { get; set; }

        ObservableCollection<Grid> _childTaskGrids { get; set; }
        ObservableCollection<Grid> _childTaskCanvasGrids { get; set; }

        ITaskService _taskService { get; set; }
        ISubtaskService _subtaskService { get; set; }
        ISubtaskRelationshipService _subtaskRelationshipService { get; set; }
        IRoleService _roleService { get; set; }
        IProjectService _projectService { get; set; }

        private ICurrentUser _currentUser;
        public ICurrentUser CurrentUser => _currentUser;


        double parentRectHeight { get; set; }

        readonly Brush background;
        readonly Brush headerBackground;
        readonly Brush primaryTaskBG;
        readonly Brush secondaryTaskBG;
        readonly Brush primaryRectBG;
        readonly Brush secondaryRectBG;
        readonly Brush lineColor;
        readonly Brush textColor;


        public TaskGanttView(ITaskService taskRepo, ISubtaskService subtaskService, ICurrentUser currentUser, ISubtaskRelationshipService subtaskRelationshipService,
            IRoleService roleService, IProjectService projectService)
        {
            InitializeComponent();

            DataContext = this;

            _taskService = taskRepo;
            _subtaskService = subtaskService;
            _subtaskRelationshipService = subtaskRelationshipService;
            _roleService = roleService;
            _projectService = projectService;

            _currentUser = currentUser;

            background = new SolidColorBrush((Color)Application.Current.Resources["BackgroundColor"]);
            headerBackground = new SolidColorBrush((Color)Application.Current.Resources["HeaderBackgroundColor"]);
            primaryTaskBG = new SolidColorBrush((Color)Application.Current.Resources["PrimaryTaskBGColor"]);
            secondaryTaskBG = new SolidColorBrush((Color)Application.Current.Resources["SecondaryTaskBGColor"]);
            primaryRectBG = new SolidColorBrush((Color)Application.Current.Resources["PrimaryRectBGColor"]);
            secondaryRectBG = new SolidColorBrush((Color)Application.Current.Resources["SecondaryRectBGColor"]);
            textColor = new SolidColorBrush((Color)Application.Current.Resources["TextColor"]);
            lineColor = new SolidColorBrush((Color)Application.Current.Resources["LineColor"]);

            _childTaskGrids = new ObservableCollection<Grid>();
            _childTaskCanvasGrids = new ObservableCollection<Grid>();

            SaveSubtaskRelationshipCommand = new RelayCommand(OnSaveSubtaskRelationship);
            CancelSubtaskRelationshipAddingCommand = new RelayCommand(OnCancelSubtaskRelationshipAdding);

            AddTaskCommand = new RelayCommand(OnAddTask);
            TaskDetailsCommand = new RelayCommand<int>(OnTaskDetails);
            EditTaskCommand = new RelayCommand<int>(OnEditTask, CanEditTask);
            RemoveTaskCommand = new RelayCommand<int>(OnRemoveTask, CanRemoveTask);

            AddSubtaskCommand = new RelayCommand<int>(OnAddSubtask);
            AddSubtaskWithRelationshipCommand = new RelayCommand<int>(OnAddSubtaskWithRelationship);
            SubtaskDetailsCommand = new RelayCommand<int>(OnSubtaskDetails);

            BackToBlockListCommand = new RelayCommand(OnBackToBlockList);

            parentRectHeight = 10;
        }

        private Project project;

        public Project Project
        {
            get { return project; }
            set { project = value; }
        }

        private Block _block;

        public Block Block
        {
            get { return _block; }
            set { _block = value; }
        }


        public async void LoadTasks(Block block)
        {
            if (_roleService.HasPermissions(CurrentUser.UserRole, "Задача Создавать") || _roleService.GetRoleById(CurrentUser.UserRole).RoleName == "SuperUser")
            {
                AddTaskBtn.Visibility = Visibility.Visible;
            }
            else
            {
                AddTaskBtn.Visibility = Visibility.Collapsed;
            }


            Project = await _projectService.GetProjectByIdAsync(block.ProjectId);
            Block = block;

            Tasks = new ObservableCollection<Models.Task>(_taskService.GetTasksWithBlockId(block.Id));
            Subtasks = new ObservableCollection<Subtask>(_subtaskService.GetAllSubtasks().Where(s => Tasks.Any(t => t.Id == s.TaskId)));

            ProjectTextBlock.Text = Project.Name;
            BlockTextBlock.Text = block.Name;

            CreateMainGrid();
        }

        private void CreateMainGrid()
        {

            CreateMonthGrid();
            CreateColumn();
            //AddDates();
            AddMonths();
            CreateParentTask(sheetStackPanel, canvasStackPanel);

        } // CreateMainGrid

        private void AddDates()
        {
            DateTime firstStartDate = Tasks.OrderBy(t => t.StartDate).Select(t => t.StartDate).FirstOrDefault();
            DateTime lastEndDate = Tasks.OrderBy(t => t.EndDate).Select(t => t.EndDate).LastOrDefault();

            int lastPoint = (lastEndDate.Date - firstStartDate.Date).Days;
            lastPoint += 10;

            Style borderStyle = new Style(typeof(Border));
            borderStyle.Setters.Add(new Setter(Border.BorderBrushProperty, lineColor));
            borderStyle.Setters.Add(new Setter(Border.BorderThicknessProperty, new Thickness(1)));
            borderStyle.Setters.Add(new Setter(Border.BackgroundProperty, primaryTaskBG));
            borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(2, 2, 2, 2)));
            //borderStyle.Setters.Add(new Setter(Border.MarginProperty, new Thickness(5, 2, 0, 5)));

            for (int i = 0; i <= lastPoint + 1; i++)
            {
                Border dateBorder = new Border();
                dateBorder.Style = borderStyle;

                TextBlock dateTexBlock = new TextBlock();
                dateTexBlock.Text = firstStartDate.AddDays(i).Day.ToString();
                dateTexBlock.HorizontalAlignment = HorizontalAlignment.Center;
                dateTexBlock.VerticalAlignment = VerticalAlignment.Center;
                dateTexBlock.Foreground = textColor;
                dateTexBlock.Margin = new Thickness(10);
                dateTexBlock.Height = 30;
                dateTexBlock.Width = 15;

                dateBorder.Child = dateTexBlock;
                DateSP.Children.Add(dateBorder);

            }

        } // AddDates

        private void AddMonths()
        {
            DateTime firstStartDate = Tasks.OrderBy(t => t.StartDate).Select(t => t.StartDate).FirstOrDefault();
            DateTime lastEndDate = Tasks.OrderBy(t => t.EndDate).Select(t => t.EndDate).LastOrDefault();

            int totalMonths = ((lastEndDate.Year - firstStartDate.Year) * 12) + lastEndDate.Month - firstStartDate.Month + 2;

            Style borderStyle = new Style(typeof(Border));
            //borderStyle.Setters.Add(new Setter(Border.BorderBrushProperty, lineColor));
            borderStyle.Setters.Add(new Setter(Border.BorderThicknessProperty, new Thickness(1)));
            borderStyle.Setters.Add(new Setter(Border.BackgroundProperty, background));
            borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(2, 2, 2, 2)));

            for (int i = 0; i < totalMonths; i++)
            {
                Border dateBorder = new Border();
                dateBorder.Style = borderStyle;

                TextBlock dateTextBlock = new TextBlock();
                DateTime currentMonth = firstStartDate.AddMonths(i);
                dateTextBlock.Text = currentMonth.ToString("MMM\nyyyy"); // Display month abbreviation and year
                dateTextBlock.TextAlignment = TextAlignment.Center;
                dateTextBlock.Foreground = textColor;
                dateTextBlock.Margin = new Thickness(10);
                dateTextBlock.Height = 30;
                dateTextBlock.Width = 50;

                dateBorder.Child = dateTextBlock;
                DateSP.Children.Add(dateBorder);
            }
        }


        private void CreateColumn()
        {
            PropertyInfo[] properties = typeof(Models.Task)
            .GetProperties()
            .Where(p => p.Name == "Title"
                || p.Name == "Description"
                || p.PropertyType == typeof(DateTime)
                || p.Name == "Status"
                || p.Name == "AssignedUser")
            .ToArray();

            ObservableCollection<string> propertiesInRussian = new ObservableCollection<string>();
            propertiesInRussian.Add("Название");
            propertiesInRussian.Add("Описание");
            propertiesInRussian.Add("Начало");
            propertiesInRussian.Add("Конец");
            propertiesInRussian.Add("Статус");
            propertiesInRussian.Add("Ответсвенный мастер");

            Style borderStyle = new Style(typeof(Border));
            borderStyle.Setters.Add(new Setter(Border.BorderBrushProperty, lineColor));
            borderStyle.Setters.Add(new Setter(Border.BorderThicknessProperty, new Thickness(0)));
            borderStyle.Setters.Add(new Setter(Border.BackgroundProperty, background));
            borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(2, 2, 2, 2)));

            ColumnGrid.Resources.Add(typeof(Border), borderStyle);
            for (int i = 0; i < propertiesInRussian.Count; i++)
            {
                ColumnGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    SharedSizeGroup = properties[i].Name
                });

                Border border = new Border();
                border.SetValue(Grid.ColumnProperty, i);

                TextBlock textBlock = new TextBlock();
                textBlock.Text = propertiesInRussian[i];
                textBlock.Foreground = textColor;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Margin = new Thickness(10);
                textBlock.Height = 30;
                textBlock.FontSize = 14;
                textBlock.FontWeight = FontWeights.Bold;

                border.Child = textBlock;

                ColumnGrid.Children.Add(border);
            }

        } // CreateColumn

        //private void CreateGrid()
        //{
        //    DateTime oldestStartDate = Tasks.OrderBy(t => t.StartDate).Select(t => t.StartDate).FirstOrDefault();

        //    DateTime lastEndDate = Tasks.OrderBy(t => t.EndDate).Select(t => t.EndDate).LastOrDefault();

        //    int lastPoint = (lastEndDate.Date - oldestStartDate.Date).Days;
        //    lastPoint += 10;

        //    int width = 37;
        //    int height = 50;

        //    for (int i = 0; i <= lastPoint + 1; i++)
        //    {
        //        Line myLine = new Line();

        //        myLine.Stroke = lineColor;
        //        myLine.StrokeThickness = 1;
        //        myLine.X1 = i * width;
        //        myLine.Y1 = 0;
        //        myLine.X2 = i * width;
        //        myLine.Y2 = (Tasks.Count + Subtasks.Count) * height;

        //        MainCanvas.Children.Add(myLine);
        //    }

        //    for (int i = 0; i <= Tasks.Count + Subtasks.Count; i++)
        //    {
        //        Line horizontalLine = new Line();
        //        horizontalLine.Stroke = lineColor;
        //        horizontalLine.StrokeThickness = 1;
        //        horizontalLine.X1 = 0;
        //        horizontalLine.Y1 = i * height;
        //        horizontalLine.X2 = (lastPoint + 1) * width;
        //        horizontalLine.Y2 = i * height;

        //        MainCanvas.Children.Add(horizontalLine);
        //    }
        //} // CreateGrid

        private void CreateMonthGrid()
        {
            DateTime oldestStartDate = Tasks.OrderBy(t => t.StartDate).Select(t => t.StartDate).FirstOrDefault();
            DateTime lastEndDate = Tasks.OrderBy(t => t.EndDate).Select(t => t.EndDate).LastOrDefault();

            int monthsInRange = ((lastEndDate.Year - oldestStartDate.Year) * 12) + lastEndDate.Month - oldestStartDate.Month + 1;

            int width = 72;
            int height = 50;

            for (int i = 0; i <= monthsInRange; i++)
            {
                Line verticalLine = new Line();
                verticalLine.Stroke = lineColor;
                verticalLine.StrokeThickness = 1;
                verticalLine.X1 = i * width;
                verticalLine.Y1 = 0;
                verticalLine.X2 = i * width;
                verticalLine.Y2 = (Tasks.Count + Subtasks.Count) * height;

                MainCanvas.Children.Add(verticalLine);
            }

            for (int i = 0; i <= Tasks.Count + Subtasks.Count; i++)
            {
                Line horizontalLine = new Line();
                horizontalLine.Stroke = lineColor;
                horizontalLine.StrokeThickness = 1;
                horizontalLine.X1 = 0;
                horizontalLine.Y1 = i * height;
                horizontalLine.X2 = monthsInRange * width;
                horizontalLine.Y2 = i * height;

                MainCanvas.Children.Add(horizontalLine);
            }
        }


        private void CreateParentTask(StackPanel stackPanel, StackPanel stackPanelCanvas)
        {
            ObservableCollection<Models.Task> tasks = new ObservableCollection<Models.Task>(Tasks.OrderBy(t => t.StartDate));

            int taskCount = 0;

            PropertyInfo[] properties = typeof(Models.Task)
                .GetProperties()
                .Where(p => p.Name == "Title"
                    || p.Name == "Description"
                    || p.PropertyType == typeof(DateTime)
                    || p.Name == "Status"
                    || p.Name == "AssignedUser")
                .ToArray();

            foreach (Models.Task task in tasks)
            {
                Grid parentTaskGrid = new Grid();
                parentTaskGrid.Name = "TaskGrid" + taskCount;
                parentTaskGrid.Height = 50;

                Style borderStyle = new Style(typeof(Border));
                //borderStyle.Setters.Add(new Setter(Border.BorderBrushProperty, lineColor));
                //borderStyle.Setters.Add(new Setter(Border.BorderThicknessProperty, new Thickness(.5)));
                borderStyle.Setters.Add(new Setter(Border.BackgroundProperty, primaryTaskBG));
                borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(2, 2, 2, 2)));
                borderStyle.Setters.Add(new Setter(Border.MarginProperty, new Thickness(5,5,5,0)));

                Style textBlockStyle = new Style(typeof(TextBlock));
                textBlockStyle.Setters.Add(new Setter(TextBlock.FontWeightProperty, FontWeights.Bold));
                textBlockStyle.Setters.Add(new Setter(TextBlock.FontSizeProperty, 13.0));
                textBlockStyle.Setters.Add(new Setter(TextBlock.MarginProperty, new Thickness(5,0,5,0)));

                parentTaskGrid.Resources.Add(typeof(Border), borderStyle);
                parentTaskGrid.Resources.Add(typeof(TextBlock), textBlockStyle);

                Grid parentTaskCanvasGrid = new Grid();
                parentTaskCanvasGrid.Height = 50;
                //parentTaskCanvasGrid.Background = new BrushConverter().ConvertFrom("#141414") as Brush;

                int parentId = task.Id;
                string addButtonName = "Button" + task.Id.ToString();

                int i = 0;

                foreach (PropertyInfo prop in properties)
                {
                    parentTaskGrid.ColumnDefinitions.Add(new ColumnDefinition
                    {
                        SharedSizeGroup = prop.Name,
                        Width = new GridLength(0, GridUnitType.Auto)
                    });

                    if (prop.Name == "Title")
                    {
                        Grid parentTaskTitleGrid = new Grid();
                        parentTaskTitleGrid.ColumnDefinitions.Add(new ColumnDefinition 
                        {
                            SharedSizeGroup = "TitleButtonColumn"
                        });
                        parentTaskTitleGrid.ColumnDefinitions.Add(new ColumnDefinition());
                        parentTaskTitleGrid.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Auto);

                        StackPanel parentTaskTitleButtons = new StackPanel
                        {
                            Orientation = Orientation.Horizontal
                        };
                        Grid.SetColumn(parentTaskTitleButtons, 0);
                        parentTaskTitleGrid.Children.Add(parentTaskTitleButtons);

                        Button showButton = new Button();
                        showButton.Content = "-";
                        showButton.Name = addButtonName;
                        showButton.Click += Button_Click;
                        showButton.Foreground = textColor;
                        showButton.Width = 50;
                        showButton.HorizontalAlignment = HorizontalAlignment.Left;
                        showButton.SetValue(Grid.ColumnProperty, 0);
                        showButton.Style = (Style)FindResource("LeftRoundedButtonStyle");
                        showButton.Margin = new Thickness(0, 5, 2, 0);

                        parentTaskTitleButtons.Children.Add(showButton);

                        Button infoButton = new Button();
                        infoButton.Content = "Info";
                        infoButton.Command = TaskDetailsCommand;
                        infoButton.CommandParameter = task.Id;
                        infoButton.Foreground = textColor;
                        infoButton.Width = 30;
                        infoButton.HorizontalAlignment = HorizontalAlignment.Left;
                        infoButton.SetValue(Grid.ColumnProperty, 1);
                        infoButton.Style = (Style)FindResource("RoundedButtonStyle");
                        infoButton.Margin = new Thickness(2, 5, 2, 0);
                        parentTaskTitleButtons.Children.Add(infoButton);

                        Button addButton = new Button();
                        addButton.Content = "Add";
                        addButton.Command = AddSubtaskCommand;
                        addButton.CommandParameter = parentId;
                        addButton.Foreground = textColor;
                        addButton.Width = 30;
                        addButton.HorizontalAlignment = HorizontalAlignment.Stretch;
                        addButton.SetValue(Grid.ColumnProperty, 2);
                        addButton.Style = (Style)FindResource("RoundedButtonStyle");
                        addButton.Margin = new Thickness(2, 5, 2, 0);
                        parentTaskTitleButtons.Children.Add(addButton);

                        Border border = new Border();
                        Grid.SetColumn(border, 1);

                        TextBlock title = new TextBlock();
                        title.Text = prop.GetValue(task).ToString();
                        title.Height = 29;
                        title.HorizontalAlignment = HorizontalAlignment.Left;
                        title.Foreground = textColor;
                        title.Margin = new Thickness(45, 0, 45, 0);

                        border.Child = title;
                        parentTaskTitleGrid.Children.Add(border);
                        parentTaskGrid.Children.Add(parentTaskTitleGrid);

                    }
                    else
                    {
                        Border cellBorder = new Border();
                        cellBorder.SetValue(Grid.ColumnProperty, i);
                        cellBorder.Style = borderStyle;

                        TextBlock textBlock = new TextBlock();
                        textBlock.Name = prop.Name;

                        if (typeof(Models.Task).GetProperty(prop.Name).PropertyType == typeof(System.DateTime) || typeof(Models.Task).GetProperty(prop.Name).PropertyType == typeof(System.DateTime?))
                        {
                            if (prop.GetValue(task) != null)
                            {
                                DateTime date = (DateTime)prop.GetValue(task);
                                string formattedDate = date.ToString("dd/MMMM/yyyy");
                                textBlock.Text = formattedDate;
                            }
                        }
                        else if (prop.Name == "AssignedUser")
                        {
                            textBlock.Text = task.AssignedUser.FirstName + " " + task.AssignedUser.LastName;
                        }
                        else
                        {
                            textBlock.Text = prop.GetValue(task).ToString();
                        }

                        textBlock.Height = 29;
                        textBlock.Width = double.NaN; // Set to Auto
                        textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                        double desiredWidth = textBlock.DesiredSize.Width + 20;
                        textBlock.Width = desiredWidth;
                        textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                        textBlock.Foreground = textColor;
                        cellBorder.Child = textBlock;

                        parentTaskGrid.Children.Add(cellBorder);
                    }

                    i++;
                } //foreach


                //DrawParentRect(parentTaskCanvasGrid, task, taskCount);
                DrawParentRectMonth(parentTaskCanvasGrid, task);

                //if (task.PreviousTaskId != null)
                //{
                //    DateTime oldestStartDate = parentTasks.OrderBy(t => t.StartDate).Select(t => t.StartDate).FirstOrDefault();

                //    double taskStartPoint = (task.StartDate.Date - oldestStartDate.Date).Days + .5;

                //    Task taskToFind = parentTasks.FirstOrDefault(t => t.Id == task.PreviousTaskId);
                //    int linkedTaskIndex = parentTasks.IndexOf(taskToFind);

                //    double previousTaskEndPoint = (taskToFind.EndDate.Date - oldestStartDate.Date).Days + .5;

                //    DrawParentTaskPath(parentTaskCanvasGrid, 0, taskStartPoint * 30, 20, linkedTaskIndex * 30, previousTaskEndPoint);
                //}


                stackPanel.Children.Add(parentTaskGrid);
                stackPanelCanvas.Children.Add(parentTaskCanvasGrid);
                CreateSubtask(stackPanel, stackPanelCanvas, parentId, addButtonName);

                taskCount++;
            } //tasks foreach


        } //CreateParentTask

        Dictionary<int, Grid> subtaskGrids = new Dictionary<int, Grid>();

        private void CreateSubtask(StackPanel stackPanel, StackPanel stackPanelCanvas, int parentId, string buttonName)
        {
            ObservableCollection<Subtask> subtasks = new ObservableCollection<Subtask>(Subtasks.Where(t => t.TaskId == parentId));

            Grid childTaskGrid = new Grid();
            childTaskGrid.Name = "Grid" + buttonName;
            _childTaskGrids.Add(childTaskGrid);

            Style borderStyle = new Style(typeof(Border));
            //borderStyle.Setters.Add(new Setter(Border.BorderBrushProperty, lineColor));
            //borderStyle.Setters.Add(new Setter(Border.BorderThicknessProperty, new Thickness(.5)));
            borderStyle.Setters.Add(new Setter(Border.BackgroundProperty, secondaryTaskBG));
            borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(2, 2, 2, 2)));
            borderStyle.Setters.Add(new Setter(Border.MarginProperty, new Thickness(5, 5, 5, 0)));

            childTaskGrid.Resources.Add(typeof(Border), borderStyle);


            Grid childTaskCanvasGrid = new Grid();
            childTaskCanvasGrid.Name = "CanvasGrid" + buttonName;
            childTaskCanvasGrid.Height = subtasks.Count * 50;

            _childTaskCanvasGrids.Add(childTaskCanvasGrid);
            subtaskGrids.Add(parentId, childTaskCanvasGrid);

            Canvas canvas = new Canvas();

            childTaskCanvasGrid.Children.Add(canvas);

            PropertyInfo[] properties = typeof(Models.Subtask)
                .GetProperties()
                .Where(p => p.Name == "Title"
                    || p.Name == "Description"
                    || p.PropertyType == typeof(DateTime)
                    || p.Name == "Status"
                    || p.Name == "AssignedUser")
                .ToArray();

            int rowIndex = 0;

            foreach (Subtask subtask in subtasks)
            {
                int i = 0;

                childTaskGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });

                foreach (PropertyInfo prop in properties)
                {
                    if (rowIndex == 0)
                    {
                        childTaskGrid.ColumnDefinitions.Add(new ColumnDefinition
                        {
                            SharedSizeGroup = prop.Name,
                            Width = GridLength.Auto
                        });
                    }

                    

                    if (prop.Name == "Title")
                    {
                        Grid grid = new Grid();
                        grid.ColumnDefinitions.Add(new ColumnDefinition
                        {
                            SharedSizeGroup = "TitleButtonColumn"
                        });
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        
                        Grid.SetRow(grid, rowIndex);

                        StackPanel taskTitleButtons = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            HorizontalAlignment = HorizontalAlignment.Right
                        };

                        Grid.SetColumn(taskTitleButtons, 0);
                        grid.Children.Add(taskTitleButtons);


                        Button infoButton = new Button();
                        infoButton.Content = "Info";
                        infoButton.Command = SubtaskDetailsCommand;
                        infoButton.CommandParameter = subtask.Id;
                        infoButton.Foreground = textColor;
                        infoButton.Width = 30;
                        infoButton.HorizontalAlignment = HorizontalAlignment.Right;
                        infoButton.Margin = new Thickness(2, 5, 2, 0);
                        infoButton.Style = (Style)FindResource("LeftRoundedButtonStyle");
                        taskTitleButtons.Children.Add(infoButton);


                        Border cellBorder = new Border();
                        cellBorder.SetValue(Grid.ColumnProperty, 1);
                        grid.Children.Add(cellBorder);

                        TextBlock textBlock = new TextBlock();
                        textBlock.Name = prop.Name;
                        textBlock.Text = prop.GetValue(subtask).ToString();
                        textBlock.Height = 29;
                        textBlock.HorizontalAlignment = HorizontalAlignment.Right;
                        textBlock.Foreground = textColor;
                        textBlock.Margin = new Thickness(45, 0, 5, 0);
                        cellBorder.Child = textBlock;

                        childTaskGrid.Children.Add(grid);
                    }
                    else
                    {
                        Border cellBorder = new Border();
                        cellBorder.SetValue(Grid.ColumnProperty, i);
                        cellBorder.SetValue(Grid.RowProperty, rowIndex);

                        TextBlock textBlock = new TextBlock();
                        textBlock.Name = prop.Name;

                        if (typeof(Models.Task).GetProperty(prop.Name).PropertyType == typeof(System.DateTime) || typeof(Models.Task).GetProperty(prop.Name).PropertyType == typeof(System.DateTime?))
                        {
                            if (prop.GetValue(subtask) != null)
                            {
                                DateTime date = (DateTime)prop.GetValue(subtask);
                                string formattedDate = date.ToString("dd/MMMM/yyyy");
                                textBlock.Text = formattedDate;
                            }
                        }
                        else if (prop.Name == "AssignedUser")
                        {
                            textBlock.Text = subtask.AssignedUser.FirstName + " " + subtask.AssignedUser.LastName;
                        }
                        else
                        {
                            textBlock.Text = prop.GetValue(subtask).ToString();
                        }

                        textBlock.Height = 29;
                        textBlock.HorizontalAlignment = HorizontalAlignment.Right;
                        textBlock.Foreground = textColor;
                        cellBorder.Child = textBlock;

                        childTaskGrid.Children.Add(cellBorder);
                    }

                    i++;
                } //foreach prop

                DrawChildRect(childTaskCanvasGrid, canvas, subtask, rowIndex, subtasks.Count * 30);


                //if (subtask.IsChildSubtask() != null)
                //{
                //    DateTime oldestStartDate = Tasks.OrderBy(t => t.StartDate).Select(t => t.StartDate).FirstOrDefault();

                //    double taskStartPoint = (subtask.StartDate.Date - oldestStartDate.Date).Days + .5;

                //    foreach (SubtaskRelationship relationship in subtask.ChildSubtasks)
                //    {
                //        Subtask taskToFind = subtasks.FirstOrDefault(t => t.Id == relationship.ParentSubtaskId);
                //        int linkedTaskIndex = subtasks.IndexOf(taskToFind);

                //        double parentTaskEndPoint = (taskToFind.EndDate.Date - oldestStartDate.Date).Days + .5;

                //        DrawPath(childTaskCanvasGrid, rowIndex * 30, taskStartPoint * 30, 23, linkedTaskIndex * 30, parentTaskEndPoint, relationship);
                //    }
                //}

                if (subtask.HasChildRelationships() != null)
                {
                    DateTime oldestStartDate = Tasks.OrderBy(t => t.StartDate).Select(t => t.StartDate).FirstOrDefault();

                    double taskStartPoint = ((subtask.StartDate.Year - oldestStartDate.Year) * 12) + subtask.StartDate.Month - oldestStartDate.Month;
                    double taskEndPoint = taskStartPoint + ( ((subtask.EndDate.Year - subtask.StartDate.Year) * 12) + subtask.EndDate.Month - subtask.StartDate.Month );

                    double startPointCoefficient = 0;
                    double endPointCoefficient = 0;

                    if (subtask.StartDate.Day <= 10)
                    {
                        startPointCoefficient = .25;
                    }
                    else if (subtask.StartDate.Day > 10 && subtask.StartDate.Day <= 20)
                    {
                        startPointCoefficient = .5;
                    }
                    else
                    {
                        startPointCoefficient = .75;
                    }

                    taskStartPoint += startPointCoefficient;

                    if (subtask.EndDate.Day <= 10)
                    {
                        endPointCoefficient = .25;
                    }
                    else if (subtask.EndDate.Day > 10 && subtask.EndDate.Day <= 20)
                    {
                        endPointCoefficient = .5;
                    }
                    else
                    {
                        endPointCoefficient = .75;
                    }

                    taskEndPoint -= startPointCoefficient;
                    taskEndPoint += endPointCoefficient;

                    foreach (SubtaskRelationship relationship in subtask.ParentSubtasks)
                    {
                        Subtask taskToFind = subtasks.FirstOrDefault(t => t.Id == relationship.ChildSubtaskId);
                        int linkedTaskIndex = subtasks.IndexOf(taskToFind);

                        
                        double childTaskStartPoint = ((taskToFind.StartDate.Year - oldestStartDate.Year) * 12) + taskToFind.StartDate.Month - oldestStartDate.Month;

                        double childTaskStartPointCoefficient = 0;

                        if (subtask.StartDate.Day <= 10)
                        {
                            childTaskStartPointCoefficient = .25;
                        }
                        else if (subtask.StartDate.Day > 10 && subtask.StartDate.Day <= 20)
                        {
                            childTaskStartPointCoefficient = .5;
                        }
                        else
                        {
                            childTaskStartPointCoefficient = .75;
                        }
                        childTaskStartPoint += childTaskStartPointCoefficient;
                        DrawPath(childTaskCanvasGrid, rowIndex * 50, taskEndPoint * 72, 30, linkedTaskIndex * 72, childTaskStartPoint, relationship);
                    }
                }

                rowIndex++;
            } //foreach task

            stackPanel.Children.Add(childTaskGrid);
            stackPanelCanvas.Children.Add(childTaskCanvasGrid);
        } //CreateChildTask


        private void DrawParentRect(Grid parentGrid, Models.Task task, int taskCount)
        {
            DateTime oldestStartDate = Tasks.OrderBy(t => t.StartDate).Select(t => t.StartDate).FirstOrDefault();

            double taskStartPoint = (task.StartDate.Date - oldestStartDate.Date).Days + .5;

            int taskEndPoint = (task.EndDate.Date - task.StartDate.Date).Days;

            //DateTime lastEndDate = Tasks.OrderBy(t => t.EndDate).Select(t => t.EndDate).LastOrDefault();

            //int lastPoint = (lastEndDate.Date - oldestStartDate.Date).Days;

            #region Canvas

            //Canvas canvas = new Canvas();
            //canvas.Width = (lastPoint + 1) * 30;
            //canvas.Height = 30;
            //canvas.Background = Brushes.Transparent;
            //canvas.HorizontalAlignment = HorizontalAlignment.Left;
            //Canvas.SetTop(canvas, 0);
            //Canvas.SetLeft(canvas, 0);

            //if (taskCount == 0)
            //{
            //    Rectangle rowRectangle = new Rectangle();
            //    rowRectangle.Fill = background;
            //    rowRectangle.Width = (lastPoint + 1) * 30;
            //    rowRectangle.Height = (Tasks.Count) * 30;
            //    Canvas.SetTop(rowRectangle, 0);
            //    Canvas.SetLeft(rowRectangle, 0);

            //    canvas.Children.Add(rowRectangle);


            //    for (int i = 0; i <= lastPoint + 1; i++)
            //    {
            //        Line myLine = new Line();

            //        myLine.Stroke = lineColor;
            //        myLine.StrokeThickness = 1;
            //        myLine.X1 = i * 30;
            //        myLine.Y1 = 0;
            //        myLine.X2 = i * 30;
            //        myLine.Y2 = Tasks.Count * 30;

            //        canvas.Children.Add(myLine);

            //        Line horizontalLine = new Line();
            //        horizontalLine.Stroke = lineColor;
            //        horizontalLine.StrokeThickness = 1;
            //        horizontalLine.X1 = 0;
            //        horizontalLine.Y1 = i * 30;
            //        horizontalLine.X2 = (lastPoint + 1) * 30;
            //        horizontalLine.Y2 = i * 30;

            //        canvas.Children.Add(horizontalLine);
            //    }
            //}
            #endregion


            Rectangle rectangle = new Rectangle();
            rectangle.Fill = primaryRectBG;
            rectangle.Width = taskEndPoint * 30;
            rectangle.Height = parentRectHeight;
            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
            rectangle.VerticalAlignment = VerticalAlignment.Top;
            rectangle.Margin = new Thickness(taskStartPoint * 30, (50 - parentRectHeight) / 2, 0, 0);

            Polygon leftTriangle = new Polygon();
            leftTriangle.Fill = primaryRectBG;

            PointCollection leftPoints = new PointCollection();
            leftPoints.Add(new Point(taskStartPoint * 30, 25));
            leftPoints.Add(new Point(taskStartPoint * 30, 40));
            leftPoints.Add(new Point(taskStartPoint * 30 + 10, 25));

            leftTriangle.Points = leftPoints;

            Polygon rightTriangle = new Polygon();
            rightTriangle.Fill = primaryRectBG;

            PointCollection rightPoints = new PointCollection();
            rightPoints.Add(new Point(taskStartPoint * 30 + taskEndPoint * 30, 25));
            rightPoints.Add(new Point(taskStartPoint * 30 + taskEndPoint * 30, 40));
            rightPoints.Add(new Point(taskStartPoint * 30 + taskEndPoint * 30 - 10, 25));

            rightTriangle.Points = rightPoints;

            parentGrid.Children.Add(rectangle);
            parentGrid.Children.Add(leftTriangle);
            parentGrid.Children.Add(rightTriangle);

            //parentGrid.Children.Add(canvas);
        } // DrawParentRect


        private void DrawParentRectMonth(Grid parentGrid, Models.Task task)
        {
            DateTime oldestStartDate = Tasks.OrderBy(t => t.StartDate).Select(t => t.StartDate).FirstOrDefault();

            double taskStartPoint = ((task.StartDate.Year - oldestStartDate.Year) * 12) + task.StartDate.Month - oldestStartDate.Month; 
            //double taskStartPoint = (task.StartDate - oldestStartDate).TotalDays / 30;
            double taskEndPoint = ((task.EndDate.Year - task.StartDate.Year) * 12) + task.EndDate.Month - task.StartDate.Month;

            double startPointCoefficient = 0;
            double endPointCoefficient = 0;

            if (task.StartDate.Day <= 10)
            {
                startPointCoefficient = .25;
            }
            else if (task.StartDate.Day > 10 && task.StartDate.Day <= 20)
            {
                startPointCoefficient = .5;
            }
            else
            {
                startPointCoefficient = .75;
            }

            taskStartPoint += startPointCoefficient;

            if (task.EndDate.Day <= 10)
            {
                endPointCoefficient = .25;
            }
            else if (task.EndDate.Day > 10 && task.EndDate.Day <= 20)
            {
                endPointCoefficient = .5;
            }
            else
            {
                endPointCoefficient = .75;
            }

            taskEndPoint -= startPointCoefficient;
            taskEndPoint += endPointCoefficient;

            int width = 72;

            Rectangle rectangle = new Rectangle();
            rectangle.Fill = primaryRectBG;
            rectangle.Width = taskEndPoint * width;
            rectangle.Height = parentRectHeight;
            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
            rectangle.VerticalAlignment = VerticalAlignment.Top;
            rectangle.Margin = new Thickness(taskStartPoint * width, (50 - parentRectHeight) / 2, 0, 0);

            Polygon leftTriangle = new Polygon();
            leftTriangle.Fill = primaryRectBG;

            PointCollection leftPoints = new PointCollection();
            leftPoints.Add(new Point(taskStartPoint * width, 25));
            leftPoints.Add(new Point(taskStartPoint * width, 40));
            leftPoints.Add(new Point(taskStartPoint * width + 10, 25));

            leftTriangle.Points = leftPoints;

            Polygon rightTriangle = new Polygon();
            rightTriangle.Fill = primaryRectBG;

            PointCollection rightPoints = new PointCollection();
            rightPoints.Add(new Point(taskStartPoint * width + taskEndPoint * width, 25));
            rightPoints.Add(new Point(taskStartPoint * width + taskEndPoint * width, 40));
            rightPoints.Add(new Point(taskStartPoint * width + taskEndPoint * width - 10, 25));

            rightTriangle.Points = rightPoints;

            parentGrid.Children.Add(rectangle);
            parentGrid.Children.Add(leftTriangle);
            parentGrid.Children.Add(rightTriangle);
        }


        SubtaskRelationship subtaskRelationship;

        GanttRectangle? parentRectangle;
        GanttRectangle? childRectangle;

        private void DrawChildRect(Grid grid, Canvas canvas, Subtask subtask, int topPoint, int canvasHeight)
        {
            GanttRectangle ganttRectangle = new GanttRectangle();
            ganttRectangle.Margin = new Thickness(0, topPoint * 50, 0, 0);
            //ganttRectangle.DrawChildRect(subtask, topPoint, Tasks);
            ganttRectangle.DrawChildRectMonth(subtask, topPoint, Tasks);
            ganttRectangle.Check += CheckEventHandler;
            ganttRectangle.AddRelationshipAsParentRequested += SubtaskRelationshipParent;
            ganttRectangle.AddRelationshipAsChildRequested += SubtaskRelationshipChild;

            grid.Children.Add(ganttRectangle);
        } // DrawChildRect

        
        private void CheckEventHandler(object sender, CheckEventArgs e)
        {
            if (subtaskRelationship != null)
            {
                e.GanttRectangle.CheckEventResponse(e.Button, false);
            }
            else
            {
                e.GanttRectangle.CheckEventResponse(e.Button, true);
            }
        }

        private void SubtaskRelationshipParent(object sender, RelationshipEventArgs e)
        {
            SubtaskRelationship relationship = e.Relationship;

            if (subtaskRelationship == null)
            {
                parentRectangle = e.GanttRectangle;

                subtaskRelationship = new SubtaskRelationship();
                subtaskRelationship.Id = relationship.Id;
                subtaskRelationship.ParentSubtaskId = relationship.ParentSubtaskId;
                subtaskRelationship.RelationshipType = relationship.RelationshipType;
            }
            else
            {
                subtaskRelationship.ParentSubtaskId = relationship.ParentSubtaskId;

                if (parentRectangle != null && e.GanttRectangle != parentRectangle)
                {
                    parentRectangle.ResetRectable();
                    parentRectangle = e.GanttRectangle;
                }
                else
                {
                    parentRectangle = e.GanttRectangle;
                }
            }
            
        
            RectangleButtons.Visibility = Visibility.Visible;
        }

        private void SubtaskRelationshipChild(object sender, RelationshipEventArgs e)
        {
            SubtaskRelationship relationship = e.Relationship;
            

            if (subtaskRelationship == null)
            {
                childRectangle = e.GanttRectangle;

                subtaskRelationship = new SubtaskRelationship();
                subtaskRelationship.Id = relationship.Id;
                subtaskRelationship.ChildSubtaskId = relationship.ChildSubtaskId;
                subtaskRelationship.RelationshipType = relationship.RelationshipType;
            }
            else
            {
                subtaskRelationship.ChildSubtaskId = relationship.ChildSubtaskId;

                if (childRectangle != null && e.GanttRectangle != childRectangle)
                {
                    childRectangle.ResetRectable();
                    childRectangle = e.GanttRectangle;
                }
                else
                {
                    childRectangle = e.GanttRectangle;
                }
            }

            RectangleButtons.Visibility = Visibility.Visible;
        }

        public RelayCommand SaveSubtaskRelationshipCommand { get; set; }

        public void OnSaveSubtaskRelationship()
        {
            if (subtaskRelationship.ParentSubtaskId != null && subtaskRelationship.ChildSubtaskId != null)
            {
                _subtaskRelationshipService.CreateSubtaskRelationship(subtaskRelationship);

                RectangleButtons.Visibility = Visibility.Collapsed;

                DateTime oldestStartDate = Tasks.OrderBy(t => t.StartDate).Select(t => t.StartDate).FirstOrDefault();

                Subtask subtask = _subtaskService.GetSubtaskById(subtaskRelationship.ParentSubtaskId);

                double taskStartPoint = ((subtask.StartDate.Year - oldestStartDate.Year) * 12) + subtask.StartDate.Month - oldestStartDate.Month;
                double taskEndPoint = taskStartPoint + (((subtask.EndDate.Year - subtask.StartDate.Year) * 12) + subtask.EndDate.Month - subtask.StartDate.Month);

                double startPointCoefficient = 0;
                double endPointCoefficient = 0;

                if (subtask.StartDate.Day <= 10)
                {
                    startPointCoefficient = .25;
                }
                else if (subtask.StartDate.Day > 10 && subtask.StartDate.Day <= 20)
                {
                    startPointCoefficient = .5;
                }
                else
                {
                    startPointCoefficient = .75;
                }

                taskStartPoint += startPointCoefficient;

                if (subtask.EndDate.Day <= 10)
                {
                    endPointCoefficient = .25;
                }
                else if (subtask.EndDate.Day > 10 && subtask.EndDate.Day <= 20)
                {
                    endPointCoefficient = .5;
                }
                else
                {
                    endPointCoefficient = .75;
                }

                taskEndPoint -= startPointCoefficient;
                taskEndPoint += endPointCoefficient;


                ObservableCollection<Subtask> subtasks = new ObservableCollection<Subtask>(Subtasks.Where(t => t.TaskId == subtask.TaskId));
                
                int rowIndex = subtasks.IndexOf(subtask);

                foreach (SubtaskRelationship relationship in subtask.ParentSubtasks)
                {
                    Subtask taskToFind = subtasks.FirstOrDefault(t => t.Id == relationship.ChildSubtaskId);
                    int linkedTaskIndex = subtasks.IndexOf(taskToFind);


                    double childTaskStartPoint = ((taskToFind.StartDate.Year - oldestStartDate.Year) * 12) + taskToFind.StartDate.Month - oldestStartDate.Month;

                    double childTaskStartPointCoefficient = 0;

                    if (subtask.StartDate.Day <= 10)
                    {
                        childTaskStartPointCoefficient = .25;
                    }
                    else if (subtask.StartDate.Day > 10 && subtask.StartDate.Day <= 20)
                    {
                        childTaskStartPointCoefficient = .5;
                    }
                    else
                    {
                        childTaskStartPointCoefficient = .75;
                    }
                    childTaskStartPoint += childTaskStartPointCoefficient;

                    DrawPath(subtaskGrids.GetValueOrDefault(subtask.TaskId), rowIndex * 50, taskEndPoint * 72, 30, linkedTaskIndex * 72, childTaskStartPoint, subtaskRelationship);
                }
            }

            parentRectangle.ResetRectable();
            childRectangle.ResetRectable();

            parentRectangle = null;
            childRectangle = null;

            subtaskRelationship = null;
        }

        public RelayCommand CancelSubtaskRelationshipAddingCommand { get; set; }

        private void OnCancelSubtaskRelationshipAdding()
        {
            if (parentRectangle != null) parentRectangle.ResetRectable();
            if (childRectangle != null) childRectangle.ResetRectable();

            parentRectangle = null;
            childRectangle = null;

            subtaskRelationship = null;

            RectangleButtons.Visibility = Visibility.Collapsed;
        }

        private void DrawParentTaskPath(Grid grid, int topPoint, double leftPoint, int height, int linkedTaskIndex, double previousTaskEndPoint)
        {
            Path myPath = new Path();
            myPath.Stroke = (Brush)new BrushConverter().ConvertFrom("#AFFC41");
            myPath.StrokeThickness = 1;

            PathGeometry pathGeometry = new PathGeometry();

            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = new Point(leftPoint, topPoint + (height / 2) + 6);

            PolyLineSegment polyLineSegment = new PolyLineSegment();
            //polyLineSegment.Points.Add(new Point((previousTaskEndPoint * 30) - 10, topPoint + (height / 2) + 6));
            //polyLineSegment.Points.Add(new Point((previousTaskEndPoint * 30) - 10, linkedTaskIndex + height));

            pathFigure.Segments.Add(polyLineSegment);

            pathGeometry.Figures.Add(pathFigure);

            myPath.Data = pathGeometry;

            EllipseGeometry ellipseGeometry = new EllipseGeometry();
            ellipseGeometry.Center = new Point(leftPoint + 5, topPoint + 10);
            ellipseGeometry.RadiusX = 2;
            ellipseGeometry.RadiusY = 2;

            Path ellipsePath = new Path();
            ellipsePath.Fill = (Brush)new BrushConverter().ConvertFrom("#AFFC41");
            ellipsePath.Stroke = (Brush)new BrushConverter().ConvertFrom("#AFFC41");
            ellipsePath.StrokeThickness = 0;
            ellipsePath.Data = ellipseGeometry;

            grid.Children.Add(myPath);
            grid.Children.Add(ellipsePath);
        } // DrawParentTaskPath

        private void DrawPath(Grid grid, int topPoint, double leftPoint, int height, int linkedTaskIndex, double parentTaskEndPoint, SubtaskRelationship relationship)
        {
            Brush mainBrush = (Brush)new BrushConverter().ConvertFrom("#61E8E1");

            Path myPath = new Path();
            myPath.StrokeThickness = 2;

            PathGeometry pathGeometry = new PathGeometry();

            PathFigure pathFigure = new PathFigure();

            PolyLineSegment polyLineSegment = new PolyLineSegment();            

            pathFigure.Segments.Add(polyLineSegment);

            pathGeometry.Figures.Add(pathFigure);

            myPath.Data = pathGeometry;

            #region secondPath
            Path mySecondPath = new Path();
            mySecondPath.Stroke = (Brush)new BrushConverter().ConvertFrom("#1c1c1c");
            mySecondPath.StrokeThickness = 3;

            PathGeometry secondPathGeometry = new PathGeometry();

            PathFigure secondPathFigure = new PathFigure();
            secondPathFigure.StartPoint = new Point(leftPoint, topPoint + (height / 2) + 4);

            secondPathFigure.Segments.Add(polyLineSegment);

            secondPathGeometry.Figures.Add(pathFigure);

            mySecondPath.Data = secondPathGeometry;
            #endregion

            EllipseGeometry ellipseGeometry = new EllipseGeometry();
            ellipseGeometry.RadiusX = 3;
            ellipseGeometry.RadiusY = 3;

            Path ellipsePath = new Path();
            ellipsePath.StrokeThickness = 0;
            ellipsePath.Data = ellipseGeometry;


            EllipseGeometry secondEllipseGeometry = new EllipseGeometry();
            secondEllipseGeometry.RadiusX = 1;
            secondEllipseGeometry.RadiusY = 1;

            Path secondEllipsePath = new Path();
            secondEllipsePath.Fill = (Brush)new BrushConverter().ConvertFrom("#1c1c1c");
            secondEllipsePath.Stroke = (Brush)new BrushConverter().ConvertFrom("#1c1c1c");
            secondEllipsePath.StrokeThickness = 0;
            secondEllipsePath.Data = secondEllipseGeometry;

            int width = 72;

            if (relationship.RelationshipType == RelationshipType.FS)
            {
                int xOffsetAtParentRect = 0;
                int yOffsetFromParentRect = -15;

                pathFigure.StartPoint = new Point(leftPoint - xOffsetAtParentRect, topPoint + height);
                polyLineSegment.Points.Add(new Point(leftPoint - xOffsetAtParentRect, linkedTaskIndex + yOffsetFromParentRect));
                polyLineSegment.Points.Add(new Point(parentTaskEndPoint * width + 5, linkedTaskIndex + yOffsetFromParentRect));
                polyLineSegment.Points.Add(new Point(parentTaskEndPoint * width + 5, linkedTaskIndex + -10));

                ellipseGeometry.Center = new Point((parentTaskEndPoint * width) + 5, linkedTaskIndex + -10);
                secondEllipseGeometry.Center = new Point((parentTaskEndPoint * width) + 5, linkedTaskIndex + -10);

                myPath.Stroke = mainBrush;
                ellipsePath.Fill = mainBrush;
            }
            else if (relationship.RelationshipType == RelationshipType.FF)
            {
                int xOffsetAtParentRect = 12;
                int yOffsetFromParentRect = 0;

                pathFigure.StartPoint = new Point(leftPoint - xOffsetAtParentRect, topPoint + height);
                polyLineSegment.Points.Add(new Point(leftPoint - xOffsetAtParentRect, linkedTaskIndex + yOffsetFromParentRect));
                polyLineSegment.Points.Add(new Point(parentTaskEndPoint * width - 15, linkedTaskIndex + yOffsetFromParentRect));
                polyLineSegment.Points.Add(new Point(parentTaskEndPoint * width - 15, linkedTaskIndex + (height / 2) + 2));
                polyLineSegment.Points.Add(new Point(parentTaskEndPoint * width - 5, linkedTaskIndex + (height / 2) + 2));

                ellipseGeometry.Center = new Point((parentTaskEndPoint * width) - 5, linkedTaskIndex + (height / 2) + 2);
                secondEllipseGeometry.Center = new Point((parentTaskEndPoint * width) - 5, linkedTaskIndex + (height / 2) + 2);

                mainBrush = (Brush)new BrushConverter().ConvertFrom("#F19953");
                myPath.Stroke = mainBrush;
                ellipsePath.Fill = mainBrush;
            }

            else if (relationship.RelationshipType == RelationshipType.SF)
            {
                int xOffsetAtParentRect = 16;
                int yOffsetFromParentRect = 5;

                int xOffsetAtChildRect = 20;
                int yOffsetAtChildRect = 8;

                pathFigure.StartPoint = new Point(leftPoint - xOffsetAtParentRect, topPoint + height);
                polyLineSegment.Points.Add(new Point(leftPoint - xOffsetAtParentRect, linkedTaskIndex + yOffsetFromParentRect));
                polyLineSegment.Points.Add(new Point(parentTaskEndPoint * width - xOffsetAtChildRect, linkedTaskIndex + yOffsetFromParentRect));
                polyLineSegment.Points.Add(new Point(parentTaskEndPoint * width - xOffsetAtChildRect, linkedTaskIndex + (height / 2) + yOffsetAtChildRect));
                polyLineSegment.Points.Add(new Point(parentTaskEndPoint * width - 5, linkedTaskIndex + (height / 2) + yOffsetAtChildRect));

                ellipseGeometry.Center = new Point((parentTaskEndPoint * width) - 5, linkedTaskIndex + (height / 2) + yOffsetAtChildRect);
                secondEllipseGeometry.Center = new Point((parentTaskEndPoint * width) - 5, linkedTaskIndex + (height / 2) + yOffsetAtChildRect);

                mainBrush = (Brush)new BrushConverter().ConvertFrom("#E5625E");
                myPath.Stroke = mainBrush;
                ellipsePath.Fill = mainBrush;
            }

            else if (relationship.RelationshipType == RelationshipType.SS)
            {
                int xOffsetAtParentRect = 20;
                int yOffsetFromParentRect = 10;

                int xOffsetAtChildRect = 26;
                int yOffsetAtChildRect = 22;



                pathFigure.StartPoint = new Point(leftPoint - xOffsetAtParentRect, topPoint + height);
                polyLineSegment.Points.Add(new Point(leftPoint - xOffsetAtParentRect, linkedTaskIndex + yOffsetFromParentRect));
                polyLineSegment.Points.Add(new Point(parentTaskEndPoint * width - xOffsetAtChildRect, linkedTaskIndex + yOffsetFromParentRect));
                polyLineSegment.Points.Add(new Point(parentTaskEndPoint * width - xOffsetAtChildRect, linkedTaskIndex + (height / 2) + yOffsetAtChildRect));
                polyLineSegment.Points.Add(new Point(parentTaskEndPoint * width + 5, linkedTaskIndex + (height / 2) + yOffsetAtChildRect));
                polyLineSegment.Points.Add(new Point(parentTaskEndPoint * width + 5, linkedTaskIndex + (height / 2) + 18));

                ellipseGeometry.Center = new Point((parentTaskEndPoint * width) + 5, linkedTaskIndex + (height / 2) + 18);
                secondEllipseGeometry.Center = new Point((parentTaskEndPoint * width) + 5, linkedTaskIndex + (height / 2) + 18);

                mainBrush = (Brush)new BrushConverter().ConvertFrom("#F0EC57");
                myPath.Stroke = mainBrush;
                ellipsePath.Fill = mainBrush;
            }

            grid.Children.Add(myPath);
            //grid.Children.Add(mySecondPath);
            grid.Children.Add(ellipsePath);
            grid.Children.Add(secondEllipsePath);
        } // DrawPath

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string gridName = "Grid" + button.Name;
            string canvasGridName = "CanvasGrid" + button.Name;

            Grid desiredGrid = _childTaskGrids.FirstOrDefault(grid => grid.Name == gridName);
            Grid desiredCanvasGrid = _childTaskCanvasGrids.FirstOrDefault(grid => grid.Name == canvasGridName);


            // Check if the desired grid was found
            if (desiredGrid != null)
            {
                if (desiredGrid.Visibility == Visibility.Visible)
                {
                    desiredGrid.Visibility = Visibility.Collapsed;
                    desiredCanvasGrid.Visibility = Visibility.Collapsed;
                    button.Content = "+";
                }
                else
                {
                    desiredGrid.Visibility = Visibility.Visible;
                    desiredCanvasGrid.Visibility = Visibility.Visible;
                    button.Content = "-";
                }
            }
            else
            {
                Console.WriteLine("Grid with the specified name was not found.");
            }
        } // Button_Click

        private void sheetSV_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0)
            {
                columnSV.ScrollToHorizontalOffset(sheetSV.HorizontalOffset);
            }
        }


        private void canvasSV_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange != 0)
            {
                sheetSV.ScrollToVerticalOffset(canvasSV.VerticalOffset);
            }

            if (e.HorizontalChange != 0)
            {
                dateSV.ScrollToHorizontalOffset(canvasSV.HorizontalOffset);
            }
        }

        public RelayCommand AddTaskCommand { get; private set; }
        public RelayCommand<int> TaskDetailsCommand { get; private set; }
        public RelayCommand<int> EditTaskCommand { get; private set; }
        public RelayCommand<int> RemoveTaskCommand { get; private set; }

        public event Action<Models.Task> AddTaskRequested = delegate { };
        public event Action<Models.Task> TaskDetailsRequested = delegate { };
        public event Action<Models.Task> EditTaskRequested = delegate { };

        private void OnAddTask()
        {
            AddTaskRequested(new Models.Task() { ProjectId = project.Id, BlockId = Block.Id, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(1) });
        }

        private void OnTaskDetails(int taskId)
        {
            Models.Task task = Tasks.FirstOrDefault(t => t.Id == taskId);
            TaskDetailsRequested(task);
        }

        private void OnEditTask(int taskId)
        {
            Models.Task task = Tasks.Where(t => t.Id == taskId).FirstOrDefault();
            Debug.WriteLine(task);
            EditTaskRequested(task);
        }

        private bool CanEditTask(int taskId)
        {
            return true;
        }

        private void OnRemoveTask(int taskId)
        {
            Debug.WriteLine(taskId);
            //_taskRepo.DeleteTask(SelectedTask.Id);
            //Tasks.Remove(SelectedTask);
            //DrawTasksOnCanvas();
        }

        private bool CanRemoveTask(int taskId)
        {
            return true;
        }


        public RelayCommand<int> AddSubtaskCommand { get; private set; }
        public RelayCommand<int> AddSubtaskWithRelationshipCommand { get; private set; }
        public RelayCommand<int> SubtaskDetailsCommand { get; private set; }

        public event Action<Subtask> AddChildTaskRequested = delegate { };
        public event Action<Subtask> AddSubtaskWithRelationshipRequested = delegate { };
        public event Action<Subtask> SubtaskDetailsRequested = delegate { };


        private void OnAddSubtask(int parentTaskId)
        {
            Models.Task parentTask = Tasks.FirstOrDefault(t => t.Id == parentTaskId);

            AddChildTaskRequested(new Subtask()
            {
                TaskId = parentTaskId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            });
        }

        private void OnAddSubtaskWithRelationship(int parentSubtaskId)
        {
            Models.Subtask parentSubtask = Subtasks.FirstOrDefault(t => t.Id == parentSubtaskId);

            AddSubtaskWithRelationshipRequested(new Subtask()
            {
                TaskId = parentSubtaskId,
                StartDate = parentSubtask.EndDate,
                EndDate = parentSubtask.EndDate.AddDays(1)
            });
        }

        private void OnSubtaskDetails(int subtaskId)
        {
            Subtask subtask = Subtasks.FirstOrDefault(s => s.Id == subtaskId);
            SubtaskDetailsRequested(subtask);
        }

        public RelayCommand BackToBlockListCommand { get; set; }
        public event Action<Project> BackToBlockListRequested = delegate { };

        private void OnBackToBlockList()
        {
            BackToBlockListRequested(Project);
        }
    }
}
