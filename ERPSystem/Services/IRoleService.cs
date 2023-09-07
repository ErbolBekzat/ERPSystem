using ERPSystem.Models;
using System.Collections.Generic;

namespace ERPSystem.Services
{
    public interface IRoleService
    {
        List<Role> GetAllRoles();
        Role GetRoleById(int roleId);
        Role GetRoleByName(string roleName);
        int CreateRole(Role role);
        int UpdateRole(Role role);
        int DeleteRole(Role role);
        bool HasPermissions(int roleId, string permissionName);
    }
}
