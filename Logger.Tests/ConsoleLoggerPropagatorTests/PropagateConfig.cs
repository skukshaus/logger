namespace Ksh.Logger.Tests.ConsoleLoggerPropagatorTests;

public partial class ConsoleLoggerPropagatorTest
{
    [Fact] public void PropagateC_UsingConfigurationMustAlsoLoggedIfMatch()
    {
        // Arrange
        var message = new LogMessage("hello world");
        var sut = new ConsoleLoggerPropagator(
            new() {
                Filter = LogSeverity.Info,
                Verbosity = LogSeverity.Debug
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
        var sut = new ConsoleLoggerPropagator(
            new() {
                Filter = LogSeverity.Info,
                Verbosity = LogSeverity.Debug
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
        var message = new LogMessage("hello world", LogSeverity.Info);
        var sut = new ConsoleLoggerPropagator(
            new() {
                Filter = LogSeverity.Info,
                Verbosity = LogSeverity.Warn
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
        formatter.Setup(x => x.Format(It.IsAny<LogMessage>())).Returns("foobar");

        var message = new LogMessage("hello world");
        var sut = new ConsoleLoggerPropagator(new() { Formatter = formatter.Object });

        // Act
        var output = sut.Propagate(message);

        // Assert
        output.Should().Be("foobar");
    }

    [Fact] public void PropagateC_DefaultFormatterMustBeUsedIfNullWasProvided()
    {
        // Arrange
        var message = new LogMessage("hello world");
        var sut = new ConsoleLoggerPropagator(new() { Formatter = null });

        // Act
        var output = sut.Propagate(message);

        // Assert
        output.Should().Contain("hello world");
    }
}