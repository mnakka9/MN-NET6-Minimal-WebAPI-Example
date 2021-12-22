using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Notes.WebAPI.Attributes;
using Notes.WebAPI.Data.Models;
using Notes.WebAPI.Data.Repository;
using System;
using System.Collections.Generic;

namespace Notes.WebAPI.Controllers
{
	/// <summary>
	/// Notes API Controller
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	[ApiKey]
	public class NotesAPIController : ControllerBase
	{
		private INotesRepo notesRepository;
		private ILogger<NotesAPIController> logger;

		/// <summary>
		/// Constructor with dependency injection
		/// </summary>
		/// <param name="_notesRepository"></param>
		/// <param name="_logger"></param>
		public NotesAPIController(INotesRepo _notesRepository, ILogger<NotesAPIController> _logger)
		{
			notesRepository = _notesRepository;
			logger = _logger;
		}

		/// <summary>
		/// Get request - gets all notes
		/// </summary>
		/// <returns>List of notes</returns>
		// GET: api/<NotesAPIController>
		[HttpGet]
		public IEnumerable<Note> Get()
		{
			try
			{
				return notesRepository.GetAll();
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message, ex);
			}

			return new List<Note>();
		}

		/// <summary>
		/// Get a note by Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// GET api/<NotesAPIController>/5
		[HttpGet("{id}")]
		public Note Get(int id)
		{
			try
			{
				var note = notesRepository.GetById(id);

				if (note != null)
				{
					return note;
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message, ex);
			}

			return null;
		}

		[HttpGet("{searchterm}")]
		public List<Note> Get(string searchterm)
		{
			try
			{
				var notes = notesRepository.SearchNotes(searchterm);

				if (notes != null && notes.Count > 0)
				{
					return notes;
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message, ex);
			}

			return null;
		}

		/// <summary>
		/// Add a new note
		/// </summary>
		/// <param name="note"></param>
		/// <returns></returns>
		// POST api/<NotesAPIController>
		[HttpPost]
		public IActionResult Post([FromBody] Note note)
		{
			if (!ModelState.IsValid)
			{
				return new JsonResult(new { StatusCode = 400, Value = "Required information is missing" });
			}

			try
			{
				bool exist = notesRepository.IsNoteExists(note.Id);

				if (!exist)
				{
					notesRepository.Add(note);

					return new JsonResult(new { StatusCode = 201, Value = "Added successfully", Note = note });
				}
				else
				{
					return new JsonResult(new { StatusCode = 403, Value = "Note already exists" });
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message, ex);
			}

			return new JsonResult(new { StatusCode = 500, Value = "Error occured" });
		}

		/// <summary>
		/// Update an existing note
		/// </summary>
		/// <param name="id"></param>
		/// <param name="note"></param>
		/// <returns></returns>
		// PUT api/<NotesAPIController>/5
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] Note note)
		{
			try
			{
				var exist = notesRepository.IsNoteExists(id);
				if (exist)
				{
					var existingNote = notesRepository.GetById(id);

					if (existingNote != null)
					{
						note.Id = existingNote.Id;
						note.ModifiedDate = DateTime.Now;

						notesRepository.Update(note);

						return new JsonResult(new { StatusCode = 200, Value = "Updated successfully" });
					}
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message, ex);
			}

			return new JsonResult(new { StatusCode = 500, Value = "Updated failed" });
		}

		/// <summary>
		/// Delete a specific note
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		// DELETE api/<NotesAPIController>/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			try
			{
				var existingNote = notesRepository.GetById(id);

				if (existingNote != null)
				{
					if (notesRepository.Delete(existingNote))
					{
						return new JsonResult(new { StatusCode = 200, Value = "Deleted successfully" });
					}
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message, ex);
			}

			return new JsonResult(new { StatusCode = 400, Value = "Deletion failed" });
		}
	}
}
