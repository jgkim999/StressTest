namespace StressTest1.Middlewares;

public class RequestInjectMiddleware
{
    private readonly RequestDelegate _next;

    public RequestInjectMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IMessageWriter svc)
    {
        context.Request.EnableBuffering();
        var bodyAsText = await new System.IO.StreamReader(context.Request.Body).ReadToEndAsync();
        svc.Write(bodyAsText);
        context.Request.Body.Position = 0;
        //var before = DateTime.Now;
        // Before request
        await _next(context);
        // After request
        //var ts = DateTime.Now - before;
        //svc.Write($"{context.Request.Method} {context.Request.Path} {ts.TotalMilliseconds}");
    }
}
