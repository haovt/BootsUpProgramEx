using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace NetCoreMVCApp.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class FirstLevelMiddleware
    {
        private readonly RequestDelegate _next;

        public FirstLevelMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.HasFormContentType)
            {
                Console.WriteLine("Read request data from FirstLevelMiddleware:");
                var form = httpContext.Request.Form;
                foreach (var key in form.Keys)
                {
                    Console.WriteLine($"${key}: {form[key]}");
                }
            }

            return _next(httpContext);
        }
    }
}
