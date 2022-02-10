namespace GestionBibliothequeAPI.Controllers.v1
{
    [Authorize]
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();

            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);

            return category == null ? NotFound("Cet auteur n'existe pas") : Ok(category);
        }

        // PUT: api/Categories
        [HttpPut]
        public async Task<IActionResult> UpdateCategoryByIdAsync(Category category)
        {
            try
            {
                var result = await _unitOfWork.Categories.Upsert(category);

                if (!result) return BadRequest("Erreur lors de la modification");
                await _unitOfWork.CompleteAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<Category>> AddCategoryAsync(Category category)
        {
            var a = await _unitOfWork.Categories.Add(category);

            if (!a) return BadRequest("échec ajout");
            await _unitOfWork.CompleteAsync();

            return Ok(a);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryByIdAsync(int id)
        {
            try
            {
                var result = await _unitOfWork.Categories.Delete(id);

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
