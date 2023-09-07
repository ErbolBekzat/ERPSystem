using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERPSystem.Views.TaskView
{
    public class SimpleEditableTask : BindableBase
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _title;
        [Required]
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _description;
        [Required]
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private DateTime _startDate;
        [Required]
        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value); }
        }

        private DateTime _endDate;
        [Required]
        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value); }
        }

        private DateTime? _completedDate;
        public DateTime? CompletedDate
        {
            get { return _completedDate; }
            set { SetProperty(ref _completedDate, value); }
        }

        private TaskStatus _status;
        public TaskStatus Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        private int _assignedUserId;
        [Required]
        public int AssignedUserId
        {
            get { return _assignedUserId; }
            set { SetProperty(ref _assignedUserId, value); }
        }

        private User _assignedUser;
        public User AssignedUser
        {
            get { return _assignedUser; }
            set { SetProperty(ref _assignedUser, value); }
        }

        private int _projectId;
        public int ProjectId
        {
            get { return _projectId; }
            set { SetProperty(ref _projectId, value); }
        }

        private Project _project;
        public Project Project
        {
            get { return _project; }
            set { SetProperty(ref _project, value); }
        }

        private int _blockId;
        public int BlockId
        {
            get { return _blockId; }
            set { SetProperty(ref _blockId, value); }
        }

        public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
        public virtual ICollection<Subtask> Subtasks { get; set; } = new List<Subtask>();
        public virtual ICollection<TaskRelationship> ParentTasks { get; set; }
        public virtual ICollection<TaskRelationship> ChildTasks { get; set; }
    }

}
