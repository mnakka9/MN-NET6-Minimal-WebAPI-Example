using Microsoft.EntityFrameworkCore;
using Notes.WebAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.WebAPI.Data.Repository
{
    public class NotesRepository : INotesRepo
    {
        /// <summary>
        /// Constructor - making sure database is created
        /// </summary>
        public NotesRepository()
        {
            using (var context = new NotesDbContext())
            {
                context.Database.EnsureCreated();
            }
        }

        /// <summary>
        /// Adding a note to db
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public Note Add(Note note)
        {
            using (var context = new NotesDbContext())
            {
                context.Notes.Add(note);
                context.SaveChanges();

                return note;
            }
        }

        /// <summary>
        /// Verify if a note exists in db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsNoteExists(int id)
        {
            using (var context = new NotesDbContext())
            {
                return context.Notes.Find(id) != null;
            }
        }

        /// <summary>
        /// Delete a note
        /// </summary>
        /// <param name="newNote"></param>
        /// <returns></returns>
        public bool Delete(Note newNote)
        {
            using (var context = new NotesDbContext())
            {
                context.Notes.Remove(newNote);
                context.SaveChanges();

                return true;
            }
        }

        /// <summary>
        /// Get all notes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Note> GetAll()
        {
            using (var context = new NotesDbContext())
            {
                return context.Notes.ToList();
            }
        }

        /// <summary>
        /// Get a note by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Note GetById(int id)
        {
            using (var context = new NotesDbContext())
            {
                var note = context.Notes.Find(id);

                if (!(note is null))
                {
                    return note;
                }

                return null;
            }
        }

        /// <summary>
        /// Update a note
        /// </summary>
        /// <param name="newNote"></param>
        /// <returns></returns>
        public Note Update(Note newNote)
        {
            using (var context = new NotesDbContext())
            {
                var note = context.Notes.Find(newNote.Id);

                if (!(note is null))
                {
                    note.NoteContent = newNote.NoteContent;
                    note.NoteId = newNote.NoteId;
                    note.Title = newNote.Title;
                    note.ModifiedDate = newNote.ModifiedDate;
                    context.SaveChanges();

                    return note;
                }

                return null;
            }
        }

        public List<Note> SearchNotes(string term)
        {
            using (var context = new NotesDbContext())
            {
               var allNotes = context.Notes.AsNoTracking().ToList();

                return allNotes.FindAll(n => (n.Title.Contains(term, StringComparison.OrdinalIgnoreCase) || n.NoteContent.Contains(term, StringComparison.OrdinalIgnoreCase)));
            }
        }
    }
}
