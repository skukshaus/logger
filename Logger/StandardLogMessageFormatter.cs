namespace Ksh.Logger;

public class StandardLogMessageFormatter : ILogMessageFormatter
{
    public string Format(LogMessage message)
    {
        var severity = SeverityToName(message.Severity);
        
        var output = $"[{severity,5} @ {message.TimeOfDay:s}] ";
        output += message.Message;
        output += FormatException(message.Exception);

        return output;
    }


    private string FormatException(Exception? exception)
    {
        if (exception == null)
            return "";

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
                error.Append($"{Environment.NewLine}{prefix}  {line.Trim()}");
            }
        }

        if (exception.InnerException != null)
        {
            error.Append(Environment.NewLine);
            error.Append(prefix.PadRight(80, '-'));
            error.Append(FormatException(exception.InnerException));
        }

        return error.ToString();
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