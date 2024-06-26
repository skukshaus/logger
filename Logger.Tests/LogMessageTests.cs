namespace Ksh.Logger.Tests;

public class LogMessageTests
{
    [Fact]
    public void LogMessageMustBeDeconstructed()
    {
        var exn = new ArgumentException();
        var sut = new LogMessage("foo", LogSeverity.Trace, exn);

        sut.Deconstruct(out var message, out var severity, out var exception, out var dateTime);

        message.Should().Be("foo");
        severity.Should().Be(LogSeverity.Trace);
        exception.Should().Be(exn);
        dateTime.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(1000));
    }

    [Fact]
    public void GetHashCode_MustCalculateTheHash()
    {
        // Arrange
        var sut1 = new LogMessage("foo");
        var sut2 = new LogMessage("foo");

        // Act
        var code1 = sut1.GetHashCode();
        var code2 = sut2.GetHashCode();

        // Assert
        using var _ = new AssertionScope();
        code1.Should().Be(code2);
    }
}