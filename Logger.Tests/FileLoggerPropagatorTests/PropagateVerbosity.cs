namespace Ksh.Logger.Tests.FileLoggerPropagatorTests;

public partial class FileLoggerPropagatorTest
{
    [Theory] [InlineData(LogSeverity.Trace)] [InlineData(LogSeverity.Debug)] [InlineData(LogSeverity.Info)]
    public void PropagateV_TraceDebugAndInfoMustAllLogInfos(LogSeverity verbosity)
    {
        // Arrange
        var message = new LogMessage("hello world");
        var sut = new FileLoggerPropagator(LogFileName, verbosity: verbosity);

        // Act
        var output = sut.Propagate(message);

        // Assert
        output.Should().Contain("hello world");
    }

    [Theory] [InlineData(LogSeverity.Warn)] [InlineData(LogSeverity.Error)] [InlineData(LogSeverity.Fatal)]
    public void PropagateV_WarnErrorAndFatalMustAllIgnoreInfos(LogSeverity verbosity)
    {
        // Arrange
        var message = new LogMessage("hello world");
        var sut = new FileLoggerPropagator(LogFileName, verbosity: verbosity);

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
    [InlineData(LogSeverity.Fatal)]
    public void PropagateV_FatalErrorsMustBeAlwaysBeCaught(LogSeverity verbosity)
    {
        // Arrange
        var message = new LogMessage("bye world", LogSeverity.Fatal);
        var sut = new FileLoggerPropagator(LogFileName, verbosity: verbosity);

        // Act
        var output = sut.Propagate(message);

        // Assert
        output.Should().Contain("bye world");
    }

    [Theory]
    [InlineData(LogSeverity.Debug)]
    [InlineData(LogSeverity.Info)]
    [InlineData(LogSeverity.Warn)]
    [InlineData(LogSeverity.Error)]
    [InlineData(LogSeverity.Fatal)]
    public void PropagateV_TracesCanBeIgnoredMostOfTheTime(LogSeverity verbosity)
    {
        // Arrange
        var message = new LogMessage("hello world", LogSeverity.Trace);
        var sut = new FileLoggerPropagator(LogFileName, verbosity: verbosity);

        // Act
        var output = sut.Propagate(message);

        // Assert
        output.Should().BeEmpty();
    }
}