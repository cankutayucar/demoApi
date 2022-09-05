using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    public class ExceptionMidelWare
    {
        private RequestDelegate _next;

        public ExceptionMidelWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HnadleExceptionAsync(httpContext, e);
                throw;
            }
        }

        private Task HnadleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Internal Server Error";
            IEnumerable<ValidationFailure> errors;
            if (exception.GetType() == typeof(ValidationException))
            {
                message = exception.Message;
                errors = ((ValidationException)exception).Errors;
                return httpContext.Response.WriteAsync(new ValidationErrorException
                {
                    Errors = errors,
                    Message = message,
                    StatusCode = 400
                }.ToString());
            }

            return httpContext.Response.WriteAsync(new ErrorHandlingException
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }
}
