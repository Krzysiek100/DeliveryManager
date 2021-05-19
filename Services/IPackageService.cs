using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Services
{
    public interface IPackageService
    {
         Task<IAsyncResult> AddPackageAsync(PackageToAddDTO packageToAddDTO);
         Task<IAsyncResult> DeletePackageAsync(int packageID);
         Task<IEnumerable<PackageToReturnDTO>> GetPackagesForDeliveryManAsync(int userID);
         Task<PackageToReturnDTO> GetSinglePackageAsync(int packageID);
         Task<IEnumerable<PackageToReturnDTO>> GetAllPackagesAsync();
    }
}