using Microsoft.EntityFrameworkCore;
using Notes.WebAPI.Data.Models;
using System;

namespace Notes.WebAPI.Data
{
    /// <summary>
    /// Notes Db Context class
    /// </summary>
    public class NotesDbContext: DbContext
    {
        /// <summary>
        /// Notes table
        /// </summary>
        public DbSet<Note> Notes { get; set; }

        /// <summary>
        /// DbPath is used to get the db path
        /// </summary>
        public string DbPath { get; private set; }

        public NotesDbContext()
        {
            var folder = Environment.CurrentDirectory;
            DbPath = $"{folder}{System.IO.Path.DirectorySeparatorChar}NotesApp.sqlite";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }
    }
}
