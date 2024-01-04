using Microsoft.AspNetCore.Mvc;
using LibrarySPSTApi.Entities;
using LibrarySPSTApi.Interfaces;

namespace LibrarySPSTApi.Controllers
{
    [ApiController]
    [Route("/Books")] 
    public class BookController(IBookService bookService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var books = await bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            var addedBook = await bookService.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBookById), new { id = addedBook!.Id }, addedBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            await bookService.UpdateBookAsync(id, book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await bookService.DeleteBookAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}