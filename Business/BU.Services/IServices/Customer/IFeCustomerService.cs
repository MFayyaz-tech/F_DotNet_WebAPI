using BU.DTO.DTOs.Customer;
using BU.DTO.DTOs.RequestDTO.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BU.Services.IServices.Customer
{
    public interface IFeCustomerService
    {
        FeCustomerDTO Add(FeCustomerDTO obj);
        FeCustomerDTO GetFeCustomerById(FeCustomerDTO obj);
        List<FeCustomerDTO> GetList();
        bool Delete(FeCustomerDTO obj);
        bool Update(FeCustomerDTO obj);
        IEnumerable<FeCustomerDTO> loadGrid(string[] parameters);
        FeCustomerDTO Get(long id);
        public CustomerRegistrationRequestDTO RegisterCustomer(CustomerRegistrationRequestDTO obj);
        
        public CustomerRegistrationRequestDTO RegisterCustomerViaGoogle(CustomerRegistrationRequestDTO obj);
        public bool UpdateCustomer(UpdateCustomerRequestDTO obj);
    }
}
