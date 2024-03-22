namespace Ksh.Logger.Abstractions.DataClasses;

public record LogPropagationConfiguration
{
    public ILogMessageFormatter? Formatter { get; init; }
    
    /// <summary>
    /// If defined, will not log any entries with a lower priority 
    /// </summary>
    public LogSeverity? Verbosity { get; init; }
    
    /// <summary>
    /// If defined, will not log any other log messages than specified. 
    /// </summary>
    public LogSeverity? Filter { get; init; }
}

public record FileLogPropagationConfiguration : LogPropagationConfiguration
{
    public required string OutputFile { get; init; }
}