namespace Ksh.Logger.Abstractions;

public interface ILoggerFactory
{
    ILoggerFactory AddConsoleLogger(ILogMessageFormatter? formatter = null);

    ILoggerFactory AddFileLogger(string pathToLogFile, ILogMessageFormatter? formatter = null);

    ILoggerFactory AddPropagator(ILogMessagePropagator propagator);

    ILoggerFactory SetFormatter(ILogMessageFormatter formatter);

    ILogger CreateLogger();
}