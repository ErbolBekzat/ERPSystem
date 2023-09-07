using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Models
{
    public class SimpleEditableSubtask : BindableBase
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

        private int _taskId;
        public int TaskId
        {
            get { return _taskId; }
            set { SetProperty(ref _taskId, value); }
        }

        private Task _task;
        public Task Task
        {
            get { return _task; }
            set { SetProperty(ref _task, value); }
        }

        public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
        public virtual ICollection<SubtaskRelationship> ParentSubtasks { get; set; }
        public virtual ICollection<SubtaskRelationship> ChildSubtasks { get; set; }
    }

}
