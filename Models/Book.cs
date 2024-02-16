using System.ComponentModel.DataAnnotations;

namespace moment3_efc.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        [Display(Name = "Titel")]
        public string? Title { get; set; }
    
        [Display(Name = "Publiceringsår")]
        public DateTime PublicationDate { get; set; }

        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
