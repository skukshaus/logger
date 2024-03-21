# Ksh.Logger

[//]: # ([![devel]&#40;https://github.com/skukshaus/logger/actions/workflows/devel.yml/badge.svg&#41;]&#40;https://github.com/skukshaus/logger/actions/workflows/devel.yml&#41;)
[//]: # ([![main]&#40;https://github.com/skukshaus/logger/actions/workflows/main.yml/badge.svg&#41;]&#40;https://github.com/skukshaus/logger/actions/workflows/main.yml&#41;)

[![test](https://github.com/skukshaus/logger/actions/workflows/test.yml/badge.svg)](https://github.com/skukshaus/logger/actions/workflows/test.yml)
[![GitHub License](https://img.shields.io/github/license/skukshaus/logger)](https://github.com/skukshaus/logger/blob/main/LICENSE)

| package                 | version                                                                                                                         | downloads                                                                                                                        |
|-------------------------|---------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------|
| Ksh.Logger              | [![NuGet](https://img.shields.io/nuget/v/ksh.logger.svg)](https://www.nuget.org/packages/ksh.logger/)                           | [![NuGet](https://img.shields.io/nuget/dt/ksh.logger.svg)](https://www.nuget.org/packages/ksh.logger/)                           |
| Ksh.Logger.Abstractions | [![NuGet](https://img.shields.io/nuget/v/ksh.logger.abstractions.svg)](https://www.nuget.org/packages/ksh.logger.abstractions/) | [![NuGet](https://img.shields.io/nuget/dt/ksh.logger.abstractions.svg)](https://www.nuget.org/packages/ksh.logger.abstractions/) |

The most flexible logger on the planet!

## Main contents

+ [LoggerFactory](#loggerfactory)
+ [ServiceCollection](#servicecollection)
+ [Customisation](#customization)
  + [Propagators](#propagators)

## LoggerFactory

```csharp
var logger = new StandardLoggerFactory()
    .AddConsoleLogger()
    .AddFileLogger("path.to.log")
    .CreateLogger();

logger.Info("hello world");
```
It looks simpler, but you have also the direct dependencies and violate the 
DIP (dependency inversion principle).

## ServiceCollection

Please make sure, the package is installed already.
```
dotnet add package Microsoft.Extensions.DependencyInjection
```

Afterwards you can register the module and configure the logger. Afterwards you
are able to inject the logger in any class you want.

```csharp
var services = new ServiceCollection();

services.AddKshLogger();
services.AddSingleton<ILogger>(
    srv => srv.GetService<ILoggerFactory>()!
        .AddConsoleLogger()
        .AddFileLogger("path.to.log")
        .CreateLogger()
);

// just for demo, you don't need this in production 
var kernel = services.BuildServiceProvider();
var logger = kernel.GetService<ILogger>()!;

logger.Info("hello world");
```

You are already ready to log into a file as well as into console. Of course, 
you can skip the console or the file logger if you wish.

## Customization

As mentioned, this Logger is very flexible. It is even possible to connect 
multiple existing logging frameworks. 

### Propagators

The power of this Logger are the Propagators 

Here is an example, how to be notified if your crashed and reported a fatal 
error.

```csharp
public class CustomPropagator(IEmailClient emailClient) : ILogMessagePropagator
{
    public void Propagate(LogMessage message)
    {
        if (message.Severity == LogSeverity.Fatal)
        {
            emailClient.SendEmail(message.Message);
        }
    }
}
```

Just put this propagator into your factory or directly into the logger.

```csharp
loggerFactory.AddPropagator(new CustomPropagator(emailClient));
```