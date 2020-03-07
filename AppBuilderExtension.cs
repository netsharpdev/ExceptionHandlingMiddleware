using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Netsharpdev.ExceptionHandlingMiddleware
{
    public static class AppBuilderExtension
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder applicationBuilder) => UseExceptionHandlingMiddleware(null);
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder applicationBuilder, string errorMessage)
        {
            applicationBuilder.UseMiddleware<ExceptionHandlingMiddleware>(errorMessage);
            return applicationBuilder;
        }
    }
}
