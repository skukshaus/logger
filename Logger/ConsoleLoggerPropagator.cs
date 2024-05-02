namespace Ksh.Logger;

public class ConsoleLoggerPropagator(
    ILogMessageFormatter? formatter = null,
    LogSeverity? verbosity = null,
    LogSeverity? filter = null
) : LogMessagePropagatorBase(formatter, verbosity, filter)
{
    public ConsoleLoggerPropagator(LogPropagationConfiguration? config) :
        this(config?.Formatter, config?.Verbosity, config?.Filter)
    {
    }
    
    protected override void HandleLogMessage(string message)
    {
        Console.WriteLine(message);
    }
}