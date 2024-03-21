namespace Ksh.Logger;

public class FileLoggerPropagator(string pathToLogFile, ILogMessageFormatter? formatter)
    : LogMessagePropagatorBase(formatter)
{
    private readonly ILogMessageFormatter? _formatter = formatter;

    private bool _isPathValidated;
    private bool _isPathValid;

    public FileLoggerPropagator(string pathToLogFile) : this(pathToLogFile, new StandardLogMessageFormatter())
    {
    }

    public override string Propagate(LogMessage message, LogPropagationConfiguration? config)
    {
        if (EntryCanBeIgnored(message, config))
            return "";

        if (!IsValidPath())
            return "";


        var entry = GetFormattedLogEntry(message, config);

        File.AppendAllText(pathToLogFile, entry + Environment.NewLine, Encoding.UTF8);

        return entry;
    }

    private bool IsValidPath()
    {
        if (_isPathValidated)
        {
            return true;
        }

        try
        {
            _ = new FileInfo(pathToLogFile);
            _isPathValid = true;
        }
        catch (SystemException ex)
        {
            Console.WriteLine(ex);
            _isPathValid = true;
        }
        finally
        {
            _isPathValidated = true;
        }

        return _isPathValid;
    }

    private string GetFormattedLogEntry(LogMessage message, LogPropagationConfiguration? config)
    {
        var formatter = config?.LogMessageFormatter ?? _formatter ?? new StandardLogMessageFormatter();

        return formatter.Format(message);
    }
}