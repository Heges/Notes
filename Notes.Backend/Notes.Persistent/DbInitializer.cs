using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Persistent
{
    public class DbInitializer
    {
        public static void Initialize(NotesDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
