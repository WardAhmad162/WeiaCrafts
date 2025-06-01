using Microsoft.EntityFrameworkCore;
using WeiaCraftsApplication.DTOs;
using WeiaCraftsApplication.Interfaces;
using WeiaCraftsDomain.Entities;
using WeiaCraftsInfrastructure.Data;

namespace WeiaCraftsApplication.Services;

public class RoleService : IRoleService
{
    private readonly WeiaCraftsContext _context;

    public RoleService(WeiaCraftsContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<RoleDto>> CreateRoleAsync(CreateRoleDto createRoleDto)
    {
        var response = new ApiResponse<RoleDto>();

        try
        {
            // Check if role name already exists
            if (await _context.Roles.AnyAsync(r => r.Name.ToUpper() == createRoleDto.Name.ToUpper()))
            {
                response.Success = false;
                response.Message = "Role creation failed";
                response.Errors.Add("Role name already exists");
                return response;
            }

            var role = new Role
            {
                Name = createRoleDto.Name,
                NormalizedName = createRoleDto.Name.ToUpper(),
                Description = createRoleDto.Description,
                IsDefault = createRoleDto.IsDefault,
                IsSystem = createRoleDto.IsSystem
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Role created successfully";
            response.Data = MapRoleToDto(role);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Role creation failed";
            response.Errors.Add("An unexpected error occurred while creating the role");
        }

        return response;
    }

    public async Task<ApiResponse<RoleDto>> UpdateRoleAsync(int roleId, UpdateRoleDto updateRoleDto)
    {
        var response = new ApiResponse<RoleDto>();

        try
        {
            var role = await _context.Roles.FindAsync(roleId);

            if (role == null)
            {
                response.Success = false;
                response.Message = "Role update failed";
                response.Errors.Add("Role not found");
                return response;
            }

            if (role.IsSystem)
            {
                response.Success = false;
                response.Message = "Role update failed";
                response.Errors.Add("System roles cannot be modified");
                return response;
            }

            if (updateRoleDto.Name != null)
            {
                var nameExists = await _context.Roles
                    .AnyAsync(r => r.Id != roleId && r.Name.ToUpper() == updateRoleDto.Name.ToUpper());
                if (nameExists)
                {
                    response.Success = false;
                    response.Message = "Role update failed";
                    response.Errors.Add("Role name already exists");
                    return response;
                }

                role.Name = updateRoleDto.Name;
                role.NormalizedName = updateRoleDto.Name.ToUpper();
            }

            if (updateRoleDto.Description != null)
                role.Description = updateRoleDto.Description;

            if (updateRoleDto.IsDefault.HasValue)
                role.IsDefault = updateRoleDto.IsDefault.Value;

            role.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Role updated successfully";
            response.Data = MapRoleToDto(role);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Role update failed";
            response.Errors.Add("An unexpected error occurred while updating the role");
        }

        return response;
    }

    public async Task<ApiResponse> DeleteRoleAsync(int roleId)
    {
        var response = new ApiResponse();

        try
        {
            var role = await _context.Roles
                .Include(r => r.UserRoles)
                .FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
            {
                response.Success = false;
                response.Message = "Role deletion failed";
                response.Errors.Add("Role not found");
                return response;
            }

            if (role.IsSystem)
            {
                response.Success = false;
                response.Message = "Role deletion failed";
                response.Errors.Add("System roles cannot be deleted");
                return response;
            }

            if (role.UserRoles.Any())
            {
                response.Success = false;
                response.Message = "Role deletion failed";
                response.Errors.Add("Cannot delete role with assigned users");
                return response;
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Role deleted successfully";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Role deletion failed";
            response.Errors.Add("An unexpected error occurred while deleting the role");
        }

        return response;
    }

    public async Task<ApiResponse<RoleDto>> GetRoleByIdAsync(int roleId)
    {
        var response = new ApiResponse<RoleDto>();

        try
        {
            var role = await _context.Roles
                .Include(r => r.UserRoles)
                .FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
            {
                response.Success = false;
                response.Message = "Role retrieval failed";
                response.Errors.Add("Role not found");
                return response;
            }

            response.Success = true;
            response.Message = "Role retrieved successfully";
            response.Data = MapRoleToDto(role);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Role retrieval failed";
            response.Errors.Add("An unexpected error occurred while retrieving the role");
        }

        return response;
    }

    public async Task<ApiResponse<List<RoleDto>>> GetAllRolesAsync()
    {
        var response = new ApiResponse<List<RoleDto>>();

        try
        {
            var roles = await _context.Roles
                .Include(r => r.UserRoles)
                .ToListAsync();

            response.Success = true;
            response.Message = "Roles retrieved successfully";
            response.Data = roles.Select(MapRoleToDto).ToList();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Roles retrieval failed";
            response.Errors.Add("An unexpected error occurred while retrieving roles");
        }

        return response;
    }

    public async Task<ApiResponse> AssignRoleToUserAsync(string userName, int roleId, string? assignedBy = null, DateTime? expiresAt = null)
    {
        var response = new ApiResponse();

        try
        {
            var user = await _context.Users.FindAsync(userName);
            if (user == null)
            {
                response.Success = false;
                response.Message = "Role assignment failed";
                response.Errors.Add("User not found");
                return response;
            }

            var role = await _context.Roles.FindAsync(roleId);
            if (role == null)
            {
                response.Success = false;
                response.Message = "Role assignment failed";
                response.Errors.Add("Role not found");
                return response;
            }

            var existingUserRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserName == userName && ur.RoleId == roleId);

            if (existingUserRole != null)
            {
                if (!existingUserRole.IsActive)
                {
                    existingUserRole.IsActive = true;
                    existingUserRole.ExpiresAt = expiresAt;
                    existingUserRole.AssignedBy = assignedBy;
                    await _context.SaveChangesAsync();

                    response.Success = true;
                    response.Message = "Role reactivated successfully";
                    return response;
                }

                response.Success = false;
                response.Message = "Role assignment failed";
                response.Errors.Add("User already has this role");
                return response;
            }

            var userRole = new UserRole
            {
                UserName = userName,
                User = user,
                RoleId = roleId,
                Role = role,
                ExpiresAt = expiresAt,
                AssignedBy = assignedBy
            };

            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Role assigned successfully";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Role assignment failed";
            response.Errors.Add("An unexpected error occurred while assigning the role");
        }

        return response;
    }

    public async Task<ApiResponse> RemoveRoleFromUserAsync(string userName, int roleId)
    {
        var response = new ApiResponse();

        try
        {
            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserName == userName && ur.RoleId == roleId);

            if (userRole == null)
            {
                response.Success = false;
                response.Message = "Role removal failed";
                response.Errors.Add("User does not have this role");
                return response;
            }

            var role = await _context.Roles.FindAsync(roleId);
            if (role?.IsSystem == true)
            {
                response.Success = false;
                response.Message = "Role removal failed";
                response.Errors.Add("System roles cannot be removed");
                return response;
            }

            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Role removed successfully";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Role removal failed";
            response.Errors.Add("An unexpected error occurred while removing the role");
        }

        return response;
    }

    public async Task<ApiResponse> DeactivateUserRoleAsync(string userName, int roleId)
    {
        var response = new ApiResponse();

        try
        {
            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserName == userName && ur.RoleId == roleId);

            if (userRole == null)
            {
                response.Success = false;
                response.Message = "Role deactivation failed";
                response.Errors.Add("User does not have this role");
                return response;
            }

            var role = await _context.Roles.FindAsync(roleId);
            if (role?.IsSystem == true)
            {
                response.Success = false;
                response.Message = "Role deactivation failed";
                response.Errors.Add("System roles cannot be deactivated");
                return response;
            }

            userRole.IsActive = false;
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Role deactivated successfully";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Role deactivation failed";
            response.Errors.Add("An unexpected error occurred while deactivating the role");
        }

        return response;
    }

    public async Task<ApiResponse> ReactivateUserRoleAsync(string userName, int roleId)
    {
        var response = new ApiResponse();

        try
        {
            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserName == userName && ur.RoleId == roleId);

            if (userRole == null)
            {
                response.Success = false;
                response.Message = "Role reactivation failed";
                response.Errors.Add("User does not have this role");
                return response;
            }

            userRole.IsActive = true;
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Role reactivated successfully";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Role reactivation failed";
            response.Errors.Add("An unexpected error occurred while reactivating the role");
        }

        return response;
    }

    public async Task<ApiResponse<List<string>>> GetUserRolesAsync(string userName)
    {
        var response = new ApiResponse<List<string>>();

        try
        {
            var roles = await _context.UserRoles
                .Include(ur => ur.Role)
                .Where(ur => ur.UserName == userName && ur.IsActive)
                .Select(ur => ur.Role.Name)
                .ToListAsync();

            response.Success = true;
            response.Message = "User roles retrieved successfully";
            response.Data = roles;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "User roles retrieval failed";
            response.Errors.Add("An unexpected error occurred while retrieving user roles");
        }

        return response;
    }

    public async Task<ApiResponse> SetDefaultRoleAsync(int roleId)
    {
        var response = new ApiResponse();

        try
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role == null)
            {
                response.Success = false;
                response.Message = "Setting default role failed";
                response.Errors.Add("Role not found");
                return response;
            }

            role.IsDefault = true;
            role.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Default role set successfully";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Setting default role failed";
            response.Errors.Add("An unexpected error occurred while setting the default role");
        }

        return response;
    }

    public async Task<ApiResponse> SetSystemRoleAsync(int roleId)
    {
        var response = new ApiResponse();

        try
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role == null)
            {
                response.Success = false;
                response.Message = "Setting system role failed";
                response.Errors.Add("Role not found");
                return response;
            }

            role.IsSystem = true;
            role.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "System role set successfully";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Setting system role failed";
            response.Errors.Add("An unexpected error occurred while setting the system role");
        }

        return response;
    }

    public async Task<ApiResponse<List<RoleDto>>> GetDefaultRolesAsync()
    {
        var response = new ApiResponse<List<RoleDto>>();

        try
        {
            var roles = await _context.Roles
                .Include(r => r.UserRoles)
                .Where(r => r.IsDefault)
                .ToListAsync();

            response.Success = true;
            response.Message = "Default roles retrieved successfully";
            response.Data = roles.Select(MapRoleToDto).ToList();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "Default roles retrieval failed";
            response.Errors.Add("An unexpected error occurred while retrieving default roles");
        }

        return response;
    }

    private RoleDto MapRoleToDto(Role role)
    {
        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            NormalizedName = role.NormalizedName,
            Description = role.Description,
            CreatedAt = role.CreatedAt,
            UpdatedAt = role.UpdatedAt,
            IsDefault = role.IsDefault,
            IsSystem = role.IsSystem,
            UsersCount = role.UserRoles.Count(ur => ur.IsActive)
        };
    }
} 