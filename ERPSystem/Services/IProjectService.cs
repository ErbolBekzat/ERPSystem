using ERPSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetAllProjectsAsync();
        Task<Project> GetProjectByIdAsync(int projectId);
        Task<int> CreateProjectAsync(Project project);
        Task<int> UpdateProjectAsync(Project project);
        Task<int> DeleteProjectAsync(Project project);
    }
}
