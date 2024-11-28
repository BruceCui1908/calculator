using System.Text.Json;
using System.Text.Json.Serialization;
using Calculator.Models;

namespace Calculator.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
        // Configure the JSON options to ignore null properties
        _jsonOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }


    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            // Proceed to the next middleware
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred during the request.");
            httpContext.Response.StatusCode = 200;
            httpContext.Response.ContentType = "application/json";

            // Serialize and return the response
            var jsonResponse =
                JsonSerializer.Serialize(CommonApiResponse<Object>.Error(ex.Message, 500), _jsonOptions);
            await httpContext.Response.WriteAsync(jsonResponse);
        }
    }
}