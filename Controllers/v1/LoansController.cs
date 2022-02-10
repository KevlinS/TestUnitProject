namespace GestionBibliothequeAPI.Controllers.v1
{
    [Authorize]
    public class LoansController : BaseController
    {
        public LoansController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        // GET: api/Loans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoansAsync()
        {
            var loan = await _unitOfWork.Loans.GetAllAsync();

            return Ok(loan);
        }

        // GET: api/Loans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoanByIdAsync(int id)
        {
            var loan = await _unitOfWork.Loans.GetByIdAsync(id);

            return loan == null ? NotFound("Cet auteur n'existe pas") : Ok(loan);
        }

        // GET: api/Loans/params
        [HttpGet("params")]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoansByParams(int? idBook, int? idUser, string? status, DateTime? minStartDate, DateTime? minEndDate)
        {
            var loans = await _unitOfWork.Loans.GetLoansByParams(idBook, idUser, status, minStartDate, minEndDate);

            return loans == null ? this.BadRequest("pas de livres") : Ok(loans);
        }

        // PUT: api/Loans
        [HttpPut]
        public async Task<IActionResult> UpdateLoanByIdAsync(Loan loan)
        {
            try
            {
                var result = await _unitOfWork.Loans.Upsert(loan);

                if (!result) return BadRequest("Erreur lors de la modification");
                await _unitOfWork.CompleteAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Loans
        [HttpPost]
        public async Task<ActionResult<Loan>> AddLoanAsync(Loan loan)
        {
            try
            {
                var a = await _unitOfWork.Loans.AddLoanAsync(loan);
                if (a < 1) return BadRequest("échec ajout");
                await _unitOfWork.CompleteAsync();

                return Ok(a);
            }
            catch (ArgumentException)
            {
                return BadRequest("User or Book do not exist");
            }  
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Loans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoanByIdAsync(int id)
        {
            try
            {
                var result = await _unitOfWork.Loans.Delete(id);

                if (!result) return BadRequest(result);

                await _unitOfWork.CompleteAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return Ok("Suppression avec succès");
        }
    }
}
