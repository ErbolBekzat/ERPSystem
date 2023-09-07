using ERPSystem.Data;
using ERPSystem.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ERPSystem.Services.SubtaskRelationshipServices
{
    public class SubtaskRelationshipService : ISubtaskRelationshipService
    {
        private readonly DataBaseContext _dbContext;

        public SubtaskRelationshipService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<SubtaskRelationship> GetAllSubtaskRelationships()
        {
            return _dbContext.SubtaskRelationships.ToList();
        }

        public List<SubtaskRelationship> GetSubtaskRelationshipsBySubtaskId(int subtaskId)
        {
            return _dbContext.SubtaskRelationships
                .Where(r => r.ParentSubtaskId == subtaskId || r.ChildSubtaskId == subtaskId)
                .ToList();
        }


        public SubtaskRelationship GetSubtaskRelationshipById(int relationshipId)
        {
            return _dbContext.SubtaskRelationships.Find(relationshipId);
        }

        public int CreateSubtaskRelationship(SubtaskRelationship relationship)
        {
            _dbContext.SubtaskRelationships.Add(relationship);
            return _dbContext.SaveChanges();
        }

        public int UpdateSubtaskRelationship(SubtaskRelationship relationship)
        {
            _dbContext.SubtaskRelationships.Update(relationship);
            return _dbContext.SaveChanges();
        }

        public int DeleteSubtaskRelationship(SubtaskRelationship relationship)
        {
            _dbContext.SubtaskRelationships.Remove(relationship);
            return _dbContext.SaveChanges();
        }
    }
}
