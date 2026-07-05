using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_net_ecommerce_web_api.Controllers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public int StatusCode { get; set; }
        public DateTime TimeStamp { get; set; }

        // constructor for successful response
        public ApiResponse(T data, int statuscode, string message = "")
        {
            Success = true;
            Message = message;
            Data = data;
            Errors = null;
            StatusCode = statuscode;
            TimeStamp = DateTime.UtcNow;
        }
        // constructor for error response
    }
}