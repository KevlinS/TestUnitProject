namespace GestionBibliothequeAPI.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        IAuthorRepository Authors { get; }

        IBookRepository Books { get; }

        ICategoryRepository Categories { get; }

        ILoanRepository Loans { get; }

        Task CompleteAsync();
    }
}
