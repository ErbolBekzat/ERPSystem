using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystem.Services.TaskMaterialServices
{
    public interface ITaskMaterialService
    {
        IEnumerable<TaskMaterials> GetAllTaskMaterials();
        IEnumerable<TaskMaterials> GetTaskMaterialsBySubtaskId(int taskId);
        void AddTaskMaterial(TaskMaterials taskMaterial);
        void AddTaskMaterialsRange(IEnumerable<TaskMaterials> taskMaterials);
        void UpdateTaskMaterial(TaskMaterials taskMaterial);
        void UpdateTaskMaterialsRange(ObservableCollection<TaskMaterials> taskMaterials);
        void DeleteTaskMaterial(TaskMaterials taskMaterial);
    }
}
