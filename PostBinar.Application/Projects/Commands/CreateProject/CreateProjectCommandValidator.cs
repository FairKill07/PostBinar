using FluentValidation;

namespace PostBinar.Application.Projects.Commands.CreateProject;

public sealed class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(command =>
            command.Name).NotEmpty().MaximumLength(100);
        RuleFor(command =>
            command.Description).MaximumLength(500);
        RuleFor(command =>
            command.OwnerId).NotEmpty();
    }
}
