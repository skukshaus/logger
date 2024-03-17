namespace Ksh.Logger;

[ExcludeFromCodeCoverage] [SuppressMessage("ReSharper", "InconsistentNaming")]
public static class LoggerModule
{
    public static IServiceCollection AddKshLogger(this IServiceCollection services)
        => services.AddScoped<ILogger, StandardLogger>()
            .AddScoped<ILoggerFactory, StandardLoggerFactory>()
            .AddTransient<ILogMessageFormatter, StandardLogMessageFormatter>();
}