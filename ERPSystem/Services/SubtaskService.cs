using ERPSystem.Data;
using ERPSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPSystem.Services
{
    public class SubtaskService : ISubtaskService
    {
        private readonly DataBaseContext _context;

        public DataBaseContext Context => _context;

        public SubtaskService(DataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Subtask> GetAllSubtasks()
        {
            var subtasks = _context.Subtasks
                .Include(s => s.Task)
                    .ThenInclude(t => t.AssignedUser)
                .Include(s => s.AssignedUser)
                .Include(s => s.Materials)
                .ToList();

            // Explicitly load ParentSubtasks and ChildSubtasks for each subtask
            foreach (var subtask in subtasks)
            {
                _context.Entry(subtask)
                    .Collection(s => s.ParentSubtasks)
                    .Query()
                    .Include(r => r.ParentSubtask)
                    .Load();

                _context.Entry(subtask)
                    .Collection(s => s.ChildSubtasks)
                    .Query()
                    .Include(r => r.ChildSubtask)
                    .Load();
            }

            return subtasks;
        }


        public IEnumerable<Subtask> GetAllSubtasksWithTaskId(int taskId)
        {
            var subtasks = _context.Subtasks
                .Include(s => s.Task)
                    .ThenInclude(t => t.AssignedUser)
                .Include(s => s.AssignedUser)
                .Include(s => s.Materials)
                .Where(s => s.TaskId == taskId)
                .ToList();

            foreach (var subtask in subtasks)
            {
                _context.Entry(subtask)
                    .Collection(s => s.ParentSubtasks)
                    .Query()
                    .Include(r => r.ParentSubtask)
                    .Load();

                _context.Entry(subtask)
                    .Collection(s => s.ChildSubtasks)
                    .Query()
                    .Include(r => r.ChildSubtask)
                    .Load();
            }

            return subtasks;
        }

        public Subtask GetSubtaskById(int subtaskId)
        {
            var subtask = _context.Subtasks
                .Include(s => s.Task)
                    .ThenInclude(t => t.AssignedUser)
                .Include(s => s.AssignedUser)
                .Include(s => s.Materials)
                .FirstOrDefault(s => s.Id == subtaskId);

            if (subtask != null)
            {
                _context.Entry(subtask)
                    .Collection(s => s.ParentSubtasks)
                    .Query()
                    .Include(r => r.ParentSubtask)
                    .Load();

                _context.Entry(subtask)
                    .Collection(s => s.ChildSubtasks)
                    .Query()
                    .Include(r => r.ChildSubtask)
                    .Load();
            }

            return subtask;
        }

        public void AddSubtask(Subtask subtask)
        {
            _context.Subtasks.Add(subtask);
            _context.SaveChanges();
        }

        public void AddRangeOfSubtasks(IEnumerable<Subtask> subtasks)
        {
            _context.Subtasks.AddRange(subtasks);
            _context.SaveChanges();
        }



        public void UpdateSubtask(Subtask subtask)
        {
            _context.Subtasks.Update(subtask);
            _context.SaveChanges();
        }

        public bool DeleteSubtask(int id)
        {
            // Delete SubtaskRelationships with ParentSubtaskId equal to id
            var parentRelationship = _context.SubtaskRelationships.Any(r => r.ParentSubtaskId == id);

            // Delete SubtaskRelationships with ChildSubtaskId equal to id
            var childRelationship = _context.SubtaskRelationships.Any(r => r.ChildSubtaskId == id);

            var stockMovement = _context.StockMovements.Any(sm => sm.SubtaskId == id);

            bool problem = _context.Problems.Any(p => p.SubtaskId == id);

            if (parentRelationship || childRelationship || stockMovement || problem) { return false; }
            
            // Delete the Subtask
            var subtask = _context.Subtasks.Find(id);
            _context.Subtasks.Remove(subtask);

            // Save changes to the database
            _context.SaveChanges();

            return true;
        }


        public void DeleteAllSubtasks()
        {
            var allSubtasks = _context.Subtasks.ToList();
            _context.Subtasks.RemoveRange(allSubtasks);
            _context.SaveChanges();
        }
    }
}
