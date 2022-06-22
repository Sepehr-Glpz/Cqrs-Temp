namespace SGSX.CqrsTemp.Domain.Results;
public class Result : object
{
    protected Result() : base()
    {
    }

    public bool Success { get; internal protected set; }
    private List<Validation>? _validationMessages;
    public IReadOnlyList<Validation>? ValidationMessages { get => _validationMessages; }

    public virtual Result Successful()
    {
        Success = true;
        return this;
    }
    public virtual Result Failed()
    {
        Success = false;
        return this;
    }

    public virtual Result WithValidation(Validation? validation)
    {
        if(validation is null)
        {
            return this;
        }
        (_validationMessages ??= new List<Validation>()).Add(validation);
        return this;
    }
    public virtual Result WithValidation(IEnumerable<Validation>? validations)
    {
        if (validations is null || validations?.Any() != true)
        {
            return this;
        }
        (_validationMessages ??= new List<Validation>(validations.Count())).AddRange(validations);
        return this;
    }
    public virtual Result WithValidation(params Validation[]? validations)
    {
        if (validations is null || validations?.Any() != true)
        {
            return this;
        }
        (_validationMessages ??= new List<Validation>(validations.Length)).AddRange(validations);
        return this;
    }

    public static Result CreateSuccesful()
    {
        return new Result().Successful();
    }
    public static Result CreateSuccessful(params string[] messages)
    {
        return new Result().Successful().WithValidation(messages.Select(s => (Validation)s));
    }
    public static Result CreateFailed()
    {
        return new Result().Failed();
    }
    public static Result CreateFailed(params string[] messages)
    {
        return new Result().Failed().WithValidation(messages.Select(s => (Validation)s));
    }
    public static Result CreateFailed(params Exception[] exceptions)
    {
        return new Result().Failed().WithValidation(exceptions.Select(s => (Validation)s));
    }

    public sealed override string ToString()
    {
        return Success.ToString().ToLower();
    }
}

