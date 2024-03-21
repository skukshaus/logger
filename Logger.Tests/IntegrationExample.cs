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
}