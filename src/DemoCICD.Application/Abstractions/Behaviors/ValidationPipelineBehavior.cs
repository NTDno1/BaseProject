using DemoCICD.Contract.Share;
using FluentValidation;
using MediatR;

namespace DemoCICD.Application.Abstractions.Behaviors;
public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        Error[] errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => new Error(
                failure.PropertyName,
                failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Any())
        {
            return CreateValidationResult<TResponse>(errors);
        }

        return await next();
    }

    private static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (ValidationResult.WithErrors(errors) as TResult)!;
        }

        object validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object?[] { errors })!;

        return (TResult)validationResult;
    }
}

//public class GetValidation<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//    where TRequest : IRequest<TResponse>
//    where TResponse : Result
//{
//    private readonly IEnumerable<IValidator<TRequest>> _validators;

//    public GetValidation(IEnumerable<IValidator<TRequest>> validators)
//    {
//        _validators = validators;
//    }

//    public async Task<TResponse> Handle(TRequest request)
//    {
//        if (!_validators.Any())
//        {
//            return CreateErrorResponse();
//        }
//        var errors = _validators
//            .Select(validator => validator.Validate(request))
//            .SelectMany(validationResult => validationResult.Errors)
//            .Where(validationFailure => validationFailure is not null)
//            .Select(failure => new Error(
//                failure.PropertyName,
//                failure.ErrorMessage))
//            .Distinct()
//            .ToArray();
//        if (errors.Length > 0)
//        {
//            return CreateErrorResponse(errors);
//        }

//        return await HandleRequestAsync(request);
//    }

//    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//    {
//        throw new NotImplementedException();
//    }

//    private TResponse CreateErrorResponse(params Error[] errors)
//    {
//        throw new NotImplementedException("Create a proper error response based on TResponse.");
//    }

//    private Task<TResponse> HandleRequestAsync(TRequest request)
//    {
//        throw new NotImplementedException("Implement the core request handling logic.");
//    }
//}
