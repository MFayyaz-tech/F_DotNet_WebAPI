using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helper
{
    public class Request<T>
    {
        public T Data { get; set; }
        public string FilePath { get; set; }
        public Dictionary<string, string> Criteria { get; set; }
       
    }
}
