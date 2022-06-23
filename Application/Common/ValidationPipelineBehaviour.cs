using MediatR;
using SGSX.CqrsTemp.Domain.Results;
using System.Threading;
using FluentValidation;

namespace SGSX.CqrsTemp.Application.Common;
public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : Result
{
    protected IValidator<TRequest>? Validator { get; }
    public ValidationPipelineBehaviour(IValidator<TRequest>? validator)
    {
        if(validator is null)
        {
            _shouldSkip = true;
        }
        else
        {
            Validator = validator;
            _shouldSkip = false;
        }
    }
    private readonly bool _shouldSkip;
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if(_shouldSkip)
        {
            return await next();
        }

        var validation = await Validator!.ValidateAsync(request);
        if(validation.IsValid)
        {
            return await next();
        }

        var responseConstructor = typeof(TResponse).GetConstructor(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, Type.EmptyTypes);
        var response = responseConstructor?.Invoke(Array.Empty<object>()) as Result;
        return (response!.Failed().WithValidation(validation.Errors.Select(s => Validation.FromApplicationError(s.ErrorMessage))) as TResponse)!;
    }
}

