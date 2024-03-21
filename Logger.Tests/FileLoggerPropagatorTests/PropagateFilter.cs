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
    public void PropagateF_RequestedMessagesMustAlwaysBeTracked(LogSeverity verbosity)
    {
        // Arrange
        var message = new LogMessage("hello world", verbosity);

        // Act
        var output = _propagator.Propagate(message, logFilter: verbosity);

        // Assert
        output.Should().Contain("hello world");
    }

    [Theory]
    [InlineData(LogSeverity.Debug)]
    [InlineData(LogSeverity.Info)]
    [InlineData(LogSeverity.Warn)]
    [InlineData(LogSeverity.Error)]
    [InlineData(LogSeverity.Fatal)]
    public void PropagateF_TraceShouldBeMutedIfItIsIrrelevant(LogSeverity verbosity)
    {
        // Arrange
        var message = new LogMessage("hello world", LogSeverity.Trace);

        // Act
        var output = _propagator.Propagate(message, logFilter: verbosity);

        // Assert
        output.Should().BeEmpty();
    }

    [Theory]
    [InlineData(LogSeverity.Trace)]
    [InlineData(LogSeverity.Info)]
    [InlineData(LogSeverity.Warn)]
    [InlineData(LogSeverity.Error)]
    [InlineData(LogSeverity.Fatal)]
    public void PropagateF_DebugShouldBeMutedIfItIsIrrelevant(LogSeverity verbosity)
    {
        // Arrange
        var message = new LogMessage("hello world", LogSeverity.Debug);

        // Act
        var output = _propagator.Propagate(message, logFilter: verbosity);

        // Assert
        output.Should().BeEmpty();
    }

    [Theory]
    [InlineData(LogSeverity.Trace)]
    [InlineData(LogSeverity.Debug)]
    [InlineData(LogSeverity.Warn)]
    [InlineData(LogSeverity.Error)]
    [InlineData(LogSeverity.Fatal)]
    public void PropagateF_InfoShouldBeMutedIfItIsIrrelevant(LogSeverity verbosity)
    {
        // Arrange
        var message = new LogMessage("hello world", LogSeverity.Info);

        // Act
        var output = _propagator.Propagate(message, logFilter: verbosity);

        // Assert
        output.Should().BeEmpty();
    }

    [Theory]
    [InlineData(LogSeverity.Trace)]
    [InlineData(LogSeverity.Debug)]
    [InlineData(LogSeverity.Info)]
    [InlineData(LogSeverity.Error)]
    [InlineData(LogSeverity.Fatal)]
    public void PropagateF_WarnShouldBeMutedIfItIsIrrelevant(LogSeverity verbosity)
    {
        // Arrange
        var message = new LogMessage("hello world", LogSeverity.Warn);

        // Act
        var output = _propagator.Propagate(message, logFilter: verbosity);

        // Assert
        output.Should().BeEmpty();
    }

    [Theory]
    [InlineData(LogSeverity.Trace)]
    [InlineData(LogSeverity.Debug)]
    [InlineData(LogSeverity.Info)]
    [InlineData(LogSeverity.Warn)]
    [InlineData(LogSeverity.Fatal)]
    public void PropagateF_ErrorShouldBeMutedIfItIsIrrelevant(LogSeverity verbosity)
    {
        // Arrange
        var message = new LogMessage("hello world", LogSeverity.Error);

        // Act
        var output = _propagator.Propagate(message, logFilter: verbosity);

        // Assert
        output.Should().BeEmpty();
    }

    [Theory]
    [InlineData(LogSeverity.Trace)]
    [InlineData(LogSeverity.Debug)]
    [InlineData(LogSeverity.Info)]
    [InlineData(LogSeverity.Warn)]
    [InlineData(LogSeverity.Error)]
    public void PropagateF_FatalShouldBeMutedIfItIsIrrelevant(LogSeverity verbosity)
    {
        // Arrange
        var message = new LogMessage("hello world", LogSeverity.Fatal);

        // Act
        var output = _propagator.Propagate(message, logFilter: verbosity);

        // Assert
        output.Should().BeEmpty();
    }

}