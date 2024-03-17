namespace Ksh.Logger.Abstractions;

public interface ILoggerFactory
{
    void AddConsoleLogger();

    void AddFileLogger(string pathToLogFile);

    void AddPropagator(ILogMessagePropagator propagator);

    void SetFormatter(ILogMessageFormatter formatter);

    ILogger CreateLogger();
}