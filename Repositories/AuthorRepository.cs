namespace GestionBibliothequeAPI.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
