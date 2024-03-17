namespace Ksh.Logger;

public sealed class StandardLogger(IEnumerable<ILogMessagePropagator> propagators) : ILogger
{
    private bool _isTurnedOff;

    public void TurnOff() => _isTurnedOff = true;

    public void TurnOn() => _isTurnedOff = false;

    public void Trace<T>(T message) => Log(message, LogSeverity.Trace);

    public void Debug<T>(T message) => Log(message, LogSeverity.Debug);

    public void Info<T>(T message) => Log(message, LogSeverity.Info);

    public void Warn<T>(T message) => Log(message, LogSeverity.Warn);

    public void Warn<T>(T message, Exception exn) => Log(message, LogSeverity.Warn, exn);

    public void Error<T>(T message, Exception exn) => Log(message, LogSeverity.Error, exn);

    public void Fatal<T>(T message, Exception exn) => Log(message, LogSeverity.Fatal, exn);

    public void Log(LogMessage message)
    {
        if (_isTurnedOff)
        {
            return;
        }

        foreach (var propagator in propagators)
        {
            propagator.Propagate(message);
        }
    }

    private void Log<T>(T? message, LogSeverity severity)
    {
        var safeMessage = GetMessageOrDefault(message);

        Log(new(safeMessage, severity));
    }

    private void Log<T>(T? message, LogSeverity severity, Exception exn)
    {
        var safeMessage = GetMessageOrDefault(message);

        Log(new(safeMessage, severity, exn));
    }

    private string GetMessageOrDefault<T>(T? message) => message?.ToString() ?? "[null]";
}