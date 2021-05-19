using System;
using System.Threading.Tasks;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{userID}")]
        public async Task<IActionResult> DeleteUserAsync(int userID)
        {
            try
            {
                return Ok(await _userService.DeleteUserAsync(userID));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("{userID}")]
        public async Task<IActionResult> GetSingleUserByID(int userID)
        {
            try
            {
                return Ok(await _userService.GetSingleUserByID(userID));
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
        
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        public async Task<IActionResult> GetAllUserAsync()
        {
            try
            {
                var id =
                    int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    
                return Ok(await _userService.GetAllUserAsync(id));
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

    }
}