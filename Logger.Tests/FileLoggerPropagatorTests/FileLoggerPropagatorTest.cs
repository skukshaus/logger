namespace Ksh.Logger.Tests.FileLoggerPropagatorTests;

public partial class FileLoggerPropagatorTest
{
    private readonly ILogMessageFormatter _formatter;
    private const string LogFileName = "mock.log";

    public FileLoggerPropagatorTest()
    {
        var formatterMock = new Mock<ILogMessageFormatter>();
        formatterMock
            .Setup(x => x.Format(It.IsAny<LogMessage>()))
            .Returns((LogMessage x) => x.Message);

        _formatter = formatterMock.Object;
    }

    private class CustomFileLogger(
        string pathToLogFile,
        ILogMessageFormatter? formatter = null,
        LogSeverity? verbosity = null,
        LogSeverity? filter = null
    ) : LogMessagePropagatorBase(formatter, verbosity, filter)
    {
        public List<string> LogMessages { get; } = [];

        // protected void HandleLogMessage(string message) => LogMessages.Add(message);
        protected override void HandleLogMessage(string message) => LogMessages.Add(message);
    }
}