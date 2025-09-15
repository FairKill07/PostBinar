using FluentValidation;

namespace PostBinar.Application.Users.Commands.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(registerCommand => 
            registerCommand.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(registerCommand =>
            registerCommand.LastName).NotEmpty().MaximumLength(50);
        RuleFor(registerCommand =>
            registerCommand.Email).NotEmpty().EmailAddress().MaximumLength(100);
        RuleFor(registerCommand =>
            registerCommand.Password).NotEmpty().MinimumLength(6).MaximumLength(100);
        RuleFor(registerCommand =>
            registerCommand.SpecializationId).NotEmpty();
    }
}
