using System.ComponentModel.DataAnnotations;

namespace moment3_efc.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        [Display(Name = "Författare")]
        public string? Name { get; set; }

        // relation
        public List<Book>? Books { get; set; } 

    }
}
