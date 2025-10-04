using AutoMapper;
using BU.DTO.DTOs.Customer;
using BU.Services.IServices.Customer;
using Common;
using Common.Helper;
using DA.DAO.DAO.Customer;
using DA.Entities.Customer;
using DAO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.Services.Services.Customer
{
    public class FeCustomerCardsService : IFeCustomerCardsService
    {
        private readonly IRepository<Fe_customer_cards> _FeCustomerCardsRepository;
        IConfiguration _configuration;
        private readonly IMapper _mapper;
        public FeCustomerCardsService(IRepository<Fe_customer_cards> FeCustomerCardsRepository, IMapper mapper, IConfiguration configuration)
        {
            _FeCustomerCardsRepository = FeCustomerCardsRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        public FeCustomerCardsDTO Add(FeCustomerCardsDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            Fe_customer_cards cards = _mapper.Map<FeCustomerCardsDTO, Fe_customer_cards>(obj);
            obj.CustomerCardId = _FeCustomerCardsRepository.Insert(cards);
            return obj;
        }
        public List<FeCustomerCardsDTO> GetList()
        {
            throw new NotImplementedException();
        }
        public bool Delete(FeCustomerCardsDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.UpdatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            bool response = _FeCustomerCardsRepository.Archive(new {
                CustomerCardId = obj.CustomerCardId,
                UserId = obj.UpdatedBy
            });
            return response;
        }
        public bool Update(FeCustomerCardsDTO obj)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<FeCustomerCardsDTO> loadGrid(string[] parameters)
        {
            throw new NotImplementedException();
        }
        public FeCustomerCardsDTO Get(long id)
        {
            throw new NotImplementedException();
        }

        public bool AddCustomerCards(List<FeCustomerCardsDTO> obj)
        {
            _FeCustomerCardsRepository.BulkInsert(_mapper.Map<List<FeCustomerCardsDTO>, List<Fe_customer_cards>>(obj));
            return true;
        }

        public IEnumerable<FeCustomerCardsDTO> GetCustomerCards(long customerId)
        {
            var cardsList = _FeCustomerCardsRepository.GetList(Database.MAIN, FeCustomerCardsDAO.GetCustomerCardsByCustomerIdQuery, new { CustomerId = customerId });
            return _mapper.Map<IEnumerable<Fe_customer_cards>, IEnumerable<FeCustomerCardsDTO>>(cardsList);
        }
    }
}
