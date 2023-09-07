using ERPSystem.Data;
using ERPSystem.Models;
using ERPSystem.Services.TaskMaterialServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ERPSystem.Services
{
    public class TaskMaterialService : ITaskMaterialService
    {
        private readonly DataBaseContext _context;

        public TaskMaterialService(DataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<TaskMaterials> GetAllTaskMaterials()
        {
            return _context.TaskMaterials
                .Include(tm => tm.Subtask)
                .Include(tm => tm.Material)
                .ToList();
        }

        public IEnumerable<TaskMaterials> GetTaskMaterialsBySubtaskId(int taskId)
        {
            return _context.TaskMaterials
                .Include(tm => tm.Material)
                .Where(tm => tm.TaskId == taskId)
                .ToList();
        }


        public void AddTaskMaterial(TaskMaterials taskMaterial)
        {
            _context.TaskMaterials.Add(taskMaterial);
            _context.SaveChanges();
        }

        public void AddTaskMaterialsRange(IEnumerable<TaskMaterials> taskMaterials)
        {
            List<TaskMaterials> tempList = new List<TaskMaterials>(taskMaterials);

            foreach (TaskMaterials taskMaterial in taskMaterials.ToList())
            {
                if (taskMaterial.MaterialId == null || taskMaterial.QuantityRequired == null || taskMaterial.Cost == null)
                {
                    tempList.Remove(taskMaterial);
                }
                else
                {
                    bool isValidMaterialId = CheckIfValidMaterialId(taskMaterial.MaterialId);

                    if (!isValidMaterialId)
                    {
                        tempList.Remove(taskMaterial);
                    }
                }
            }

            if (tempList.Count > 0)
            {
                _context.TaskMaterials.AddRange(tempList);
                _context.SaveChanges();
            }
        }

        private bool CheckIfValidMaterialId(int materialId)
        {
            return _context.Materials.Any(m => m.Id == materialId);
        }



        public void UpdateTaskMaterial(TaskMaterials taskMaterial)
        {
            _context.Entry(taskMaterial).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void UpdateTaskMaterialsRange(ObservableCollection<TaskMaterials> taskMaterials)
        {
            var modifiedTaskMaterials = taskMaterials.Where(t => _context.Entry(t).State == EntityState.Modified || _context.Entry(t).Properties.Any(p => p.IsModified));

            _context.SaveChanges();
        }



        public void DeleteTaskMaterial(TaskMaterials taskMaterial)
        {
            _context.TaskMaterials.Remove(taskMaterial);
            _context.SaveChanges();
        }
    }
}
