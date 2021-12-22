using System;

namespace Notes.WebAPI.ViewModels
{
    public class NoteViewModel
    {
        public int Id { get; set; }
        public Guid NoteId { get; set; }
        public string NoteContent { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate {  get; set; }
    }
}
