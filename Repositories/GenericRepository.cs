namespace GestionBibliothequeAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LibraryContext _context;
        internal DbSet<T> dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(LibraryContext context, ILogger logger)
        {
            _context = context;
            dbSet = context.Set<T>();
            _logger = logger;
        }

        public virtual async Task<bool> Add(T entity)
        {
            var i = await dbSet.AddAsync(entity);

            return true;
        }

        public virtual async Task<bool> Delete(int id)
        {
            var entity = await GetByIdAsync(id);
            dbSet.Remove(entity);
            
            return true;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<bool> Upsert(T entity)
        {
            dbSet.Update(entity);

            return true;
        }
    }
}
