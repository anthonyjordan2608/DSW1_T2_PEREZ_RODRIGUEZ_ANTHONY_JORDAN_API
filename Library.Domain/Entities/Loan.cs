using System;

namespace Library.Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public DateTime LoanDate { get; set; } = DateTime.UtcNow;
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = "Active"; // Active, Returned
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public virtual Book Book { get; set; } = null!;
    }
}
