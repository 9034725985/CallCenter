namespace CallCenter.Server.MyPersons;

public readonly struct Result<TValue, TError>
{
    public bool IsError { get; }
    public bool IsSuccess => !IsError;
    private readonly TValue? _value;
    private readonly TError? _error;
    public Result(TValue value)
    {
        IsError = false;
        _value = value;
        _error = default;
    }
}
