using System;
using FluentValidation;

namespace Notes.Application.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteCommandValidator()
        {
            RuleFor(unk => unk.Id).NotEqual(Guid.Empty);
            RuleFor(unk => unk.UserId).NotEqual(Guid.Empty);
        }
    }
}
