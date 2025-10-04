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
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _RoleRepository;
        IConfiguration _configuration;
        private readonly IMapper _mapper;
        public RoleService(IRepository<Role> RoleRepository, IMapper mapper, IConfiguration configuration)
        {
            _RoleRepository = RoleRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        public RoleDTO Add(RoleDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            Role data = _mapper.Map<RoleDTO, Role>(obj);
            //data.created_by = Convert.ToInt64(CryptoEngine.Decrypt(obj.EncUserID));

            data.Role_id = _RoleRepository.Insert(data);
            return _mapper.Map<Role, RoleDTO>(data);
        }
        public List<RoleDTO> GetList()
        {
            throw new NotImplementedException();
        }
        public bool Delete(RoleDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.UpdatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            Role data = _RoleRepository.Get(Database.MAIN, obj.RoleID);
            data.Is_deleted = true;
            data.Updated_by = obj.UpdatedBy;
            return _RoleRepository.Update(data);
        }
        public bool Update(RoleDTO obj)
        {
            Role data = _mapper.Map<RoleDTO, Role>(obj);
            bool Succes = false;
            data.Updated_by = Convert.ToInt64(CryptoEngine.Decrypt(obj.EncUserID));
            Succes = _RoleRepository.Update(data);
            return Succes;
        }
        public IEnumerable<RoleDTO> loadGrid(string[] parameters)
        {
            return _mapper.Map<IEnumerable<Role>, IEnumerable<RoleDTO>>(_RoleRepository.GetSearchData(Database.MAIN));
        }
        public RoleDTO Get(long id)
        {
            Role obj = _RoleRepository.Get(Database.MAIN, id);
            RoleDTO objDto = _mapper.Map<Role, RoleDTO>(obj);
            return objDto;
        }
    }
}
