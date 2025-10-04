using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Resource : BaseEntity
    {
        public string name_first { get; set; }
        public string name_last { get; set; }
        public string login_id { get; set; }
        public string encrypted_pwd { get; set; }
        
    }
}
