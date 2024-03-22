using Ksh.Logger.Abstractions.Exceptions;

namespace Ksh.Logger.Tests.FileLoggerPropagatorTests;

public partial class FileLoggerPropagatorTest
{
    [Fact] public void Propagate_AssignedFile_MustWrite()
    {
        // Arrange
        var sut = new FileLoggerPropagator(LogFileName, formatter: _formatter);

        // Act
        var entry = sut.Propagate(new("Hello World"));

        // Assert
        using var _ = new AssertionScope();
        entry.Should().Be("Hello World");
    }

    [Fact] public void Propagate_TwoMessages_WriteBoth()
    {
        // Arrange
        var sut = new CustomFileLogger(LogFileName, formatter: _formatter);

        // Act
        sut.Propagate(new("Hello World"));
        sut.Propagate(new("Bye World"));

        // Assert
        using var _ = new AssertionScope();
        sut.LogMessages.Should().Equal("Hello World", "Bye World");
    }

    [Fact] public void Propagate_HelloWorld_WithDefaultFormatter()
    {
        // Arrange
        var message = new LogMessage("hello world");
        var sut = new FileLoggerPropagator(LogFileName);

        // Act
        var entry = sut.Propagate(message);

        // Assert
        using var _ = new AssertionScope();

        entry.Should().Match("[ info @ 20??-??-??T??:??:??] hello world*");
    }

    [Fact] public void Propagate_WriteIntoMalformedFile_ThrowException()
    {
        // Arrange
        var message = new LogMessage("hello world");
        var sut = new FileLoggerPropagator("malformed.<$>.log");

        // Act
        var entry = sut.Invoking(x => x.Propagate(message));

        // Assert
        using var _ = new AssertionScope();

        entry.Should().ThrowExactly<LoggerException>().WithInnerException<IOException>();
    }

    [Fact] public void Propagate_WriteIntoNullFile_ThrowException()
    {
        // Arrange
        var message = new LogMessage("hello world");
        var sut = new FileLoggerPropagator(null!, verbosity: LogSeverity.Trace);

        // Act
        var entry = sut.Invoking(x => x.Propagate(message));

        // Assert
        using var _ = new AssertionScope();

        entry.Should().ThrowExactly<LoggerException>().WithInnerException<ArgumentNullException>();
    }
}