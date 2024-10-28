using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Application.Dtos.ResponseObjects
{
    public class ApiDataResponse<T> : ApiResponse
    {
        public new T Data { get; set; }

        public ApiDataResponse() { }

        public ApiDataResponse(T data, string message = null)
        {
            Succeeded = true;
            Data = data;
            Message = message;
        }

        public static ApiDataResponse<T> Success(T data, string message = "Success")
        {
            return new ApiDataResponse<T> { Succeeded = true, Data = data, Message = message };
        }

        public static ApiDataResponse<T> Failed(T data, string message = "Failure", List<string> errors = null)
        {
            return new ApiDataResponse<T> { Succeeded = false, Data = data, Message = message, Errors = errors };
        }
    }
}
