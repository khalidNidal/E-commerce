﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities
{
    public class ApiResponse
    {
        public int? StatusCode { get; set; }

        public bool  IsSuccess { get; set; }

        public string? Message { get; set; }

        public object? Result { get; set; }

        public ApiResponse(int ? statusCode =null , string? message=null, object?result = null)
        {
            StatusCode = statusCode;
            Message = message??getMessageForStatusCode(StatusCode);
            Result = result;
            IsSuccess = statusCode >= 200 && StatusCode<=300;

        }
        private string? getMessageForStatusCode(int? statusCode)
        {
            return statusCode switch
            {
                200 => "Successfuly",
                400 => "Bad Request",
                404 => "Not found",
                500 => "Internal Server Error",
                _ => null
            } ;
        }

    }
}
