using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.WebAPI.Data.Models;
using System.Linq;

namespace Notes.WebAPI.Data.Tests
{
    [TestClass]
    public class RepoTest
    {
        private TestRepo testRepo;

        [TestInitialize]
        public void TestInit()
        {
            testRepo = new TestRepo();
        }

        [TestMethod]
        public void AddNoteTest()
        {
            Note note = new Note();
            note.Id = 6;
            note.NoteContent = "Test 6";
            note.Title = "Test";

            int numberOfNotes = GetCount();

            var result = testRepo.Add(note);

            Assert.IsNotNull(result);
            Assert.AreEqual(numberOfNotes + 1, GetCount());
            Assert.AreEqual(result.NoteContent, note.NoteContent);
        }

        private int GetCount()
        {
            return testRepo.GetAll().ToList().Count;
        }

        [TestMethod]
        public void UpdateNoteTest()
        {
            Note note = testRepo.GetById(2);
            note.NoteContent = "Test 6";
            note.Title = "Test";

            int numberOfNotes = GetCount();

            var result = testRepo.Update(note);

            Assert.IsNotNull(result);
            Assert.AreEqual(numberOfNotes, GetCount());
            Assert.AreEqual(result.NoteContent, note.NoteContent);
        }

        [TestMethod]
        public void DeleteNoteTest()
        {
            Note note = testRepo.GetById(4);

            int numberOfNotes = GetCount();

            var result = testRepo.Delete(note);

            Assert.IsNotNull(result);
            Assert.AreEqual(numberOfNotes - 1, GetCount());
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetByIdNoteTest()
        {
            Note note = testRepo.GetById(1);

            Assert.IsNotNull(note);
            Assert.AreEqual("Test1", note.Title);
        }

        [TestMethod]
        public void GetAllNoteTest()
        {
            var notes = testRepo.GetAll().ToList();

            Assert.IsNotNull(notes);
            Assert.IsTrue(notes.Count > 1);
        }
    }
}
