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

        _propagator = new(formatter.Object, LogFileName);
    }

    private string GetContent() => File.ReadAllText(LogFileName);

    ~FileLoggerPropagatorTest()
    {
        ReleaseUnmanagedResources();
    }

    private void ReleaseUnmanagedResources()
    {
        if (File.Exists(LogFileName))
        {
            File.Delete(LogFileName);
        }
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }
}