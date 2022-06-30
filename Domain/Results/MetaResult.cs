namespace SGSX.CqrsTemp.Domain.Results;
public class MetaResult<T> : Result<T>
{
    protected MetaResult()
    {
    }

    private Dictionary<string, object>? _meta;
    public IReadOnlyDictionary<string, object>? MetaData { get => _meta; }

    public virtual MetaResult<T> WithMeta(string key, object? value)
    {
        if (key is null || value is null)
        {
            return this;
        }
        (_meta ??= new Dictionary<string, object>()).Add(key, value);
        return this;
    }

    public override MetaResult<T> Successful()
    {
        return (base.Successful() as MetaResult<T>)!;
    }
    public override MetaResult<T> Failed()
    {
        return (base.Failed() as MetaResult<T>)!;
    }
    public override MetaResult<T> WithValue(T? value)
    {
        return (base.WithValue(value) as MetaResult<T>)!;
    }
    public override MetaResult<T> WithValidation(IEnumerable<Validation>? validations)
    {
        return (base.WithValidation(validations) as MetaResult<T>)!;
    }
    public override MetaResult<T> WithValidation(Validation? validation)
    {
        return (base.WithValidation(validation) as MetaResult<T>)!;
    }
    public override MetaResult<T> WithValidation(params Validation[]? validations)
    {
        return (base.WithValidation(validations) as MetaResult<T>)!;
    }

    public new static MetaResult<T> CreateSuccessful(T value) =>
        new MetaResult<T>().Successful().WithValue(value);
    public new static MetaResult<T> CreateSuccessful(params string[] messages) =>
        new MetaResult<T>().WithValidation(messages.Select(s => (Validation)s)).Successful();
    public new static MetaResult<T> CreateFailed() =>
        new MetaResult<T>().Failed();
    public new static MetaResult<T> CreateFailed(params string[] messages) =>
        new MetaResult<T>().WithValidation(messages.Select(s => (Validation)s)).Failed();

    public new static Result CreateFailed(params Exception[] exceptions) =>
        new MetaResult<T>().WithValidation(exceptions.Select(s => (Validation)s)).Failed();

    public static implicit operator MetaResult<T>?(T? value) =>
    value switch
    {
        null => null,
        _ => new MetaResult<T>().WithValue(value).Successful(),
    };
}

