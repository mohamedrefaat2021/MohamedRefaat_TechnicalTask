using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Helper
{
    public class ServiceResponse<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string MessageCode { get; set; }
        public int ResponseCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> ValidationIssue { get; set; }
        public T Data { get; set; }

        public static ServiceResponse<T> Fail(string errorMessage = "", string messageCode = "")
        {
            return new ServiceResponse<T> { Succeeded = false, ResponseCode = (int)HTTPStatusCodes.Error, MessageCode = messageCode, Message = string.IsNullOrEmpty(errorMessage) ? "Something went wrong Internal Server Error!" : errorMessage };
        }

        public static ServiceResponse<T> ValidationException(List<string> validation, string errorMessage = "")
        {
            return new ServiceResponse<T> { Succeeded = false, ResponseCode = (int)HTTPStatusCodes.BadRequest, ValidationIssue = validation, Message = string.IsNullOrEmpty(errorMessage) ? "Validation Exception!" : errorMessage };
        }

        public static ServiceResponse<T> NotFound(string errorMessage = "")
        {
            throw new Exception(string.IsNullOrEmpty(errorMessage) ? "No Record Found!" : errorMessage);


        }
        public static ServiceResponse<T> Success(T data, string message = "")
        {
            return new ServiceResponse<T> { Succeeded = true, Data = data, ResponseCode = (int)HTTPStatusCodes.Success, Message = string.IsNullOrEmpty(message) ? "Request Success" : message };
        }
        public static ServiceResponse<T> Created(T data, string message = "")
        {
            return new ServiceResponse<T> { Succeeded = true, Data = data, ResponseCode = (int)HTTPStatusCodes.Created, Message = string.IsNullOrEmpty(message) ? "Successfully Created" : message };
        }
        public static ServiceResponse<T> Updated(string message = "")
        {
            return new ServiceResponse<T> { Succeeded = true, ResponseCode = (int)HTTPStatusCodes.Updated, Message = string.IsNullOrEmpty(message) ? "Successfully Updated" : message };
        }

        public static ServiceResponse<T> NoResults(string errorMessage = "")
        {
            return new ServiceResponse<T>
            {
                Succeeded = true,
                ResponseCode = (int)HTTPStatusCodes.NotFound,
                Message = string.IsNullOrEmpty(errorMessage) ? "No Results Found Matchs Your Request - Invalid Request!" : errorMessage
            };
        }


    }
}
