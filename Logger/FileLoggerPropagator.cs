namespace Ksh.Logger;

public class FileLoggerPropagator(string pathToLogFile, ILogMessageFormatter? formatter)
    : LogMessagePropagatorBase(formatter)
{
    private readonly ILogMessageFormatter? _formatter = formatter;
    
    private bool _isPathValidated;

    public FileLoggerPropagator(string pathToLogFile) : this(pathToLogFile, new StandardLogMessageFormatter())
    {
    }

    public override string Propagate(LogMessage message, LogPropagationConfiguration? config)
    {
        ValidatePath();

        var formattedMessage = _formatter.Format(message);

        if (!string.IsNullOrWhiteSpace(pathToLogFile))
        {
            File.AppendAllText(pathToLogFile, formattedMessage + Environment.NewLine, Encoding.UTF8);

            return formattedMessage;
        }

        return string.Empty;
    }

    private void ValidatePath()
    {
        if (_isPathValidated)
        {
            return;
        }

        _ = new FileInfo(pathToLogFile);
        _isPathValidated = true;
    }
}