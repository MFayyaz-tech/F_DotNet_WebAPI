using Dapper.Contrib.Extensions;
using Entities.Base;
using System;

namespace Entities.Setup
{
    [Table("Role_Permission")]
    public class Role_Permission : BaseEntity
    {
        [Key]
        public long Role_permission_id { get; set; }
        public long Role_id { get; set; }
        public long Permission_id { get; set; }
        public bool Is_deleted { get; set; }
        public bool Is_active { get; set; }
    }
}
