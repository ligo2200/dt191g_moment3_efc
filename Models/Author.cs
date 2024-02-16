namespace moment3_efc.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string? Name { get; set; }

        // Navigeringsegenskap för böcker skrivna av författaren
        public List<Book>? Books { get; set; } 

    }
}
