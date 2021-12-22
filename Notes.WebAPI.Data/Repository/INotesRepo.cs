using Notes.WebAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.WebAPI.Data.Repository
{
	public interface INotesRepo
	{
		/// <summary>
		/// Add a note
		/// </summary>
		/// <param name="note"></param>
		/// <returns></returns>
		Note Add(Note note);

		/// <summary>
		/// Update a note
		/// </summary>
		/// <param name="newNote"></param>
		/// <returns></returns>
		Note Update(Note newNote);

		/// <summary>
		/// Delete a note
		/// </summary>
		/// <param name="newNote"></param>
		/// <returns></returns>
		bool Delete(Note newNote);

		/// <summary>
		/// Get all notes
		/// </summary>
		/// <returns></returns>
		IEnumerable<Note> GetAll();

		/// <summary>
		/// Get note by Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Note GetById(int id);

		/// <summary>
		/// Verify if a note exists
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		bool IsNoteExists(int id);

		List<Note> SearchNotes(string term);
	}
}
