namespace Ksh.Logger.Tests.FileLoggerPropagatorTests;

public partial class FileLoggerPropagatorTest
{
    [Theory]
    [InlineData(LogSeverity.Trace)]
    [InlineData(LogSeverity.Debug)]
    [InlineData(LogSeverity.Info)]
    [InlineData(LogSeverity.Warn)]
    [InlineData(LogSeverity.Error)]
    [InlineData(LogSeverity.Fatal)]
    public void PropagateF_RequestedMessagesMustAlwaysBeTracked(LogSeverity filter)
    {
        // Arrange
        var message = new LogMessage("hello world", filter);
        var sut = new FileLoggerPropagator(LogFileName, filter: filter);
        
        // Act
        var output = sut.Propagate(message);

        // Assert
        output.Should().Contain("hello world");
    }

    [Theory]
    [InlineData(LogSeverity.Debug)]
    [InlineData(LogSeverity.Info)]
    [InlineData(LogSeverity.Warn)]
    [InlineData(LogSeverity.Error)]
    [InlineData(LogSeverity.Fatal)]
    public void PropagateF_TraceShouldBeMutedIfItIsIrrelevant(LogSeverity filter)
    {
        // Arrange
        var message = new LogMessage("hello world", LogSeverity.Trace);
        var sut = new FileLoggerPropagator(LogFileName, filter: filter);
        
        // Act
        var output = sut.Propagate(message);

        // Assert
        output.Should().BeEmpty();
    }

    [Theory]
    [InlineData(LogSeverity.Trace)]
    [InlineData(LogSeverity.Info)]
    [InlineData(LogSeverity.Warn)]
    [InlineData(LogSeverity.Error)]
    [InlineData(LogSeverity.Fatal)]
    public void PropagateF_DebugShouldBeMutedIfItIsIrrelevant(LogSeverity filter)
    {
        // Arrange
        var message = new LogMessage("hello world", LogSeverity.Debug);
        var sut = new FileLoggerPropagator(LogFileName, filter: filter);
        
        // Act
        var output = sut.Propagate(message);

        // Assert
        output.Should().BeEmpty();
    }

    [Theory]
    [InlineData(LogSeverity.Trace)]
    [InlineData(LogSeverity.Debug)]
    [InlineData(LogSeverity.Warn)]
    [InlineData(LogSeverity.Error)]
    [InlineData(LogSeverity.Fatal)]
    public void PropagateF_InfoShouldBeMutedIfItIsIrrelevant(LogSeverity filter)
    {
        // Arrange
        var message = new LogMessage("hello world", LogSeverity.Info);
        var sut = new FileLoggerPropagator(LogFileName, filter: filter);
        
        // Act
        var output = sut.Propagate(message);

        // Assert
        output.Should().BeEmpty();
    }

    [Theory]
    [InlineData(LogSeverity.Trace)]
    [InlineData(LogSeverity.Debug)]
    [InlineData(LogSeverity.Info)]
    [InlineData(LogSeverity.Error)]
    [InlineData(LogSeverity.Fatal)]
    public void PropagateF_WarnShouldBeMutedIfItIsIrrelevant(LogSeverity filter)
    {
        // Arrange
        var message = new LogMessage("hello world", LogSeverity.Warn);
        var sut = new FileLoggerPropagator(LogFileName, filter: filter);
        
        // Act
        var output = sut.Propagate(message);

        // Assert
        output.Should().BeEmpty();
    }

    [Theory]
    [InlineData(LogSeverity.Trace)]
    [InlineData(LogSeverity.Debug)]
    [InlineData(LogSeverity.Info)]
    [InlineData(LogSeverity.Warn)]
    [InlineData(LogSeverity.Fatal)]
    public void PropagateF_ErrorShouldBeMutedIfItIsIrrelevant(LogSeverity filter)
    {
        // Arrange
        var message = new LogMessage("hello world", LogSeverity.Error);
        var sut = new FileLoggerPropagator(LogFileName, filter: filter);
        
        // Act
        var output = sut.Propagate(message);

        // Assert
        output.Should().BeEmpty();
    }

    [Theory]
    [InlineData(LogSeverity.Trace)]
    [InlineData(LogSeverity.Debug)]
    [InlineData(LogSeverity.Info)]
    [InlineData(LogSeverity.Warn)]
    [InlineData(LogSeverity.Error)]
    public void PropagateF_FatalShouldBeMutedIfItIsIrrelevant(LogSeverity filter)
    {
        // Arrange
        var message = new LogMessage("hello world", LogSeverity.Fatal);
        var sut = new FileLoggerPropagator(LogFileName, filter: filter);
        
        // Act
        var output = sut.Propagate(message);

        // Assert
        output.Should().BeEmpty();
    }
}