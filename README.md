# Ksh.Logger

[//]: # ([![devel]&#40;https://github.com/skukshaus/logger/actions/workflows/devel.yml/badge.svg&#41;]&#40;https://github.com/skukshaus/logger/actions/workflows/devel.yml&#41;)
[//]: # ([![main]&#40;https://github.com/skukshaus/logger/actions/workflows/main.yml/badge.svg&#41;]&#40;https://github.com/skukshaus/logger/actions/workflows/main.yml&#41;)

[![test](https://github.com/skukshaus/logger/actions/workflows/test.yml/badge.svg)](https://github.com/skukshaus/logger/actions/workflows/test.yml)
[![GitHub License](https://img.shields.io/github/license/skukshaus/logger)](https://github.com/skukshaus/logger/blob/main/LICENSE)

| package                 | version                                                                                                                         | downloads                                                                                                                        |
|-------------------------|---------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------|
| Ksh.Logger              | [![NuGet](https://img.shields.io/nuget/v/ksh.logger.svg)](https://www.nuget.org/packages/ksh.logger/)                           | [![NuGet](https://img.shields.io/nuget/dt/ksh.logger.svg)](https://www.nuget.org/packages/ksh.logger/)                           |
| Ksh.Logger.Abstractions | [![NuGet](https://img.shields.io/nuget/v/ksh.logger.abstractions.svg)](https://www.nuget.org/packages/ksh.logger.abstractions/) | [![NuGet](https://img.shields.io/nuget/dt/ksh.logger.abstractions.svg)](https://www.nuget.org/packages/ksh.logger.abstractions/) |

The world's most flexible and light-weight logger!

## Main contents

+ [Usage](#usage)
  + [LoggerFactory](#loggerfactory)
  + [ServiceCollection](#servicecollection)
+ [Customization](#customization)
  + [ILogMessagePropagator](#ilogmessagepropagator)
  + [ILogMessageFormatter](#ilogmessageformatter)
  + [LogSeverity](#logseverity)
  + [Filter and Verbosity](#filter-and-verbosity)

## Usage

### LoggerFactory

```csharp
var logger = new StandardLoggerFactory()
    .AddConsoleLogger()
    .AddFileLogger("path.to.log")
    .CreateLogger();

logger.Info("hello world");
```

### ServiceCollection

Please make sure, the package is installed already.
```
dotnet add package Microsoft.Extensions.DependencyInjection
```

You can then set up the logger and register the module. The logger can be added to any class you want after that.

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

You can now log in to a file or console. You can get rid of the console and file propagator if you want to.

## Customization

This logger, as mentioned, is extremely adaptable. If you're using a logging framework, you can let it handle the stuff
like formatting or so. After a while, you can switch to using only one logger.

There's a layer of abstraction that can be used independently or in UnitTests. That abstraction layer will make you 
future-proof.

### ILogMessagePropagator

This Logger's superpowers are propagators. You can notify anything you want. Simply use the ``ILogMessagePropagator`` 
Interface and do whatever you want with the message.

Every log message will utilize this method.

This example shows how to know if your service has stopped working and send an email when it happens.

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

You can put this propagator directly into the logger or into the factory.

```csharp
loggerFactory.AddPropagator(new CustomPropagator(emailClient));
```

### ILogMessageFormatter

Let's talk about the formatters. These sneaky guys are extremely helpful by changing the appearance of the message.

I think it is an edge case to use something different from what was implemented. But you can format any message
depending on your needs.

All built-in propagators are using the formatter. Custom propagators are not, but I highly recommend using them as
well. Just for consistency purposes.

````csharp
public class MyCustomLogMessageFormatter : ILogMessageFormatter
{
    public string Format(LogMessage message)
        => message.Exception == null
            ? $"{message.Severity} {message.TimeOfDay:s} {message.Message}"
            : $"{message.Severity} {message.TimeOfDay:s} {message.Message}{Environment.NewLine}{message.Exception}";
}
````

### LogSeverity

We have 6 different severities: ``Trace``, ``Debug``, ``Info``, ``Warn``, ``Error`` and ``Fatal``.

Of course, you can use these classifications as you like! From my experience, I would like to provide you with a 
recommendation. Maybe it is helpful. :-)

| severity  | recommendation                                                                                                                  |
|-----------|---------------------------------------------------------------------------------------------------------------------------------|
| ``Trace`` | You don't need it most of the time. It can be used if you want to log which method is currently being called. :eyes:            |
| ``Debug`` | I like to put some additional and noisy stuff into here. This is a good Log Level to save the state of the variables. :speaker: |
| ``Info``  | Any regular log message :speech_balloon:                                                                                        |
| ``Warn``  | Something strange is happening, but I (the app) can manage it :fearful:                                                         |
| ``Error`` | Something strange is happening, but the user can manage it :sos:                                                                |
| ``Fatal`` | Okay, I am dead :skull:                                                                                                         |


### Filter and Verbosity

Okay, real-talk. Who the heck is reading log files? Really, no one. They're only useful when you're looking into some 
issues. One full log file and some only for warnings, errors and so on are what I like to have.

````csharp
var logger = new StandardLoggerFactory()
    // will log debug and above
    .AddConsoleLogger(new() { 
        Verbosity = LogSeverity.Debug
    })
    // by default everything will be logged!
    .AddFileLogger("noisy.log")
    // will log only infos
    .AddFileLogger(new() {
        OutputFile = "info.log",
        Filter = LogSeverity.Info
    })
    // will track only warnings
    .AddFileLogger("warnings.log", filter: LogSeverity.Warn)
    // will tracks errors and fatals
    .AddFileLogger("errors.log", verbosity: LogSeverity.Error)
    // use some custom propagator
    .CreateLogger();
````