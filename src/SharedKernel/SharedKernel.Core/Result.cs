namespace SharedKernel.Core;

public record Result<A, TError>
{
    private readonly Option<A> _value;
    private readonly Option<TError> _error;

    public bool IsError => _error.IsSome;
    public bool IsSuccess => !IsError;

    public Result(A value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
        _error = Option<TError>.None();
    }

    public Result(TError error)
    {
        _error = error ?? throw new ArgumentNullException(nameof(error));
        _value = Option<A>.None();
    }

    public static implicit operator Result<A, TError>(A value) => new(value);
    public static implicit operator Result<A, TError>(TError error) => new(error);

    public TResult Match<TResult>(
        Func<A, TResult> whenSuccess,
        Func<TError, TResult> whenError
    )
    {
        if (_value.TryGetValue(out var value))
            return whenSuccess(value);

        if (_error.TryGetValue(out var error))
            return whenError(error);

        throw new InvalidOperationException($"{nameof(_value)} and {nameof(_error)} are both None !");
    }
}