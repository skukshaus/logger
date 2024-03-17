namespace Ksh.Logger.Tests.StandardLogMessageFormatterTests;

public class StandardLogMessageFormatterTest(ITestOutputHelper xout)
{
    private readonly StandardLogMessageFormatter _formatter = new();
    private readonly DateTime _fixedDate = new(2020, 5, 4, 13, 37, 42);

    [Fact]
    public void Format_DefaultMessageMustBePrinted()
    {
        // Arrange
        var msg = new LogMessage("hello world") { TimeOfDay = _fixedDate };

        // Act
        var output = _formatter.Format(msg);

        // Assert
        xout.WriteLine(output);
        output.Should().Be("[ info @ 2020-05-04T13:37:42] hello world");
    }

    [Theory]
    [InlineData(LogSeverity.Trace, "trace")]
    [InlineData(LogSeverity.Debug, "debug")]
    [InlineData(LogSeverity.Info, " info")]
    [InlineData(LogSeverity.Warn, " warn")]
    [InlineData(LogSeverity.Error, "error")]
    [InlineData(LogSeverity.Fatal, "fatal")]
    [InlineData((LogSeverity)1337, "unkwn")]
    public void Format_DifferentSeverities(LogSeverity severity, string withinMessage)
    {
        // Arrange
        var msg = new LogMessage("hello world", severity) { TimeOfDay = _fixedDate };

        // Act
        var output = _formatter.Format(msg);

        // Assert
        xout.WriteLine(output);
        output.Should().Be($"[{withinMessage} @ 2020-05-04T13:37:42] hello world");
    }

    [Fact]
    public void Format_WithException()
    {
        // Arrange
        var exception = new FakeException("something went wrong");
        var msg = new LogMessage("hello world", LogSeverity.Error, exception) { TimeOfDay = _fixedDate };

        // Act
        var output = _formatter.Format(msg);

        // Assert
        xout.WriteLine(output);
        output.Should().Be(
            """
            [error @ 2020-05-04T13:37:42] hello world
             ~> Ksh.Logger.Tests.FakeException: something went wrong
             ~>   at FakeException line 42
             ~>   at ImportantClass.FragileMethod() in ImportantClass.cs:line 42
            """
        );
    }
    
    [Fact]
    public void Format_WithInnerException()
    {
        // Arrange
        var exception = new FakeException("something went wrong", new ArgumentNullException());
        var msg = new LogMessage("hello world", LogSeverity.Error, exception) { TimeOfDay = _fixedDate };

        // Act
        var output = _formatter.Format(msg);

        // Assert
        xout.WriteLine(output);
        output.Should().Be(
            """
            [error @ 2020-05-04T13:37:42] hello world
             ~> Ksh.Logger.Tests.FakeException: something went wrong
             ~>   at FakeException line 42
             ~>   at ImportantClass.FragileMethod() in ImportantClass.cs:line 42
             ~> ----------------------------------------------------------------------------
             ~> System.ArgumentNullException: Value cannot be null.
            """
        );
    }
}