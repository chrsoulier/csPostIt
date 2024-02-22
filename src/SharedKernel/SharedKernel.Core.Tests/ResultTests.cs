using NFluent;
using SharedKernel.TestCommon;

namespace SharedKernel.Core.Tests;

public class ResultTests
{
    [Fact]
    public void WhenICreateASuccess()
    {
        var sut = new Result<int, TestFailed>(10);
        Check.That(sut.IsSuccess).IsTrue();
        Check.That(sut.IsError).IsFalse();
    }

    [Fact]
    public void WhenICreateAnError()
    {
        var sut = new Result<int, TestFailed>(new TestFailed());
        Check.That(sut.IsSuccess).IsFalse();
        Check.That(sut.IsError).IsTrue();
    }

    [Fact]
    public void WhenImplicitValue()
    {
        var sut = (Result<int, TestFailed>) 10;
        Check.That(sut.IsSuccess).IsTrue();
    }

    [Fact]
    public void WhenImplicitError()
    {
        var sut = (Result<int, TestFailed>) new TestFailed();
        Check.That(sut.IsError).IsTrue();
    }

    [Fact]
    public void GivenResultIsError_WhenMatch_ThenWhenErrorShouldBeExecuted()
    {
        var sut = new Result<int, TestFailed>(new TestFailed());
        var value = sut.Match(
            _ => throw new TestFailedException(),
            error => error
        );

        Check.That(value).IsInstanceOf<TestFailed>();
    }

    private record TestFailed;
}