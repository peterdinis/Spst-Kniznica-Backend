using LibrarySPSTApi.Dtos;
using LibrarySPSTApi.Interfaces;
namespace LibrarySPSTApi.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class StudentController: ControllerBase
{
    private readonly IAuthService _authService;

    public StudentController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var registerResult = await _authService.RegisterAsync(registerDto);

        if (registerResult.IsSucceed)
            return Ok(registerResult);

        return BadRequest(registerResult);
    }
}