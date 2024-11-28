using System.Text.Json.Serialization;
using Calculator.Controllers;
using Calculator.Middleware;
using Calculator.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;

namespace Calculator;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // treat enum as string
                options.JsonSerializerOptions.IgnoreNullValues = true; // Ignores properties with null values
            });

        builder.Services.AddValidatorsFromAssemblyContaining<CalculationValidator>();
        builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();


        builder.Services.AddSwaggerGen();

        // Register services to the container.
        builder.Services.RegisterServices();

        // Configure Serilog to Log to console
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();

        var app = builder.Build();

        // Use global exception handler middleware
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}