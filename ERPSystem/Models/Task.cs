using System;
using System.Collections.Generic;

namespace ERPSystem.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public TaskStatus Status { get; set; }
        public int AssignedUserId { get; set; }
        public virtual User AssignedUser { get; set; }
        public virtual ICollection<Material>? Materials { get; set; } = new List<Material>(); // Collection navigation property for materials
        public int ProjectId { get; set; } // Foreign key for the project
        public virtual Project Project { get; set; } // Reference navigation property for the project
        public int BlockId { get; set; }
        public virtual ICollection<Subtask>? Subtasks { get; set; } = new List<Subtask>(); // Collection navigation property for subtasks

        public ICollection<TaskRelationship> ParentTasks { get; set; } // Collection of parent tasks
        public ICollection<TaskRelationship> ChildTasks { get; set; } // Collection of child tasks

        public ICollection<StockMovement> StockMovements { get; set; }
    }

    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Completed
    }
}
