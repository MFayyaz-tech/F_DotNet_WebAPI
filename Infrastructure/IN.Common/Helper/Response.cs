using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helper
{
    public enum ResponseStatus
    {
        Success,
        Error
    }

    public class Response<T>
    {
        public Response()
        {
            Result = string.Empty;
        }

        public ResponseStatus Status { get; set; }
        public string Result { get; set; }
        public List<T> Data { get; set; }

    }
}
