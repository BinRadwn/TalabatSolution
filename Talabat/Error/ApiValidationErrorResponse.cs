using System.Collections.Generic;

namespace Talabat.Error
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        //Property
        public IEnumerable<string> Errors { get; set; }

        //Constructor
        public ApiValidationErrorResponse() : base(400)
        {

        }
    }
}
