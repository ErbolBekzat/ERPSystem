using ERPSystem.Models;
using System.Collections.Generic;

namespace ERPSystem.Services
{
    public interface IPermissionService
    {
        List<Permission> GetAllPermissions();
        Permission GetPermissionById(int permissionId);
        int CreatePermission(Permission permission);
        int UpdatePermission(Permission permission);
        int DeletePermission(Permission permission);
    }
}
