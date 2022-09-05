using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace Core.Extensions
{
    public class ErrorHandlingException
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }

    public class ValidationErrorException : ErrorHandlingException
    {
        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}
