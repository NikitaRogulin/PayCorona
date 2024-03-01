using System.Reflection;
using System.Text.Json;
using PayCorona.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;

namespace PayCorona.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<LoggingMiddleware> logger)
    {
        var request = context.Request;
        var endpoint = context.GetEndpoint();
        if (endpoint == null)
        {
            await _next(context);
            return;
        }

        var requestMessage = $"Request: {endpoint.DisplayName}, headers: {JsonSerializer.Serialize(request.Headers)}, query: {JsonSerializer.Serialize(request.Query)}";
        logger.LogInformation(requestMessage);

        Stream originalBody = context.Response.Body;
        using var memStream = new MemoryStream();
        context.Response.Body = memStream;
        
        await _next(context);
        
        try
        {
            memStream.Position = 0;
            string responseBody = new StreamReader(memStream).ReadToEnd();

            memStream.Position = 0;
            await memStream.CopyToAsync(originalBody);
            logger.LogInformation($"Response: {endpoint.DisplayName}, headers: {JsonSerializer.Serialize(context.Request.Headers)}, body: {responseBody}");
        }
        finally
        {
            context.Response.Body = originalBody;
        }
    }
}
