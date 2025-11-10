using J3m_BE.DTOs.Users.AuthDtos;
using J3m_BE.Models;                    
using J3m_BE.Services.Interfaces;      
using Microsoft.AspNetCore.Identity;   
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace J3m_BE.Services.Implementations;


// Handles user registration & login and issues JWT tokens.

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _config;

    public AuthService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _config = config;
    }

    
    //Creates an AppUser, assigns default "User" role, and returns a JWT.
    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var user = new AppUser
        {
            Email = dto.Email,
            UserName = dto.UserName,
            DisplayName = dto.DisplayName
        };

        var create = await _userManager.CreateAsync(user, dto.Password);
        if (!create.Succeeded)
            throw new Exception(string.Join("; ", create.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, "User");

        return await CreateTokenAsync(user);
    }

    
    //Validates credentials (email or username) and returns a JWT.
    
    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user =
            await _userManager.FindByEmailAsync(dto.EmailOrUserName) ??
            await _userManager.FindByNameAsync(dto.EmailOrUserName);

        if (user is null)
            throw new Exception("Invalid credentials.");

        var check = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: true);
        if (!check.Succeeded)
            throw new Exception("Invalid credentials.");

        return await CreateTokenAsync(user);
    }

    
    // Builds and signs a JWT containing user id, name, email, and role claims.
    private async Task<AuthResponseDto> CreateTokenAsync(AppUser user)
    {
        var jwtSection = _config.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
        };
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var expiresMinutes = double.TryParse(jwtSection["ExpiresMinutes"], out var m) ? m : 60d;
        var expires = DateTime.UtcNow.AddMinutes(expiresMinutes);

        var token = new JwtSecurityToken(
            issuer: jwtSection["Issuer"],
            audience: jwtSection["Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds);

        return new AuthResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAtUtc = expires
        };
    }
}
