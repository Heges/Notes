using System;
using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Persistent;

namespace Notes.Tests.Common
{
    public class NotesContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid NoteIdForDelete = Guid.NewGuid();
        public static Guid NoteIdForUpdate = Guid.NewGuid();

        public static NotesDbContext Create()
        {
            var options = new DbContextOptionsBuilder<NotesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new NotesDbContext(options);
            context.Database.EnsureCreated();
            context.AddRange(
                new Note
            {
                CreationDate = DateTime.Today,
                Details = "Details1",
                EditDate = null,
                Id = Guid.Parse("29B63B58-B42E-4C0E-B69A-0962BCEDF8DD"),
                Title = "Title1",
                UserId = UserAId
            },
                 new Note
                 {
                     CreationDate = DateTime.Today,
                     Details = "Details2",
                     EditDate = null,
                     Id = Guid.Parse("3B0AFE2A-8E17-4DC1-BA97-F25046325A52"),
                     Title = "Title2",
                     UserId = UserBId
                 }, 
                 new Note
                 {
                     CreationDate = DateTime.Today,
                     Details = "Details3",
                     EditDate = null,
                     Id = NoteIdForDelete,
                     Title = "Title3",
                     UserId = UserAId
                 },
                 new Note
                 {
                     CreationDate = DateTime.Today,
                     Details = "Details4",
                     EditDate = null,
                     Id = NoteIdForUpdate,
                     Title = "Title4",
                     UserId = UserBId
                 }
                );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(NotesDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
