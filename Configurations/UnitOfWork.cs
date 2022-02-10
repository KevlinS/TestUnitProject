namespace GestionBibliothequeAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly LibraryContext _context;

        private readonly ILogger _logger;

        public IUserRepository Users { get; private set; }

        public IBookRepository Books { get; private set; }

        public IAuthorRepository Authors { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public ILoanRepository Loans { get; private set; }

        public UnitOfWork(LibraryContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("db_logs");

            Users = new UserRepository(context, _logger);
            Authors = new AuthorRepository(context, _logger);
            Books = new BookRepository(context, _logger);
            Categories = new CategoryRepository(context, _logger);
            Loans = new LoanRepository(context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
