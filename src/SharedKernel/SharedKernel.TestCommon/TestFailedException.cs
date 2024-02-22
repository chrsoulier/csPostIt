namespace SharedKernel.TestCommon;

public sealed class TestFailedException : Exception;

public sealed class TestFailedException<TError>(TError? error) : Exception
{
    public TError? Error { get; } = error;
}