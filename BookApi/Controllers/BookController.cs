using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Route("api/books")] // Route Decorotor
    [ApiController] // Api Decorator 

    // Controller class that is extended by ControllerBase Class 
    public class BookController : ControllerBase
    {

        //Declare an instance of the IBookRepositories 
        public readonly IBookRepositories _bookRepository;

        //Construct of the classs
        public BookController(IBookRepositories bookRepositories)
        {
            _bookRepository = bookRepositories;
        }


        // Http Get method for all books 
        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.Get();
        }


        // Http Get method for a book 
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            return await _bookRepository.Get(id);
        }


        // Http Post method to create a book
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            var newbook = await _bookRepository.Create(book);
            return CreatedAtAction(nameof(GetBooks), new { id = newbook.BookId }, newbook);
        }

        // Http Put methhod to update a book

        [HttpPut("{id}")]
        public async Task<ActionResult> PutBook(int id, Book book)

        {

            if ( book == null  || book.BookId != id)
            {
                return BadRequest();
            }

            var book_to_update = await _bookRepository.Get(id);


             if (book_to_update == null)
            {
                return BadRequest("Invalid book Id");
            }

            book_to_update.Description = book.Description;
            book_to_update.Title = book.Title;
            book_to_update.Author = book.Author;

           await _bookRepository.Update(book);
            return NoContent();


        }

        // Http delete method 

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book_to_delete = _bookRepository.Get(id);
            if ( book_to_delete == null)
            {
                return BadRequest("Invalid Book Id");
            }
            await _bookRepository.Delete(id);
            return NoContent();
        }
    }
}
