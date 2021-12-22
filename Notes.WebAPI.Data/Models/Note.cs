using System;
using System.ComponentModel.DataAnnotations;

namespace Notes.WebAPI.Data.Models
{
    public class Note
    {
        [Key]
        public int Id {  get; set; }

        public Guid NoteId {  get; set; } = Guid.NewGuid();

        [Required]
        public string NoteContent {  get; set; }

        [Required]
        public string Title {  get; set; }

        public DateTime CreatedDate { get; set;  } = DateTime.Now;

        public DateTime ModifiedDate { get; set; }
    }
}
