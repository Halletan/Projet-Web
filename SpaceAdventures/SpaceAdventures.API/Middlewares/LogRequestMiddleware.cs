using System.Diagnostics;

namespace SpaceAdventures.API.Middlewares;

public class LogRequestMiddleware
{
    private readonly RequestDelegate _next;

    public LogRequestMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        await using var buffer = new MemoryStream();
        var request = httpContext.Request;
        var response = httpContext.Response;
        var stream = response.Body;
        response.Body = buffer;
        await _next(httpContext);
        Debug.WriteLine($"Request Content TYpe : {httpContext.Request.Headers["Accept"]} {Environment.NewLine}" +
                        $"Request Path : {request.Path} {Environment.NewLine} " +
                        $"Response Type : {response.ContentType} {Environment.NewLine}" +
                        $"Response Lenght : {response.ContentLength ?? buffer.Length}");
        buffer.Position = 0;
        await buffer.CopyToAsync(stream);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class LogRequestMiddlewareExtensions
{
    public static IApplicationBuilder UseLogRequestMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LogRequestMiddleware>();
    }
}