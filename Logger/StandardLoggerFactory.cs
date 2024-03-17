namespace Ksh.Logger;

public class StandardLoggerFactory(ILogMessageFormatter formatter) : ILoggerFactory
{
    private readonly IList<ILogMessagePropagator> _propagators = [];

    public void AddConsoleLogger() => AddPropagator(new ConsoleLoggerPropagator(formatter));

    public void AddFileLogger(string pathToLogFile)
        => AddPropagator(new FileLoggerPropagator(formatter, pathToLogFile));

    public void AddPropagator(ILogMessagePropagator propagator)
    {
        _propagators.Add(propagator);
    }

    public void SetFormatter(ILogMessageFormatter formatter1)
    {
        formatter = formatter1;
    }

    public ILogger CreateLogger()
    {
        return new StandardLogger(_propagators);
    }

    public IEnumerable<ILogMessagePropagator> MessagePropagators => _propagators;
    public ILogMessageFormatter MessageFormatter => formatter;
}