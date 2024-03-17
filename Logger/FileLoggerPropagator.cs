namespace Ksh.Logger;

public class FileLoggerPropagator(ILogMessageFormatter formatter, string? pathToLogFile) : ILogMessagePropagator
{
    private bool _isPathValidated;

    public FileLoggerPropagator(string? pathToLogFile) : this(new StandardLogMessageFormatter(), pathToLogFile)
    {
    }

    public void Propagate(LogMessage message)
    {
        ValidatePath();

        var formattedMessage = formatter.Format(message);

        if (!string.IsNullOrWhiteSpace(pathToLogFile))
        {
            File.AppendAllText(pathToLogFile, formattedMessage + Environment.NewLine, Encoding.UTF8);
        }
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