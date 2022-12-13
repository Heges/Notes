﻿using Microsoft.AspNetCore.Builder;

namespace Notes.WebApi.Middleware
{
    public static class CustomExceptionHandleMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
