using System;
using Dapper.Contrib.Extensions;
using Entities.Base;

namespace DA.Entities.Notifications
{
   
    [Table("Fe_notification_tokens")]
    public class Fe_notifications_tokens : BaseEntity
    {

        [Key]
        public long Token_id { get; set; }
        public long User_id { get; set; }
        public string token { get; set; }

    }
}

