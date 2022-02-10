namespace GestionBibliothequeAPI.Repositories.Interfaces
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task<IEnumerable<Loan>> GetLoansByParams(int? idBook, int? idUser, string? status, DateTime? minStartDate, DateTime? minEndDate);

        Task<int> AddLoanAsync(Loan loan);
    }
}
