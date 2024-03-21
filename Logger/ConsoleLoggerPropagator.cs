namespace Ksh.Logger;

public class ConsoleLoggerPropagator(ILogMessageFormatter formatter) : LogMessagePropagatorBase(formatter)
{
    private readonly ILogMessageFormatter _formatter = formatter;

    public ConsoleLoggerPropagator() : this(new StandardLogMessageFormatter())
    {
    }

    public override string Propagate(LogMessage message, LogPropagationConfiguration? config)
    {
        var formattedMessage = _formatter.Format(message);
        Console.WriteLine(formattedMessage);

        return formattedMessage;
    }
}