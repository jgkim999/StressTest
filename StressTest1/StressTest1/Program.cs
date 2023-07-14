using Microsoft.EntityFrameworkCore;

using StressTest1.Middlewares;
using StressTest1.Models;

using Serilog;
using StressTest1.HealthCheck;

internal class Program
{
    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Async(a => a.Console())
            .CreateLogger();
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IMessageWriter, LoggingMessageWriter>();
            builder.Services.AddHealthChecks().AddCheck<SampleHealthCheck>(
                "Sample",
                failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded,
                tags: new[] { "sample" });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRequestInjectMiddleware();
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapHealthChecks("/healthz");
            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}