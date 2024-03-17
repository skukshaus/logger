namespace Ksh.Logger.Tests.StandardLoggerTests;

public partial class StandardLoggerTest
{
    [Fact] public void TurnOff_DoNothing()
    {
        // arrange
        _logger.TurnOff();

        // act
        _logger.Log(_dummyMessage);

        // assert
        _propagatorMoq.Verify(x => x.Propagate(_dummyMessage), Times.Never);
    }

    [Fact] public void TurnOn_BeginToLog()
    {
        // arrange
        _logger.TurnOff();
        _logger.Log(_dummyMessage);
        _logger.TurnOn();
        
        // act
        _logger.Log(_dummyMessage);

        // assert
        _propagatorMoq.Verify(x => x.Propagate(_dummyMessage), Times.Once);
    }
}