namespace Ksh.Logger.Tests.StandardLoggerFactoryTests;

public partial class StandardLoggerFactoryTest
{
    [Fact]
    public void SetFormatter_Default()
    {
        // Arrange

        // Act
        // nothing to act with, because it will be injected via ctor
        
        // Assert
        using var _ = new AssertionScope();
        _factory.MessageFormatter.Should().NotBeNull();
        _factory.MessageFormatter.Should().BeOfType<StandardLogMessageFormatter>();
    }
    
    [Fact]
    public void SetFormatter_CustomFormatter()
    {
        // Arrange
        
        // Act
        _factory.SetFormatter(new InternalFormatter());
        
        // Assert
        using var _ = new AssertionScope();
        _factory.MessageFormatter.Should().NotBeNull();
        _factory.MessageFormatter.Should().BeOfType<InternalFormatter>();
    }
    
    
}