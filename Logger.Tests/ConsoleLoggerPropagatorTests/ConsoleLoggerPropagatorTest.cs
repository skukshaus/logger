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

public class ConsoleObserver : IDisposable
{
    private readonly StringWriter _stringWriter = new();
    private readonly TextWriter _originalTextWriter;

    public ConsoleObserver()
    {
        _originalTextWriter = Console.Out;
        Console.SetOut(_stringWriter);
    }

    public string Output => _stringWriter.ToString();

    public void Dispose()
    {
        Console.SetOut(_originalTextWriter);
        _stringWriter.Dispose();
        GC.SuppressFinalize(this);
    }
}