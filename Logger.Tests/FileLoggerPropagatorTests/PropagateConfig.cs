namespace Ksh.Logger.Tests.FileLoggerPropagatorTests;

public partial class FileLoggerPropagatorTest
{
    [Fact] public void PropagateC_UsingConfigurationMustAlsoLoggedIfMatch()
    {
        // Arrange
        var message = new LogMessage("hello world");
        var conf = new LogPropagationConfiguration {
            LogSeverityFilter = LogSeverity.Info,
            LogSeverityVerbosity = LogSeverity.Debug
        };

        // Act
        var output = _propagator.Propagate(message, conf);

        // Assert
        output.Should().Contain("hello world");
    }

    [Fact] public void PropagateC_UsingConfigurationMustIgnoreIfFilterDoesNotMatchButVerbosity()
    {
        // Arrange
        var message = new LogMessage("hello world", LogSeverity.Warn);
        var conf = new LogPropagationConfiguration {
            LogSeverityFilter = LogSeverity.Info,
            LogSeverityVerbosity = LogSeverity.Debug
        };

        // Act
        var output = _propagator.Propagate(message, conf);

        // Assert
        output.Should().BeEmpty();
    }

    [Fact] public void PropagateC_UsingConfigurationMustIgnoreIfVerbosityDoesNotMatchButFilter()
    {
        // Arrange
        var message = new LogMessage("hello world", LogSeverity.Info);
        var conf = new LogPropagationConfiguration {
            LogSeverityFilter = LogSeverity.Info,
            LogSeverityVerbosity = LogSeverity.Warn
        };

        // Act
        var output = _propagator.Propagate(message, conf);

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
        var conf = new LogPropagationConfiguration { LogMessageFormatter = formatter.Object };

        // Act
        var output = _propagator.Propagate(message, conf);

        // Assert
        output.Should().Be("foobar");
    }

    [Fact] public void PropagateC_DefaultFormatterMustBeUsedIfNullWasProvided()
    {
        // Arrange
        var message = new LogMessage("hello world");
        var conf = new LogPropagationConfiguration { LogMessageFormatter = null };

        // Act
        var output = _propagator.Propagate(message, conf);

        // Assert
        output.Should().Contain("hello world");
    }
}