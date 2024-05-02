namespace Ksh.Logger;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class LoggerModule
{
    public static IServiceCollection AddKshLogger(this IServiceCollection services)
        => services.AddSingleton<ILogger, StandardLogger>()
            .AddSingleton<ILoggerFactory, StandardLoggerFactory>()
            .AddTransient<ILogMessageFormatter, StandardLogMessageFormatter>();
}