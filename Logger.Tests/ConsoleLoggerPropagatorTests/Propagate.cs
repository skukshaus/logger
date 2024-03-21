namespace Ksh.Logger.Tests.ConsoleLoggerPropagatorTests;

public partial class ConsoleLoggerPropagatorTest
{
    [Fact] public void Propagate_HelloWorld_MustBePrintToConsole()
    {
        // Arrange
        var message = new LogMessage("hello world");

        // Act
        var msg = _propagator.Propagate(message);

        // Assert
        msg.Should().Contain("hello world");
    }

    [Fact] public void Propagate_HelloWorld_WithDefaultFormatter()
    {
        // Arrange
        var message = new LogMessage("hello world");
        var sut = new ConsoleLoggerPropagator();

        // Act
        var msg = sut.Propagate(message);

        // Assert
        msg.Should().Match("[ info @ 20??-??-??T??:??:??] hello world*");
    }
}