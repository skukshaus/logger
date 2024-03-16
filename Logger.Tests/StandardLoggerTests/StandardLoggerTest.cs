namespace Kukshaus.Logger.Tests.StandardLoggerTests;

public class StandardLoggerTest
{
    private StandardLogger _systemUnderTest;

    public StandardLoggerTest()
    {
        _systemUnderTest = new();
    }

    [Fact] 
    public void AfterInitialisationInstanceIsNotNull()
    {
        // Arrange
        
        // Act
        
        // Assert
        using var _ = new AssertionScope();
        
        _systemUnderTest.Should().NotBeNull();
    }
}