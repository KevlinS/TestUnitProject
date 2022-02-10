namespace GestionBibliothequeAPI.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Book>> GetAllAsync()
        {
            try
            {
                return await dbSet.Include(s => s.Author).Include(c => c.Category)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method has generated error", typeof(UserRepository));

                return new List<Book>();
            }
        }
        
        public override async Task<Book> GetByIdAsync(int id)
        {
            try
            {
                return await dbSet.Include(s => s.Author).Include(c => c.Category).FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method has generated error", typeof(UserRepository));

                return new Book();
            }
        }

        public async Task<IEnumerable<Book>> GetBooksByParams(int? idAuthor, int? idCategory, string? status, string? title, string? publishingHouse, DateTime? publishDate)
        {
            var result = await _context.Books.Include(s => s.Author).Include(c => c.Category).ToListAsync();

            if (idAuthor != null && idAuthor > 0)
                result = result.Where(x => x.Author.Id == idAuthor).ToList();
            if(idCategory != null && idCategory > 0)
                result = result.Where(x => x.Category?.Id == idCategory).ToList();
            if(status != null && status != String.Empty)
                result = result.Where(x => x.Status == status).ToList();
            if(title != null && title != String.Empty)
                result = result.Where(x => x.Title == title).ToList();
            if(publishingHouse != null && publishingHouse != String.Empty)
                result = result.Where(x => x.PublishingHouse == publishingHouse).ToList();
            if(publishDate != null && publishDate != DateTime.MinValue)
                result = result.Where(x => x.PublishDate >= publishDate).ToList();

            return result;
        }
    }
}
