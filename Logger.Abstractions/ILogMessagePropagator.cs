namespace Ksh.Logger.Abstractions;

public interface ILogMessagePropagator
{
    string Propagate(LogMessage message);
    string Propagate(LogMessage message, LogPropagationConfiguration? config);
    string Propagate(LogMessage message, LogSeverity? logSeverityFilter = null, LogSeverity? logSeveritySwitch = null);
}

public abstract class LogMessagePropagatorBase(ILogMessageFormatter? formatter) : ILogMessagePropagator
{
    public string Propagate(LogMessage message) => Propagate(message, null);

    public string Propagate(
        LogMessage message,
        LogSeverity? logSeverityFilter = null,
        LogSeverity? logSeveritySwitch = null
    )
        => Propagate(
            message, new() {
                LogMessageFormatter = formatter,
                LogSeverityFilter = logSeverityFilter,
                LogSeveritySwitch = logSeveritySwitch
            }
        );

    public abstract string Propagate(LogMessage message, LogPropagationConfiguration? config);
}