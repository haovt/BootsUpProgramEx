using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetCoreMVCApp.Exceptions
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                var usernameException = ex as UsernameException;
                if (usernameException != null)
                {
                    // Log related information for this kind of exception
                    var stream = context.Response.Body;
                    var problem = new ProblemDetails
                    {
                        Status = 500,
                        Title = "Your name has exception",
                        //Detail = details
                    };
                    //await JsonSerializer.SerializeAsync(stream, problem);
                }

                throw;
            }
        }
    }
}
