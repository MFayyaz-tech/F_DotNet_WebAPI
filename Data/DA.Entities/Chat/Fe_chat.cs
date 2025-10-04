using Dapper.Contrib.Extensions;
using Entities.Base;
using System;

namespace DA.Entities.Chat
{
    [Table("fe_chat")]
    public class Fe_chat : BaseEntity
    {
        [Key]
        public long Chat_id { get; set; }
        public long Sender_id { get; set; }
        public long Receiver_id { get; set; }
        public string Message { get; set; }
        public bool? Is_read { get; set; }
        public string Message_type { get; set; }
        public bool? Is_active { get; set; }
        [Write(false)]
        [Computed]
        public string Sender_user_name { get; set; }
        [Write(false)]
        [Computed]
        public string Receiver_user_name { get; set; }

    }
}
