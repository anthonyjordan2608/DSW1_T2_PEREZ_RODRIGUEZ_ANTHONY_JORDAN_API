using System.ComponentModel.DataAnnotations;

namespace Library.Application.Dtos
{
    public class CreateLoanDto
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        [MaxLength(150)]
        public string StudentName { get; set; } = string.Empty;
    }
}