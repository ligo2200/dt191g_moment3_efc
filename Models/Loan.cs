using System.ComponentModel.DataAnnotations;

namespace moment3_efc.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int BookId { get; set; }
        [Required]
        public Book? Book { get; set; }


        public int BorrowerId { get; set; }
        [Required]
        public Borrower? Borrower { get; set; }
        public DateTime LoanDate { get; set; }
    }
}
