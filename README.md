# Ksh.Logger

[![test](https://github.com/skukshaus/logger/actions/workflows/test.yml/badge.svg)](https://github.com/skukshaus/logger/actions/workflows/test.yml)
[![devel](https://github.com/skukshaus/logger/actions/workflows/devel.yml/badge.svg)](https://github.com/skukshaus/logger/actions/workflows/devel.yml)
[![main](https://github.com/skukshaus/logger/actions/workflows/main.yml/badge.svg)](https://github.com/skukshaus/logger/actions/workflows/main.yml)
[![GitHub License](https://img.shields.io/github/license/skukshaus/logger)](https://github.com/skukshaus/logger/blob/main/LICENSE)

| package                 | version                                                                                                                         | downloads                                                                                                                        |
|-------------------------|---------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------|
| Ksh.Logger              | [![NuGet](https://img.shields.io/nuget/v/ksh.logger.svg)](https://www.nuget.org/packages/ksh.logger/)                           | [![NuGet](https://img.shields.io/nuget/dt/ksh.logger.svg)](https://www.nuget.org/packages/ksh.logger/)                           |
| Ksh.Logger.Abstractions | [![NuGet](https://img.shields.io/nuget/v/ksh.logger.abstractions.svg)](https://www.nuget.org/packages/ksh.logger.abstractions/) | [![NuGet](https://img.shields.io/nuget/dt/ksh.logger.abstractions.svg)](https://www.nuget.org/packages/ksh.logger.abstractions/) |

The most flexible logger on the planet!

## Main contents

* [Simple usage](#simple-usage)
* [Using with Dependency Injection](#using-with-microsoft-dependency-injection)
* [Customisation](#customization)
    * [Propagators](#propagators)

## Simple usage

`````csharp
var propagators = new List<ILogMessagePropagator> {
    new ConsoleLoggerPropagator(),
    new FileLoggerPropagator("path.to.log")
};
var logger = new StandardLogger(propagators);

logger.Info("hello world");
`````

It looks simpler, but you have also the direct dependencies and violate the DIP (dependency inversion principle).



## Using with Microsoft Dependency Injection

`````csharp
var services = new ServiceCollection();
services.AddKshLogger();

var kernel = services.BuildServiceProvider();
var loggerFactory = kernel.GetService<ILoggerFactory>()!;

loggerFactory.AddConsoleLogger();
loggerFactory.AddFileLogger("path.to.log");

var logger = loggerFactory.CreateLogger();
logger.Info("hello world");
`````
You are already ready to log into a file as well as into console. Of course, you can skip the console or the file 
logger if you wish.

## Customization

As mentioned, this Logger is very flexible. It is even possible to connect multiple existing logging frameworks. 

### Propagators

The power of this Logger are the Propagators 

Here is an example, how to be notified if your crashed and reported a fatal error.

`````csharp
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
`````

Just put this propagator into your factory or directly into the logger.
`````csharp
loggerFactory.AddPropagator(new CustomPropagator(emailClient));
`````