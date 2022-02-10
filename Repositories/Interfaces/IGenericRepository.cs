namespace GestionBibliothequeAPI.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<bool> Add(T entity);

        Task<bool> Delete(int id);

        Task<bool> Upsert(T entity);
    }
}
