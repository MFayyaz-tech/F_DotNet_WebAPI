using AutoMapper;
using BU.DTO.DTOs.Agency;
using BU.DTO.DTOs.Customer;
using BU.DTO.DTOs.Users;
using BU.Services.IServices.Agency;
using Common;
using Common.Helper;
using DA.DAO.DAO.Agency;
using DA.DAO.DAO.Customer;
using DA.Entities.Agency;
using DA.Entities.Customer;
using DAO;
using DAO.DAO.User;
using DTO.DTOs.User;
using Entities.Users;
using Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Services.IServices.Email;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Http;

using static Org.BouncyCastle.Asn1.Cmp.Challenge;
using BU.DTO.DTOs.ResponseDTO.Testimonials;
using DA.DAO.DAO.Trainings;
using DA.Entities.Trainings;
using DA.Entities.Jobs;
using BU.DTO.DTOs.Trainings;
using static Google.Apis.Requests.BatchRequest;
using DA.Entities.Billing;

namespace BU.Services.Services.Agency
{
    public class AgencyService : IAgencyService
	{
		private readonly IRepository<Fe_agency> _AgencyRepository;
        private readonly IRepository<Fe_job_contract> _JobContractRepositoy;
        private readonly IRepository<Fe_payment> _PaymentRepository;
        private readonly IRepository<Fe_agency_bank_details> _AgencyBankDetailsRepository;
		private readonly IRepository<Fe_agency_license> _AgencyLicenseRepository;
		private readonly IRepository<Fe_users> _UserRepository;
		private readonly IEmailService _emailService;



        IConfiguration _configuration;
		private readonly IMapper _mapper;

		public AgencyService(IRepository<Fe_agency> AgencyRepository,
            IRepository<Fe_users> UserRepository,
            IRepository<Fe_payment> PaymentRepository,
        IRepository<Fe_job_contract> JobContractRepositoy,
        IRepository<Fe_agency_bank_details> AgencyBankDetailsRepository,
             IRepository<Fe_agency_license> AgencyLicenseRepository,
            IEmailService emailService, IMapper mapper, IConfiguration configuration)
		{
			_AgencyRepository = AgencyRepository;
            _PaymentRepository = PaymentRepository;
            _JobContractRepositoy = JobContractRepositoy;
            _AgencyBankDetailsRepository = AgencyBankDetailsRepository;
            _AgencyLicenseRepository = AgencyLicenseRepository;
			_UserRepository = UserRepository;
			_emailService = emailService;

            _configuration = configuration;
			_mapper = mapper;
		}
        public RegisterAgencyRequestDTO RegisterAgency(RegisterAgencyRequestDTO obj)
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
                    System.Data.SqlClient.SqlTransaction trx = _UserRepository.DataAccess.GetActiveTransaction(connection);
                    long nextIdentity = _AgencyRepository.GetNextIdentityId("fe_agency");
                    string randomPassword = obj.Password;//_emailService.GenerateRandomPassword(8);

                    if (!string.IsNullOrEmpty(obj.Base64Image))
                    {
                        string rootPath = _configuration["Web:DocumentPath"];
                        string encodeImage = obj.Base64Image.Replace("data:image/png;base64,", string.Empty);
                        byte[] imageBytes = Convert.FromBase64String(encodeImage);
                        string folderPath = $"\\Documents\\Agencies\\{nextIdentity}";
                        string fullPath = $"{rootPath}\\Documents\\Agencies\\{nextIdentity}";
                        if (!Directory.Exists(fullPath))
                            Directory.CreateDirectory(fullPath);
                        string fileName = Guid.NewGuid().ToString() + ".jpg";

                        File.WriteAllBytes(Path.Combine(fullPath, fileName), imageBytes);
                        obj.PhotoPath = Path.Combine(folderPath, fileName);
                    }

                    Fe_users userRecord = _mapper.Map<RegisterAgencyRequestDTO, Fe_users>(obj);
                    userRecord.User_name = obj.AgencyName;
                    userRecord.User_type = "Agency";
                    userRecord.Approval_status = "Approved";
                   
                    userRecord.Password = CryptoEngine.Encrypt(randomPassword);
                    userRecord.Is_active = true;
                    userRecord.Last_login_date = null;
                    userRecord.Login_type = obj.LoginType;
                    userRecord.Social_id = obj.GoogleId;

                    userRecord.Created_by = actorId;
                    userRecord.User_id = _UserRepository.Insert(userRecord, connection, trx);


                    // save agency record in fe_agency
                    Fe_agency agencyRecord = _mapper.Map<RegisterAgencyRequestDTO, Fe_agency>(obj);
                    agencyRecord.User_id = userRecord.User_id;
                    agencyRecord.Created_by = actorId;
                    long agencyId = _AgencyRepository.Insert(agencyRecord, connection, trx);

                    if (obj.AgencyBankDetails != null)
                    {
                        foreach (var bank in obj.AgencyBankDetails)
                        {
                            bank.AgencyId = agencyId;
                            bank.CreatedBy = actorId;
                        }
                        _AgencyBankDetailsRepository.BulkInsert(_mapper.Map<List<AgencyBankDetailsDTO>, List<Fe_agency_bank_details>>(obj.AgencyBankDetails), connection, trx);
                    }
                    if (obj.AgencyLicenses != null)
                    {
                        foreach (var license in obj.AgencyLicenses)
                        {
                            license.AgencyId = agencyId;
                            license.CreatedBy = actorId;
                        }
                        _AgencyLicenseRepository.BulkInsert(_mapper.Map<List<AgencyLicenseDTO>, List<Fe_agency_license>>(obj.AgencyLicenses), connection, trx);
                    }
                    userRecord.Password = randomPassword;
                    _UserRepository.DataAccess.CommitTransaction();
                }
            }
            catch (Exception exc)
            {
                _UserRepository.DataAccess.RollbackTransaction();

                throw new Exception("Registration failed: please contact administrator");
            }
            finally
            {
                _UserRepository.DataAccess.Close();
            }
            //_emailService.AgencyRegistrationEmail(userRecord, "");
            return obj;
        }



        public RegisterAgencyRequestDTO RegisterAgencyViaGoogle(RegisterAgencyRequestDTO obj)
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

                    long nextIdentity = _AgencyRepository.GetNextIdentityId("fe_agency");

                    if (!string.IsNullOrEmpty(obj.Base64Image))
                    {
                        string rootPath = _configuration["Web:DocumentPath"];
                        string encodeImage = obj.Base64Image.Replace("data:image/png;base64,", string.Empty);
                        byte[] imageBytes = Convert.FromBase64String(encodeImage);
                        string folderPath = $"\\Documents\\Agencies\\{nextIdentity}";
                        string fullPath = $"{rootPath}\\Documents\\Agencies\\{nextIdentity}";
                        if (!Directory.Exists(fullPath))
                            Directory.CreateDirectory(fullPath);
                        string fileName = Guid.NewGuid().ToString() + ".jpg";

                        File.WriteAllBytes(Path.Combine(fullPath, fileName), imageBytes);
                        obj.PhotoPath = Path.Combine(folderPath, fileName);
                    }

                    // Map and create user record
                    Fe_users userRecord = _mapper.Map<RegisterAgencyRequestDTO, Fe_users>(obj);
                    userRecord.User_name = obj.AgencyName;
                    userRecord.User_type = "Agency";
                    userRecord.Approval_status = "Approved";
                    userRecord.Is_active = true;
                    userRecord.Login_type = obj.LoginType;
                    userRecord.Social_id = obj.GoogleId;

                    userRecord.Last_login_date = null;
                    userRecord.Created_by = actorId;

                    userRecord.User_id = _UserRepository.Insert(userRecord, connection, trx);

                    // Save agency record
                    Fe_agency agencyRecord = _mapper.Map<RegisterAgencyRequestDTO, Fe_agency>(obj);
                    agencyRecord.User_id = userRecord.User_id;
                    agencyRecord.Created_by = actorId;
                    long agencyId = _AgencyRepository.Insert(agencyRecord, connection, trx);

                    // Save bank details if provided
                    if (obj.AgencyBankDetails != null)
                    {
                        foreach (var bank in obj.AgencyBankDetails)
                        {
                            bank.AgencyId = agencyId;
                            bank.CreatedBy = actorId;
                        }
                        _AgencyBankDetailsRepository.BulkInsert(
                            _mapper.Map<List<AgencyBankDetailsDTO>, List<Fe_agency_bank_details>>(obj.AgencyBankDetails),
                            connection,
                            trx);
                    }

                    // Save licenses if provided
                    if (obj.AgencyLicenses != null)
                    {
                        foreach (var license in obj.AgencyLicenses)
                        {
                            license.AgencyId = agencyId;
                            license.CreatedBy = actorId;
                        }
                        _AgencyLicenseRepository.BulkInsert(
                            _mapper.Map<List<AgencyLicenseDTO>, List<Fe_agency_license>>(obj.AgencyLicenses),
                            connection,
                            trx);
                    }

                    _UserRepository.DataAccess.CommitTransaction();
                }
            }
            catch (Exception exc)
            {
                _UserRepository.DataAccess.RollbackTransaction();
                throw new Exception("Registration failed: please contact administrator" + exc, exc);
            }
            finally
            {
                _UserRepository.DataAccess.Close();
            }

            return obj;
        
    }




    public IEnumerable<AgenciesListReponseDTO> LoadAgenciesList(string[] parameters)
		{
			var agencies = _AgencyRepository.GetList(Common.Database.MAIN, AgencyDAO.GetAgencyListQuery);
			return _mapper.Map<IEnumerable<Fe_agency>,IEnumerable<AgenciesListReponseDTO>>(agencies);
		}

		public bool DeleteAgency(RegisterAgencyRequestDTO obj)
		{
			bool isDeleted = false;
			Fe_users  user = _UserRepository.GetList(Database.MAIN, UserDAO.GetUserById, new { obj.UserId }).FirstOrDefault();
			if(user != null)
			{
				user.Is_deleted = true;
				isDeleted = _UserRepository.Delete(user);
				var agency = _AgencyRepository.GetList(Database.MAIN, AgencyDAO.GetAgencyById, new { obj.UserId }).FirstOrDefault();
				agency.Is_deleted = true;
				_AgencyRepository.Delete(agency);
			}
			return isDeleted;
		}

        public bool UpdateAgency(UpdateAgencyRequestDTO obj)
        {
            long actorId = 0;
            if (!string.IsNullOrWhiteSpace(obj.EncUserID))
            {
                actorId = long.Parse(CryptoEngine.Decrypt(obj.EncUserID));
            }
            try
            {
                  
                _AgencyRepository.DataAccess.BeginTransaction();
                using (var connection = _AgencyRepository.DataAccess.GetActiveConnection(Database.MAIN))
                {
                    System.Data.SqlClient.SqlTransaction trx = _AgencyRepository.DataAccess.GetActiveTransaction(connection);
                    var agency = _AgencyRepository.GetList(Common.Database.MAIN,
                        AgencyDAO.GetAgencyById,
                        new {obj.AgencyId },
                        connection, trx
                        )
                        .FirstOrDefault();
                    agency.Company_name = obj.AgencyName;
                    agency.Agency_profile = obj.AgencyProfile;
                    agency.Phone = obj.Phone;
                    agency.Address1 = obj.Address1;
                    agency.City = obj.City;
                    agency.State = obj.State;
                    agency.Zip_code = obj.Zip_Code;
                    agency.Lat = obj.Lat;
                    agency.Lng = obj.Lng;
                    agency.Agency_site = obj.AgencySite;
                    agency.Agency_support_email = obj.AgencySupportEmail;
                   
                

                    if (!string.IsNullOrEmpty(obj.Base64Image))
                    {
                        string rootPath = _configuration["Web:DocumentPath"];
                        string encodeImage = obj.Base64Image.Replace("data:image/png;base64,", string.Empty);
                        byte[] imageBytes = Convert.FromBase64String(encodeImage);
                        string folderPath = $"\\Documents\\Customers\\{obj.AgencyId}";
                        string fullPath = $"{rootPath}\\Documents\\Customers\\{obj.AgencyId}";
                        if (!Directory.Exists(fullPath))
                            Directory.CreateDirectory(fullPath);
                        string fileName = Guid.NewGuid().ToString() + ".jpg";

                        File.WriteAllBytes(Path.Combine(fullPath, fileName), imageBytes);
                        agency.Photo_path = Path.Combine(folderPath, fileName);
                    }

                    _AgencyRepository.Update(agency, connection, trx);

                    var feUser = _UserRepository.GetList(Common.Database.MAIN, UserDAO.GetUserById, new { UserId = agency.User_id }, connection, trx).FirstOrDefault();
                    feUser.User_name = obj.AgencyName;
                    _UserRepository.Update(feUser, connection, trx);
                    _AgencyRepository.DataAccess.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Agency update failed, please try again" + ex);
            }
            return true;
        }

        public AgencyDTO GetFeAgencyById(AgencyDTO obj)
        {
            var customer = _AgencyRepository.GetList(Database.MAIN, AgencyDAO.GetAgencyById, new {obj.AgencyId }).FirstOrDefault();
            AgencyDTO response = _mapper.Map<Fe_agency, AgencyDTO>(customer);
            return response;
        }

        public AgencyBankDetailsDTO AddBankDetail(AgencyBankDetailsDTO obj)
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

                    // Map and create bank detail record
                    Fe_agency_bank_details bankDetail = _mapper.Map<AgencyBankDetailsDTO, Fe_agency_bank_details>(obj);
                    bankDetail.Created_by = actorId;

                    _AgencyBankDetailsRepository.Insert(bankDetail, connection, trx);

                    _UserRepository.DataAccess.CommitTransaction();
                }
            }
            catch (Exception exc)
            {
                _UserRepository.DataAccess.RollbackTransaction();
                throw new Exception("Failed to add bank detail: please contact administrator" + exc.Message, exc);
            }
            finally
            {
                _UserRepository.DataAccess.Close();
            }

            return obj;
        }

        public AgencyJobsDetailDTO GetAgencyJobsDetail(RegisterAgencyRequestDTO obj)
        {
            var agencyDetail = _JobContractRepositoy.GetList(Database.MAIN, AgencyDAO.GetAgencyJobDetail, new { obj.AgencyId }).FirstOrDefault();
            var agencyJobsDetail = _mapper.Map<Fe_job_contract, AgencyJobsDetailDTO>(agencyDetail);
            var agencyFeedBack = _JobContractRepositoy.GetList(Database.MAIN, AgencyDAO.GetAgencyJobFeedBack, new { AgencyId = obj.AgencyId }).ToList();
            agencyJobsDetail.AgencyFeedBack  = _mapper.Map<List<Fe_job_contract>, List<AgencyJobsFeedBack>>(agencyFeedBack);
            return agencyJobsDetail;
        }

        public IEnumerable<AgencyBankDetailsDTO> GetAgencyCard(AgencyBankDetailsDTO obj)
        {
            var agencyBankDetail = _AgencyBankDetailsRepository.GetList(Database.MAIN, AgencyDAO.GetAgencyBankDetail, new { AgencyId = obj.AgencyId });

            var bankDetail = _mapper.Map<IEnumerable<Fe_agency_bank_details>, IEnumerable<AgencyBankDetailsDTO>>(agencyBankDetail);

            return bankDetail;
        }

        public AgencyEarningDTO GetAgencyEarning(AgencyEarningDTO obj)
        {
            var agencyEarning = _PaymentRepository.GetList(Database.MAIN, AgencyDAO.GetAgencyEarning, new { AgencyId = obj.AgencyId }).FirstOrDefault();
            return _mapper.Map<Fe_payment, AgencyEarningDTO>(agencyEarning);
        }
    }






}
