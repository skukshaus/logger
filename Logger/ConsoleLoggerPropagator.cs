namespace Ksh.Logger;

public class ConsoleLoggerPropagator(ILogMessageFormatter formatter) : ILogMessagePropagator
{
    public ConsoleLoggerPropagator() : this(new StandardLogMessageFormatter())
    {
    }

    public void Propagate(LogMessage message)
    {
        var formattedMessage = formatter.Format(message);
        Console.WriteLine(formattedMessage);
    }
}