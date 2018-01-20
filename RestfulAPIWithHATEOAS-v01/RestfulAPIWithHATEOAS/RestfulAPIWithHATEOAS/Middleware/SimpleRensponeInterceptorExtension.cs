using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAPIWithHATEOAS.Middleware
{
    /// <summary>
    /// Extension class for registration
    /// </summary>
    public static class SimpleRensponeInterceptorExtension
    {
        public static IApplicationBuilder UseSimpleRensponeInterceptor(
         this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SimpleRensponeInterceptor>();
        }
    }
}