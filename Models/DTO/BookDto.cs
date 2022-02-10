namespace GestionBibliothequeAPI.Models.DTO
{
    public class BookDto
    {
        public int Id { get; set; }

        [MaxLength(75)]
        [Required]
        public string Title { get; set; }

        [MaxLength(100)]
        public string? PublishingHouse { get; set; }

        public int Quantity { get; set; }

        public string Status { get; set; } = "disponible";

        public string? Description { get; set; }

        public DateTime? PublishDate { get; set; }

        public Author Author { get; set; }

        public Category? Category { get; set; }
    }
}
