using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPSystem.Models
{
    public class Subtask
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
        public int TaskId { get; set; } // Foreign key for the task
        public virtual Task Task { get; set; } // Reference navigation property for the task

        public ICollection<SubtaskRelationship> ParentSubtasks { get; set; } // Collection of parent subtasks
        public ICollection<SubtaskRelationship> ChildSubtasks { get; set; } // Collection of child subtasks

        public ICollection<TaskMaterials> TaskMaterials { get; set; }
        public ICollection<StockMovement> StockMovements { get; set; }

        public bool HasChildRelationships()
        {
            return ChildSubtasks != null && ChildSubtasks.Any();
        }

        public bool IsChildSubtask()
        {
            return ParentSubtasks != null && ParentSubtasks.Any();
        }
    }
}
