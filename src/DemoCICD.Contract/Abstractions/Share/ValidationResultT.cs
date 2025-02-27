﻿namespace DemoCICD.Contract.Share;
public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    private ValidationResult(Error[] errors)
        : base(default, false, IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public Error[] Errors { get; private set; }

    public static ValidationResult<TValue> WithErrors(Error[] errors)
    {
        return new(errors);
    }
}
