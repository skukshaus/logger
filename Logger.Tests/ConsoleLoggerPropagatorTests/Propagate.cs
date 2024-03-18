namespace Ksh.Logger.Tests.ConsoleLoggerPropagatorTests;

public partial class ConsoleLoggerPropagatorTest
{
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
