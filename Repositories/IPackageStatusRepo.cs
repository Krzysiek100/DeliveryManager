using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Repositories
{
    public interface IPackageStatusRepo
    {
         Task AddStatusAsync(PackageStatus packageStatus);
         void DeleteStatus(PackageStatus packageStatus);
         Task<bool> SaveAllAsync();
         Task<PackageStatus> GetPackageStatusAsync(int statusID);
         Task<IEnumerable<PackageStatus>> GetAllPackageStatusesAsync(int statusID);

    }
}