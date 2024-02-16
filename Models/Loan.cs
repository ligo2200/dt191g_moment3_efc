using System.ComponentModel.DataAnnotations;

namespace moment3_efc.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int BookId { get; set; }
        [Required]
        [Display(Name = "Bok")]
        public Book? Book { get; set; }


        public int BorrowerId { get; set; }
        [Required]
        [Display(Name = "Låntagarnamn")]
        public Borrower? Borrower { get; set; }
        [Display(Name = "Lånedatum")]
        public DateTime LoanDate { get; set; }
    }
}
