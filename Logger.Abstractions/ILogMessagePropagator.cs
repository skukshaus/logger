namespace Ksh.Logger.Abstractions;

public interface ILogMessagePropagator
{
    string Propagate(LogMessage message);
    string Propagate(LogMessage message, LogPropagationConfiguration? config);
    string Propagate(LogMessage message, LogSeverity? logVerbosity = null, LogSeverity? logFilter = null);
}