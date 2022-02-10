namespace GestionBibliothequeAPI.Controllers.v1
{
    [Authorize]
    public class AuthorsController : BaseController
    {
        public AuthorsController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthorsAsync()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();

            return Ok(authors);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorByIdAsync(int id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);

            return author == null ? NotFound("Cet auteur n'existe pas") : Ok(author);
        }

        // PUT: api/Authors
        [HttpPut]
        public async Task<IActionResult> UpdateAuthorByIdAsync(Author author)
        {
            try
            {
                var result = await _unitOfWork.Authors.Upsert(author);

                if (!result) return BadRequest("Erreur lors de la modification");
                await _unitOfWork.CompleteAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Authors
        [HttpPost]
        public async Task<ActionResult<Author>> AddAuthorAsync(Author author)
        {
            var a = await _unitOfWork.Authors.Add(author);

            if (!a) return BadRequest("échec ajout");
            await _unitOfWork.CompleteAsync();

            return Ok(a);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthorByIdAsync(int id)
        {
            try
            {
                var result = await _unitOfWork.Authors.Delete(id);

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
