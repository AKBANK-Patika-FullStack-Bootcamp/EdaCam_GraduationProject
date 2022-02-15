using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
        public class BaseResponse<T>
        {
            public string Message { get; set; }
            public bool Success { get; set; }
            public T Data { get; set; }
        }

        public class BaseResponse : BaseResponse<object> { }
}
