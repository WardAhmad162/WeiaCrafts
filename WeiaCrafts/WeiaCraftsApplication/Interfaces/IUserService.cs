using Microsoft.Exchange.WebServices.Data;
using System.Security.Claims;
using WeiaCraftsApplication.DTOs;
using WeiaCraftsApplication.Services;

namespace WeiaCraftsApplication.Interfaces;

public interface IUserService
{
    Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterDto registerDto);
    Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto loginDto);
    Task<ApiResponse<UserProfileDto>> GetProfileAsync(ClaimsPrincipal user);
    Task<ApiResponse> UpdateProfileAsync(ClaimsPrincipal user, UpdateProfileDto updateDto);
}
