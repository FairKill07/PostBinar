using FluentValidation;

namespace PostBinar.Application.Users.Commands.LogIn;

public sealed class LogInCommandValidator : AbstractValidator<LogInCommand>
{
    public LogInCommandValidator()
    {
        RuleFor(logInCommand =>
            logInCommand.Email).NotEmpty().EmailAddress().MaximumLength(100);
        RuleFor(logInCommand =>
            logInCommand.Password).NotEmpty().MinimumLength(6).MaximumLength(100);
    }
}
