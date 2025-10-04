using AutoMapper;
using BU.DTO.DTOs.Agency;
using BU.DTO.DTOs.Customer;
using BU.DTO.DTOs.Jobs;
using BU.DTO.DTOs.RequestDTO.Customer;
using BU.Services.IServices.Customer;
using Common;
using Common.Helper;
using DA.DAO.DAO.Customer;
using DA.DAO.DAO.Jobs;
using DA.Entities.Agency;
using DA.Entities.Customer;
using DA.Entities.Jobs;
using DAO;
using DAO.DAO.User;
using Entities.Users;
using Logging;
using Microsoft.Extensions.Configuration;
using Services.IServices.Email;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BU.Services.Services.Customer
{
    public class FeCustomerService : IFeCustomerService
    {
        private readonly IRepository<Fe_customers> _FeCustomerRepository;
        private readonly IRepository<Fe_users> _UserRepository;
        private readonly IFeCustomerCardsService _customerCardsService;
        private readonly IEmailService _emailService;
        IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogging _logging;
        public FeCustomerService(IRepository<Fe_customers> FeCustomerRepository, IRepository<Fe_users> UserRepository, IFeCustomerCardsService customerCardsService, IEmailService emailService, IMapper mapper, IConfiguration configuration, ILogging logging)
        {
            _FeCustomerRepository = FeCustomerRepository;
            _UserRepository = UserRepository;
            _customerCardsService = customerCardsService;
            _configuration = configuration;
            _mapper = mapper;
            _emailService = emailService;
            _logging = logging;
        }

        public CustomerRegistrationRequestDTO RegisterCustomer(CustomerRegistrationRequestDTO obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                obj.CreatedBy = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }

            long nextIdentity = _FeCustomerRepository.GetNextIdentityId("fe_customer");
            string randomPassword = obj.Password;//_emailService.GenerateRandomPassword(8);

            if (!string.IsNullOrEmpty(obj.Base64Image))
            {
                string rootPath = _configuration["Web:DocumentPath"];
                string encodeImage = obj.Base64Image.Replace("data:image/png;base64,", string.Empty);
                byte[] imageBytes = Convert.FromBase64String(encodeImage);
                string folderPath = $"\\Documents\\Customers\\{nextIdentity}";
                string fullPath = $"{rootPath}\\Documents\\Customers\\{nextIdentity}";
                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);
                string fileName = Guid.NewGuid().ToString() + ".jpg";

                File.WriteAllBytes(Path.Combine(fullPath, fileName), imageBytes);
                obj.PhotoPath = Path.Combine(folderPath, fileName);
            }

            Fe_users userRecord = _mapper.Map<CustomerRegistrationRequestDTO, Fe_users>(obj);
            userRecord.User_name = obj.FirstName + " "+ obj.LastName;
            userRecord.User_type = "Customer";
            userRecord.Approval_status = "Approved";
            userRecord.Login_type = obj.LoginType;
            userRecord.Social_id = obj.GoogleId;
            userRecord.Password = CryptoEngine.Encrypt(randomPassword);
            userRecord.Is_active = true;
            userRecord.Last_login_date = null;
            userRecord.User_id = _UserRepository.Insert(userRecord);
            obj.UserId = userRecord.User_id;

            // save customer record in fe_customer
            Fe_customers customerRecord = _mapper.Map<CustomerRegistrationRequestDTO, Fe_customers>(obj);
            customerRecord.User_id = userRecord.User_id;
            long customerId = _FeCustomerRepository.Insert(customerRecord);

            if(obj.CustomerCards != null && obj.CustomerCards.Count > 0)
            {
                foreach (var card in obj.CustomerCards)
                {
                    card.CreatedBy = obj.CreatedBy;
                    card.CustomerId = customerId;
                }
                _customerCardsService.AddCustomerCards(obj.CustomerCards);
            }

            userRecord.Password = randomPassword;
            _emailService.CustomerRegistrationEmail(userRecord, "");
            return obj;
        }


        public CustomerRegistrationRequestDTO RegisterCustomerViaGoogle(CustomerRegistrationRequestDTO obj)
        {
            long actorId = 0;
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                actorId = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }

            try
            {
                _UserRepository.DataAccess.BeginTransaction();
                using (var connection = _UserRepository.DataAccess.GetActiveConnection(Database.MAIN))
                {
                    var trx = _UserRepository.DataAccess.GetActiveTransaction(connection);

                    long nextIdentity = _FeCustomerRepository.GetNextIdentityId("fe_customer");

                    if (!string.IsNullOrEmpty(obj.Base64Image))
                    {
                        string rootPath = _configuration["Web:DocumentPath"];
                        string encodeImage = obj.Base64Image.Replace("data:image/png;base64,", string.Empty);
                        byte[] imageBytes = Convert.FromBase64String(encodeImage);
                        string folderPath = $"\\Documents\\Customers\\{nextIdentity}";
                        string fullPath = $"{rootPath}\\Documents\\Customers\\{nextIdentity}";
                        if (!Directory.Exists(fullPath))
                            Directory.CreateDirectory(fullPath);
                        string fileName = Guid.NewGuid().ToString() + ".jpg";

                        File.WriteAllBytes(Path.Combine(fullPath, fileName), imageBytes);
                        obj.PhotoPath = Path.Combine(folderPath, fileName);
                    }

                    // Map and create user record
                    Fe_users userRecord = _mapper.Map<CustomerRegistrationRequestDTO, Fe_users>(obj);
                    userRecord.User_name = obj.FirstName;
                    userRecord.User_type = "Customer";
                    userRecord.Approval_status = "Approved";
                    userRecord.Login_type = obj.LoginType;
                    userRecord.Social_id = obj.GoogleId;
            
                    userRecord.Is_active = true;
                    userRecord.Last_login_date = null;
                    userRecord.Created_by = actorId;

                    userRecord.User_id = _UserRepository.Insert(userRecord, connection, trx);

                    // Save customer record
                    Fe_customers customerRecord = _mapper.Map<CustomerRegistrationRequestDTO, Fe_customers>(obj);
                    customerRecord.User_id = userRecord.User_id;
                    long customerId = _FeCustomerRepository.Insert(customerRecord, connection, trx);

                    // Save customer cards if provided
                    

                    _UserRepository.DataAccess.CommitTransaction();
                }
            }
            catch (Exception exc)
            {
                _UserRepository.DataAccess.RollbackTransaction();
                throw new Exception("Registration failed: please contact administrator", exc);
            }
            finally
            {
                _UserRepository.DataAccess.Close();
            }

            return obj;
        }


        public FeCustomerDTO Add(FeCustomerDTO obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(FeCustomerDTO obj)
        {
            throw new NotImplementedException();
        }

        public FeCustomerDTO Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<FeCustomerDTO> GetList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FeCustomerDTO> loadGrid(string[] parameters)
        {
            throw new NotImplementedException();
        }

        public bool Update(FeCustomerDTO obj)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCustomer(UpdateCustomerRequestDTO obj)
        {
            long actorId = 0;
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                actorId = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            try
            {
                _FeCustomerRepository.DataAccess.BeginTransaction();
                using (var connection = _FeCustomerRepository.DataAccess.GetActiveConnection(Database.MAIN))
                {
                    System.Data.SqlClient.SqlTransaction trx = _FeCustomerRepository.DataAccess.GetActiveTransaction(connection);
                    var customer = _FeCustomerRepository.GetList(Common.Database.MAIN, 
                        FeCustomerDAO.GetCustomerByCustomerIdQuery, 
                        new { CustomerId = obj.CustomerId },
                        connection, trx
                        )
                        .FirstOrDefault();
                    customer.First_name = obj.FirstName;
                    customer.Last_name = obj.LastName;
                    customer.Phone = obj.Phone;
                    customer.Address1 = obj.Address1;
                    customer.City = obj.City;
                    customer.State = obj.State;
                    customer.Zip_code = obj.Zip_Code;
                    customer.Lat = obj.Lat;
                    customer.Lng = obj.Lng;
                    if (!string.IsNullOrEmpty(obj.Base64Image))
                    {
                        string rootPath = _configuration["Web:DocumentPath"];
                        string encodeImage = obj.Base64Image.Replace("data:image/png;base64,", string.Empty);
                        byte[] imageBytes = Convert.FromBase64String(encodeImage);
                        string folderPath = $"\\Documents\\Customers\\{obj.CustomerId}";
                        string fullPath = $"{rootPath}\\Documents\\Customers\\{obj.CustomerId}";
                        if (!Directory.Exists(fullPath))
                            Directory.CreateDirectory(fullPath);
                        string fileName = Guid.NewGuid().ToString() + ".jpg";

                        File.WriteAllBytes(Path.Combine(fullPath, fileName), imageBytes);
                        customer.Photo_path = Path.Combine(folderPath, fileName);
                    }

                    _FeCustomerRepository.Update(customer, connection, trx);

                    var feUser = _UserRepository.GetList(Common.Database.MAIN, UserDAO.GetUserById, new { UserId = customer.User_id }, connection, trx).FirstOrDefault();
                    feUser.User_name = obj.FirstName + " " + obj.LastName;
                    _UserRepository.Update(feUser, connection, trx);
                    _FeCustomerRepository.DataAccess.CommitTransaction();
                }
            }
            catch(Exception ex)
            {
                _logging.Fatal($"Method: UpdateCustomer  @Error: {ex.Message}");
                throw new Exception("Customer update failed, please try again");
            }
            return true;
        }

        public FeCustomerDTO GetFeCustomerById(FeCustomerDTO obj)
        {

            var customer = _FeCustomerRepository.GetList(Database.MAIN, FeCustomerDAO.GetCustomerByCustomerIdQuery, new { customerId = obj.CustomerId }).FirstOrDefault();
            FeCustomerDTO response = _mapper.Map<Fe_customers, FeCustomerDTO>(customer);
            return response;
        }
    }
}
