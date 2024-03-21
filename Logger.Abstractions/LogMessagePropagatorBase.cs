namespace Ksh.Logger.Abstractions;

public abstract class LogMessagePropagatorBase(ILogMessageFormatter? formatter) : ILogMessagePropagator
{
    public string Propagate(LogMessage message) => Propagate(message, null);

    public string Propagate(LogMessage message, LogSeverity? logVerbosity = null, LogSeverity? logFilter = null)
        => Propagate(
            message, new() {
                LogMessageFormatter = formatter,
                LogSeverityFilter = logFilter,
                LogSeverityVerbosity = logVerbosity
            }
        );

    public abstract string Propagate(LogMessage message, LogPropagationConfiguration? config);

    protected virtual bool EntryCanBeIgnored(LogMessage message, LogPropagationConfiguration? config)
    {
        var safeConfig = config ?? new LogPropagationConfiguration();
        
        message.Deconstruct(out _, out var severity, out _, out _);
        safeConfig.Deconstruct(out _, out var verbosity, out var filter);

        return verbosity > severity || filter != null && filter != severity;
    }
}