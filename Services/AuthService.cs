using LibrarySPSTApi.Dtos;
using LibrarySPSTApi.Entities;
using LibrarySPSTApi.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LibrarySPSTApi.Services;

public class AuthService: IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly JwtService _jwtService;
    
    public AuthService(JwtService jwtService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser = await _userManager.FindByNameAsync(registerDto.UserName);

        if (existingUser != null)
        {
            throw new Exception("User already exists");
        }

        ApplicationUser newUser = new ApplicationUser()
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            UserName = registerDto.UserName,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        
        var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

        if (!createUserResult.Succeeded)
        {
            var errorString = "User Creation Failed Beacause: ";
            foreach (var error in createUserResult.Errors)
            {
                errorString += " # " + error.Description;
            }
            return new AuthResponseDto()
            {
                IsSucceed = false,
                Message = errorString,
                ApplicationUser = newUser
            };
        }
        
        await _userManager.AddToRoleAsync(newUser, "USER");

        return new AuthResponseDto()
        {
            IsSucceed = true,
            Message = "User Created Successfully",
            ApplicationUser = newUser,
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
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim("JWTID", Guid.NewGuid().ToString()),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = _jwtService.GenerateToken(authClaims); 

        return new AuthResponseDto()
        {
            IsSucceed = true,
            Message = token,
            ApplicationUser = user
        };
        
    }
}
