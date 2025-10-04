using BU.DTO.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.Services.IServices.Customer
{
    public interface IFeCustomerCardsService
    {
        FeCustomerCardsDTO Add(FeCustomerCardsDTO obj);
        bool AddCustomerCards(List<FeCustomerCardsDTO> obj);
        List<FeCustomerCardsDTO> GetList();
        bool Delete(FeCustomerCardsDTO obj);
        bool Update(FeCustomerCardsDTO obj);
        IEnumerable<FeCustomerCardsDTO> loadGrid(string[] parameters);
        FeCustomerCardsDTO Get(long id);
        IEnumerable<FeCustomerCardsDTO> GetCustomerCards(long customerId);
    }
}
