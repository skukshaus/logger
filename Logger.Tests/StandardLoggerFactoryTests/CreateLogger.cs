namespace Ksh.Logger.Tests.StandardLoggerFactoryTests;

public partial class StandardLoggerFactoryTest
{
    [Fact]
    public void CreateLogger()
    {
        // Arrange
        
        // Act
        var inst = _factory.CreateLogger();
        
        // Assert
        using var _ = new AssertionScope();
        inst.Should().NotBeNull();
        inst.Should().BeOfType<StandardLogger>();
    }
}