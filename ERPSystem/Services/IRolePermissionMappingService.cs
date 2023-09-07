using ERPSystem.Models;
using System.Collections.Generic;

namespace ERPSystem.Services
{
    public interface IRolePermissionMappingService
    {
        List<RolePermissionMapping> GetAllRolePermissionMappings();
        List<RolePermissionMapping> GetRolePermissionMappingsByRoleId(int roleId);
        RolePermissionMapping GetRolePermissionMappingById(int mappingId);
        int CreateRolePermissionMapping(RolePermissionMapping mapping);
        int UpdateRolePermissionMapping(RolePermissionMapping mapping);
        int DeleteRolePermissionMapping(RolePermissionMapping mapping);
    }
}
