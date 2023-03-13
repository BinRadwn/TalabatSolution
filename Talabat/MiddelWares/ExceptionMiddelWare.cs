using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Error;

namespace Talabat.MiddelWares
{
    public class ExceptionMiddelWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddelWare> logger;
        private readonly IHostEnvironment evn;

        //Constructors
        public ExceptionMiddelWare(RequestDelegate Next, ILogger<ExceptionMiddelWare> logger, IHostEnvironment evn)
        {
            next = Next;
            this.logger = logger;
            this.evn = evn;
        }

        //Function 
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);


                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


                var errorResponse = evn.IsDevelopment() ?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    :
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);


                var option = new JsonSerializerOptions { PropertyNamingPolicy=JsonNamingPolicy.CamelCase};
                var Json = JsonSerializer.Serialize(errorResponse , option);
                await context.Response.WriteAsync(Json);

            }
        }
    }
}
