using System;
using System.Threading.Tasks;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageStatusController : ControllerBase
    {
        private readonly IPackageStatusService _packageStatusService;

        public PackageStatusController(IPackageStatusService packageStatusService)
        {
            _packageStatusService = packageStatusService;
        }

        [Authorize(Policy = "RequireDeliveryManRole")]
        [HttpPost]
        public async Task<IActionResult> AddStatusAsync(int packageID, StatusName status)
        {
            try
            {
                return Ok(await _packageStatusService.AddStatusAsync(packageID, status));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "RequireDeliveryManRole")]
        [HttpDelete("{statusID}")]
        public async Task<IActionResult> DeleteStatusAsync(int statusID)
        {
            try
            {
                return Ok(await _packageStatusService.DeleteStatusAsync(statusID));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //User also sees that
        [HttpGet("{packageID}")]
        public async Task<IActionResult> GetAllPackageStatusesAsync(int packageID)
        {
            try
            {
                return Ok(await _packageStatusService.GetAllPackageStatusesAsync(packageID));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}