namespace Talabat.Error
{
    public class ApiExceptionResponse:ApiResponse
    {
        //Property
        public string Details { get; set; }


        public ApiExceptionResponse(int statusCode, string message = null, string details = null):base(statusCode, message)
        {
            Details = details;
        }


    }
}
