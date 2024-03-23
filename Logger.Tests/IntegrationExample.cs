namespace Ksh.Logger.Tests;

public class IntegrationExample
{
    [Fact] public void ServiceCollectionUsage()
    {
        var services = new ServiceCollection();
        services.AddKshLogger();

        services.AddSingleton<ILogger>(
            srv => srv.GetService<ILoggerFactory>()!
                .AddConsoleLogger()
                .AddFileLogger("path.to.log")
                .CreateLogger()
        );

        var kernel = services.BuildServiceProvider();

        var logger = kernel.GetService<ILogger>()!;

        logger.Info("hello world");
    }

    [Fact] public void FactoryUsage()
    {
        var logger = new StandardLoggerFactory()
            .AddConsoleLogger()
            .AddFileLogger("path.to.log")
            .CreateLogger();

        logger.Info("hello world");
    }

    [Fact] public void UsageWithFilterAndVerbosity()
    {
        var logger = new StandardLoggerFactory()
            // will log info and above
            .AddConsoleLogger(
                new() { Verbosity = LogSeverity.Info }
            )
            // will log only infos
            .AddFileLogger(
                new() {
                    OutputFile = "info.log",
                    Filter = LogSeverity.Info
                }
            )
            // will track only warnings
            .AddFileLogger("warnings.log", filter: LogSeverity.Warn)
            // will tracks errors and fatals
            .AddFileLogger("errors.log", verbosity: LogSeverity.Error)
            // use some custom propagator
            .CreateLogger();

        logger.Info("hello world");
    }
}