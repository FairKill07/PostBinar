using FluentValidation;

namespace PostBinar.Application.Categorys.Commands.CreateSpecialization;

public sealed class CreateSpecializationCommandValidator : AbstractValidator<CreateSpecializationCommand>
{
    public CreateSpecializationCommandValidator() 
    {
        RuleFor(command =>
            command.Name).NotEmpty().MaximumLength(100);
        RuleFor(command =>
            command.ColorCode).NotEmpty().MaximumLength(7);
    }
}
