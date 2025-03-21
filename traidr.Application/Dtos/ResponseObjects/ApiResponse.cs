﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Application.Dtos.ResponseObjects
{
    public class ApiResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public object Data { get; set; }

        public ApiResponse(string message = null)
        {
            Succeeded = true;
            Message = message;
        }

        public static ApiResponse Success(object data)
        {
            return new ApiResponse { Succeeded = true, Message = "Success", Data = data };
        }

        public static ApiResponse Failed(object data, string message = "Failure", List<string> errors = null)
        {
            return new ApiResponse { Succeeded = false, Data = data, Message = message, Errors = errors };
        }
    }

}
