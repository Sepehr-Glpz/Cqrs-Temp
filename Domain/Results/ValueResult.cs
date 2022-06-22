namespace SGSX.CqrsTemp.Domain.Results;
public class Result<T> : Result
{
    protected Result() : base()
    {
    }

    public T? Value { get; internal protected set; }

    public virtual Result<T> WithValue(T? value)
    {
        Value = value;
        return this;
    }

    public override Result<T> Failed()
    {
        return (base.Failed() as Result<T>)!;
    }
    public override Result<T> Successful()
    {
        return (base.Successful() as Result<T>)!;
    }
    public override Result<T> WithValidation(Validation? validation)
    {
        return (base.WithValidation(validation) as Result<T>)!;
    }
    public override Result<T> WithValidation(IEnumerable<Validation>? validations)
    {
        return (base.WithValidation(validations) as Result<T>)!;
    }
    public override Result<T> WithValidation(params Validation[]? validations)
    {
        return (base.WithValidation(validations) as Result<T>)!;
    }
    public new static Result<T> CreateSuccessful(params string[] messages)
    {
        var newResult = new Result<T>().WithValidation(messages.Select(s => (Validation)s));
        newResult.Success = true;
        return newResult;
    }
    public static Result<T> CreateSuccessful(T? value)
    {
        return new Result<T>().WithValue(value).Successful();
    }
    public new static Result<T> CreateFailed()
    {
        return new Result<T>().Failed();
    }
    public new static Result<T> CreateFailed(params string[] messages)
    {
        var newResult = new Result<T>().WithValidation(messages.Select(s => (Validation)s));
        newResult.Success = false;
        return newResult;
    }
    public new static Result CreateFailed(params Exception[] exceptions)
    {
        var newResult = new Result<T>().WithValidation(exceptions.Select(s => (Validation)s));
        newResult.Success = false;
        return newResult;
    }

    public static implicit operator Result<T>?(T? value) => 
        value switch
        {
            null => null,
            _ => new Result<T>().WithValue(value).Successful(),
        };

    public Result<U> ToResult<U>() =>
        new Result<U>()
        {
            Success = Success,
            Value = default,
        }.WithValidation(ValidationMessages);


}

