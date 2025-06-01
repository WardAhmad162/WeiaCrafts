using WeiaCraftsApplication.DTOs;
using WeiaCraftsApplication.Services;

namespace WeiaCraftsApplication.Interfaces
{
    public interface IAdminService
    {
        Task<ApiResponse<AdminDto>> CreateAdminAsync(CreateAdminDto createAdminDto);
        Task<ApiResponse<AdminDto>> UpdateAdminAsync(int id, UpdateAdminDto updateAdminDto);
        Task<ApiResponse<AdminDto>> GetAdminByIdAsync(int id);
        Task<ApiResponse<AdminDto>> GetAdminByEmailAsync(string email);
        Task<ApiResponse<List<AdminDto>>> GetAllAdminsAsync();
        Task<ApiResponse> DeleteAdminAsync(int id);
        Task<ApiResponse> DeactivateAdminAsync(int id);
        Task<ApiResponse> ReactivateAdminAsync(int id);
        Task<ApiResponse<AdminDto>> LoginAsync(string email, string password);
        
        // User Management
        Task<ApiResponse<List<UserProfileDto>>> GetAllUsersAsync();
        Task<ApiResponse> BlockUserAsync(string userName);
        Task<ApiResponse> UnblockUserAsync(string userName);

        Task<ApiResponse> UpdatePlatformPoliciesAsync(int adminId, string policies);
        Task<ApiResponse> UpdatePermissionsAsync(int adminId, string permissions);
        Task<ApiResponse> UpdateThirdPartyIntegrationsAsync(int adminId, string integrations);
        Task<ApiResponse> UpdateChatbotSettingsAsync(int adminId, string settings);
    }
}
