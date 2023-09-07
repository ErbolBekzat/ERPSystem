using System;
using System.Collections.Generic;

namespace ERPSystem.Services
{
    public interface ITaskService
    {
        IEnumerable<Models.Task> GetAllTasks();
        IEnumerable<Models.Task> GetTasksWithBlockId(int id);
        IEnumerable<Models.Task> GetTasksWithProjectId(int id);
        Models.Task GetTaskById(int taskId);
        void AddTask(Models.Task task);
        void UpdateTask(Models.Task task);
        bool DeleteTask(int id);
        void DeleteAllTasks();
    }
}
