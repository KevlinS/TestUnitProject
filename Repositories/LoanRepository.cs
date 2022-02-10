namespace GestionBibliothequeAPI.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(LibraryContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<int> AddLoanAsync(Loan loan)
        {
            var bookExist = _context.Books.Any(e => e.Id == loan.IdBook);
            var userExist = _context.Users.Any(e => e.Id == loan.IdUser);
            if (bookExist && userExist)
            {
                if (_context.Books.FirstOrDefault(e => e.Id == loan.IdBook)?.Status != "disponible")
                    throw new Exception("Livre pas diponible");
                _context.Loans.Add(loan);
            }
            else
                throw new ArgumentException();

            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Loan>> GetLoansByParams(int? idBook, int? idUser, string? status, DateTime? minStartDate, DateTime? minEndDate)
        {
            var result = await _context.Loans.ToListAsync();

            if (idBook != null && idBook > 0)
                result = result.Where(x => x.IdBook == idBook).ToList();
            if (idUser != null && idUser > 0)
                result = result.Where(x => x.IdUser == idUser).ToList();
            if (status != null && status != String.Empty)
                result = result.Where(x => x.Status == status).ToList();
            if(minStartDate != null)
                result = result.Where(x => x.StartDate >= minStartDate).ToList();  
            if(minEndDate != null)
                result = result.Where(x => x.EndDate >= minEndDate).ToList();    

            return result;
        }
    }
}
