using System.ComponentModel.DataAnnotations;

namespace Library.Application.Dtos
{
    public class CreateBookDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Author { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string ISBN { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }
}