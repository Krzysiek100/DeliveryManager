using System;
using System.Threading.Tasks;
using API.DTOs;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync(UserToRegisterDTO userToRegisterDTO, RoleName roleName)
        {
            try
            {
                return Ok(await _authService.RegisterUserAsync(userToRegisterDTO, roleName));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync(UserToLoginDTO userToLoginDTO)
        {
            try
            {
                return Ok(await _authService.LoginUserAsync(userToLoginDTO));
            }
            catch(Exception e)
            {
                return Unauthorized(e.Message);
            }
        }
    }
}