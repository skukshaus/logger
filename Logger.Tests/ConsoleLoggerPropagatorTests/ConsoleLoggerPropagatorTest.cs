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

    [Fact]
    public void Propagate_HelloWorld_MustBePrintToConsole()
    {
        // Arrange
        using var console = new ConsoleObserver();
        var message = new LogMessage("hello world");

        // Act
        _propagator.Propagate(message);

        // Assert
        using var _ = new AssertionScope();

        console.Output.Should().Be(
            """
            hello world

            """
        );
    }


    [Fact]
    public void Propagate_HelloWorld_WithDefaultFormatter()
    {
        // Arrange
        using var console = new ConsoleObserver();
        var message = new LogMessage("hello world");
        var sut = new ConsoleLoggerPropagator();

        // Act
        sut.Propagate(message);

        // Assert
        using var _ = new AssertionScope();

        console.Output.Should().Match("[ info @ 20??-??-??T??:??:??] hello world*");
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