using ERPSystem.Data;
using ERPSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ERPSystem.Services
{
    public class TaskService : ITaskService
    {
        private readonly DataBaseContext _context;

        public DataBaseContext Context => _context;

        public TaskService(DataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Models.Task> GetAllTasks()
        {
            return _context.Tasks
                .Include(t => t.AssignedUser)
                .Include(t => t.Materials)
                .Include(t => t.Project)
                .Include(t => t.Subtasks)
                .ToList();
        }

        public IEnumerable<Models.Task> GetTasksWithBlockId(int blockId)
        {
            return _context.Tasks
                .Include(t => t.AssignedUser)
                .Include(t => t.Materials)
                .Include(t => t.Project)
                .Include(t => t.Subtasks)
                .Where(t => t.BlockId == blockId)
                .ToList();
        }

        public IEnumerable<Models.Task> GetTasksWithProjectId(int projectId)
        {
            return _context.Tasks
                .Include(t => t.AssignedUser)
                .Include(t => t.Materials)
                .Include(t => t.Project)
                .Include(t => t.Subtasks)
                .Where(t => t.ProjectId == projectId)
                .ToList();
        }


        public Models.Task GetTaskById(int taskId)
        {
            return _context.Tasks
                .Include(t => t.AssignedUser)
                .Include(t => t.Materials)
                .Include(t => t.Project)
                .Include(t => t.Subtasks)
                .FirstOrDefault(t => t.Id == taskId);
        }


        public void AddTask(Models.Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(Models.Task task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public bool DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);

            // Check if there is a subtask with Status == "Completed"
            bool hasSubtask = _context.Subtasks.Any(s => s.TaskId == id);

            if (hasSubtask)
            {
                return false;
            }
            // Delete the Task
            _context.Tasks.Remove(task);

            // Save changes to the database
            _context.SaveChanges();

            return true;
        }



        public void DeleteAllTasks()
        {
            var allTasks = _context.Tasks.ToList();
            _context.Tasks.RemoveRange(allTasks);
            _context.SaveChanges();
        }
    }
}
