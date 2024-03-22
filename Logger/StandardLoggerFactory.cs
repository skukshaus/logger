namespace Ksh.Logger;

public class StandardLoggerFactory : ILoggerFactory
{
    private readonly IList<ILogMessagePropagator> _propagators = [];
    private ILogMessageFormatter? _formatter;

    public IEnumerable<ILogMessagePropagator> MessagePropagators => _propagators;
    public ILogMessageFormatter MessageFormatter => GetFormatterSafe(_formatter);

    public StandardLoggerFactory()
    {
    }

    public StandardLoggerFactory(ILogMessageFormatter formatter)
    {
        _formatter = formatter;
    }

    public ILoggerFactory AddConsoleLogger(
        ILogMessageFormatter? formatter = null,
        LogSeverity? verbosity = null,
        LogSeverity? filter = null
    )
        => AddPropagator(
            new ConsoleLoggerPropagator(GetFormatterSafe(formatter), verbosity, filter)
        );

    public ILoggerFactory AddConsoleLogger(LogPropagationConfiguration? configuration)
        => AddPropagator(new ConsoleLoggerPropagator(configuration));

    public ILoggerFactory AddFileLogger(
        string pathToLogFile,
        ILogMessageFormatter? formatter = null,
        LogSeverity? verbosity = null,
        LogSeverity? filter = null
    )
        => AddPropagator(
            new FileLoggerPropagator(pathToLogFile, GetFormatterSafe(formatter), verbosity, filter)
        );

    public ILoggerFactory AddFileLogger(FileLogPropagationConfiguration configuration)
        => AddPropagator(new FileLoggerPropagator(configuration));


    public ILoggerFactory AddPropagator(ILogMessagePropagator propagator)
    {
        _propagators.Add(propagator);

        return this;
    }

    public ILoggerFactory SetFormatter(ILogMessageFormatter formatter1)
    {
        _formatter = formatter1;

        return this;
    }

    public ILogger CreateLogger()
    {
        return new StandardLogger(_propagators);
    }

    private ILogMessageFormatter GetFormatterSafe([NotNullWhen(true)] ILogMessageFormatter? formatter)
        => formatter ?? _formatter ?? new StandardLogMessageFormatter();
}