using ERPSystem.Models;
using System.Collections.Generic;

namespace ERPSystem.Services.SubtaskRelationshipServices
{
    public interface ISubtaskRelationshipService
    {
        List<SubtaskRelationship> GetAllSubtaskRelationships();
        List<SubtaskRelationship> GetSubtaskRelationshipsBySubtaskId(int subtaskId);
        SubtaskRelationship GetSubtaskRelationshipById(int relationshipId);
        int CreateSubtaskRelationship(SubtaskRelationship relationship);
        int UpdateSubtaskRelationship(SubtaskRelationship relationship);
        int DeleteSubtaskRelationship(SubtaskRelationship relationship);
    }
}
