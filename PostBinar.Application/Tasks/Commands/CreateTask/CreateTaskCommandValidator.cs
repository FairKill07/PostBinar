using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBinar.Application.Tasks.Commands.CreateTask
{
    public sealed class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
            RuleFor(x => x.Deadline)
                .GreaterThan(DateTimeOffset.Now).WithMessage("Deadline must be a future date.");
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid status value.");
            RuleFor(x => x.Priority)
                .IsInEnum().WithMessage("Invalid priority value.");
        }
    }
}
