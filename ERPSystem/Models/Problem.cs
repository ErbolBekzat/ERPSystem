using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Models
{

    public class Problem : BindableBase
    {
        private int _id;
        private string _title;
        private string _description;
        private string? _image;
        private TaskStatus _problemStatus;
        private int _subtaskId;
        private Subtask _subtask;
        private int _assignedUserId;
        private User _assignedUser;

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public string? Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        public TaskStatus ProblemStatus
        {
            get { return _problemStatus; }
            set { SetProperty(ref _problemStatus, value); }
        }

        public int SubtaskId
        {
            get { return _subtaskId; }
            set { SetProperty(ref _subtaskId, value); }
        }

        public Subtask Subtask
        {
            get { return _subtask; }
            set { SetProperty(ref _subtask, value); }
        }

        public int AssignedUserId
        {
            get { return _assignedUserId; }
            set { SetProperty(ref _assignedUserId, value); }
        }

        public User AssignedUser
        {
            get { return _assignedUser; }
            set { SetProperty(ref _assignedUser, value); }
        }
    }
}
