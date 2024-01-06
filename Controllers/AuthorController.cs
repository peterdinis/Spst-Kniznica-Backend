using LibrarySPSTApi.Entities;
using LibrarySPSTApi.Interfaces;
using LibrarySPSTApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySPSTApi.Controllers;

[Route("Authors")]
[ApiController]
public class AuthorController(IAuthorService authorService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Author>>> GetAllAuthors()
    {
        var authors = await authorService.GetAllAuthorsAsync();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> GetAuthorById(int id)
    {
        var author = await authorService.GetAuthorByIdAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        return Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<Author>> CreateAuthor(Author newAuthor)
    {
        var createdAuthor = await authorService.CreateAuthorAsync(newAuthor);
        return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, createdAuthor);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuthor(int id)
    {
        var author = await authorService.GetAuthorByIdAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        var deleted = await authorService.DeleteAuthorAsync(id);

        if (deleted)
        {
            return NoContent();
        }

        return StatusCode(500);
    }
}
