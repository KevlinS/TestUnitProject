namespace GestionBibliothequeAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        public Guid IdentityId { get; set; }

        [MaxLength(75)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        [MaxLength(25)]
        public string Email { get; set; }

        [MaxLength(25)]
        public string? Phone { get; set; }

        public  DateTime? CreatedDate { get; set; }

        [MaxLength(25)]
        public string? Country { get; set; }

        [MaxLength(25)]
        public string? City { get; set; }

        public int? PostalCode { get; set; }

        [MaxLength(75)]
        public string? Address { get; set; }

        public int? status { get; set; } = 1;
    }
}
