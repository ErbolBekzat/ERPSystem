using ERPSystem.ImageManagement;
using ERPSystem.Models;
using ERPSystem.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ERPSystem.Views.ProjectView
{
    public class ProjectListViewModel : BindableBase
    {
        IProjectService _projectService;
        ITaskService _taskService;

        Grid mainGrid;

        ObservableCollection<Project> Projects;

        public string TestString = "Test";


        public ProjectListViewModel(IProjectService projectService, ITaskService taskService, Grid mainGrid)
        {
            this.mainGrid = mainGrid;

            _projectService = projectService;
            _taskService = taskService;

            AddProjectCommand = new RelayCommand(OnAddProject);
            NavToStockCommand = new RelayCommand<Project>(OnNavToStock);
            NavToTasksCommand = new RelayCommand<Project>(OnNavToTasks);
        }


        public async void DisplayProjects()
        {
            mainGrid.Children.Clear();
            mainGrid.RowDefinitions.Clear();

            Projects = new ObservableCollection<Project>(await _projectService.GetAllProjectsAsync());

            int i = 0;
            int rowIndex = 0;

            foreach (Project project in Projects)
            {
                if (i == 0)
                {
                    mainGrid.RowDefinitions.Add(new RowDefinition());
                }

                ProjectBriefInfo projectBriefInfo = new ProjectBriefInfo(project, NavToStockCommand, NavToTasksCommand, _taskService);
                Grid.SetColumn(projectBriefInfo, i);
                Grid.SetRow(projectBriefInfo, rowIndex);
                mainGrid.Children.Add(projectBriefInfo);


                i++;

                if (i > 3)
                {
                    i = 0;
                    rowIndex++;
                }
            }
        }

        public RelayCommand<Project> NavToStockCommand { get; set; }

        public event Action<Project> NavToStockRequested = delegate { };

        public void OnNavToStock(Project project)
        {
            NavToStockRequested(project);
        }

        public RelayCommand<Project> NavToTasksCommand { get; set; }

        public event Action<Project> NavToTasksRequested = delegate { };

        public void OnNavToTasks(Project project)
        {
           NavToTasksRequested(project);
        }

        public RelayCommand AddProjectCommand { get; set; }

        public event Action<Project> AddProjectRequested;

        public void OnAddProject()
        {
            AddProjectRequested(new Project());
        }
	}
}
