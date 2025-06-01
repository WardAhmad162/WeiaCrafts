using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using WeiaCraftsApplication.DTOs;
using WeiaCraftsApplication.Interfaces;
using WeiaCraftsDomain.Entities;
using WeiaCraftsInfrastructure.Data;

namespace WeiaCraftsApplication.Services;

public class UserService : IUserService
{
    private readonly WeiaCraftsContext _context;
    private readonly TokenService _tokenService;

    public UserService(WeiaCraftsContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterDto registerDto)
    {
        var response = new ApiResponse<AuthResponseDto>();

        try
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == registerDto.UserName || u.Email == registerDto.Email);

            if (existingUser != null)
            {
                response.Success = false;
                response.Message = "Registration failed";
                if (existingUser.UserName == registerDto.UserName)
                    response.Errors.Add("Username is already taken");
                if (existingUser.Email == registerDto.Email)
                    response.Errors.Add("Email is already registered");
                return response;
            }

            // Create new trainee
            var newUser = new Trainee
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Password = HashPassword(registerDto.Password),
                Gender = registerDto.Gender,
                BirthDate = registerDto.DateOfBirth,
                AccountStatusId = 1, // Active status
                IsActive = true,
                RegisterationDate = DateOnly.FromDateTime(DateTime.UtcNow),
                CreatedAt = DateTime.UtcNow
            };

            // Add trainee role
            var traineeRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Trainee");
            if (traineeRole == null)
            {
                traineeRole = new Role { Name = "Trainee" };
                _context.Roles.Add(traineeRole);
                await _context.SaveChangesAsync();
            }

            newUser.UserRoles.Add(new UserRole
            {
                User = newUser,
                Role = traineeRole
            });

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Registration successful";
            response.Data = new AuthResponseDto 
            { 
                Token = _tokenService.GenerateToken(newUser),
                UserName = newUser.UserName,
                Email = newUser.Email,
                Roles = new List<string> { "Trainee" }
            };
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Registration failed";
            response.Errors.Add("An unexpected error occurred during registration");
        }

        return response;
    }

    public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto loginDto)
    {
        var response = new ApiResponse<AuthResponseDto>();

        try
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null || !VerifyPassword(loginDto.Password, user.Password))
            {
                response.Success = false;
                response.Message = "Login failed";
                response.Errors.Add("Invalid email or password");
                return response;
            }

            if (!user.IsActive)
            {
                response.Success = false;
                response.Message = "Login failed";
                response.Errors.Add("Account is disabled");
                return response;
            }

            response.Success = true;
            response.Message = "Login successful";
            response.Data = new AuthResponseDto
            {
                Token = _tokenService.GenerateToken(user),
                UserName = user.UserName,
                Email = user.Email,
                Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList()
            };
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Login failed";
            response.Errors.Add("An unexpected error occurred during login");
        }

        return response;
    }

    public async Task<ApiResponse<UserProfileDto>> GetProfileAsync(ClaimsPrincipal user)
    {
        var response = new ApiResponse<UserProfileDto>();

        try
        {
            var username = user.Identity?.Name;
            var dbUser = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (dbUser == null)
            {
                response.Success = false;
                response.Message = "Profile retrieval failed";
                response.Errors.Add("User not found");
                return response;
            }

            response.Success = true;
            response.Message = "Profile retrieved successfully";
            response.Data = new UserProfileDto
            {
                UserName = dbUser.UserName,
                Email = dbUser.Email,
                Gender = dbUser.Gender,
                BirthDate = dbUser.BirthDate,
                ProfilePictureUrl = dbUser.ProfilePictureUrl,
                Roles = dbUser.UserRoles.Select(ur => ur.Role.Name).ToList()
            };
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Profile retrieval failed";
            response.Errors.Add("An unexpected error occurred while retrieving the profile");
        }

        return response;
    }

    public async Task<ApiResponse> UpdateProfileAsync(ClaimsPrincipal user, UpdateProfileDto updateDto)
    {
        var response = new ApiResponse();

        try
        {
            var username = user.Identity?.Name;
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (dbUser == null)
            {
                response.Success = false;
                response.Message = "Profile update failed";
                response.Errors.Add("User not found");
                return response;
            }

            if (!string.IsNullOrEmpty(updateDto.Email))
            {
                var emailExists = await _context.Users
                    .AnyAsync(u => u.Email == updateDto.Email && u.UserName != username);
                if (emailExists)
                {
                    response.Success = false;
                    response.Message = "Profile update failed";
                    response.Errors.Add("Email is already in use");
                    return response;
                }
                dbUser.Email = updateDto.Email;
            }

            if (!string.IsNullOrEmpty(updateDto.Gender))
                dbUser.Gender = updateDto.Gender;
            if (updateDto.BirthDate.HasValue)
                dbUser.BirthDate = updateDto.BirthDate;
            if (!string.IsNullOrEmpty(updateDto.ProfilePictureUrl))
                dbUser.ProfilePictureUrl = updateDto.ProfilePictureUrl;

            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Profile updated successfully";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Profile update failed";
            response.Errors.Add("An unexpected error occurred while updating the profile");
        }

        return response;
    }

    private string HashPassword(string password)
    {
        using var hmac = new HMACSHA512();
        var salt = hmac.Key;
        var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        
        byte[] hashBytes = new byte[salt.Length + hash.Length];
        Array.Copy(salt, 0, hashBytes, 0, salt.Length);
        Array.Copy(hash, 0, hashBytes, salt.Length, hash.Length);
        
        return Convert.ToBase64String(hashBytes);
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        byte[] hashBytes = Convert.FromBase64String(storedHash);
        byte[] salt = new byte[64];
        Array.Copy(hashBytes, 0, salt, 0, salt.Length);
        
        using var hmac = new HMACSHA512(salt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != hashBytes[i + salt.Length])
                return false;
        }
        return true;
    }
}
