using WeiaCraftsApplication.DTOs;
using WeiaCraftsApplication.Services;
using WeiaCraftsDomain.Entities;

namespace WeiaCraftsApplication.Interfaces;

public interface IRoleService
{
    Task<ApiResponse<RoleDto>> CreateRoleAsync(CreateRoleDto createRoleDto);
    Task<ApiResponse<RoleDto>> UpdateRoleAsync(int roleId, UpdateRoleDto updateRoleDto);
    Task<ApiResponse> DeleteRoleAsync(int roleId);
    Task<ApiResponse<RoleDto>> GetRoleByIdAsync(int roleId);
    Task<ApiResponse<List<RoleDto>>> GetAllRolesAsync();
    
    // User role management
    Task<ApiResponse> AssignRoleToUserAsync(string userName, int roleId, string? assignedBy = null, DateTime? expiresAt = null);
    Task<ApiResponse> RemoveRoleFromUserAsync(string userName, int roleId);
    Task<ApiResponse> DeactivateUserRoleAsync(string userName, int roleId);
    Task<ApiResponse> ReactivateUserRoleAsync(string userName, int roleId);
    Task<ApiResponse<List<string>>> GetUserRolesAsync(string userName);
    
    // System role management
    Task<ApiResponse> SetDefaultRoleAsync(int roleId);
    Task<ApiResponse> SetSystemRoleAsync(int roleId);
    Task<ApiResponse<List<RoleDto>>> GetDefaultRolesAsync();
} 