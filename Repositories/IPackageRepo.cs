using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Repositories
{
    public interface IPackageRepo
    {
         Task AddPackageAsync(Package package);
         void DeletePackage(Package package);
         Task<bool> SaveAllAsync();
         Task<Package> GetSinglePackageAsync(int packageID);
         Task<IEnumerable<Package>> GetPackagesForDeliveryManAsync(int userID);
         Task<IEnumerable<Package>> GetAllPackagesAsync();
    }
}