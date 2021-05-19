using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Services
{
    public interface IPackageStatusService
    {
         Task<IAsyncResult> AddStatusAsync(int packageID, StatusName status);
         Task<IAsyncResult> DeleteStatusAsync(int statusID);
         Task<IEnumerable<PackageStatusToReturnDTO>> GetAllPackageStatusesAsync(int packageID);
    }
}