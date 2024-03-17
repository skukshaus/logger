namespace Ksh.Logger.Tests.StandardLoggerFactoryTests;

public partial class StandardLoggerFactoryTest
{
    private StandardLoggerFactory _factory;

    public StandardLoggerFactoryTest()
    {
        var formatter = new StandardLogMessageFormatter();

        _factory = new(formatter);
    }

    private class InternalPropagator : ILogMessagePropagator
    {
        public void Propagate(LogMessage message)
        {
        }
    }

    private class InternalFormatter : ILogMessageFormatter
    {
        public string Format(LogMessage message) => "";
    }
}