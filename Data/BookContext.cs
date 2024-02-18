using moment3_efc.Models;
using Microsoft.EntityFrameworkCore;

namespace moment3_efc.Data
{
    public class BookContext : DbContext
    {
        // constructor
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {

        }

        // properties 
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}