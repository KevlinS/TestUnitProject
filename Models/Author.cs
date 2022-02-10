namespace GestionBibliothequeAPI.Models
{
    public class Author
    {
        public int Id { get; set; }

        [MaxLength(75)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(75)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string? Country { get; set; }

        public DateTime? BirthDay { get; set; }
    }
}
