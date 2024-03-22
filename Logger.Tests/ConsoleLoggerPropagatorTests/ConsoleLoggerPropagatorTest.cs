namespace Ksh.Logger.Tests.ConsoleLoggerPropagatorTests;

public partial class ConsoleLoggerPropagatorTest
{
    private readonly ILogMessageFormatter _formatter;

    public ConsoleLoggerPropagatorTest()
    {
        var formatterMock = new Mock<ILogMessageFormatter>();
        formatterMock
            .Setup(x => x.Format(It.IsAny<LogMessage>()))
            .Returns((LogMessage x) => x.Message);

        _formatter = formatterMock.Object;
    }
}