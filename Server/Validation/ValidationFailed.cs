using FluentValidation.Results;

namespace CallCenter.Server.Validation;

public class ValidationFailed
{
    List<ValidationFailure> Errors { get; set; } = new();
    public ValidationFailed(List<ValidationFailure> errors)
    {
        if (errors != null && errors.Any()) { Errors.AddRange(errors); }
    }
}
