using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WeiaCraftsApplication.DTOs;
using WeiaCraftsApplication.Interfaces;
using WeiaCraftsDomain.Entities;
using WeiaCraftsInfrastructure.Data;

namespace WeiaCraftsApplication.Services;

public class AdminService : IAdminService
{
    private readonly WeiaCraftsContext _context;
    private readonly TokenService _tokenService;

    public AdminService(WeiaCraftsContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<ApiResponse<AdminDto>> CreateAdminAsync(CreateAdminDto createAdminDto)
    {
        var response = new ApiResponse<AdminDto>();

        try
        {
            var existingAdmin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Email == createAdminDto.Email || a.AdminName == createAdminDto.AdminName);

            if (existingAdmin != null)
            {
                response.Success = false;
                response.Message = "Admin creation failed";
                if (existingAdmin.Email == createAdminDto.Email)
                    response.Errors.Add("Email is already registered");
                if (existingAdmin.AdminName == createAdminDto.AdminName)
                    response.Errors.Add("Admin name is already taken");
                return response;
            }

            var admin = new Admin
            {
                AdminName = createAdminDto.AdminName,
                Email = createAdminDto.Email,
                Password = HashPassword(createAdminDto.Password),
                Permissions = createAdminDto.Permissions,
                PlatformPolicies = createAdminDto.PlatformPolicies,
                ThirdPartyIntegrations = createAdminDto.ThirdPartyIntegrations,
                ChatbotUpdates = createAdminDto.ChatbotUpdates,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Admin created successfully";
            response.Data = MapAdminToDto(admin);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Admin creation failed";
            response.Errors.Add("An unexpected error occurred");
        }

        return response;
    }

    public async Task<ApiResponse<AdminDto>> UpdateAdminAsync(int id, UpdateAdminDto updateAdminDto)
    {
        var response = new ApiResponse<AdminDto>();

        try
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                response.Success = false;
                response.Message = "Admin update failed";
                response.Errors.Add("Admin not found");
                return response;
            }

            if (updateAdminDto.Email != null && updateAdminDto.Email != admin.Email)
            {
                var emailExists = await _context.Admins
                    .AnyAsync(a => a.Email == updateAdminDto.Email && a.Id != id);
                if (emailExists)
                {
                    response.Success = false;
                    response.Message = "Admin update failed";
                    response.Errors.Add("Email is already registered");
                    return response;
                }
                admin.Email = updateAdminDto.Email;
            }

            if (updateAdminDto.AdminName != null && updateAdminDto.AdminName != admin.AdminName)
            {
                var nameExists = await _context.Admins
                    .AnyAsync(a => a.AdminName == updateAdminDto.AdminName && a.Id != id);
                if (nameExists)
                {
                    response.Success = false;
                    response.Message = "Admin update failed";
                    response.Errors.Add("Admin name is already taken");
                    return response;
                }
                admin.AdminName = updateAdminDto.AdminName;
            }

            if (!string.IsNullOrEmpty(updateAdminDto.Password))
            {
                admin.Password = HashPassword(updateAdminDto.Password);
            }

            if (updateAdminDto.Permissions != null)
                admin.Permissions = updateAdminDto.Permissions;

            if (updateAdminDto.PlatformPolicies != null)
                admin.PlatformPolicies = updateAdminDto.PlatformPolicies;

            if (updateAdminDto.ThirdPartyIntegrations != null)
                admin.ThirdPartyIntegrations = updateAdminDto.ThirdPartyIntegrations;

            if (updateAdminDto.ChatbotUpdates != null)
                admin.ChatbotUpdates = updateAdminDto.ChatbotUpdates;

            admin.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Admin updated successfully";
            response.Data = MapAdminToDto(admin);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Admin update failed";
            response.Errors.Add("An unexpected error occurred");
        }

        return response;
    }

    public async Task<ApiResponse<AdminDto>> GetAdminByIdAsync(int id)
    {
        var response = new ApiResponse<AdminDto>();

        try
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                response.Success = false;
                response.Message = "Admin retrieval failed";
                response.Errors.Add("Admin not found");
                return response;
            }

            response.Success = true;
            response.Message = "Admin retrieved successfully";
            response.Data = MapAdminToDto(admin);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Admin retrieval failed";
            response.Errors.Add("An unexpected error occurred");
        }

        return response;
    }

    public async Task<ApiResponse<AdminDto>> GetAdminByEmailAsync(string email)
    {
        var response = new ApiResponse<AdminDto>();

        try
        {
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Email == email);

            if (admin == null)
            {
                response.Success = false;
                response.Message = "Admin retrieval failed";
                response.Errors.Add("Admin not found");
                return response;
            }

            response.Success = true;
            response.Message = "Admin retrieved successfully";
            response.Data = MapAdminToDto(admin);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Admin retrieval failed";
            response.Errors.Add("An unexpected error occurred");
        }

        return response;
    }

    public async Task<ApiResponse<List<AdminDto>>> GetAllAdminsAsync()
    {
        var response = new ApiResponse<List<AdminDto>>();

        try
        {
            var admins = await _context.Admins.ToListAsync();

            response.Success = true;
            response.Message = "Admins retrieved successfully";
            response.Data = admins.Select(MapAdminToDto).ToList();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Admins retrieval failed";
            response.Errors.Add("An unexpected error occurred");
        }

        return response;
    }

    public async Task<ApiResponse> DeleteAdminAsync(int id)
    {
        var response = new ApiResponse();

        try
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                response.Success = false;
                response.Message = "Admin deletion failed";
                response.Errors.Add("Admin not found");
                return response;
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Admin deleted successfully";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Admin deletion failed";
            response.Errors.Add("An unexpected error occurred");
        }

        return response;
    }

    public async Task<ApiResponse> DeactivateAdminAsync(int id)
    {
        var response = new ApiResponse();

        try
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                response.Success = false;
                response.Message = "Admin deactivation failed";
                response.Errors.Add("Admin not found");
                return response;
            }

            admin.IsActive = false;
            admin.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Admin deactivated successfully";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Admin deactivation failed";
            response.Errors.Add("An unexpected error occurred");
        }

        return response;
    }

    public async Task<ApiResponse> ReactivateAdminAsync(int id)
    {
        var response = new ApiResponse();

        try
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                response.Success = false;
                response.Message = "Admin reactivation failed";
                response.Errors.Add("Admin not found");
                return response;
            }

            admin.IsActive = true;
            admin.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Admin reactivated successfully";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Admin reactivation failed";
            response.Errors.Add("An unexpected error occurred");
        }

        return response;
    }

    public async Task<ApiResponse<AdminDto>> LoginAsync(string email, string password)
    {
        var response = new ApiResponse<AdminDto>();

        try
        {
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Email == email);

            if (admin == null || !VerifyPassword(password, admin.Password))
            {
                response.Success = false;
                response.Message = "Login failed";
                response.Errors.Add("Invalid email or password");
                return response;
            }

            if (!admin.IsActive)
            {
                response.Success = false;
                response.Message = "Login failed";
                response.Errors.Add("Account is deactivated");
                return response;
            }

            admin.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Login successful";
            response.Data = MapAdminToDto(admin);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Login failed";
            response.Errors.Add("An unexpected error occurred");
        }

        return response;
    }

    public async Task<ApiResponse<List<UserProfileDto>>> GetAllUsersAsync()
    {
        var response = new ApiResponse<List<UserProfileDto>>();

        try
        {
            var users = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();

            response.Success = true;
            response.Message = "Users retrieved successfully";
            response.Data = users.Select(u => new UserProfileDto
            {
                UserName = u.UserName,
                Email = u.Email,
                Gender = u.Gender,
                BirthDate = u.BirthDate,
                ProfilePictureUrl = u.ProfilePictureUrl,
                Roles = u.UserRoles.Select(ur => ur.Role.Name).ToList()
            }).ToList();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Users retrieval failed";
            response.Errors.Add("An unexpected error occurred");
        }

        return response;
    }

    public async Task<ApiResponse> BlockUserAsync(string userName)
    {
        var response = new ApiResponse();

        try
        {
            var user = await _context.Users.FindAsync(userName);

            if (user == null)
            {
                response.Success = false;
                response.Message = "User block failed";
                response.Errors.Add("User not found");
                return response;
            }

            user.IsActive = false;
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "User blocked successfully";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "User block failed";
            response.Errors.Add("An unexpected error occurred");
        }

        return response;
    }

    public async Task<ApiResponse> UnblockUserAsync(string userName)
    {
        var response = new ApiResponse();

        try
        {
            var user = await _context.Users.FindAsync(userName);

            if (user == null)
            {
                response.Success = false;
                response.Message = "User unblock failed";
                response.Errors.Add("User not found");
                return response;
            }

            user.IsActive = true;
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "User unblocked successfully";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "User unblock failed";
            response.Errors.Add("An unexpected error occurred");
        }

        return response;
    }

    public async Task<ApiResponse> UpdatePlatformPoliciesAsync(int adminId, string policies)
    {
        var response = new ApiResponse();

        try
        {
            var admin = await _context.Admins.FindAsync(adminId);

            if (admin == null)
            {
                response.Success = false;
                response.Message = "Policy update failed";
                response.Errors.Add("Admin not found");
                return response;
            }

            admin.PlatformPolicies = policies;
            admin.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Platform policies updated successfully";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Policy update failed";
            response.Errors.Add("An unexpected error occurred");
        }

        return response;
    }

    public async Task<ApiResponse> UpdatePermissionsAsync(int adminId, string permissions)
    {
        var response = new ApiResponse();

        try
        {
            var admin = await _context.Admins.FindAsync(adminId);

            if (admin == null)
            {
                response.Success = false;
                response.Message = "Permissions update failed";
                response.Errors.Add("Admin not found");
                return response;
            }

            admin.Permissions = permissions;
            admin.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Permissions updated successfully";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Permissions update failed";
            response.Errors.Add("An unexpected error occurred");
        }

        return response;
    }

    public async Task<ApiResponse> UpdateThirdPartyIntegrationsAsync(int adminId, string integrations)
    {
        var response = new ApiResponse();

        try
        {
            var admin = await _context.Admins.FindAsync(adminId);

            if (admin == null)
            {
                response.Success = false;
                response.Message = "Integrations update failed";
                response.Errors.Add("Admin not found");
                return response;
            }

            admin.ThirdPartyIntegrations = integrations;
            admin.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Third-party integrations updated successfully";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Integrations update failed";
            response.Errors.Add("An unexpected error occurred");
        }

        return response;
    }

    public async Task<ApiResponse> UpdateChatbotSettingsAsync(int adminId, string settings)
    {
        var response = new ApiResponse();

        try
        {
            var admin = await _context.Admins.FindAsync(adminId);

            if (admin == null)
            {
                response.Success = false;
                response.Message = "Settings update failed";
                response.Errors.Add("Admin not found");
                return response;
            }

            admin.ChatbotUpdates = settings;
            admin.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Chatbot settings updated successfully";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Settings update failed";
            response.Errors.Add("An unexpected error occurred");
        }

        return response;
    }

    private AdminDto MapAdminToDto(Admin admin)
    {
        return new AdminDto
        {
            Id = admin.Id,
            AdminName = admin.AdminName,
            Email = admin.Email,
            Permissions = admin.Permissions,
            PlatformPolicies = admin.PlatformPolicies,
            ThirdPartyIntegrations = admin.ThirdPartyIntegrations,
            ChatbotUpdates = admin.ChatbotUpdates,
            IsActive = admin.IsActive,
            CreatedAt = admin.CreatedAt,
            LastLoginAt = admin.LastLoginAt,
            UpdatedAt = admin.UpdatedAt
        };
    }

    private string HashPassword(string password)
    {
        using var hmac = new HMACSHA512();
        var salt = hmac.Key;
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        
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
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != hashBytes[i + salt.Length])
                return false;
        }
        return true;
    }
}