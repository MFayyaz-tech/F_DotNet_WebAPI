using System;

namespace DTO.DTOs.Base
{
    public class BaseDTO
    {  
        public DateTime? CreateDate { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public string EncUserID { get; set; }
        public string EncRoleID { get; set; }
		public DateTime? UpdateDate { get; set; }
	}
}
