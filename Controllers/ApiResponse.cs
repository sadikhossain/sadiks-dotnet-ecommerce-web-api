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
        private ApiResponse(bool success, string message, T data, List<string> errors, int statuscode)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
            StatusCode = statuscode;
            TimeStamp = DateTime.UtcNow;
        }

        // static method for creating a successful response
        public static ApiResponse<T> SuccessResponse(T data, int statuscode, string message = "")
        {
            return new ApiResponse<T> (true, message, data, null, statuscode);
        }
        // constructor for error response
    }
}