namespace Ksh.Logger.Tests.StandardLoggerFactoryTests;

public partial class StandardLoggerFactoryTest
{
    private readonly StandardLoggerFactory _factory;

    public StandardLoggerFactoryTest()
    {
        var formatter = new StandardLogMessageFormatter();

        _factory = new(formatter);
    }

    private class InternalPropagator(ILogMessageFormatter? formatter) : LogMessagePropagatorBase(formatter)
    {
        public override string Propagate(LogMessage message, LogPropagationConfiguration? config) => "";
    }

    private class InternalFormatter : ILogMessageFormatter
    {
        public string Format(LogMessage message) => "";
    }
}