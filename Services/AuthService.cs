using LibrarySPSTApi.Dtos;
using LibrarySPSTApi.Entities;
using LibrarySPSTApi.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LibrarySPSTApi.Services;

public class AuthService: IAuthService
{
    private readonly UserManager<Student> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly JwtService _jwtService;

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser = await _userManager.FindByNameAsync(registerDto.UserName);

        if (existingUser != null)
        {
            throw new Exception("User already exists");
        }

        Student student = new Student()
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            UserName = registerDto.UserName,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        
        var createUserResult = await _userManager.CreateAsync(student, registerDto.Password);

        if (!createUserResult.Succeeded)
        {
            var errorString = createUserResult.Errors.Aggregate("User Creation Failed Beacause: ", (current, error) => current + (" # " + error.Description));
            return new AuthResponseDto()
            {
                IsSucceed = false,
                Message = errorString
            };
        }
        
        await _userManager.AddToRoleAsync(student, "STUDENT");

        return new AuthResponseDto()
        {
            IsSucceed = true,
            Message = "User Created Successfully",
            ApplicationUser = student
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);

        if (user is null)
            return new AuthResponseDto()
            {
                IsSucceed = false,
                Message = "Invalid Credentials"
            };

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!isPasswordCorrect)
            return new AuthResponseDto()
            {
                IsSucceed = false,
                Message = "Invalid Credentials"
            };

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim("JWTID", Guid.NewGuid().ToString()),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName),
        };
        authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        var token = _jwtService.GenerateToken(authClaims); 

        return new AuthResponseDto()
        {
            IsSucceed = true,
            Message = token,
            ApplicationUser = user
        };
        
    }
}
