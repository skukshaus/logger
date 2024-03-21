namespace Ksh.Logger.Tests.StandardLoggerFactoryTests;

public partial class StandardLoggerFactoryTest
{
    private readonly StandardLoggerFactory _factory;

    public StandardLoggerFactoryTest()
    {
        var formatter = new StandardLogMessageFormatter();

        _factory = new(formatter);
    }

    private class InternalPropagator : ILogMessagePropagator
    {
        public string Propagate(LogMessage message, LogPropagationConfiguration? config) => throw new NotImplementedException();

        public ILogMessageFormatter GetFormatter() => throw new NotImplementedException();
    }

    private class InternalFormatter : ILogMessageFormatter
    {
        public string Format(LogMessage message) => "";
    }
}