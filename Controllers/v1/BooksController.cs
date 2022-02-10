namespace GestionBibliothequeAPI.Controllers.v1
{
    [Authorize]
    public class BooksController : BaseController
    {
        public BooksController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksAsync()
        {
            var books = await _unitOfWork.Books.GetAllAsync();

            return Ok(books.Select(book => book.AsBookDto()));
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookByIdAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);

            return book == null ? NotFound("Cet auteur n'existe pas") : Ok(book.AsBookDto());
        }
        
        // GET: api/Books/params
        [HttpGet("params")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByParams(int? idAuthor, int? idCategory, string? status, string? title, string? publishingHouse, DateTime? publishDate)
        {
            var books = await _unitOfWork.Books.GetBooksByParams(idAuthor, idCategory, status, title, publishingHouse, publishDate);
           
            return books == null ? this.BadRequest("pas de livres") : Ok(books.Select(book => book.AsBookDto()));
        }

        // PUT: api/Books
        [HttpPut]
        public async Task<IActionResult> UpdateBookByIdAsync(Book book)
        {
            try
            {
                var result = await _unitOfWork.Books.Upsert(book);

                if (!result) return BadRequest("Erreur lors de la modification");

                await _unitOfWork.Authors.Upsert(book.Author);
                if(book.Category != null) await _unitOfWork.Categories.Upsert(book.Category);
                await _unitOfWork.CompleteAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<int>> AddBookAsync(Book book)
        {
            var a = await _unitOfWork.Books.Add(book);

            if(!a) return BadRequest("échec ajout");
            await _unitOfWork.CompleteAsync();

            return Ok(a);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookByIdAsync(int id)
        {
            try
            {
                var result = await _unitOfWork.Books.Delete(id);

                if (!result) return BadRequest(result);

                await _unitOfWork.CompleteAsync();
            }catch (Exception)
            {
                return NotFound();
            }
            
            return Ok("Suppression avec succès");
        }
    }
}
