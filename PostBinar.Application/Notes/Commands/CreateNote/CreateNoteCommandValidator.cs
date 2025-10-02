using FluentValidation;

namespace PostBinar.Application.Notes.Commands.CreateNote;

public sealed class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator() 
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
        RuleFor(x => x.Content)
            .MaximumLength(4000).WithMessage("Content must not exceed 4000 characters.");
    }
}
