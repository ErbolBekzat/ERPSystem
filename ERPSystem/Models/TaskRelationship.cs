using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Models
{
    public class TaskRelationship
    {
        public int Id { get; set; }
        public int ParentTaskId { get; set; } // Foreign key to parent Task
        public Task ParentTask { get; set; } // Parent task
        public int ChildTaskId { get; set; } // Foreign key to child Task
        public Task ChildTask { get; set; } // Child task
        public RelationshipType RelationshipType { get; set; } // Relationship type property
    }

    public enum RelationshipType
    {
        FS, // Finish to Start
        FF, // Finish to Finish
        SF, // Start to Finish
        SS  // Start to Start
    }
}
