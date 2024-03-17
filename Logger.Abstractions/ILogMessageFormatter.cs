namespace Ksh.Logger.Abstractions;

public interface ILogMessageFormatter
{
    string Format(LogMessage message);
}