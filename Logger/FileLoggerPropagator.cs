namespace Ksh.Logger;

public class FileLoggerPropagator(
    string pathToLogFile,
    ILogMessageFormatter? formatter = null,
    LogSeverity? verbosity = null,
    LogSeverity? filter = null
) : LogMessagePropagatorBase(formatter, verbosity, filter)
{
    public FileLoggerPropagator(FileLogPropagationConfiguration config) : this(
        config.OutputFile,
        config.Formatter,
        config.Verbosity,
        config.Filter
    )
    {
    }

    protected override void HandleLogMessage(string message)
    {
        try
        {
            File.AppendAllText(pathToLogFile, message + Environment.NewLine, Encoding.UTF8);
        }
        catch (Exception ex)
        {
            throw new LoggerException($"Cannot write to file [{pathToLogFile}]", ex);
        }
    }
}