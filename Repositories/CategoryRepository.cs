namespace GestionBibliothequeAPI.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(LibraryContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
