using AutoMapper;
using Common;
using DAO;
using DTO.DTOs.Setup;
using Entities.Setup;
using Services.IServices.Setup;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Common.Helper;
namespace Services.Services.Setup
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRepository<Role_Permission> _RolePermissionRepository;
        IConfiguration _configuration;
        private readonly IMapper _mapper;
        public RolePermissionService(IRepository<Role_Permission> RolePermissionRepository, IMapper mapper, IConfiguration configuration)
        {
            _RolePermissionRepository = RolePermissionRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        public RolePermissionDTO Add(RolePermissionDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            Role_Permission data = _mapper.Map<RolePermissionDTO, Role_Permission>(obj);
            //data.created_by = Convert.ToInt64(CryptoEngine.Decrypt(obj.EncUserID));

            data.Role_permission_id = _RolePermissionRepository.Insert(data);
            return _mapper.Map<Role_Permission, RolePermissionDTO>(data);
        }
        public List<RolePermissionDTO> GetList()
        {
            throw new NotImplementedException();
        }
        public bool Delete(RolePermissionDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.UpdatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            Role_Permission data = _RolePermissionRepository.Get(Database.MAIN, obj.RolePermissionID);
            data.Is_deleted = true;
            data.Updated_by = obj.UpdatedBy;
            return _RolePermissionRepository.Update(data);
        }
        public bool Update(RolePermissionDTO obj)
        {
            Role_Permission data = _mapper.Map<RolePermissionDTO, Role_Permission>(obj);
            bool Succes = false;
            data.Updated_by = Convert.ToInt64(CryptoEngine.Decrypt(obj.EncUserID));
            Succes = _RolePermissionRepository.Update(data);
            return Succes;
        }
        public IEnumerable<RolePermissionDTO> loadGrid(string[] parameters)
        {
            return _mapper.Map<IEnumerable<Role_Permission>, IEnumerable<RolePermissionDTO>>(_RolePermissionRepository.GetSearchData(Database.MAIN));
        }
        public RolePermissionDTO Get(long id)
        {
            Role_Permission obj = _RolePermissionRepository.Get(Database.MAIN, id);
            RolePermissionDTO objDto = _mapper.Map<Role_Permission, RolePermissionDTO>(obj);
            return objDto;
        }
    }
}
