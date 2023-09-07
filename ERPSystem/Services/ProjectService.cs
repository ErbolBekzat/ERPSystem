using ERPSystem.Data;
using ERPSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPSystem.Services
{
    public class ProjectService : IProjectService
    {
        private readonly DataBaseContext _dbContext;

        public ProjectService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _dbContext.Projects
                .Include(p => p.Stock) // Include the Stock entity in the query
                .ToListAsync();
        }


        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            return await _dbContext.Projects
                .Include(p => p.Stock) // Include the Stock entity in the query
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }


        public async Task<int> CreateProjectAsync(Project project)
        {
            _dbContext.Projects.Add(project);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateProjectAsync(Project project)
        {
            _dbContext.Projects.Update(project);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteProjectAsync(Project project)
        {
            _dbContext.Projects.Remove(project);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
