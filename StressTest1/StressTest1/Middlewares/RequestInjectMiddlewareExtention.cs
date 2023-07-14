namespace StressTest1.Middlewares;

public static class RequestInjectMiddlewareExtention
{
    public static IApplicationBuilder UseRequestInjectMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestInjectMiddleware>();
    }
}
