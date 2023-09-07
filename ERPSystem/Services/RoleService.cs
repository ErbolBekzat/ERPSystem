using ERPSystem.Data;
using ERPSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPSystem.Services
{
    public class RoleService : IRoleService
    {
        private readonly DataBaseContext _dbContext;

        public RoleService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Role> GetAllRoles()
        {
            return _dbContext.Roles.ToList();
        }

        public Role GetRoleById(int roleId)
        {
            return _dbContext.Roles.Find(roleId);
        }

        public Role GetRoleByName(string roleName)
        {
            return _dbContext.Roles.FirstOrDefault(r => r.RoleName == roleName);
        }

        public int CreateRole(Role role)
        {
            _dbContext.Roles.Add(role);
            return _dbContext.SaveChanges();
        }

        public int UpdateRole(Role role)
        {
            _dbContext.Roles.Update(role);
            return _dbContext.SaveChanges();
        }

        public int DeleteRole(Role role)
        {
            _dbContext.Roles.Remove(role);
            return _dbContext.SaveChanges();
        }

        public bool HasPermissions(int roleId, string permissionName)
        {
            var rolePermissionQuery = from mapping in _dbContext.RolePermissionMappings
                                      join permission in _dbContext.Permissions
                                      on mapping.PermissionId equals permission.PermissionId
                                      where mapping.RoleId == roleId
                                      select permission.PermissionName;

            var matchingPermissions = rolePermissionQuery.ToList();

            return matchingPermissions.Contains(permissionName, StringComparer.OrdinalIgnoreCase);
        }
    }
}
