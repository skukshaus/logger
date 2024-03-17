namespace Ksh.Logger.Tests.StandardLoggerTests;

public partial class StandardLoggerTest
{
    [Fact]
    public void Log_Message()
    {
        // arrange

        // act
        _logger.Log(_dummyMessage);

        // assert
        _propagatorMoq.Verify(x => x.Propagate(_dummyMessage), Times.Once);
        _propagator2Moq.Verify(x => x.Propagate(_dummyMessage), Times.Once);
        _propagator3Moq.Verify(x => x.Propagate(_dummyMessage), Times.Once);
    }

    [Fact]
    public void Trace_MustBeCalledWithTraceSeverity()
    {
        // arrange
        var expected = new LogMessage("foo", LogSeverity.Trace);

        // act
        _logger.Trace("foo");

        // assert
        _propagatorMoq.Verify(x => x.Propagate(expected), Times.Once);
    }

    [Fact]
    public void Debug_MustBeCalledWithDebugSeverity()
    {
        // arrange
        var expected = new LogMessage("foo", LogSeverity.Debug);

        // act
        _logger.Debug("foo");

        // assert
        _propagatorMoq.Verify(x => x.Propagate(expected), Times.Once);
    }

    [Fact]
    public void Info_MustBeCalledWithInfoSeverity()
    {
        // arrange
        var expected = new LogMessage("foo", LogSeverity.Info);

        // act
        _logger.Info("foo");

        // assert
        _propagatorMoq.Verify(x => x.Propagate(expected), Times.Once);
    }

    [Fact]
    public void Warn_MustBeCalledWithWarnSeverity()
    {
        // arrange
        var expected = new LogMessage("foo", LogSeverity.Warn);

        // act
        _logger.Warn("foo");

        // assert
        _propagatorMoq.Verify(x => x.Propagate(expected), Times.Once);
    }

    [Fact]
    public void Error_WithException_MustBeCalledWithErrorSeverity()
    {
        // arrange
        var exn = new FakeException();
        var expected = new LogMessage("foo", LogSeverity.Warn, exn);

        // act
        _logger.Warn("foo", exn);

        // assert
        _propagatorMoq.Verify(x => x.Propagate(expected), Times.Once);
    }

    
    [Fact]
    public void Error_MustBeCalledWithErrorSeverity()
    {
        // arrange
        var exn = new FakeException();
        var expected = new LogMessage("foo", LogSeverity.Error, exn);

        // act
        _logger.Error("foo", exn);

        // assert
        _propagatorMoq.Verify(x => x.Propagate(expected), Times.Once);
    }

    [Fact]
    public void Fatal_MustBeCalledWithFatalSeverity()
    {
        // arrange
        var exn = new FakeException();
        var expected = new LogMessage("foo", LogSeverity.Fatal, exn);

        // act
        _logger.Fatal("foo", exn);

        // assert
        _propagatorMoq.Verify(x => x.Propagate(expected), Times.Once);
    }
}