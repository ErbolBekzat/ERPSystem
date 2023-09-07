using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Models
{
    public class SubtaskRelationship
    {
        public int Id { get; set; }
        public int ParentSubtaskId { get; set; } // Foreign key to parent Subtask
        public Subtask ParentSubtask { get; set; } // Parent subtask
        public int ChildSubtaskId { get; set; } // Foreign key to child Subtask
        public Subtask ChildSubtask { get; set; } // Child subtask
        public RelationshipType RelationshipType { get; set; } // Relationship type property
    }
}
