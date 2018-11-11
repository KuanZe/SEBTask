using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SEBTask.Models
{
    public class ApiClientResult<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Body { get; set; }

        public ApiClientResult(HttpStatusCode statusCode, T body)
        {
            StatusCode = statusCode;
            Body = body;
        }
    }
}
