namespace GestionBibliothequeAPI.Models
{
    public class Loan
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "En pret";

        public int IdBook { get; set; }

        public int IdUser { get; set; }

        //  "LibraryConnection": "Data Source=LAPTOP-GB079B4I\\SQLEXPRESS;Initial Catalog=OnlineLibraryAPI;Integrated Security=true;Connection Lifetime=60;"
    }
}
