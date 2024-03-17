namespace Ksh.Logger.Tests.StandardLoggerFactoryTests;

public partial class StandardLoggerFactoryTest
{
    [Fact]
    public void AddConsoleLogger()
    {
        // Arrange

        // Act
        _factory.AddConsoleLogger();

        // Assert
        using var _ = new AssertionScope();

        _factory.MessagePropagators.Should().HaveCount(1);
        _factory.MessagePropagators.First().Should().BeOfType(typeof(ConsoleLoggerPropagator));
    }
    
    [Fact]
    public void AddFileLogger()
    {
        // Arrange

        // Act
        _factory.AddFileLogger("fancy.log");

        // Assert
        using var _ = new AssertionScope();

        _factory.MessagePropagators.Should().HaveCount(1);
        _factory.MessagePropagators.First().Should().BeOfType(typeof(FileLoggerPropagator));
    }

    [Fact]
    public void AddPropagator()
    {
        // Arrange

        // Act
        _factory.AddPropagator(new InternalPropagator());

        // Assert
        using var _ = new AssertionScope();

        _factory.MessagePropagators.Should().HaveCount(1);
        _factory.MessagePropagators.First().Should().BeOfType(typeof(InternalPropagator));
    }

    [Fact]
    public void AddMultiplePropagators()
    {
        // Arrange

        // Act
        _factory.AddConsoleLogger();
        _factory.AddFileLogger("fancy.log");
        _factory.AddPropagator(new InternalPropagator());

        // Assert
        using var _ = new AssertionScope();

        _factory.MessagePropagators.Should().SatisfyRespectively(
            p => p.Should().BeOfType(typeof(ConsoleLoggerPropagator)),
            p => p.Should().BeOfType(typeof(FileLoggerPropagator)),
            p => p.Should().BeOfType(typeof(InternalPropagator))
        );
    }
}