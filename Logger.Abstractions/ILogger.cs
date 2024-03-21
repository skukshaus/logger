namespace Ksh.Logger.Abstractions;

public interface ILogger
{
    bool TurnOff();
    bool TurnOn();

    IEnumerable<string> Log(LogMessage message);
    IEnumerable<string> Trace<T>(T message);
    IEnumerable<string> Debug<T>(T message);
    IEnumerable<string> Info<T>(T message);
    IEnumerable<string> Warn<T>(T message);
    IEnumerable<string> Warn<T>(T message, Exception exn);
    IEnumerable<string> Error<T>(T message, Exception exn);
    IEnumerable<string> Fatal<T>(T message, Exception exn);
}