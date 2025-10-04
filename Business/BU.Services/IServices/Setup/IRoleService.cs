using DTO.DTOs.Setup;
using System.Collections.Generic;
using System.Linq;
using System;


namespace Services.IServices.Setup
{
    public interface IRoleService
    {
        RoleDTO Add(RoleDTO obj);
        List<RoleDTO> GetList();
        bool Delete(RoleDTO obj);
        bool Update(RoleDTO obj);
        IEnumerable<RoleDTO> loadGrid(string[] parameters);
        RoleDTO Get(long id);
    }
}
