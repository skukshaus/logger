namespace Ksh.Logger.Abstractions.DataClasses;

public record LogMessage(string Message, LogSeverity Severity = LogSeverity.Info, Exception? Exception = default)
{
    public DateTime TimeOfDay { get; init; } = DateTime.Now;

    public void Deconstruct(out string message, out LogSeverity severity, out Exception? exception, out DateTime time)
        => (message, severity, exception, time) = (Message, Severity, Exception, TimeOfDay);

    public virtual bool Equals(LogMessage? other)
    {
        var isEqual = true;

        isEqual &= Equals(Message, other?.Message);
        isEqual &= Equals(Severity, other?.Severity);
        isEqual &= Equals(Exception, other?.Exception);

        return isEqual;
    }

    public override int GetHashCode()
    {
        //unchecked
        {
            var number = 17;
            number = number * 23 + Message.GetHashCode();
            number = number * 23 + Severity.GetHashCode();
            number = number * 23 + Exception?.GetHashCode() ?? 0;

            return number;
        }
    }
}