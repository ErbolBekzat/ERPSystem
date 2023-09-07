using ERPSystem.Models;
using ERPSystem.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Views.ProjectView
{
    public class ProjectBriefInfoViewModel : BindableBase
    {
        ITaskService _taskService;

        private Project project;

        public Project Project
        {
            get { return project; }
            set { SetProperty(ref project, value); }
        }

        private ObservableCollection<Models.Task> _projectTasks;

        public ObservableCollection<Models.Task> ProjectTasks
        {
            get { return _projectTasks; }
            set { SetProperty(ref _projectTasks, value); }
        }

        private ObservableCollection<Models.Subtask> _projectSubtasks;

        public ObservableCollection<Models.Subtask> ProjectSubtasks
        {
            get { return _projectSubtasks; }
            set { SetProperty(ref _projectSubtasks, value); }
        }

        #region Counts

        private int _projectTasksCount;

        public int ProjectTasksCount
        {
            get { return _projectTasksCount; }
            set { SetProperty(ref _projectTasksCount, value); }
        }

        private int _projectNotStartedTasksCount;

        public int ProjectNotStartedTasksCount
        {
            get { return _projectNotStartedTasksCount; }
            set { SetProperty(ref _projectNotStartedTasksCount, value); }
        }

        private int _projectInProgressTasksCount;

        public int ProjectInProgressTasksCount
        {
            get { return _projectInProgressTasksCount; }
            set { SetProperty(ref _projectInProgressTasksCount, value); }
        }

        private int _projectCompletedTasksCount;

        public int ProjectCompletedTasksCount
        {
            get { return _projectCompletedTasksCount; }
            set { SetProperty(ref _projectCompletedTasksCount, value); }
        }

        private int _projectSubtasksCount;

        public int ProjectSubtasksCount
        {
            get { return _projectSubtasksCount; }
            set { SetProperty(ref _projectSubtasksCount, value); }
        }

        private int _projectNotStartedSubtasksCount;

        public int ProjectNotStartedSubtasksCount
        {
            get { return _projectNotStartedSubtasksCount; }
            set { SetProperty(ref _projectNotStartedSubtasksCount, value); }
        }

        private int _projectInProgressSubtasksCount;

        public int ProjectInProgressSubtasksCount
        {
            get { return _projectInProgressSubtasksCount; }
            set { SetProperty(ref _projectInProgressSubtasksCount, value); }
        }

        private int _projectCompletedSubtasksCount;

        public int ProjectCompletedSubtasksCount
        {
            get { return _projectCompletedSubtasksCount; }
            set { SetProperty(ref _projectCompletedSubtasksCount, value); }
        }


        #endregion

        #region Percents

        private float _projectNotStartedTasksCountPercent;

        public float ProjectNotStartedTasksCountPercent
        {
            get { return _projectNotStartedTasksCountPercent; }
            set { SetProperty(ref _projectNotStartedTasksCountPercent, value); }
        }

        private float _projectInProgressTasksCountPercent;

        public float ProjectInProgressTasksCountPercent
        {
            get { return _projectInProgressTasksCountPercent; }
            set { SetProperty(ref _projectInProgressTasksCountPercent, value); }
        }

        private float _projectCompletedTasksCountPercent;

        public float ProjectCompletedTasksCountPercent
        {
            get { return _projectCompletedTasksCountPercent; }
            set { SetProperty(ref _projectCompletedTasksCountPercent, value); }
        }

        private float _projectNotStartedSubtasksCountPercent;

        public float ProjectNotStartedSubtasksCountPercent
        {
            get { return _projectNotStartedSubtasksCountPercent; }
            set { SetProperty(ref _projectNotStartedSubtasksCountPercent, value); }
        }

        private float _projectInProgressSubtasksCountPercent;

        public float ProjectInProgressSubtasksCountPercent
        {
            get { return _projectInProgressSubtasksCountPercent; }
            set { SetProperty(ref _projectInProgressSubtasksCountPercent, value); }
        }

        private float _projectCompletedSubtasksCountPercent;

        public float ProjectCompletedSubtasksCountPercent
        {
            get { return _projectCompletedSubtasksCountPercent; }
            set { SetProperty(ref _projectCompletedSubtasksCountPercent, value); }
        }

        #endregion

        public ProjectBriefInfoViewModel(Project project, ITaskService taskService)
        {
            Project = project;
            _taskService = taskService;

            ProjectTasks = new ObservableCollection<Models.Task>(_taskService.GetTasksWithProjectId(Project.Id));
            ProjectSubtasks = new ObservableCollection<Subtask>( ProjectTasks
                              .SelectMany(task => task.Subtasks)
                              .ToList());

            Calculation();
        }

        private void Calculation()
        {
            ProjectTasksCount = ProjectTasks.Count;

            ProjectNotStartedTasksCount = ProjectTasks.Count(task => task.Status == Models.TaskStatus.NotStarted);
            ProjectInProgressTasksCount = ProjectTasks.Count(task => task.Status == Models.TaskStatus.InProgress);
            ProjectCompletedTasksCount = ProjectTasks.Count(task => task.Status == Models.TaskStatus.Completed);

            ProjectSubtasksCount = ProjectSubtasks.Count;

            ProjectNotStartedSubtasksCount = ProjectSubtasks.Count(subtask => subtask.Status == Models.TaskStatus.NotStarted);
            ProjectInProgressSubtasksCount = ProjectSubtasks.Count(subtask => subtask.Status == Models.TaskStatus.InProgress);
            ProjectCompletedSubtasksCount = ProjectSubtasks.Count(subtask => subtask.Status == Models.TaskStatus.Completed);

            ProjectNotStartedTasksCountPercent = ((float)ProjectNotStartedTasksCount / ProjectTasksCount) * 140;
            ProjectInProgressTasksCountPercent = ((float)ProjectInProgressTasksCount / ProjectTasksCount) * 140;
            ProjectCompletedTasksCountPercent = ((float)ProjectCompletedTasksCount / ProjectTasksCount) * 140;

            ProjectNotStartedSubtasksCountPercent = ((float)ProjectNotStartedSubtasksCount / ProjectSubtasksCount) * 140;
            ProjectInProgressSubtasksCountPercent = ((float)ProjectInProgressSubtasksCount / ProjectSubtasksCount) * 140;
            ProjectCompletedSubtasksCountPercent = ((float)ProjectCompletedSubtasksCount / ProjectSubtasksCount) * 140;

        }

    }
}
