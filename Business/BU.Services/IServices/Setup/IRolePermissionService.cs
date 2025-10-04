using DTO.DTOs.Setup;
using System.Collections.Generic;

namespace Services.IServices.Setup
{
    public interface IRolePermissionService
    {
        RolePermissionDTO Add(RolePermissionDTO obj);
        List<RolePermissionDTO> GetList();
        bool Delete(RolePermissionDTO obj);
        bool Update(RolePermissionDTO obj);
        IEnumerable<RolePermissionDTO> loadGrid(string[] parameters);
        RolePermissionDTO Get(long id);
    }
}
