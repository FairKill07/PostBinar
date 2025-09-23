using FluentValidation;
using PostBinar.Domain.Enums;

namespace PostBinar.Application.ProjectMemberships.Commands.AddMember;

public sealed class AddMemberCommandValidator : AbstractValidator<AddMemberCommand>
{
    public AddMemberCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage("ProjectId is required.");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");

        RuleFor(x => x.Role)
            .NotEmpty()
            .WithMessage("Role is required.")
            .Must(role => Enum.TryParse<Role>(role, ignoreCase: true, out _))
            .WithMessage("Role is invalid.");
    }
}
