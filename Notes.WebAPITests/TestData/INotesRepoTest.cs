using Notes.WebAPI.Data.Models;
using Notes.WebAPI.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.WebAPITests.TestData
{
    internal class TestRepo : INotesRepo
    {
        private IList<Note> _notes;
        public TestRepo()
        {
            _notes = new List<Note>()
            {
                new Note() { Id=1, Title ="Test1", NoteContent="Test" },
                new Note() { Id=2, Title ="Test2", NoteContent="Test" },
                new Note() { Id=3, Title ="Test3", NoteContent="Test" },
                new Note() { Id=4, Title ="Test4", NoteContent="Test" }
            };
        }
        public Note Add(Note note)
        {
            _notes.Add(note);

            return note;
        }

        public bool Delete(Note newNote)
        {
            return _notes.Remove(newNote);
        }

        public IEnumerable<Note> GetAll()
        {
            return _notes;
        }

        public Note GetById(int id)
        {
            var note = _notes.Where(n => n.Id == id)?.First();

            if (!(note is null))
            {
                return note;
            }

            return null;
        }

        public bool IsNoteExists(int id)
        {
            return GetById(id) != null;
        }

		public List<Note> SearchNotes(string term)
		{
			throw new NotImplementedException();
		}

		public Note Update(Note newNote)
        {
            var note = _notes.Where(n => n.Id == newNote.Id)?.First();

            if (!(note is null))
            {
                note.NoteContent = newNote.NoteContent;

                return note;
            }

            return null;
        }
    }
}
