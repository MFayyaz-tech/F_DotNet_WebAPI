using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helper
{
    public class Result
    {
        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
