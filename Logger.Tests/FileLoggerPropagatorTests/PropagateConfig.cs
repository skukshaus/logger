namespace Ksh.Logger.Tests.FileLoggerPropagatorTests;

public partial class FileLoggerPropagatorTest
{
    [Fact] public void PropagateC_UsingConfigurationMustAlsoLoggedIfMatch()
    {
        // Arrange
        var message = new LogMessage("hello world");
        var sut = new FileLoggerPropagator(
            new() {
                OutputFile = LogFileName,
                Filter = LogSeverity.Info,
                Verbosity = LogSeverity.Debug,
                Formatter = _formatter
            }
        );

        // Act
        var output = sut.Propagate(message);

        // Assert
        output.Should().Contain("hello world");
    }

    [Fact] public void PropagateC_UsingConfigurationMustIgnoreIfFilterDoesNotMatchButVerbosity()
    {
        // Arrange
        var message = new LogMessage("hello world", LogSeverity.Warn);
        var sut = new FileLoggerPropagator(
            new() {
                OutputFile = LogFileName,
                Filter = LogSeverity.Info,
                Verbosity = LogSeverity.Debug,
                Formatter = _formatter
            }
        );

        // Act
        var output = sut.Propagate(message);

        // Assert
        output.Should().BeEmpty();
    }

    [Fact] public void PropagateC_UsingConfigurationMustIgnoreIfVerbosityDoesNotMatchButFilter()
    {
        // Arrange
        var message = new LogMessage("hello world");
        var sut = new FileLoggerPropagator(
            new() {
                OutputFile = LogFileName,
                Filter = LogSeverity.Info,
                Verbosity = LogSeverity.Warn,
                Formatter = _formatter
            }
        );

        // Act
        var output = sut.Propagate(message);

        // Assert
        output.Should().BeEmpty();
    }

    [Fact] public void PropagateC_CustomFormatterMustBeUsedIfProvided()
    {
        // Arrange
        var formatter = new Mock<ILogMessageFormatter>();
        formatter
            .Setup(x => x.Format(It.IsAny<LogMessage>()))
            .Returns("foobar");

        var message = new LogMessage("hello world");
        var sut = new FileLoggerPropagator(
            new() {
                OutputFile = LogFileName,
                Filter = LogSeverity.Info,
                Verbosity = LogSeverity.Debug,
                Formatter = formatter.Object
            }
        );

        // Act
        var output = sut.Propagate(message);

        // Assert
        output.Should().Be("foobar");
    }

    [Fact] public void PropagateC_DefaultFormatterMustBeUsedIfNullWasProvided()
    {
        // Arrange
        var message = new LogMessage("hello world");
        var sut = new FileLoggerPropagator(
            new() {
                OutputFile = LogFileName,
                Filter = LogSeverity.Info,
                Verbosity = LogSeverity.Debug,
                Formatter = null
            }
        );

        // Act
        var output = sut.Propagate(message);

        // Assert
        output.Should().Contain("hello world");
    }
}