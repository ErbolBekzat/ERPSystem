using ERPSystem.Data;
using ERPSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ERPSystem.Services
{
    public class RolePermissionMappingService : IRolePermissionMappingService
    {
        private readonly DataBaseContext _dbContext;

        public RolePermissionMappingService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<RolePermissionMapping> GetAllRolePermissionMappings()
        {
            return _dbContext.RolePermissionMappings.ToList();
        }

        public List<RolePermissionMapping> GetRolePermissionMappingsByRoleId(int roleId)
        {
            return _dbContext.RolePermissionMappings
                .Where(mapping => mapping.RoleId == roleId)
                .ToList();
        }

        public RolePermissionMapping GetRolePermissionMappingById(int mappingId)
        {
            return _dbContext.RolePermissionMappings.Find(mappingId);
        }

        public int CreateRolePermissionMapping(RolePermissionMapping mapping)
        {
            _dbContext.RolePermissionMappings.Add(mapping);
            return _dbContext.SaveChanges();
        }

        public int UpdateRolePermissionMapping(RolePermissionMapping mapping)
        {
            _dbContext.RolePermissionMappings.Update(mapping);
            return _dbContext.SaveChanges();
        }

        public int DeleteRolePermissionMapping(RolePermissionMapping mapping)
        {
            _dbContext.RolePermissionMappings.Remove(mapping);
            return _dbContext.SaveChanges();
        }
    }
}
