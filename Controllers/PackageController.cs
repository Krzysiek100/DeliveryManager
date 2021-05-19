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
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;
        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }
        
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> AddPackageAsync(PackageToAddDTO packageToAddDTO)
        {
            try
            {
                return Ok(await _packageService.AddPackageAsync(packageToAddDTO));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "RequireDeliveryManRole")]
        [HttpGet("forUser/{userID}")]
        public async Task<IActionResult> GetPackagesForDeliveryManAsync(int userID)
        {
            try
            {
                return Ok(await _packageService.GetPackagesForDeliveryManAsync(userID));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "RequireDeliveryManRole")]
        [HttpGet]
        public async Task<IActionResult> GetAllPackagesAsync()
        {
            try
            {
                return Ok(await _packageService.GetAllPackagesAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //User also sees that
        [HttpGet("{packageID}")]
        public async Task<IActionResult> GetSinglePackageAsync(int packageID)
        {
            try
            {
                return Ok(await _packageService.GetSinglePackageAsync(packageID));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{packageID}")]
        public async Task<IActionResult> DeletePackageAsync(int packageID)
        {
            try
            {
                return Ok(await _packageService.DeletePackageAsync(packageID));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}