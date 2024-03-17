namespace Ksh.Logger.Abstractions;

public interface ILogMessagePropagator
{
    void Propagate(LogMessage message);
}