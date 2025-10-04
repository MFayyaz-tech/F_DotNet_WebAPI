using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helper
{

    public class SPResult
    {
        public int ERROR_CODE { get; set; }
        public string ERROR_DESCRIPTION { get; set; }
        public byte[] Timestamp { get; set; }
        public string Record_Id { get; set; }
        public object Extra_Param { get; set; }
        public long Id { get; set; }
    }
}
