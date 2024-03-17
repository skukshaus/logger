namespace Ksh.Logger.Tests.StandardLoggerTests;

public partial class StandardLoggerTest
{
    private readonly StandardLogger _logger;

    private readonly Mock<ILogMessagePropagator> _propagatorMoq = new();
    private readonly Mock<ILogMessagePropagator> _propagator2Moq = new();
    private readonly Mock<ILogMessagePropagator> _propagator3Moq = new();

    private readonly LogMessage _dummyMessage = new("hello world");

    public StandardLoggerTest()
    {
        var propagators = new List<ILogMessagePropagator> {
            _propagatorMoq.Object,
            _propagator2Moq.Object,
            _propagator3Moq.Object
        };

        _logger = new(propagators);
    }

    [Fact]
    public void Init_MustNotBeNull()
    {
        using var _ = new AssertionScope();

        _logger.Should().NotBeNull();
    }
}