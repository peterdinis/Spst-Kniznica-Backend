using Microsoft.AspNetCore.Mvc;
using LibrarySPSTApi.Entities;
using LibrarySPSTApi.Services;

namespace LibrarySPSTApi.Controllers;

[Route("Authors")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly AuthorService _authorService;

    public AuthorController(AuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Author>>> GetAllAuthors()
    {
        var authors = await _authorService.GetAllAuthorsAsync();
        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> GetAuthorById(int id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        return Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<Author>> CreateAuthor(Author newAuthor)
    {
        var createdAuthor = await _authorService.CreateAuthorAsync(newAuthor);
        return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, createdAuthor);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Author>> UpdateAuthor(int id, Author updatedAuthor)
    {
        if (id != updatedAuthor.Id)
        {
            return BadRequest();
        }

        var author = await _authorService.GetAuthorByIdAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        var result = await _authorService.UpdateAuthorAsync(id, updatedAuthor);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuthor(int id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        var deleted = await _authorService.DeleteAuthorAsync(id);

        if (deleted)
        {
            return NoContent();
        }

        return StatusCode(500);
    }
}