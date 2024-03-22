namespace Ksh.Logger;

public class ConsoleLoggerPropagator(
    ILogMessageFormatter? formatter = null,
    LogSeverity? verbosity = null,
    LogSeverity? filter = null
) : ILogMessagePropagator
{
    public ConsoleLoggerPropagator(LogPropagationConfiguration? config) :
        this(config?.Formatter, config?.Verbosity, config?.Filter)
    {
    }

    public string Propagate(LogMessage message)
    {
        if (EntryCanBeIgnored(message))
            return "";

        var entry = GetFormattedLogEntry(message);

        Console.WriteLine(entry);

        return entry;
    }

    private bool EntryCanBeIgnored(LogMessage message)
    {
        message.Deconstruct(out _, out var severity, out _, out _);

        return verbosity > severity || filter != null && filter != severity;
    }

    private string GetFormattedLogEntry(LogMessage message)
    {
        var safeFormatter = formatter ?? new StandardLogMessageFormatter();

        return safeFormatter.Format(message);
    }
}