namespace Ksh.Logger;

public class FileLoggerPropagator(ILogMessageFormatter formatter, string pathToLogFile) : ILogMessagePropagator
{
    private bool _isPathValidated;

    public FileLoggerPropagator(string pathToLogFile) : this(new StandardLogMessageFormatter(), pathToLogFile)
    {
    }


    public string Propagate(LogMessage message, LogPropagationConfiguration? config)
    {
        ValidatePath();

        var formattedMessage = formatter.Format(message);

        if (!string.IsNullOrWhiteSpace(pathToLogFile))
        {
            File.AppendAllText(pathToLogFile, formattedMessage + Environment.NewLine, Encoding.UTF8);
            
            return formattedMessage;
        }

        return string.Empty;
    }

    public string Propagate(LogMessage message) => throw new NotImplementedException();

    public ILogMessageFormatter GetFormatter() => throw new NotImplementedException();

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