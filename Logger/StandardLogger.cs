using System.Collections.Immutable;

namespace Ksh.Logger;

public sealed class StandardLogger(IEnumerable<ILogMessagePropagator> propagators) : ILogger
{
    private bool _isTurnedOff;

    public bool TurnOff()
    {
        if (!_isTurnedOff)
        {
            _isTurnedOff = true;
            return true;
        }
        
        return false;
    }

    public bool TurnOn()
    {
        if (_isTurnedOff)
        {
            _isTurnedOff = false;
            return true;
        }
        
        return false;
    }

    public IEnumerable<string> Trace<T>(T message) => Log(message, LogSeverity.Trace);

    public IEnumerable<string> Debug<T>(T message) => Log(message, LogSeverity.Debug);

    public IEnumerable<string> Info<T>(T message) => Log(message, LogSeverity.Info);

    public IEnumerable<string> Warn<T>(T message) => Log(message, LogSeverity.Warn);

    public IEnumerable<string> Warn<T>(T message, Exception exn) => Log(message, LogSeverity.Warn, exn);

    public IEnumerable<string> Error<T>(T message, Exception exn) => Log(message, LogSeverity.Error, exn);

    public IEnumerable<string> Fatal<T>(T message, Exception exn) => Log(message, LogSeverity.Fatal, exn);

    public IEnumerable<string> Log(LogMessage message)
    {
        if (_isTurnedOff)
        {
            return ImmutableArray<string>.Empty;
        }

        var output = new HashSet<string>();
        foreach (var propagator in propagators)
        {
            var plainMessage = propagator.Propagate(message);

            output.Add(plainMessage);
        }

        output.Remove(string.Empty);
        
        return output;
    }

    private IEnumerable<string> Log<T>(T? message, LogSeverity severity)
    {
        var safeMessage = GetMessageOrDefault(message);

        return Log(new(safeMessage, severity));
    }

    private IEnumerable<string> Log<T>(T? message, LogSeverity severity, Exception exn)
    {
        var safeMessage = GetMessageOrDefault(message);

        return Log(new(safeMessage, severity, exn));
    }

    private string GetMessageOrDefault<T>(T? message) => message?.ToString() ?? "[null]";
}