using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class PackageStatusRepo : IPackageStatusRepo
    {
        private readonly ContextData _context;

        public PackageStatusRepo(ContextData context)
        {
            _context = context;
        }
        public async Task AddStatusAsync(PackageStatus packageStatus)
        {
            await _context.PackageStatuses.AddAsync(packageStatus);
        }

        public void DeleteStatus(PackageStatus packageStatus)
        {
            _context.PackageStatuses.Remove(packageStatus);
        }

        public async Task<IEnumerable<PackageStatus>> GetAllPackageStatusesAsync(int packageID)
        {
            return await _context.PackageStatuses.Where(p => p.PackageId == packageID).ToListAsync();
        }

        public async Task<PackageStatus> GetPackageStatusAsync(int statusID)
        {
            return await _context.PackageStatuses.FindAsync(statusID);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}