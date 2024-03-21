namespace Ksh.Logger;

public class ConsoleLoggerPropagator(ILogMessageFormatter? formatter) : LogMessagePropagatorBase(formatter)
{
    private readonly ILogMessageFormatter? _formatter = formatter;

    public ConsoleLoggerPropagator() : this(new StandardLogMessageFormatter())
    {
    }

    public override string Propagate(LogMessage message, LogPropagationConfiguration? config)
    {
        if (EntryCanBeIgnored(message, config))
            return "";

        var entry = GetFormattedLogEntry(message, config);
        
        Console.WriteLine(entry);

        return entry;
    }

    private string GetFormattedLogEntry(LogMessage message, LogPropagationConfiguration? config)
    {
        var formatter = config?.LogMessageFormatter ?? _formatter ?? new StandardLogMessageFormatter();

        return formatter.Format(message);
    }
}