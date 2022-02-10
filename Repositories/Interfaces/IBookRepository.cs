namespace GestionBibliothequeAPI.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {

        Task<IEnumerable<Book>> GetBooksByParams(int? idAuthor, int? idCategory, string? status, string? title, string? publishingHouse, DateTime? publishDate);
    }
}
