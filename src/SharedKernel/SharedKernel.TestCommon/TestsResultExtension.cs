using SharedKernel.Core;

namespace SharedKernel.TestCommon;

public static class TestsResultExtension
{
    public static A GetValueOrThrowIfIsError<A, TError>(this Result<A, TError> @this)
        => @this.Match(
            success => success,
            error => throw new TestFailedException<TError>(error)
        )!;
}