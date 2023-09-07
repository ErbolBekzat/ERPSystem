using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services
{
    public interface ISubtaskService
    {
        IEnumerable<Subtask> GetAllSubtasks();
        IEnumerable<Subtask> GetAllSubtasksWithTaskId(int taskId);
        Subtask GetSubtaskById(int subtaskId);
        void AddSubtask(Subtask subtask);
        void AddRangeOfSubtasks(IEnumerable<Subtask> subtasks);
        void UpdateSubtask(Subtask subtask);
        bool DeleteSubtask(int id);
        void DeleteAllSubtasks();
    }
}
