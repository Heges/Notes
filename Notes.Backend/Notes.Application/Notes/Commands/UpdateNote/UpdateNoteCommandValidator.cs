using System;
using FluentValidation;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidator()
        {
            RuleFor(unk => unk.Id).NotEqual(Guid.Empty);
            RuleFor(unk => unk.UserId).NotEqual(Guid.Empty);
            RuleFor(unk => unk.Title).NotEmpty().MaximumLength(250);
        }
    }
}
