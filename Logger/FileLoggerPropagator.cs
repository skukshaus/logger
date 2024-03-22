namespace Ksh.Logger;

public class FileLoggerPropagator(
    string pathToLogFile,
    ILogMessageFormatter? formatter = null,
    LogSeverity? verbosity = null,
    LogSeverity? filter = null
) : ILogMessagePropagator
{
    public FileLoggerPropagator(FileLogPropagationConfiguration config) : this(
        config.OutputFile,
        config.Formatter,
        config.Verbosity,
        config.Filter
    )
    {
    }

    public string Propagate(LogMessage message)
    {
        if (EntryCanBeIgnored(message))
            return "";

        try
        {
            var entry = GetFormattedLogEntry(message);

            WriteToFile(entry);
            return entry;
        }
        catch (ArgumentNullException ex)
        {
            throw new LoggerException("Provided pathname for the logger is null!", ex);
        }
        catch (Exception ex)
        {
            throw new LoggerException($"Cannot write to file [{pathToLogFile}]", ex);
        }
    }

    protected virtual void WriteToFile(string message)
        => File.AppendAllText(pathToLogFile, message + Environment.NewLine, Encoding.UTF8);

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