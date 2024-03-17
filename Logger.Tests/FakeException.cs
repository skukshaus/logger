namespace Ksh.Logger.Tests;

internal class FakeException : Exception
{
    public FakeException()
    {
    }

    public FakeException(string message) : base(message)
    {
    }

    public FakeException(string message, Exception inner) : base(message, inner)
    {
    }

    public override string? StackTrace
        => $"   at FakeException line 42"
           + $"{Environment.NewLine}"
           + $"at ImportantClass.FragileMethod() in ImportantClass.cs:line 42";
}