using Dapper.Contrib.Extensions;
using Entities.Base;
using System;

namespace Entities.Setup
{
    [Table("Role")]
    public class Role : BaseEntity
    {
        [Key]
        public long Role_id { get; set; }
        public string Role_name { get; set; }
        public string Description { get; set; }
        public bool Approval_permission { get; set; }
        public string Role_type { get; set; }
        public bool Is_default { get; set; }
        public bool Is_deleted { get; set; }
        public bool Is_active { get; set; }
       
    }
}
