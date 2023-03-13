using System;

namespace Talabat.Error
{
    public class ApiResponse
    {
        //Property
        public int StatusCode { get; set; }
        public string Message { get; set; }

        //Constructor
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(StatusCode);
        }

        //Method
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return StatusCode switch
            {
                400 => "A bad request , you have made",
                401 => "Authorized , you are not",
                404 => "Resorce found , it was not",
                500 => "Server Error",
                _ => null
            };
        }
    }
}
