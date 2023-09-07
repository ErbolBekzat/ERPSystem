using System;
using System.Collections.Generic;

namespace ERPSystem.Models
{
    public class Project : BindableBase
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string? description;
        public string? Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        private float version;
        public float Version
        {
            get { return version; }
            set { SetProperty(ref version, value); }
        }

        private ICollection<Task> tasks;
        public ICollection<Task> Tasks
        {
            get { return tasks; }
            set { SetProperty(ref tasks, value); }
        }

        private ICollection<Subtask> subtasks;
        public ICollection<Subtask> Subtasks
        {
            get { return subtasks; }
            set { SetProperty(ref subtasks, value); }
        }

        private string? imageLink;
        public string? ImageLink
        {
            get { return imageLink; }
            set { SetProperty(ref imageLink, value); }
        }

        private ProjectPublishedStatus publishedStatus;
        public ProjectPublishedStatus PublishedStatus
        {
            get { return publishedStatus; }
            set { SetProperty(ref publishedStatus, value); }
        }

        private TaskStatus status;
        public TaskStatus Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }

        private DateTime? startDate;
        public DateTime? StartDate
        {
            get { return startDate; }
            set { SetProperty(ref startDate, value); }
        }

        private DateTime? endDate;
        public DateTime? EndDate
        {
            get { return endDate; }
            set { SetProperty(ref endDate, value); }
        }

        private DateTime? completedDate;
        public DateTime? CompletedDate
        {
            get { return completedDate; }
            set { SetProperty(ref completedDate, value); }
        }

        private DateTime? updateDate;
        public DateTime? UpdateDate
        {
            get { return updateDate; }
            set { SetProperty(ref updateDate, value); }
        }

        private Stock stock;
        public Stock Stock
        {
            get { return stock; }
            set { SetProperty(ref stock, value); }
        }
    }


    public enum ProjectPublishedStatus
    {
        Published,
        NotPublished
    }
}
