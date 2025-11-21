using J3M.Shared.DTOs.Users;
using J3m_BE.Exceptions;
using J3m_BE.Models;
using J3m_BE.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace J3m_BE.Services.Implementations;


// Handles user registration & login and issues JWT tokens.

public class AuthService : IAuthService
{
    private const string DefaultRole = "User";
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;

    public AuthService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
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

        // Ensure default role exists, then assign
        if (!await _roleManager.RoleExistsAsync(DefaultRole))
            _ = await _roleManager.CreateAsync(new IdentityRole(DefaultRole));

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
            throw new ValidationDomainException("Invalid credentials.");

        var check = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: true);
        if (!check.Succeeded)
            throw new ValidationDomainException("Invalid credentials.");

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

        var expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSection["ExpiresMinutes"] ?? "60"));


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
