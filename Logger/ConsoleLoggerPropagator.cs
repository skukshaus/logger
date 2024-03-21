namespace Ksh.Logger;

public class ConsoleLoggerPropagator(ILogMessageFormatter formatter) : ILogMessagePropagator
{
    public ConsoleLoggerPropagator() : this(new StandardLogMessageFormatter())
    {
    }

    public string Propagate(LogMessage message)
    {
        var formattedMessage = formatter.Format(message);
        Console.WriteLine(formattedMessage);
        
        return formattedMessage;
    }

    public string Propagate(LogMessage message, LogPropagationConfiguration config)
    {
        throw new NotImplementedException();
    }

    public ILogMessageFormatter GetFormatter() => throw new NotImplementedException();
}