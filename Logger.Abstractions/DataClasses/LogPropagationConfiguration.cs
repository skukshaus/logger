namespace Ksh.Logger.Abstractions.DataClasses;

public record LogPropagationConfiguration(
    ILogMessageFormatter? LogMessageFormatter = null,
    LogSeverity? LogSeverityFilter = null,
    LogSeverity? LogSeveritySwitch = null
);