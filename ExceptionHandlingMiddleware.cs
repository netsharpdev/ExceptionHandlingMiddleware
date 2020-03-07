using System;
using System.Dynamic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Netsharpdev.ExceptionHandlingMiddleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _errorMessage;

        public ExceptionHandlingMiddleware(RequestDelegate next, string errorMessage)
        {
            _next = next;
            _errorMessage = errorMessage ?? "We are sorry, but something went wrong.";
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            dynamic errorObject = new ExpandoObject();
            errorObject.error = _errorMessage;
            errorObject.exception = exception;

            string result = JsonConvert.SerializeObject(
                errorObject,
                new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            return context.Response.WriteAsync(result);
        }
    }
}
