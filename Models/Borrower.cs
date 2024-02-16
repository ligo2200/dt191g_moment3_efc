using System.ComponentModel.DataAnnotations;

namespace moment3_efc.Models
{
    public class Borrower
    {
        public int BorrowerId { get; set; }
        [Required]
        [Display(Name = "Namn på Låntagare")]
        public string? BorrowerName { get; set; }
    }
}
