using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ExceptionMidleWareExtensions
    {
        public static void ConfigureCustomExceptionMidleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMidelWare>();
        }
    }
}
