using FluentValidation;
using System;
namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(cnm => cnm.Title).NotEmpty().MaximumLength(250);
            RuleFor(cnm => cnm.UserId).NotEqual(Guid.Empty);
        }
    }
}
