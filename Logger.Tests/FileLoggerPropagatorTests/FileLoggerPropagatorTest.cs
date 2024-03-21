namespace Ksh.Logger.Tests.FileLoggerPropagatorTests;

public partial class FileLoggerPropagatorTest : IDisposable
{
    private readonly FileLoggerPropagator _propagator;
    private const string LogFileName = "mock.log";

    public FileLoggerPropagatorTest()
    {
        var formatter = new Mock<ILogMessageFormatter>();
        formatter
            .Setup(x => x.Format(It.IsAny<LogMessage>()))
            .Returns((LogMessage x) => x.Message);

        _propagator = new(LogFileName, formatter.Object);
    }

    private string GetContent() => File.ReadAllText(LogFileName);

    public void Dispose()
    {
        if (File.Exists(LogFileName))
        {
            File.Delete(LogFileName);
        }
        
        GC.SuppressFinalize(this);
    }
}