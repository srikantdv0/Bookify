using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Bookify.Domain.Abstractions;

public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if(isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if(!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess {get;}
    public bool IsFailure => !IsSuccess;
    public Error Error {get;}
    public static Result Sucess() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
    public static Result<TValue> Failure<TValue>(TValue value) => new(default, false, error);

    public static Result<TValue> Create<TValue>(TValue? value) =>
            value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSucess, Error error)
        :base(isSucess,error)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Thevalue of a failure result can not be accessed.");

    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}