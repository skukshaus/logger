namespace Ksh.Logger;

public class StandardLogMessageFormatter : ILogMessageFormatter
{
    public string Format(LogMessage message)
    {
        var severity = SeverityToName(message.Severity);

        var result = new StringBuilder();
        result.Append('[');
        result.Append(severity.PadLeft(5));
        result.Append(" @ ");
        result.Append(message.TimeOfDay.ToString("s"));
        result.Append(']');
        
        if (!string.IsNullOrWhiteSpace(message.Scope))
        {
            result.Append('[');
            result.Append(message.Scope);
            result.Append(']');
        }

        result.Append(' ');
        result.Append(message.Message);
        result.Append(FormatException(message.Exception));
        

        return result.ToString();
    }

    private StringBuilder FormatException(Exception? exception)
    {
        if (exception == null)
            return new();

        const string prefix = " ~> ";

        var error = new StringBuilder();
        error.Append(Environment.NewLine);
        error.Append(prefix);
        error.Append(exception.GetType().FullName);
        error.Append(": ");
        error.Append(exception.Message);

        if (!string.IsNullOrWhiteSpace(exception.StackTrace))
        {
            foreach (var line in exception.StackTrace.Split(Environment.NewLine))
            {
                error.Append(Environment.NewLine);
                error.Append(prefix);
                error.Append(' ');
                error.Append(' ');
                error.Append(line.Trim());
            }
        }

        if (exception.InnerException != null)
        {
            error.Append(Environment.NewLine);
            error.Append(prefix.PadRight(80, '-'));
            error.Append(FormatException(exception.InnerException));
        }

        return error;
    }


    private string SeverityToName(LogSeverity severity)
        => severity switch {
            LogSeverity.Trace => "trace",
            LogSeverity.Debug => "debug",
            LogSeverity.Info => "info",
            LogSeverity.Warn => "warn",
            LogSeverity.Error => "error",
            LogSeverity.Fatal => "fatal",
            _ => "unkwn"
        };
}