namespace Ksh.Logger;

public abstract class LogMessagePropagatorBase(
    ILogMessageFormatter? formatter = null,
    LogSeverity? verbosity = null,
    LogSeverity? filter = null
) : ILogMessagePropagator
{
    public string Propagate(LogMessage message)
    {
        if (EntryCanBeIgnored(message))
            return "";

        try
        {
            var entry = GetFormattedLogEntry(message);

            HandleLogMessage(entry);

            return entry;
        }
        catch (ArgumentNullException ex)
        {
            throw new LoggerException("Provided pathname for the logger is null!", ex);
        }
    }

    protected abstract void HandleLogMessage(string message);

    private bool EntryCanBeIgnored(LogMessage message)
    {
        message.Deconstruct(out _, out var severity, out _, out _);

        return verbosity > severity || filter != null && filter != severity;
    }

    private string GetFormattedLogEntry(LogMessage message)
    {
        var formatterOrDefault = formatter ?? new StandardLogMessageFormatter();

        return formatterOrDefault.Format(message);
    }
}