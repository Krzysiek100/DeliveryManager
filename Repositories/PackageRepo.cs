using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class PackageRepo : IPackageRepo
    {
        private readonly ContextData _context;

        public PackageRepo(ContextData context)
        {
            _context = context;
        }
        public async Task AddPackageAsync(Package package)
        {
            await _context.Packages.AddAsync(package);
        }

        public void DeletePackage(Package package)
        {
            _context.Packages.Remove(package);
        }

        public async Task<IEnumerable<Package>> GetAllPackagesAsync()
        {
            return await _context.Packages.Include(x => x.Statuses)
                .Include(x => x.DeliveryMan).ToListAsync();
        }

        public async Task<IEnumerable<Package>> GetPackagesForDeliveryManAsync(int userID)
        {
            return await _context.Packages.Where(d => d.DeliveryManId == userID)
                .Include(x => x.Statuses).Include(x => x.DeliveryMan).ToListAsync();
        }

        public async Task<Package> GetSinglePackageAsync(int packageID)
        {
            var list = await  _context.Packages.Where(x => x.Id == packageID)
                .Include(x => x.Statuses).Include(x => x.DeliveryMan).ToListAsync();

            return list[0];
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}