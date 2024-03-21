namespace Ksh.Logger.Abstractions;

public interface ILogMessagePropagator
{
    string Propagate(LogMessage message, LogSeverity? logSeverityFilter = null, LogSeverity? logSeveritySwitch = null)
        => Propagate(
            message, new() {
                LogMessageFormatter = GetFormatter(),
                LogSeverityFilter = logSeverityFilter,
                LogSeveritySwitch = logSeveritySwitch
            }
        );
    
    string Propagate(LogMessage message) => Propagate(message, null);
    
    string Propagate(LogMessage message, LogPropagationConfiguration? config);
    

    ILogMessageFormatter GetFormatter();
}