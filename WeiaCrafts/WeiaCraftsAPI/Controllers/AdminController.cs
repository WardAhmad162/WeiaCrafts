using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeiaCraftsApplication.DTOs;
using WeiaCraftsApplication.Interfaces;
using WeiaCraftsApplication.Services;

namespace WeiaCraftsAPI.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/admin")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<AdminDto>>> Login([FromBody] LoginDto loginDto)
    {
        var result = await _adminService.LoginAsync(loginDto.Email, loginDto.Password);
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<AdminDto>>> CreateAdmin([FromBody] CreateAdminDto createAdminDto)
    {
        var result = await _adminService.CreateAdminAsync(createAdminDto);
        if (!result.Success)
            return BadRequest(result);
        return CreatedAtAction(nameof(GetAdminById), new { id = result.Data.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<AdminDto>>> UpdateAdmin(int id, [FromBody] UpdateAdminDto updateAdminDto)
    {
        var result = await _adminService.UpdateAdminAsync(id,updateAdminDto);
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<AdminDto>>> GetAdminById(int id)
    {
        var result = await _adminService.GetAdminByIdAsync(id);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<AdminDto>>>> GetAllAdmins()
    {
        var result = await _adminService.GetAllAdminsAsync();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> DeleteAdmin(int id)
    {
        var result = await _adminService.DeleteAdminAsync(id);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpPut("{id}/deactivate")]
    public async Task<ActionResult<ApiResponse>> DeactivateAdmin(int id)
    {
        var result = await _adminService.DeactivateAdminAsync(id);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpPut("{id}/reactivate")]
    public async Task<ActionResult<ApiResponse>> ReactivateAdmin(int id)
    {
        var result = await _adminService.ReactivateAdminAsync(id);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpGet("users")]
    public async Task<ActionResult<ApiResponse<List<UserProfileDto>>>> GetAllUsers()
    {
        var result = await _adminService.GetAllUsersAsync();
        return Ok(result);
    }

    [HttpPut("users/{userName}/block")]
    public async Task<ActionResult<ApiResponse>> BlockUser(string userName)
    {
        var result = await _adminService.BlockUserAsync(userName);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpPut("users/{userName}/unblock")]
    public async Task<ActionResult<ApiResponse>> UnblockUser(string userName)
    {
        var result = await _adminService.UnblockUserAsync(userName);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpPut("{id}/policies")]
    public async Task<ActionResult<ApiResponse>> UpdatePlatformPolicies(int id, [FromBody] string policies)
    {
        var result = await _adminService.UpdatePlatformPoliciesAsync(id, policies);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpPut("{id}/permissions")]
    public async Task<ActionResult<ApiResponse>> UpdatePermissions(int id, [FromBody] string permissions)
    {
        var result = await _adminService.UpdatePermissionsAsync(id, permissions);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpPut("{id}/integrations")]
    public async Task<ActionResult<ApiResponse>> UpdateThirdPartyIntegrations(int id, [FromBody] string integrations)
    {
        var result = await _adminService.UpdateThirdPartyIntegrationsAsync(id, integrations);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }

    [HttpPut("{id}/chatbot")]
    public async Task<ActionResult<ApiResponse>> UpdateChatbotSettings(int id, [FromBody] string settings)
    {
        var result = await _adminService.UpdateChatbotSettingsAsync(id, settings);
        if (!result.Success)
            return NotFound(result);
        return Ok(result);
    }
}
