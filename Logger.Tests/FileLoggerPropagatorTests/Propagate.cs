namespace Ksh.Logger.Tests.FileLoggerPropagatorTests;

public partial class FileLoggerPropagatorTest
{
    [Fact]
    public void Propagate_AssignedFile_MustWrite()
    {
        // Arrange

        // Act
        _propagator.Propagate(new("Hello World"));

        // Assert
        using var _ = new AssertionScope();
        GetContent().Should().Be($"Hello World{Environment.NewLine}");
    }

    [Fact]
    public void Propagate_TwoMessages_WriteBoth()
    {
        // Arrange

        // Act
        _propagator.Propagate(new("Hello World"));
        _propagator.Propagate(new("Bye World"));

        // Assert
        using var _ = new AssertionScope();
        GetContent().Should().Be(
            """
            Hello World
            Bye World
            
            """
        );
    }
    
    
    [Fact]
    public void Propagate_HelloWorld_WithDefaultFormatter()
    {
        // Arrange
        var message = new LogMessage("hello world");
        var sut = new FileLoggerPropagator(LogFileName);

        // Act
        sut.Propagate(message);

        // Assert
        using var _ = new AssertionScope();

        GetContent().Should().Match("[ info @ 20??-??-??T??:??:??] hello world*");
    }
}