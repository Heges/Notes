﻿using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Application.Interfaces;
using Notes.Persistent.EntityTypeConfiguration;

namespace Notes.Persistent
{
    public class NotesDbContext : DbContext, INotesDbContext
    {
        public DbSet<Note> Notes { get; set; }

        public NotesDbContext(DbContextOptions<NotesDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NoteConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
