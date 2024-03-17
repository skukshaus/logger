namespace Ksh.Logger.Abstractions;

public interface ILogger
{
    void TurnOff();
    void TurnOn();

    void Log(LogMessage message);
    void Trace<T>(T message);
    void Debug<T>(T message);
    void Info<T>(T message);
    void Warn<T>(T message);
    void Warn<T>(T message, Exception exn);
    void Error<T>(T message, Exception exn);
    void Fatal<T>(T message, Exception exn);
}