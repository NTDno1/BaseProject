namespace DemoCICD.Contract.Share;
public interface IValidationResult
{
    public static readonly Error ValidationError = new("ValidationError", "A validation occurred");

    Error[] Errors { get; }
}
