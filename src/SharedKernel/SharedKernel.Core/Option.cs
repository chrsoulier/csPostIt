using System.Diagnostics.CodeAnalysis;

namespace SharedKernel.Core;

public record Option<A>
{
    private readonly A _some;
    public bool IsSome { get; } = false;
    public bool IsNone => !IsSome;

    private Option()
    {
    }

    private Option(A value)
    {
        _some = value ?? throw new ArgumentNullException(nameof(value));
        IsSome = true;
    }

    public static implicit operator Option<A>(A value)
        => value is null
            ? None()
            : Some(value);

    public static Option<A> None() => new();

    public static Option<A> Some(A value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        return new Option<A>(value);
    }

    public TResult Match<TResult>(Func<A, TResult> whenSome, Func<TResult> whenNone)
        => IsSome
            ? whenSome(_some)
            : whenNone();

    public void IfSome(Action<A> whenSome)
    {
        if (IsSome) whenSome(_some);
    }

    public void IfNone(Action whenNone)
    {
        if (IsNone) whenNone();
    }

    public bool TryGetValue([MaybeNullWhen(false)] out A oValue)
    {
        if (IsNone)
        {
            oValue = default;
            return false;
        }

        oValue = _some;
        return true;
    }

    public A GetValueOrDefault(A defaultValue)
        => IsSome
            ? _some
            : defaultValue;
}