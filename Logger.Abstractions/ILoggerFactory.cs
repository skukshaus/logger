namespace Ksh.Logger.Abstractions;

public interface ILoggerFactory
{
    ILoggerFactory AddConsoleLogger(
        ILogMessageFormatter? formatter = null,
        LogSeverity? verbosity = null,
        LogSeverity? filter = null
    );

    ILoggerFactory AddConsoleLogger(LogPropagationConfiguration? configuration);

    ILoggerFactory AddFileLogger(string pathToLogFile,
        ILogMessageFormatter? formatter = null,
        LogSeverity? verbosity = null,
        LogSeverity? filter = null
    );

    ILoggerFactory AddFileLogger(FileLogPropagationConfiguration configuration);

    ILoggerFactory AddPropagator(ILogMessagePropagator propagator);

    ILoggerFactory SetFormatter(ILogMessageFormatter formatter);

    ILogger CreateLogger();
}