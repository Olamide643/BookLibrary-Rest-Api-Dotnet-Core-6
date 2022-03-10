using System.ComponentModel.DataAnnotations;

namespace BookApi.Model
{
    public class Book
    {
        [Key]
        public int BookId { set; get; }

        [Required]
        public string Title { set; get; } = string.Empty;

        [Required]
        public string Description { set; get; } = string.Empty;

        [Required]
        public string Author { set; get; } = string.Empty;

        public string? Summary { set; get; } = string.Empty;
    }


}
