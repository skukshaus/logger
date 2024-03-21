namespace Ksh.Logger.Abstractions.DataClasses;

public record LogPropagationConfiguration(
    ILogMessageFormatter? LogMessageFormatter = null,
    LogSeverity? LogSeverityVerbosity = null,
    LogSeverity? LogSeverityFilter = null
);