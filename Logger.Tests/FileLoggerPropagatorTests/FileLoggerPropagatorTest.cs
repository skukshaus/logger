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
    ) : FileLoggerPropagator(pathToLogFile, formatter, verbosity, filter)
    {
        public List<string> LogMessages { get; } = [];

        protected override void WriteToFile(string message) => LogMessages.Add(message);
    }
}