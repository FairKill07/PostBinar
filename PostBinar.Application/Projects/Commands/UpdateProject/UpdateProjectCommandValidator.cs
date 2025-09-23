using FluentValidation;

namespace PostBinar.Application.Projects.Commands.UpdateProject;

public sealed class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(command =>
            command.Name).NotEmpty().MaximumLength(100);
        RuleFor(command =>
            command.Description).MaximumLength(500);
        RuleFor(command =>
            command.OwnerId).NotEmpty();
        RuleFor(command =>
            command.ProjectId).NotEmpty();
    }
}
