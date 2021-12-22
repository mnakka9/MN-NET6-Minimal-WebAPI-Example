using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.WebAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.WebAPI.Data.Tests
{
    [TestClass]
    public class RepoTestWithRealDB
    {
        private NotesDbContext _context;

        [TestInitialize]
        public void Init()
        {
            _context = new NotesDbContext();
        }

        [TestCleanup]
        public void CleanTest()
        {
            _context.Dispose();
        }

        [TestMethod]
        public void TestAddNoteWithNoteContent()
        {
            var note = new Note();
            note.Title = "Cleanup";
            note.NoteContent = "Cleanup";

            var result = _context.Notes.Add(note);
            _context.SaveChanges();

            Assert.IsNotNull(result.Entity);
            Assert.AreEqual(result.Entity.Title, note.Title);

            _context.Notes.Remove(result.Entity);
            _context.SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void TestAddNoteWithoutNoteContent()
        {
            var note = new Note();
            note.Title = "Cleanup";
            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        [TestMethod]
        public void TestGetNoteById()
        {
            var note = _context.Notes.Find(1);
            
            Assert.IsNotNull(note);
            Assert.AreEqual(1, note.Id);
        }

        [TestMethod]
        public void TestEditNote()
        {
            var note = _context.Notes.Find(1);

            note.NoteContent = "New Content";
            note.ModifiedDate = DateTime.Now;

            _context.SaveChanges();

            Assert.IsNotNull(note);
            Assert.AreEqual(1, note.Id);
            Assert.AreEqual("New Content", note.NoteContent);
            Assert.IsNotNull(note.ModifiedDate);
        }

        [TestMethod]
        public void TestDeleteNote()
        {
            var note = new Note();
            note.Title = "Cleanup";
            note.NoteContent = "Cleanup";

            var result = _context.Notes.Add(note);
            _context.SaveChanges();

            int Id = result.Entity.Id;

            _context.Notes.Remove(result.Entity);
            _context.SaveChanges();

            var exist = _context.Notes.Find(Id);

            Assert.IsNull(exist);
        }
    }
}
