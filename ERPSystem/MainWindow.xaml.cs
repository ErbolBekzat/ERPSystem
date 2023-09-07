using ERPSystem.Models;
using ERPSystem.Views.BlockView;
using ERPSystem.Views.BlockWorkforceSummaryView;
using ERPSystem.Views.MaterialView;
using ERPSystem.Views.ProblemView;
using ERPSystem.Views.ProjectView;
using ERPSystem.Views.RoleView;
using ERPSystem.Views.SubtaskView;
using ERPSystem.Views.TaskView;
using ERPSystem.Views.UserView;
using ERPSystem.Views.WorkerView;
using Microsoft.Practices.Unity;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace ERPSystem
{
    public partial class MainWindow : Window
    {
        private WindowState _previousWindowState;

        private LoginView loginView;
        private AddSuperUserView addSuperUserView;

        private UserListView userListView;
        private AddEditUserView addEditUserView;
        private UserInfoView userInfoView;

        private RoleListView roleListView;
        private RoleInfoView roleInfoView;

        private ProjectListView projectListView;
        private AddEditProjectView addEditProjectView;

        private BlockListView blockListView;
        private BlockInfoView blockInfoView;
        
        private BlockWorkforceSummaryReportView blockWorkforceSummaryReportView;

        private TaskGanttView taskGanttView;
        private AddEditTaskView addEditTaskView;
        private TaskDetailsView taskDetailsView;

        private AddEditSubtaskView addEditSubtaskView;
        private SubtaskDetailsView subtaskDetailsView;

        private AddEditProblemView addEditProblemView;

        private MaterialListView materialListView;
        //private AddEditMaterialView addEditMaterialView;

        private WorkerListView workerListView;
        private WorkerInfoView workerInfoView;


        private AddEditRoleView addEditRoleView;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
            DataContext = this;

            NavCommand = new RelayCommand<string>(Navigate);

            addSuperUserView = ContainerHelper.Container.Resolve<AddSuperUserView>();
            loginView = ContainerHelper.Container.Resolve<LoginView>();

            if (addSuperUserView.CheckIfUserExist() == false)
            {
                NavigationGrid.Visibility = Visibility.Collapsed;
                MainContent.NavigationService.Navigate(addSuperUserView);
            }
            else
            {
                NavigationGrid.Visibility = Visibility.Collapsed;
                MainContent.NavigationService.Navigate(loginView);
            }

            addSuperUserView.Done += NavToLoginPage;
            loginView.ViewModel.Done += InitializeViews;

            //InitializeViews();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = _previousWindowState;
            }
            else
            {
                _previousWindowState = WindowState;
                WindowState = WindowState.Maximized;
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void InitializeViews()
        {
            userListView = ContainerHelper.Container.Resolve<UserListView>();
            addEditUserView = ContainerHelper.Container.Resolve<AddEditUserView>();
            userInfoView = ContainerHelper.Container. Resolve<UserInfoView>();

            roleListView = ContainerHelper.Container.Resolve<RoleListView>();
            roleInfoView = ContainerHelper.Container.Resolve<RoleInfoView>();

            projectListView = ContainerHelper.Container.Resolve<ProjectListView>();
            addEditProjectView = ContainerHelper.Container.Resolve<AddEditProjectView>();

            blockListView = ContainerHelper.Container.Resolve<BlockListView>();
            blockInfoView = ContainerHelper.Container.Resolve<BlockInfoView>();

            blockWorkforceSummaryReportView = ContainerHelper.Container.Resolve<BlockWorkforceSummaryReportView>();

            addEditTaskView = ContainerHelper.Container.Resolve<AddEditTaskView>();
            taskDetailsView = ContainerHelper.Container.Resolve<TaskDetailsView>();

            addEditSubtaskView = ContainerHelper.Container.Resolve<AddEditSubtaskView>();
            subtaskDetailsView = ContainerHelper.Container.Resolve<SubtaskDetailsView>();

            addEditProblemView = ContainerHelper.Container.Resolve<AddEditProblemView>();

            materialListView = ContainerHelper.Container.Resolve<MaterialListView>();
            //addEditMaterialView = ContainerHelper.Container.Resolve<AddEditMaterialView>();

            addEditRoleView = ContainerHelper.Container.Resolve<AddEditRoleView>();

            workerListView = ContainerHelper.Container.Resolve<WorkerListView>();
            workerInfoView = ContainerHelper.Container.Resolve<WorkerInfoView>();

            userListView.ViewModel.AddUserRequested += NavToAddUser;
            userListView.ViewModel.AddRoleRequested += NavToAddRole;
            userListView.ViewModel.ShowRolesRequested += NavToRolesList;
            userListView.ViewModel.UserInfoRequested += NavToUserInfo;

            roleListView.ViewModel.RoleInfoRequested += NavToRoleInfo;

            roleInfoView.ViewModel.EditRoleRequested += NavToEditRole;
            roleInfoView.ViewModel.Done += NavToRolesList;

            projectListView.ViewModel.AddProjectRequested += NavToAddProject;
            projectListView.ViewModel.NavToStockRequested += NavToMaterialListView;
            projectListView.ViewModel.NavToTasksRequested += NavToBlockListView;
            addEditProjectView.ViewModel.Done += NavToProjectListView;

            blockListView.ViewModel.NavToBlockTasksRequested += NavToTaskGanttView;
            blockListView.ViewModel.BlockInfoRequested += NavToBlockInfo;
            blockListView.ViewModel.ShowReportRequested += NavToBlockWorkforceSummaryReportView;

            blockInfoView.ViewModel.Done += NavToBlockListView;

            

            addEditUserView.ViewModel.Done += NavToUserListView;
            userInfoView.ViewModel.EditUserRequested += NavToEditUser;
            
            addEditRoleView.ViewModel.Done += NavToUserListView;

            addEditTaskView.ViewModel.Done += NavToTaskGanttView;

            taskDetailsView.ViewModel.BackRequest += NavToTaskGanttView;
            taskDetailsView.ViewModel.EditTaskRequest += NavToEditTask;


            addEditSubtaskView.ViewModel.Done += NavToTaskGanttView;

            subtaskDetailsView.ViewModel.BackRequested += NavToTaskGanttView;
            subtaskDetailsView.ViewModel.EditSubtaskRequested += NavToEditSubtask;
            subtaskDetailsView.ViewModel.AddNewProblemRequested += NavToAddNewProblem;

            addEditProblemView.ViewModel.BackToBlockPlanRequested += NavToTaskGanttView;

            //materialListView.ViewModel.AddMaterialRequested += NavToAddMaterial;

            //addEditMaterialView.ViewModel.Done += NavToMaterialListView;

            addEditRoleView.ViewModel.Done += NavToUserListView;

            workerListView.ViewModel.WorkerInfoRequested += NavToWorkerInfo;

            NavToProjectListView();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            Left = (screenWidth - Width) / 2;
            Top = (screenHeight - Height) / 2;
        }

        public RelayCommand<string> NavCommand { get; private set; }

        private void Navigate(string destination)
        {
            NavigationGrid.Visibility = Visibility.Visible;

            switch (destination)
            {
                case "UserListView":
                    userListView.ViewModel.LoadUsers();
                    Navigator.Margin = new Thickness(0,100,0,0);
                    MainContent.NavigationService.Navigate(userListView);
                    break;
                case "WorkerListView":
                    workerListView.ViewModel.LoadWorkers();
                    Navigator.Margin = new Thickness(0, 180, 0, 0);
                    MainContent.NavigationService.Navigate(workerListView);
                    break;
                case "ProjectListView":
                default:
                    Navigator.Margin = new Thickness(0, 140, 0, 0);
                    MainContent.NavigationService.Navigate(projectListView);
                    break;
            }
        }

        private void NavToUserListView()
        {
            Navigate("UserListView");
        }

        private void NavToAddUser(User user)
        {
            addEditUserView.ViewModel.LoadRoles();
            addEditUserView.ViewModel.EditMode = false;
            addEditUserView.ViewModel.SetUser(user);
            MainContent.NavigationService.Navigate(addEditUserView);
        }

        private void NavToEditUser(User user)
        {
            addEditUserView.ViewModel.LoadRoles();
            addEditUserView.ViewModel.EditMode = true;
            addEditUserView.ViewModel.SetUser(user);
            MainContent.NavigationService.Navigate(addEditUserView);
        }

        private void NavToUserInfo(User user)
        {
            userInfoView.ViewModel.SetUser(user);
            MainContent.NavigationService.Navigate(userInfoView);
        }

        private void NavToAddRole(Role role)
        {
            addEditRoleView.ViewModel.EditMode = false;
            addEditRoleView.ViewModel.SetRole(role);

            MainContent.NavigationService.Navigate(addEditRoleView);
        }

        private void NavToEditRole(Role role)
        {
            addEditRoleView.ViewModel.EditMode = true;
            addEditRoleView.ViewModel.SetRole(role);

            MainContent.NavigationService.Navigate(addEditRoleView);
        }

        private void NavToRolesList()
        {
            roleListView.ViewModel.LoadRoles();
            MainContent.NavigationService.Navigate(roleListView);
        }

        private void NavToRoleInfo(Role role)
        {
            roleInfoView.ViewModel.SetRole(role);
            MainContent.NavigationService.Navigate(roleInfoView);
        }

        private void NavToLoginPage()
        {
            MainContent.NavigationService.Navigate(loginView);
            NavigationGrid.Visibility = Visibility.Collapsed;
        }

        private void NavToProjectListView()
        {
            Navigate("ProjectListView");
        }

        private void NavToAddProject(Project project)
        {
            addEditProjectView.ViewModel.EditMode = false;
            addEditProjectView.ViewModel.SetProject(project);
            MainContent.NavigationService.Navigate(addEditProjectView);
        }

        private void NavToBlockListView(Project project)
        {
            blockListView.ViewModel.DisplayBlocks(project);
            MainContent.NavigationService.Navigate(blockListView);
        }

        private void NavToBlockInfo(Block block)
        {
            blockInfoView.ViewModel.SetBlock(block);
            MainContent.NavigationService.Navigate(blockInfoView);
        }

        private void NavToBlockWorkforceSummaryReportView()
        {
            blockWorkforceSummaryReportView.ViewModel.LoadBWS();
            MainContent.NavigationService.Navigate(blockWorkforceSummaryReportView);
        }

        private void NavToAddTask(Task task)
        {
            addEditTaskView.ViewModel.EditMode = false;
            addEditTaskView.ViewModel.SetTask(task);
            MainContent.NavigationService.Navigate(addEditTaskView);
        }

        private void NavToEditTask(Task task)
        {
            addEditTaskView.ViewModel.EditMode = true;
            addEditTaskView.ViewModel.SetTask(task);
            MainContent.NavigationService.Navigate(addEditTaskView);
        }

        private void NavToTaskDetails(Task task)
        {
            taskDetailsView.ViewModel.SetTask(task);
            
            MainContent.NavigationService.Navigate(taskDetailsView);
        }

        private void NavToMaterialListView(Project project)
        {
            materialListView.ViewModel.LoadMaterials(project);
            MainContent.NavigationService.Navigate(materialListView);
        }


        private void NavToTaskGanttView(Block block)
        {
            taskGanttView = ContainerHelper.Container.Resolve<TaskGanttView>();
            taskGanttView.LoadTasks(block);
            taskGanttView.AddTaskRequested += NavToAddTask;
            taskGanttView.TaskDetailsRequested += NavToTaskDetails;
            taskGanttView.SubtaskDetailsRequested += NavToSubtaskDetails;
            taskGanttView.BackToBlockListRequested += NavToBlockListView;

            taskGanttView.AddChildTaskRequested += NavToAddSubtask;

            MainContent.NavigationService.Navigate(taskGanttView);
        }

        //private void NavToAddMaterial(Material material)
        //{
        //    addEditMaterialView.ViewModel.EditMode = false;
        //    addEditMaterialView.ViewModel.SetMaterial(material);
        //    MainContent.NavigationService.Navigate(addEditMaterialView);
        //}

        private void NavToAddSubtask(Subtask subtask)
        {
            addEditSubtaskView.ViewModel.EditMode = false;
            addEditSubtaskView.ViewModel.SetSubtask(subtask);
            MainContent.NavigationService.Navigate(addEditSubtaskView);
        }

        private void NavToEditSubtask(Subtask subtask)
        {
            addEditSubtaskView.ViewModel.EditMode = true;
            addEditSubtaskView.ViewModel.SetSubtask(subtask);
            MainContent.NavigationService.Navigate(addEditSubtaskView);
        }

        private void NavToSubtaskDetails(Subtask subtask)
        {
            subtaskDetailsView.ViewModel.SetSubtask(subtask);

            MainContent.NavigationService.Navigate(subtaskDetailsView);
        }

        private void NavToAddNewProblem(Subtask subtask)
        {
            addEditProblemView.ViewModel.EditMode = false;
            addEditProblemView.ViewModel.SetProblem(subtask);

            MainContent.NavigationService.Navigate(addEditProblemView);
        }

        private void NavToWorkerInfo(Worker worker)
        {
            workerInfoView.ViewModel.SetWorker(worker);
            MainContent.NavigationService.Navigate(workerInfoView);
        }
        
    }
}
