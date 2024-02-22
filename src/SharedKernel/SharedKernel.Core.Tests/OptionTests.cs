using NFluent;
using SharedKernel.TestCommon;

namespace SharedKernel.Core.Tests;

public class OptionTests
{
    [Fact]
    public void WhenCreateNoneOption()
    {
        var sut = Option<int>.None();

        Check.That(sut.IsSome).IsFalse();
        Check.That(sut.IsNone).IsTrue();
        sut.IfSome(_ => throw new TestFailedException());
    }

    [Fact]
    public void WhenCreateSomeOption()
    {
        var sut = Option<int>.Some(10);

        Check.That(sut.IsSome).IsTrue();
        Check.That(sut.IsNone).IsFalse();
        sut.IfNone(() => throw new TestFailedException());
    }

    [Fact]
    public void GivenOptionIsNone_WhenMatch_ThenWhenNoneShouldBeExecuted()
    {
        var sut = Option<int>.None();
        var value = sut.Match(
            _ => throw new TestFailedException(),
            () => 10
        );

        Check.That(value).IsEqualTo(10);
    }

    [Fact]
    public void GivenOptionIsSome_WhenMatch_ThenWhenSomeShouldBeExecuted()
    {
        var sut = Option<int>.Some(10);
        var value = sut.Match(
            value => value,
            () => throw new TestFailedException()
        );

        Check.That(value).IsEqualTo(10);
    }

    [Fact]
    public void GivenOptionIsNone_WhenTryGetValue_ThenReturnFalse()
    {
        var sut = Option<int?>.None();
        var hasValue = sut.TryGetValue(out var value);
        Check.That(hasValue).IsFalse();
        Check.That(value).IsEqualTo(default(int?));
    }

    [Fact]
    public void GivenOptionIsSome_WhenTryGetValue_ThenReturnTrueAndValue()
    {
        var sut = Option<int>.Some(10);
        var hasValue = sut.TryGetValue(out var value);
        Check.That(hasValue).IsTrue();
        Check.That(value).IsEqualTo(10);
    }

    [Fact]
    public void GivenOptionIsNone_WhenGetValueOrDefault_ThenReturnDefaultValue()
    {
        var sut = Option<int>.None();
        var value = sut.GetValueOrDefault(-1);
        Check.That(value).IsEqualTo(-1);
    }

    [Fact]
    public void GivenOptionIsSome_WhenGetValueOrDefault_ThenReturnValue()
    {
        var sut = Option<int>.Some(8);
        var value = sut.GetValueOrDefault(-1);
        Check.That(value).IsEqualTo(8);
    }

    [Fact]
    public void WhenImplicitSome()
    {
        var sut = (Option<int>) 10;
        Check.That(sut.IsSome).IsTrue();
    }

    [Fact]
    public void WhenImplicitNone()
    {
        var sut = (Option<int?>) (int?) null;
        Check.That(sut.IsNone).IsTrue();
    }
}