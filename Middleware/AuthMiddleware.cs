using System.Reflection;
using PayCorona.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace PayCorona.Middleware;

public class AuthMiddleware
{
    public const string TokenHeaderName = "token";
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ISessionRepository sessionRepository)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint == null)
        {
            await _next(context);
            return;
        }
        
        var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
        var methodInfo = actionDescriptor?.MethodInfo;
        var attribute = methodInfo?.GetCustomAttribute(typeof(OurAuthAttribute), true);
        if (attribute == null)
        {
            await _next(context);
            return;
        }
       
        var tokenExist = context.Request.Headers.TryGetValue(TokenHeaderName, out var token);
        if(!tokenExist) 
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Error 401");
            return;
        }

        var session = sessionRepository.GetSession(Guid.Parse(token));

        if (session == null || session.ExpireTime < DateTime.UtcNow)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Error 401");
            return;
        }
        await _next(context);
    }
}