using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Passenger.Infrastructure.Exceptions;

namespace Passenger.Api.Framework
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorCode = "error";
            var statusCode = HttpStatusCode.BadRequest; 
            var exceptionType = exception.GetType();
            switch(exception)
            {
                case Exception e when exceptionType == typeof(UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case ServiceException e when exceptionType == typeof(ServiceException):
                    statusCode = HttpStatusCode.BadRequest;
                    errorCode = e.Code;
                    break;    
                case Exception e when exceptionType == typeof(Exception):
                    statusCode = HttpStatusCode.InternalServerError;
                    break;                       
            }

            var response = new { code = errorCode, message = exception.Message };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(payload);
        }        
    }
}