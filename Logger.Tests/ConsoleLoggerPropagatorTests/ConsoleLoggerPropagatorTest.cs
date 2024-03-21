namespace Ksh.Logger.Tests.ConsoleLoggerPropagatorTests;

public partial class ConsoleLoggerPropagatorTest
{
    private readonly ConsoleLoggerPropagator _propagator;

    public ConsoleLoggerPropagatorTest()
    {
        var formatter = new Mock<ILogMessageFormatter>();
        formatter
            .Setup(x => x.Format(It.IsAny<LogMessage>()))
            .Returns((LogMessage x) => x.Message);

        _propagator = new(formatter.Object);
    }
}