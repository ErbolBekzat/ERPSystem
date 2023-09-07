using ERPSystem.Data;
using ERPSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ERPSystem.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly DataBaseContext _dbContext;

        public PermissionService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Permission> GetAllPermissions()
        {
            return _dbContext.Permissions.ToList();
        }

        public Permission GetPermissionById(int permissionId)
        {
            return _dbContext.Permissions.Find(permissionId);
        }

        public int CreatePermission(Permission permission)
        {
            _dbContext.Permissions.Add(permission);
            return _dbContext.SaveChanges();
        }

        public int UpdatePermission(Permission permission)
        {
            _dbContext.Permissions.Update(permission);
            return _dbContext.SaveChanges();
        }

        public int DeletePermission(Permission permission)
        {
            _dbContext.Permissions.Remove(permission);
            return _dbContext.SaveChanges();
        }
    }
}
