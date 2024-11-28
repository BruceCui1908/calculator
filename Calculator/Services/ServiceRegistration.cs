namespace Calculator.Services;

public static class ServiceRegistration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICalculatorService, CalculatorService>();
    }
}