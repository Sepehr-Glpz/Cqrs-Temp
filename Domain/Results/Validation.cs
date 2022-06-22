namespace SGSX.CqrsTemp.Domain.Results;
public class Validation : object
{
    public Validation() : base()
    {
    }

    protected Validation(in string? message, string? stackTrace, ValidationCode code)
    {
        Message = message;
        StackTrace = stackTrace;
        Code = code;
    }

    public static Validation FromApplicationError(in string? message)
    {
        var stackTrace = new System.Diagnostics.StackTrace();
        return new Validation(in message, stackTrace.GetFrame(1)?.GetMethod()?.Name, ValidationCode.Application);
    }
    public static Validation FromSystemError(in string? message)
    {
        var stackTrace = new System.Diagnostics.StackTrace();
        return new Validation(in message, stackTrace.GetFrame(1)?.GetMethod()?.Name, ValidationCode.System);
    }

    public string? Message { get; init; }
    public string? StackTrace { get; init; }
    public ValidationCode Code { get; init; }

    public static implicit operator Validation(string message)
    {
        return new Validation(message, null, ValidationCode.Undefined);
    }

    public static implicit operator Validation(Exception? ex)
    {
        if(ex is null)
        {
            var stackTrace = new System.Diagnostics.StackTrace();
            return new Validation("Empty Exception!", stackTrace.GetFrame(1)?.GetMethod()?.Name, ValidationCode.Undefined);
        }
        return ex switch
        {
            ApplicationException => new Validation(AccumulateExceptionMessages(ex), ex.StackTrace, ValidationCode.Application),
            _ => new Validation(AccumulateExceptionMessages(ex), ex.StackTrace, ValidationCode.System),
        };
    }

    private static string AccumulateExceptionMessages(Exception ex)
    {
        var stringBuilder = new StringBuilder();
        Exception? currentException = ex;
        do
        {
            stringBuilder.AppendLine(currentException!.Message);
            currentException = ex.InnerException;
        }
        while (currentException is not null);
        return stringBuilder.ToString();
    }
}

